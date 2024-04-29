using Board.Host.Models.Dtos;

namespace Board.Host.Models.Requests
{
    public class UpdateBoardRequest
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
