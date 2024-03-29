﻿using Roadmap.Domain.Models;

namespace Roadmap.Infra.Validators.Interfaces
{
    public interface IMessageHandler
    {
        IList<string>? ValidateRoadmapCreation(RoadmapClassModel roadmapData);
        IList<string>? ValidateUserRegistration(UserModel userData);
        string ConcatRegistrationMessages(IList<string> responseMessages);
    }
}