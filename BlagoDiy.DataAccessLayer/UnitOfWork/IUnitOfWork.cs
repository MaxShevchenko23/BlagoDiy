﻿using BlagoDiy.DataAccessLayer.Repositories;

namespace BlagoDiy.DataAccessLayer.UnitOfWork;

public interface IUnitOfWork
{
    public CampaignRepository CampaignRepository { get; }
    public DonationRepository DonationRepository { get; }
    public UserRepository UserRepository { get; }

    Task<int> SaveChangesAsync();
}