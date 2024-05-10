using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class DeleteCardRequest
    {
        [Required]
        public int CardId { get; set; }
        [Required]
        public int BoardId { get; set; }
    }
}
