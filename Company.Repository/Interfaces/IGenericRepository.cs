
using Company.Data.Models;

namespace Company.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public void GetById(int id);
        public IEnumerable<TEntity> GetAll();
    }
}
