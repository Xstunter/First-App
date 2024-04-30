using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class DeleteListRequest
    {
        [Required]
        public int ListId { get; set; }
    }
}
