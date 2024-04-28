using System.ComponentModel.DataAnnotations;

namespace Board.Host.Data.Entities
{
    public class HistoryEntity
    {
        [Key]
        public int HistoryId { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int? BoardId { get; set; }
        public BoardEntity Board { get; set; }

    }
}
