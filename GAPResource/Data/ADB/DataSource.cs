using GAPResource.Data;
using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Xml;

namespace GAPResource.Data
{

    public class DataSource : DbContext, IDataSource
    {
        private string _connectionString;


        public DbSet<CityDTO> City { get; set; }
        public DbSet<TruckDTO> Truck { get; set; }

        public DbSet<EdgeDTO> Edge { get; set; }
        public DbSet<OrderDTO> Order { get; set; }
        public DbContext DB { get; set; }
        public DataSource(XmlNode config)
        {
            var connectionStringNode = config.SelectSingleNode(".//Connection");
            _connectionString = connectionStringNode.Attributes["ConnectionString"].Value;
            Database.CanConnect();
            DB = this;
        }

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EdgeDTO>(item =>
          {
              item.HasNoKey();
              item.ToTable("Edge");
          });
        }

    }
}
