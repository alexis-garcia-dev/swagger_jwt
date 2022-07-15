

using Microsoft.EntityFrameworkCore;
using swagger_jwt.Models;

namespace swagger_jwt.Data
{
    public class DataDbContext :DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }


        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Producto> producto { get; set; }
        public DbSet<Bodega> bodega { get; set; }
        public DbSet<Entradas> entradas { get; set; }

        public DbSet<Roles> roles { get; set; }

        public DbSet<Categoria> categoria { get; set; }

        public DbSet<Inventario> inventario { get; set; }
    }
}
