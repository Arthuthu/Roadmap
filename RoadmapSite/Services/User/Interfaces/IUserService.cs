﻿using RoadmapSite.Models;

namespace RoadmapSite.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<IList<UserModel>> GetAllUsers();
    }
}