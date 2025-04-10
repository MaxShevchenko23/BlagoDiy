using AutoMapper;
using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.DataAccessLayer.Repositories;
using BlagoDiy.DataAccessLayer.UnitOfWork;
using BlagoDiy.DataAccessLayer.Entites;

namespace BlagoDiy.BusinessLogic.Services;

public class DonationService
{
    private readonly IMapper mapper;
    private readonly DonationRepository donationRepository;

    public DonationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        donationRepository = unitOfWork.DonationRepository;
    }

    public async Task<IEnumerable<Donation>> GetAllDonationsAsync(int page, int pageSize)
    {
        return await donationRepository.GetAllAsync(page, pageSize);
    }

    public async Task<Donation> GetDonationByIdAsync(int id)
    {
        return await donationRepository.GetByIdAsync(id);
    }

    public async Task CreateDonationAsync(DonationPost donationDto)
    {
        var entity = mapper.Map<Donation>(donationDto);
        
        entity.CreatedAt = DateTime.Now;
        
        await donationRepository.AddAsync(entity);
    }

    public async Task UpdateDonationAsync(DonationPost donationDto)
    {
        var entity = mapper.Map<Donation>(donationDto);
        await donationRepository.UpdateAsync(entity);
    }

    public async Task DeleteDonationAsync(int id)
    {
        var donation = await donationRepository.GetByIdAsync(id);
        if (donation != null)
        {
            await donationRepository.DeleteAsync(id);
        }
    }
    
    public async Task<IEnumerable<Donation>> GetDonationsByCampaignIdAsync(int campaignId, int take = 10)
    {
        return await donationRepository.GetDonationsByCampaignIdAsync(campaignId, take);
    }
}