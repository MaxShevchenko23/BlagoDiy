﻿using AutoMapper;
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
   
       public async Task<IEnumerable<Campaign>> GetAllCampaigns()
       {
           return await campaignRepository.GetAllAsync();
       }
   
       public async Task<Campaign> GetCampaignById(int id)
       {
           return await campaignRepository.GetByIdAsync(id);
       }
       
       public async Task CreateCampaignAsync(CampaignPost campaignDto)
       {
           var entity = mapper.Map<Campaign>(campaignDto);
           await campaignRepository.AddAsync(entity);
       }
       
       public async Task UpdateCampaignAsync(CampaignPost campaignDto)
       {
           var entity = mapper.Map<Campaign>(campaignDto);
           await campaignRepository.UpdateAsync(entity);
       }
       public async Task DeleteCampaignAsync(int id)
       {
           var campaign = await campaignRepository.GetByIdAsync(id);
           if (campaign != null)
           {
               await campaignRepository.DeleteAsync(id);
           }
       }
   }