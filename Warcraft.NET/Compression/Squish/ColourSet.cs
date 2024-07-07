using System;
using System.Numerics;

namespace Warcraft.NET.Compression.Squish
{
    public class ColourSet
    {
        public int Count { get; set; } = 0;

        public Vector3[] Points { get; set; } = new Vector3[16];

        public float[] Weights { get; set; } = new float[16];

        private int[] Remap { get; set; } = new int[16];

        public bool IsTransparent { get; set; } = false;

        public ColourSet(byte[] rgba, int mask, SquishFlags flags)
        {
            // check the compression mode for dxt1
            bool isDxt1 = flags.HasFlag(SquishFlags.DXT1);
            bool weightByAlpha = flags.HasFlag(SquishFlags.WeightColourByAlpha);

            // create the minimal set
            for (int i = 0; i < 16; ++i)
            {
                // check this pixel is enabled
                int bit = 1 << i;

                if ((mask & bit) == 0)
                {
                    Remap[i] = -1;
                    continue;
                }

                // check for transparent pixels when using dxt1
                if (isDxt1 && rgba[4 * i + 3] < 128)
                {
                    Remap[i] = -1;
                    IsTransparent = true;

                    continue;
                }

                // loop over previous points for a match
                for (int j = 0; ; ++j)
                {
                    // allocate a new point
                    if (j == i)
                    {
                        // normalise coordinates to [0,1]
                        float x = rgba[4 * i] / 255.0f;
                        float y = rgba[4 * i + 1] / 255.0f;
                        float z = rgba[4 * i + 2] / 255.0f;

                        // ensure there is always non-zero weight even for zero alpha
                        float w = (rgba[4 * i + 3] + 1) / 256.0f;

                        // add the point
                        Points[Count] = new Vector3(x, y, z);
                        Weights[Count] = weightByAlpha ? w : 1.0f;
                        Remap[i] = Count;

                        // advance
                        ++Count;
                        break;
                    }

                    // check for a match
                    int oldbit = 1 << j;
                    bool match = ((mask & oldbit) != 0) &&
                        (rgba[4 * i] == rgba[4 * j]) &&
                        (rgba[4 * i + 1] == rgba[4 * j + 1]) &&
                        (rgba[4 * i + 2] == rgba[4 * j + 2]) &&
                        (rgba[4 * j + 3] >= 128 || !isDxt1);

                    if (match)
                    {
                        // get the index of the match
                        int index = Remap[j];

                        // ensure there is always non-zero weight even for zero alpha
                        float w = (rgba[4 * i + 3] + 1) / 256.0f;

                        // map to this point and increase the weight
                        Weights[index] += (weightByAlpha ? w : 1.0f);
                        Remap[i] = index;
                        break;
                    }
                }
            }

            // square root the weights
            for (int i = 0; i < Count; ++i)
            {
                Weights[i] = (float)Math.Sqrt(Weights[i]);
            }
        }

        public void RemapIndices(byte[] source, byte[] target)
        {
            for (int i = 0; i < 16; ++i)
            {
                int j = Remap[i];
                if (j == -1)
                {
                    target[i] = 3;
                }
                else
                {
                    target[i] = source[j];
                }
            }
        }
    }
}
