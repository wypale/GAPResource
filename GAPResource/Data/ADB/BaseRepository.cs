using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GAPResource.Data
{
    public class BaseDbRepository<T> where T : DTO, new()
    {
        protected DbContext DB { get; set; }

        public BaseDbRepository(DbContext db)
        {
            DB = db;
        }
    }

    public class BaseRepository<T> : BaseDbRepository<T> where T : DTO, new()   
    {
        public DbSet<T> Items { get;private set; }

        public BaseRepository(DbSet<T> items, DbContext db) : base(db)
        {
            Items = items;
        }
    }


    public class BaseIdRepository<T> : BaseRepository<T> where T : IdDTO, new()
    {
        public BaseIdRepository(DbSet<T> items, DbContext db) :base(items, db)
        {
        }

        public IOrderedQueryable<T> OrderedItems => Items.OrderBy(p => p.Id);

        public bool Add(T dto)
        {
            Items.Add(dto);
            DB.SaveChanges();
            return true;
        }

        public bool UpdateRange(List<T> dto)
        {
            Items.UpdateRange(dto);
            DB.SaveChanges();
            return true;
        }

        public bool Delete(long id)
        {
            Items.Where(c => c.Id == id).ExecuteDelete();          
            return true;
        }
    }

}
