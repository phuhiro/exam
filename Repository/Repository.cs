using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;
using Microsoft.EntityFrameworkCore;

namespace exam.Repository
{
    public class Repository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Create(TEntity o)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            var itemToDelete = await _context.Set<TEntity>().FindAsync(id);
            if (itemToDelete != null)
            {
                _context.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }                 
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);   
        }

        public async Task<List<TEntity>> getAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task Update(int id, TEntity o)
        {
            var itemToUpdate = await _context.Set<TEntity>().FindAsync(id);
            if (itemToUpdate != null)
            {
                itemToUpdate = o;
                await _context.SaveChangesAsync();
            }            
        }
    }
}
