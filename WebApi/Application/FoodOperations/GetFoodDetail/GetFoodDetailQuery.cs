using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FoodOperations.GetFoodDetail;

public class GetFoodDetailQuery
{
    private readonly IFoodOrderDBContext _context;
    private readonly IMapper _mapper;
    public int FoodId { get; set; }

    public GetFoodDetailQuery(IFoodOrderDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public FoodDetailViewModel Handle()
    {

        var food = _context.Foods.Include(f => f.Quantities).Include(f => f.Restaurant).FirstOrDefault(f => f.Id == FoodId);
        if (food is null)
            throw new InvalidOperationException("Yiyecek bulunamadı.");

        FoodDetailViewModel vm = _mapper.Map<FoodDetailViewModel>(food);
        FoodDetailViewModelShort vms = _mapper.Map<FoodDetailViewModelShort>(food);
        return vm;
    }
}
public class FoodDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImgUrl { get; set; }
    public int RestaurantId { get; set; }
}

public class FoodDetailViewModelShort
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}