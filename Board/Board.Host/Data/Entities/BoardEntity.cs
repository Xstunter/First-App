using System.ComponentModel.DataAnnotations;

namespace Board.Host.Data.Entities
{
    public class BoardEntity
    {
        [Key]
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ListEntity> Lists { get; set; }
        public ICollection<HistoryEntity> Histories { get; set; }
    }
}
