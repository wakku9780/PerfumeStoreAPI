﻿using Microsoft.EntityFrameworkCore;
using PerfumeStoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);




    }
}
