using System;
using System.Collections.Generic;
using exam.Models;

namespace exam.Repository
{
    public interface IUserRepository
    {
        User Create(User user);
        bool Update(int id);
        bool Delete(int id);
        List<User> getAll();
        User Find(int id);
        User FindByEmail(string email);
        User FindByUsername(string username);
        List<User> Search(Dictionary<String, String> query);
        bool Assign(int id, int role);
    }
}
