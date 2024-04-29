using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class GetBoardRequest
    {
        [Required]
        public int BoardId { get; set; }
    }
}
