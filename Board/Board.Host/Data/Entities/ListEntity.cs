using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Board.Host.Data.Entities
{
    public class ListEntity
    {
        [Key]
        public int ListId { get; set; }
        [Unique]
        public string StatusName { get; set; }
        public int BoardId { get; set; }
        public BoardEntity Board { get; set; }
        public ICollection<CardEntity> Cards { get; set; }
    }
}
