using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CourierOperations.CreateCourier;
using WebApi.Application.CourierOperations.DeleteCourier;
using WebApi.Application.CourierOperations.GetCourierDetail;
using WebApi.Application.CourierOperations.GetCouriers;
using WebApi.Application.CourierOperations.UpdateCourier;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]s")]
public class CourierController : ControllerBase
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;

    public CourierController(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCouriers()
    {
        GetCouriersQuery query = new GetCouriersQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourierDetail(int id)
    {
        GetCourierDetailQuery query = new GetCourierDetailQuery(_context, _mapper);
        query.CourierId = id;

        GetCourierDetailQueryValidator validator = new GetCourierDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }
    [HttpPost]
    public IActionResult CreateCourier([FromBody] CreateCourierModel model)
    {
        CreateCourierCommand command = new CreateCourierCommand(_context);
        command.Model = model;

        CreateCourierCommandValidator validator = new CreateCourierCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
    [HttpPut("{id}")]
    public IActionResult UpdateCourier(int id, [FromBody] UpdateCourierModel model)
    {
        UpdateCourierCommand command = new UpdateCourierCommand(_context);
        command.CourierId = id;
        command.Model = model;

        UpdateCourierCommandValidator validator = new UpdateCourierCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourier(int id)
    {
        DeleteCourierCommand command = new DeleteCourierCommand(_context);
        command.CourierId = id;

        DeleteCourierCommandValidator validator = new DeleteCourierCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
