using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations;

public interface IFoodOrderDBContext
{
    public DbSet<Food> Foods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Quantity> Quantities { get; set; }
    public DbSet<Courier> Couriers { get; set; }
    int SaveChanges();
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
}