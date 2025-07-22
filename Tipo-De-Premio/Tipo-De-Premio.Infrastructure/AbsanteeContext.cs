
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;


public class AbsanteeContext : DbContext
{
    public virtual DbSet<TipoDataModel> Tipos { get; set; }

    public AbsanteeContext(DbContextOptions<AbsanteeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
        
}