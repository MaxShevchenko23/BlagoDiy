using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlagoDiy.DataAccessLayer.Entites;

public class Campaign : IEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string Category { get; set; }
    
    public decimal Destination { get; set; }
    public decimal Raised { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? EndAt { get; set; }

    public string ImageUrl { get; set; }
    public string SocialUrls { get; set; }
    
    public IList<Donation> Donations { get; set; }

    
    [ForeignKey("CreatorId")]
    public int CreatorId { get; set; }
    public User User { get; set; }
}

