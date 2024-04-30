namespace Board.Host.Models.Dtos
{
    public class CardDto
    {
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
