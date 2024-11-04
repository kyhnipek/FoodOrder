using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class FoodOrderDBContext : DbContext, IFoodOrderDBContext
{
    public FoodOrderDBContext(DbContextOptions<FoodOrderDBContext> options) : base(options)
    {
    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Quantity> Quantities { get; set; }
    public DbSet<Courier> Couriers { get; set; }

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Add(entity);
    }
    public override EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Update(entity);
    }
    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Remove(entity);
    }
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }


}