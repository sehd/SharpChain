namespace SigmaSharp.SharpChain
{
    public class Block<TContent, THash>
    {
        public TContent Content { get; set; }
        public THash PreviousHash { get; set; }
        public THash Hash { get; set; }
    }
}
