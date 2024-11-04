using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.Services;


namespace WebApi.Application.UserOperations.CreateUser;

public class CreateUserCommand
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public CreateUserModel Model { get; set; }
    public CreateUserCommand(IFoodOrderDBContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {

        string passwordSalt;
        string passwordHash = PasswordService.CreatePasswordHash(Model.Password, out passwordSalt);

        User newUser = _mapper.Map<User>(Model);
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;

        _context.Users.Add(newUser);

        _context.SaveChanges();
    }

}

public class CreateUserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
