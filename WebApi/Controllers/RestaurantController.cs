using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.RestaurantOperations.CreateRestaurant;
using WebApi.Application.RestaurantOperations.DeleteRestaurant;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;
using WebApi.Application.RestaurantOperations.GetRestaurants;
using WebApi.Application.RestaurantOperations.UpdateRestaurant;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class RestaurantController : ControllerBase
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public RestaurantController(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpGet]
    public IActionResult GetRestaurants()
    {
        GetRestaurantsQuery query = new GetRestaurantsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetRestaurantDetail(int id)
    {
        GetRestaurantDetailQuery query = new GetRestaurantDetailQuery(_context, _mapper);
        query.RestaurantId = id;
        var result = query.Handle();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateRestaurant([FromBody] CreateRestaurantModel model)
    {
        CreateRestaurantCommand command = new CreateRestaurantCommand(_context, _mapper);
        command.Model = model;

        CreateRestaurantCommandValidator validator = new CreateRestaurantCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpPut("{id}")]
    public IActionResult UpdateRestaurant(int id, [FromBody] UpdateRestaurantModel model)
    {
        UpdateRestaurantCommand command = new UpdateRestaurantCommand(_context, _mapper);
        command.RestaurantId = id;
        command.Model = model;
        command.UserRole = GetUserRole();

        UpdateRestaurantCommandValidator validator = new UpdateRestaurantCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult DeleteRestaurant(int id)
    {
        DeleteRestaurantCommand command = new DeleteRestaurantCommand(_context);
        command.RestaurantId = id;

        DeleteRestaurantCommandValidator validator = new DeleteRestaurantCommandValidator();
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
