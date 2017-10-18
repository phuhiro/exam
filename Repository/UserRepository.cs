using System;
using System.Collections.Generic;
using System.Linq;
using exam.Models;
using Microsoft.EntityFrameworkCore;

namespace exam.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Assign(int id, int role)
        {
            return false;
        }

        public User Create(User user)
        {
            if(_context.users.Where(x => x.email == user.email || x.username == user.username).Any()){
                return null;
            }
            try
            {
               _context.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return null;   
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Find(int id)
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email){
            var _user = _context.users.Where(x => x.email.Equals(email)).FirstOrDefault();
            return _user;        }

        public User FindByUsername(string username){
            var _user = _context.users.Where(x => x.username.Equals(username)).FirstOrDefault();
            return _user;
        }
        public List<User> getAll()
        {
            return _context.users.ToList();
        }

        public List<User> Search(Dictionary<string, string> query)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
