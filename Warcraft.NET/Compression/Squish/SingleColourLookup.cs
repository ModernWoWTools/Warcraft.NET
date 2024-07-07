namespace Warcraft.NET.Compression.Squish
{
    public struct SingleColourLookup
    {
        public SourceBlock[] sources;

        public SingleColourLookup(SourceBlock a, SourceBlock b)
        {
            sources = new SourceBlock[] { a, b };
        }
    };
}
