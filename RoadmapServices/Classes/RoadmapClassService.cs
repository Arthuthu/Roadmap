﻿using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;
using System.Security;

namespace RoadmapServices.Classes;

public class RoadmapClassService : IRoadmapClassService
{
	private readonly IRoadmapClassRepository _roadmapData;
	private readonly IUserRepository _userRepository;

	public RoadmapClassService(IRoadmapClassRepository roadmapData, IUserRepository userRepository)
	{
		_roadmapData = roadmapData;
		_userRepository = userRepository;
	}

	public Task<IEnumerable<RoadmapClassModel>> GetAllRoadmaps()
	{
		return _roadmapData.GetAllRoadmaps();
	}

	public async Task<RoadmapClassModel?> GetRoadmapById(Guid id)
	{
		return await _roadmapData.GetRoadmapById(id);
	}

	public async Task AddRoadmap(RoadmapClassModel roadmap)
	{
		var userExists = await VerifyIfUserExists(roadmap);

		if (userExists is false)
		{
			throw new Exception("User required for creating roadmap not found");
		}

		await _roadmapData.AddRoadmap(roadmap);
	}

	public Task UpdateRoadmap(RoadmapClassModel roadmap)
	{
		return _roadmapData.UpdateRoadmap(roadmap);
	}

	public Task DeleteRoadmap(Guid id)
	{
		return _roadmapData.DeleteRoadmap(id);
	}

	private async Task<bool> VerifyIfUserExists(RoadmapClassModel roadmapModel)
	{
		var users = await _userRepository.GetAllUsers();

		foreach (var user in users)
		{
			if (roadmapModel.UserId == user.Id)
			{
				return true;
			}
		}
		return false;
	}
}
