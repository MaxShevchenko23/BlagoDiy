using AutoMapper;
using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.DataAccessLayer.Repositories;
using BlagoDiy.DataAccessLayer.UnitOfWork;
using BlagoDiy.DataAccessLayer.Entites;
    
namespace BlagoDiy.BusinessLogic.Services;
    
public class CampaignService
{
    private readonly IMapper mapper;
    private readonly CampaignRepository campaignRepository;
   
       public CampaignService(IUnitOfWork unitOfWork, IMapper mapper)
       {
           this.mapper = mapper;
           campaignRepository = unitOfWork.CampaignRepository;
       }
   
       public async Task<IEnumerable<Campaign>> GetAllCampaigns(int page, int pageSize)
       {
           return await campaignRepository.GetAllPaginatedAsync(page, pageSize);
       }
   
       public async Task<Campaign> GetCampaignById(int id)
       {
           return await campaignRepository.GetByIdAsync(id);
       }
       
       public async Task<IEnumerable<Campaign>> GetCampaignsByUserId(int userId)
       {
           return await campaignRepository.GetByUserIdAsync(userId);
       }
       
       public async Task CreateCampaignAsync(CampaignPost campaignDto)
       {
           var entity = mapper.Map<Campaign>(campaignDto);
           
           entity.CreatedAt = DateTime.Now;
           entity.CreatorId = campaignDto.CreatorId;

           await campaignRepository.AddAsync(entity);
       }
       
       public async Task UpdateCampaignAsync(CampaignPost campaignDto, int campaignId)
       {
           var entity = mapper.Map<Campaign>(campaignDto);
           entity.Id = campaignId;
           
           await campaignRepository.UpdateAsync(entity);
       }
       
       public async Task CloseCampaignAsync(int id)
       {
           var campaign = await campaignRepository.GetByIdAsync(id);
           if (campaign != null)
           {
               await campaignRepository.DeleteAsync(id);
           }
       }
   }