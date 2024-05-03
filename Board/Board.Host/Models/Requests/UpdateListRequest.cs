using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class UpdateListRequest
    {
        [Required]
        public int ListId { get; set; }
        [MaxLength(50)]
        public string StatusName { get; set; }
    }
}
