using Airportfinder.Models;

namespace Airportfinder.RepositoryPattern
{
    public interface IRepository<T> where T : class
    {
        public List<T> Get();
        void Add(T item);

    }
}
