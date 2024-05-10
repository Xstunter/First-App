using System.ComponentModel.DataAnnotations;

namespace Board.Host.Data.Entities
{
    public class CardEntity
    {
        [Key]
        public int CardId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ListId { get; set; }
        public ListEntity List { get; set; }
    }
}
