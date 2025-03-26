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
    public async Task<IActionResult> GetAllCampaigns()
    {
        var campaigns = await campaignService.GetAllCampaigns();
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
    public async Task<IActionResult> UpdateCampaign(int id, [FromBody] CampaignPost campaignDto)
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
        await campaignService.UpdateCampaignAsync(campaignDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampaign(int id)
    {
        var campaign = await campaignService.GetCampaignById(id);
        if (campaign == null)
        {
            return NotFound();
        }
        await campaignService.DeleteCampaignAsync(id);
        return NoContent();
    }
}