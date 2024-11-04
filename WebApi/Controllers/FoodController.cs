using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.FoodOperations.CreateFood;
using WebApi.Application.FoodOperations.DeleteFood;
using WebApi.Application.FoodOperations.GetFoodDetail;
using WebApi.Application.FoodOperations.GetFoods;
using WebApi.Application.FoodOperations.UpdateFood;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class FoodController : ControllerBase
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public FoodController(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetFoods()
    {
        GetFoodsQuery query = new GetFoodsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetFoodDetail(int id)
    {
        GetFoodDetailQuery query = new GetFoodDetailQuery(_context, _mapper);
        query.FoodId = id;

        GetFoodDetailQueryValidator validator = new GetFoodDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpPost]
    public IActionResult CreateFood([FromBody] CreateFoodModel model)
    {
        CreateFoodCommand command = new CreateFoodCommand(_context, _mapper);
        command.Model = model;
        command.UserId = GetUserId();
        command.UserRole = GetUserRole();

        CreateFoodCommandValidator validator = new CreateFoodCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin,Restaurant,Courier")]
    [HttpPut("{id}")]
    public IActionResult UpdateFood(int id, [FromBody] UpdateFoodModel model)
    {
        UpdateFoodCommand command = new UpdateFoodCommand(_context, _mapper);
        command.FoodId = id;
        command.Model = model;
        command.UserId = GetUserId();
        command.UserRole = GetUserRole();

        UpdateFoodCommandValidator validator = new UpdateFoodCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpDelete("{id}")]
    public IActionResult DeleteFood(int id)
    {
        DeleteFoodCommand command = new DeleteFoodCommand(_context);
        command.FoodId = id;
        command.UserId = GetUserId();
        command.UserRole = GetUserRole();

        DeleteFoodCommandValidator validator = new DeleteFoodCommandValidator();
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
