using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlagoDiy.Controllers;

[ApiController]
[Route("api/campaigns")]
public class CampaignController : ControllerBase
{
    private readonly CampaignService campaignService;
    
    public CampaignController(CampaignService campaignService)
    {
        this.campaignService = campaignService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCampaigns(int page = 1, int pageSize = 10)
    {
        var campaigns = await campaignService.GetAllCampaigns(page, pageSize);
        return Ok(campaigns);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCampaignById(int id)
    {
        var campaign = await campaignService.GetCampaignById(id);
        if (campaign == null)
        {
            return NotFound();
        }
        return Ok(campaign);
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCampaignsByUserId(int userId)
    {
        var campaigns = await campaignService.GetCampaignsByUserId(userId);
        
        if (campaigns == null || !campaigns.Any())
        {
            return NotFound();
        }
        
        return Ok(campaigns);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateCampaign([FromBody] CampaignPost campaignDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await campaignService.CreateCampaignAsync(campaignDto);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCampaign(int id, [FromBody] CampaignPost campaignDto, int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var campaign = await campaignService.GetCampaignById(id);
            
        if (campaign == null)
        {
            return NotFound();
        }


        if (userId != campaign.CreatorId)
        {
            return BadRequest("Only the creator can update the campaign.");
        }
        
        
        await campaignService.UpdateCampaignAsync(campaignDto, id);
        return NoContent();
    }
    
    [HttpPost("close/{id}")]
    public async Task<IActionResult> CloseCampaign(int id, int userId)
    {
        var campaign = await campaignService.GetCampaignById(id);
        
        if (campaign == null)
        {
            return NotFound();
        }
        
        if (userId != campaign.CreatorId)
        {
            return BadRequest("Only the creator can close the campaign.");
        }
        
        await campaignService.CloseCampaignAsync(id);
        return NoContent(); 
    }
}