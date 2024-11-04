using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.CourierOperations.CreateCourier;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CourierOperations.CreateCourier;

public class CreateCourierCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFoodOrderDBContext _context;

    public CreateCourierCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenCourierIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var courier = new Courier()
        {
            Users = new User
            {
                Name = "CourierTestName",
                Surname = "CourierTestSurname",
                Email = "CouriterTestMail@mail.com"
            }
        };
        _context.Couriers.Add(courier);
        _context.SaveChanges();

        CreateCourierCommand command = new CreateCourierCommand(_context);
        command.Model = new CreateCourierModel() { Name = courier.Users.Name, Surname = courier.Users.Surname };

        FluentActions.Invoking(() => command.Handle())
                            .Should().Throw<InvalidOperationException>()
                            .And.Message.Should().Be("Kurye zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Courier_ShouldBeCreated()
    {
        CreateCourierModel model = new CreateCourierModel()
        {
            Name = "CourierTestName2",
            Surname = "CourierTestSurname2",
            Email = "CouriterTestMail2@mail.com",
            Password = "123456",
            City = Cities.Bandırma,
            State = States.Balikesir,
            RestoranIds = [1],
        };
        CreateCourierCommand command = new CreateCourierCommand(_context);
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var courier = _context.Couriers.Include(c => c.Users).FirstOrDefault(a => a.Users.Name == model.Name && a.Users.Surname == model.Surname);

        courier.Should().NotBeNull();
        courier.Users.Name.Should().Be(model.Name);
        courier.Users.Surname.Should().Be(model.Surname);
        courier.Users.State.Should().Be(model.State);
        courier.Users.City.Should().Be(model.City);
        courier.Users.Email.Should().Be(model.Email);
    }


}