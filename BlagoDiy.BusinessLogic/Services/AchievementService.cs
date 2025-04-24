using AutoMapper;
using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.DataAccessLayer.Entites;
using BlagoDiy.DataAccessLayer.Repositories;
using BlagoDiy.DataAccessLayer.UnitOfWork;

namespace BlagoDiy.BusinessLogic.Services;

public class AchievementService
{
    private readonly IMapper mapper;
    private readonly AchievementRepository achievementRepository;

    public AchievementService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        achievementRepository = unitOfWork.AchievementRepository;
    }
    
    public async Task<IEnumerable<Achievement>> GetAllAchievementsAsync()
    {
        return await achievementRepository.GetAllAsync();
    }
    
    public async Task<Achievement> AddAchievement(AchievementPost model)
    {
        var entity = mapper.Map<Achievement>(model);
        return await achievementRepository.AddAsync(entity);
    }

    public async Task<List<AchievementToUser>?> GetUserAchievements(int userId)
    {
        var achievements = await achievementRepository.GetByUserIdAsync(userId);

        return achievements;
    }
}