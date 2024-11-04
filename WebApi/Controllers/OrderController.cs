using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.CreateOrder;
using WebApi.Application.OrderOperations.GetOrderDetail;
using WebApi.Application.OrderOperations.GetOrders;
using WebApi.Application.OrderOperations.UpdateOrder;
using WebApi.Application.OrderOperations.DeleteOrder;
using WebApi.DBOperations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class OrderController : ControllerBase
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public OrderController(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin,Restaurant,Customer,Courier")]
    [HttpGet]
    public IActionResult GetOrders()
    {
        GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);
        query.UserId = GetUserId();
        query.UserRole = GetUserRole();
        var result = query.Handle();
        return Ok(result);

    }


    [Authorize(Roles = "Admin,Restaurant,Customer,Courier")]
    [HttpGet("{id}")]
    public IActionResult GetOrderDetail(int id)
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = id;
        query.UserId = GetUserId();
        query.UserRole = GetUserRole();
        var result = query.Handle();
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Restaurant,Customer,Courier")]
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderModel model)
    {
        CreateOrderCommand command = new CreateOrderCommand(_context);
        command.UserId = GetUserId();
        command.Model = model;

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] UpdateOrderModel model)
    {
        UpdateOrderCommand command = new UpdateOrderCommand(_context);
        command.UserId = GetUserId();
        command.UserRole = GetUserRole();
        command.OrderId = id;
        command.Model = model;

        UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = id;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();

    }

    private int GetUserId()
    {
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        return Convert.ToInt32(userId);
    }
    private string GetUserRole()
    {
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userRole = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
        return userRole;
    }
}
