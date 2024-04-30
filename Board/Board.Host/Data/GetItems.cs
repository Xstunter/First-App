using System.Collections;

namespace Board.Host.Data
{
    public class GetItems<T>
    {
        public IEnumerable<T> Data { get; set; } = null!;
    }
}
