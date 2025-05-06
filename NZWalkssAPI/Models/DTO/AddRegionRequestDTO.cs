using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NZWalkssAPI.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a min of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a max of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a max of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
