using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class UpdateCardRequest
    {
        [Required]
        public int CardId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Priority { get; set; }
        [Required]
        public int BoardId { get; set; }
    }
}
