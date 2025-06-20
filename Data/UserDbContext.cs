using authLogin.Entities;
using Microsoft.EntityFrameworkCore;

namespace authLogin.Data
{
  public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
  {
    public DbSet<User> Users { get; set; }
  }
}