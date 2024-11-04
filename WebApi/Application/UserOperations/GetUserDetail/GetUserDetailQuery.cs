using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.UserOperations.GetUserDetail;

public class GetUserDetailQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int UserId { get; set; }

    public GetUserDetailQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public UserDetailViewModel Handle()
    {
        var user = _context.Users.Where(x => x.IsActive).FirstOrDefault(x => x.Id == UserId);
        if (user is null)
            throw new InvalidOperationException("Müşteri bulunamadı.");

        UserDetailViewModel vm = _mapper.Map<UserDetailViewModel>(user);
        return vm;
    }
}

public class UserDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Adress { get; set; }
    public string Phone { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Role { get; set; }
}