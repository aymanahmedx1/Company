using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Company.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        => _context.Set<TEntity>().Add(entity);


        public void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);


        public IEnumerable<TEntity> GetAll()
        => _context.Set<TEntity>().ToList();

        public TEntity GetById(int id)
        => _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        public TEntity GetByIdAsNoTracking(int id)
             => _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);

        public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    }
}
