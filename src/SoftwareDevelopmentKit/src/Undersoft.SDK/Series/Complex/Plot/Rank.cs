namespace Undersoft.SDK.Series.Complex
{
    public class Rank<T>
        where T : IIdentifiable
    {
        public Place<T> Owner { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"Rank {Value}, owner: {Owner.Value} (index: {Owner.Index})";
        }
    }
}
