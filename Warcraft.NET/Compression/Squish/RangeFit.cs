using System;
using System.Numerics;

namespace Warcraft.NET.Compression.Squish
{
    public class RangeFit : ColourFit
    {
        Vector3 m_metric;
        Vector3 m_start;
        Vector3 m_end;
        float m_besterror;

        public RangeFit(ColourSet colours, SquishFlags flags, float? metric)
            : base(colours, flags)
        {
            // initialise the metric (old perceptual = 0.2126f, 0.7152f, 0.0722f)
            if (metric != null)
            {
                //m_metric = new Vector3( metric[0], metric[1], metric[2] );
            }
            else
            {
                m_metric = new Vector3(1.0f);
            }

            // initialise the best error
            m_besterror = float.MaxValue;

            // cache some values
            int count = Colours.Count;
            Vector3[] values = Colours.Points;
            float[] weights = Colours.Weights;

            // get the covariance matrix
            Sym3x3 covariance = Sym3x3.ComputeWeightedCovariance(count, values, weights);

            // compute the principle component
            Vector3 principle = Sym3x3.ComputePrincipleComponent(covariance);

            // get the min and max range as the codebook endpoints
            Vector3 start = new Vector3(0.0f);
            Vector3 end = new Vector3(0.0f);
            if (count > 0)
            {
                float min, max;

                // compute the range
                start = end = values[0];
                min = max = Vector3.Dot(values[0], principle);
                for (int i = 1; i < count; ++i)
                {
                    float val = Vector3.Dot(values[i], principle);
                    if (val < min)
                    {
                        start = values[i];
                        min = val;
                    }
                    else if (val > max)
                    {
                        end = values[i];
                        max = val;
                    }
                }
            }

            // clamp the output to [0, 1]
            Vector3 one = new Vector3(1.0f);
            Vector3 zero = new Vector3(0.0f);
            start = Vector3.Min(one, Vector3.Max(zero, start));
            end = Vector3.Min(one, Vector3.Max(zero, end));

            // clamp to the grid and save
            Vector3 grid = new Vector3(31.0f, 63.0f, 31.0f);
            Vector3 gridrcp = new Vector3(1.0f / 31.0f, 1.0f / 63.0f, 1.0f / 31.0f);
            Vector3 half = new Vector3(0.5f);
            m_start = Helpers.Truncate(grid * start + half) * gridrcp;
            m_end = Helpers.Truncate(grid * end + half) * gridrcp;
        }

        public override void Compress3(byte[] block, int offset)
        {
            // cache some values
            int count = Colours.Count;
            Vector3[] values = Colours.Points;

            // create a codebook
            Vector3[] codes = new Vector3[3];
            codes[0] = m_start;
            codes[1] = m_end;
            codes[2] = 0.5f * m_start + 0.5f * m_end;

            // match each point to the closest code
            byte[] closest = new byte[16];
            float error = 0.0f;
            for (int i = 0; i < count; ++i)
            {
                // find the closest code
                float dist = float.MaxValue;
                int idx = 0;
                for (int j = 0; j < 3; ++j)
                {
                    float d = (m_metric * (values[i] - codes[j])).LengthSquared();
                    if (d < dist)
                    {
                        dist = d;
                        idx = j;
                    }
                }

                // save the index
                closest[i] = (byte)idx;

                // accumulate the error
                error += dist;
            }

            // save this scheme if it wins
            if (error < m_besterror)
            {
                // remap the indices
                byte[] indices = new byte[16];
                Colours.RemapIndices(closest, indices);

                // save the block
                ColourBlock.WriteColourBlock3(m_start, m_end, indices, block, offset);

                // save the error
                m_besterror = error;
            }
        }

        public override void Compress4(byte[] block, int offset) 
        {
            throw new NotImplementedException("RangeFit.Compress4");
        }
    }
}
