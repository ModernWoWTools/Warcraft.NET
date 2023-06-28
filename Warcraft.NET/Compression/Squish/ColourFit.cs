namespace Warcraft.Compression.Squish
{
    public class ColourFit
    {
        protected ColourSet Colours { get; set; }

        protected SquishFlags Flags { get; set; }

        public ColourFit(ColourSet colours, SquishFlags flags)
        {
            Colours = colours;
            Flags = flags;
        }

        public void Compress(byte[] block, int offset)
        {
            bool isDxt1 = Flags.HasFlag(SquishFlags.DXT1);

            if (isDxt1)
            {
                Compress3(block, offset);

                if (!Colours.IsTransparent)
                {
                    Compress4(block, offset);
                }
            }
            else
            {
                Compress4(block, offset);
            }
        }

        public virtual void Compress3(byte[] block, int offset) { }

        public virtual void Compress4(byte[] block, int offset) { }
    }
}
