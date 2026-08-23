using CapEx.Models;
using Microsoft.EntityFrameworkCore;

namespace CapEx.Data;

public class CapExDbContext : DbContext
{
    public CapExDbContext(DbContextOptions<CapExDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Request> Requests => Set<Request>();

    public DbSet<Approval> Approvals => Set<Approval>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CapExDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
