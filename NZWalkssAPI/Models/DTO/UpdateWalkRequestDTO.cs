using System.ComponentModel.DataAnnotations;

namespace NZWalkssAPI.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a max of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "Name has to be a max of 300 characters")]
        public string Description { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please enter a number")]
        [Range(1, 30)]
        public double LengthInKM { get; set; }


        public string? WalkImageURL { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
