using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UserOperations.CreateUser;
using WebApi.Application.UserOperations.GetUserDetail;
using WebApi.Application.UserOperations.Login;
using WebApi.Application.UserOperations.RefreshToken;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly IFoodOrderDBContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public UserController(IFoodOrderDBContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    [Authorize("Admin")]
    [HttpGet("{id}")]
    public IActionResult GetUserDetail(int id)
    {
        GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
        query.UserId = id;

        GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost("SignUp")]
    public IActionResult SignUp([FromBody] CreateUserModel model)
    {
        CreateUserCommand command = new CreateUserCommand(_context, _mapper);
        command.Model = model;

        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }


    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserLoginModel model)
    {
        LoginCommand command = new LoginCommand(_context, _configuration);
        command.Model = model;

        LoginCommandValidator validator = new LoginCommandValidator();
        validator.ValidateAndThrow(command);

        var result = command.Handle();

        return Ok(result);
    }

    [HttpPost("refreshToken")]
    public IActionResult GetRefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
        command.RefreshToken = token;
        var resultToken = command.Handle();
        return Ok(resultToken);
    }

}
