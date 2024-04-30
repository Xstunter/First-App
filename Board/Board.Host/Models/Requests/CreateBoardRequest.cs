using Board.Host.Models.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class CreateBoardRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
