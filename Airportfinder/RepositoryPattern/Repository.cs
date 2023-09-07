using Microsoft.EntityFrameworkCore;

namespace Airportfinder.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext context;
        public Repository(DatabaseContext context)
        {
            this.context = context;
        }

        public List<T> Get()
        {
         return context.Set<T>().ToList();
        }

        public void Add(T item)
        {
            context.Set<T>().AddAsync(item);
            context.SaveChanges();
        }

       
    }
}
