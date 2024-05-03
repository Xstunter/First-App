using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class ChangeListForCardRequest
    {
        [Required]
        public int CardId { get; set; }
        [Required]
        public int ListId { get; set; }
    }
}
