using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain;

namespace MinimalAPI.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<ToDo> ToDos => Set<ToDo>();
}