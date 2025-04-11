using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BlagoDiy.Controllers;

[ApiController]
[Route("api/donations")]
public class DonationController : ControllerBase
{
    private readonly DonationService donationService;
    private readonly CampaignService campaignService;

    public DonationController(DonationService donationService, CampaignService campaignService)
    {
        this.donationService = donationService;
        this.campaignService = campaignService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonations(int page = 1, int pageSize = 10)
    {
        var donations = await donationService.GetAllDonationsAsync(page, pageSize);
        return Ok(donations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonationById(int id)
    {
        var donation = await donationService.GetDonationByIdAsync(id);
        if (donation == null)
        {
            return NotFound();
        }
        return Ok(donation);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromBody] DonationPost donationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var campaign = await campaignService.GetCampaignById(donationDto.CampaignId);
        
        if (campaign == null)
        {
            return NotFound();
        }
        
        await donationService.CreateDonationAsync(donationDto);
        
        
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonation(int id, [FromBody] DonationPost donationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var donation = await donationService.GetDonationByIdAsync(id);
        if (donation == null)
        {
            return NotFound();
        }
        await donationService.UpdateDonationAsync(donationDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonation(int id)
    {
        var donation = await donationService.GetDonationByIdAsync(id);
        if (donation == null)
        {
            return NotFound();
        }
        
        await donationService.DeleteDonationAsync(id);
        return NoContent();
    }
    
    [HttpGet("campaign/{campaignId}")]
    public async Task<IActionResult> GetDonationsByCampaignId(int campaignId, int take = 10)
    {
        var donations = await donationService.GetDonationsByCampaignIdAsync(campaignId, take);
        return Ok(donations);
    }
}