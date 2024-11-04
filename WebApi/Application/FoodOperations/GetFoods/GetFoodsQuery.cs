using System.Linq.Expressions;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FoodOperations.GetFoods;

public class GetFoodsQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int UserId { get; set; }
    public string UserRole { get; set; }

    public GetFoodsQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public List<FoodsViewModel> Handle()
    {

        var foods = _context.Foods.Where(f => f.IsActive).OrderBy(f => f.Id);
        List<FoodsViewModel> vm = _mapper.Map<List<FoodsViewModel>>(foods);
        return vm;
    }
}

public class FoodsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImgUrl { get; set; }
    public int RestaurantId { get; set; }
}