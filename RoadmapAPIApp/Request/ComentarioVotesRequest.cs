﻿namespace Roadmap.API.Request;

public class ComentarioVotesRequest
{
    public Guid UserId { get; set; }
    public Guid ComentarioId { get; set; }
}
