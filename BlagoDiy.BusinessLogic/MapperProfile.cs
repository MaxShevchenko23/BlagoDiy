using AutoMapper;
using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.DataAccessLayer.Entites;

namespace BlagoDiy.BusinessLogic;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Campaign,CampaignPost>().ReverseMap();
        CreateMap<Campaign,CampaignGetFull>().ReverseMap();
        CreateMap<Campaign,CampaignGetShort>().ReverseMap();
        
        
        
        CreateMap<Donation,DonationPost>().ReverseMap();
        CreateMap<Donation,DonationGet>().ReverseMap();

        CreateMap<User, UserGetDto>().ReverseMap();
        CreateMap<User, UserPost>().ReverseMap();
        
        
        CreateMap<Achievement, AchievementPost>().ReverseMap();
    }
}