using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class CreateCardRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Priority { get; set; }
        [Required]
        public int ListId { get; set; }
        [Required]
        public int BoardId { get; set; }
    }
}
