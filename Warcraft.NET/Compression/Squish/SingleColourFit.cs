using System.Numerics;

namespace Warcraft.NET.Compression.Squish
{
    public class SingleColourFit : ColourFit
    {
        byte[] m_colour = new byte[3];
        Vector3 m_start;
        Vector3 m_end;
        byte m_index;
        int m_error;
        int m_besterror;

        public SingleColourFit(ColourSet colours, SquishFlags flags)
            : base(colours, flags)
        {
            // grab the single colour
            Vector3[] values = Colours.Points;
            m_colour[0] = (byte)ColourBlock.FloatToInt(255.0f * values[0].X, 255);
            m_colour[1] = (byte)ColourBlock.FloatToInt(255.0f * values[0].Y, 255);
            m_colour[2] = (byte)ColourBlock.FloatToInt(255.0f * values[0].Z, 255);

            // initialise the best error
            m_besterror = int.MaxValue;
        }

        public void ComputeEndPoints(SingleColourLookup[][] lookups)
        {
            // check each index combination (endpoint or intermediate)
            m_error = int.MaxValue;
            for (int index = 0; index < 2; ++index)
            {
                // check the error for this codebook index
                SourceBlock[] sources = new SourceBlock[3];
                int error = 0;
                for (int channel = 0; channel < 3; ++channel)
                {
                    // grab the lookup table and index for this channel
                    SingleColourLookup[] lookup = lookups[channel];
                    int target = m_colour[channel];

                    // store a pointer to the source for this channel
                    sources[channel] = lookup[target].sources[index];

                    // accumulate the error
                    int diff = sources[channel].error;
                    error += diff * diff;
                }

                // keep it if the error is lower
                if (error < m_error)
                {
                    m_start = new Vector3(
                            (float)sources[0].start / 31.0f,
                            (float)sources[1].start / 63.0f,
                            (float)sources[2].start / 31.0f
                    );
                    m_end = new Vector3(
                            (float)sources[0].end / 31.0f,
                            (float)sources[1].end / 63.0f,
                            (float)sources[2].end / 31.0f
                    );
                    m_index = (byte)(2 * index);
                    m_error = error;
                }
            }
        }

        public override void Compress3(byte[] block, int offset)
        {
            // build the table of lookups
            SingleColourLookup[][] lookups = new SingleColourLookup[][]
            {
                SingleColourLookupIns.Lookup53, 
                SingleColourLookupIns.Lookup63, 
                SingleColourLookupIns.Lookup53
            };

            // find the best end-points and index
            ComputeEndPoints(lookups);

            // build the block if we win
            if (m_error < m_besterror)
            {
                // remap the indices
                byte[] indices = new byte[16];
                Colours.RemapIndices(new byte[] { m_index }, indices);

                // save the block
                ColourBlock.WriteColourBlock3(m_start, m_end, indices, block, offset);

                // save the error
                m_besterror = m_error;
            }
        }

        public override void Compress4(byte[] block, int offset)
        {
            // build the table of lookups
            SingleColourLookup[][] lookups = new SingleColourLookup[][]
            {
                SingleColourLookupIns.Lookup54, 
                SingleColourLookupIns.Lookup64, 
                SingleColourLookupIns.Lookup54
            };
        
            // find the best end-points and index
            ComputeEndPoints( lookups );
        
            // build the block if we win
            if( m_error < m_besterror )
            {
                    // remap the indices
                    byte[] indices = new byte[16];
                    Colours.RemapIndices(new byte[] { m_index }, indices);
                
                    // save the block
                    ColourBlock.WriteColourBlock4( m_start, m_end, indices, block, offset );

                    // save the error
                    m_besterror = m_error;
            }
        }
    }
}
