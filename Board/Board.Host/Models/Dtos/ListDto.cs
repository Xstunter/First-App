namespace Board.Host.Models.Dtos
{
    public class ListDto
    {
        public int ListId { get; set; }
        public string StatusName { get; set; }
        public ICollection<CardDto> Cards { get; set; }
    }
}
