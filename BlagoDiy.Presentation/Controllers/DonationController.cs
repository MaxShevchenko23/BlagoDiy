using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlagoDiy.Controllers;

[ApiController]
[Route("api/donations")]
public class DonationController : ControllerBase
{
    private readonly DonationService donationService;

    public DonationController(DonationService donationService)
    {
        this.donationService = donationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonations()
    {
        var donations = await donationService.GetAllDonationsAsync();
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
}