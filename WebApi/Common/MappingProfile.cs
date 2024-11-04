using AutoMapper;
using WebApi.Application.CourierOperations.GetCourierDetail;
using WebApi.Application.CourierOperations.GetCouriers;
using WebApi.Application.CourierOperations.UpdateCourier;
using WebApi.Application.FoodOperations.CreateFood;
using WebApi.Application.FoodOperations.GetFoodDetail;
using WebApi.Application.FoodOperations.GetFoods;
using WebApi.Application.FoodOperations.UpdateFood;
using WebApi.Application.OrderOperations.GetOrderDetail;
using WebApi.Application.OrderOperations.GetOrders;
using WebApi.Application.RestaurantOperations.CreateRestaurant;
using WebApi.Application.RestaurantOperations.GetRestaurantDetail;
using WebApi.Application.RestaurantOperations.GetRestaurants;
using WebApi.Application.RestaurantOperations.UpdateRestaurant;
using WebApi.Application.UserOperations.CreateUser;
using WebApi.Application.UserOperations.GetUserDetail;
using WebApi.Entities;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Food, FoodsViewModel>();
        CreateMap<Food, FoodDetailViewModel>();
        CreateMap<Food, FoodDetailViewModelShort>();
        CreateMap<CreateFoodModel, Food>();
        CreateMap<UpdateFoodModel, Food>();


        CreateMap<Order, OrdersViewModel>();
        CreateMap<Order, OrderDetailViewModel>();

        CreateMap<Restaurant, RestaurantDetailViewModel>();
        CreateMap<Restaurant, RestaurantDetailViewModelShort>();
        CreateMap<Restaurant, RestaurantsViewModel>();
        CreateMap<CreateRestaurantModel, Restaurant>();
        CreateMap<UpdateRestaurantModel, Restaurant>();


        CreateMap<User, UserDetailViewModel>();
        CreateMap<CreateUserModel, User>();

        CreateMap<Courier, CouriersViewModel>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Users.Name))
                            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Users.Surname));
        CreateMap<Courier, CourierDetailViewModel>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Users.Name))
                            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Users.Surname));
        CreateMap<Courier, CourierDetailViewModelShort>()
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Users.Name))
                            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Users.Surname));
        CreateMap<UpdateCourierCommand, Courier>();
    }
}