using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class CreateListRequest
    {
        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }
        [Required]
        public int BoardId { get; set; }
    }
}
