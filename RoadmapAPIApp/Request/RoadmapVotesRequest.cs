﻿namespace Roadmap.API.Request;

public class RoadmapVotesRequest
{
    public Guid UserId { get; set; }
    public Guid RoadmapId { get; set; }
}
