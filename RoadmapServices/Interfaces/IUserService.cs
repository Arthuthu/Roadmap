﻿using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel?> GetUserById(Guid id);
        Task AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(Guid id);
    }
}