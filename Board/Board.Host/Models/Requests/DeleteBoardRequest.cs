using System.ComponentModel.DataAnnotations;

namespace Board.Host.Models.Requests
{
    public class DeleteBoardRequest
    {
        [Required]
        public int BoardId { get; set; }
    }
}
