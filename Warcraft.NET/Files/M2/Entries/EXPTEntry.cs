using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class EXPTEntry
    {

        /// <summary>
        /// replaces zSource from ParticleEmitter
        /// </summary>
        public float ZSource;
        /// <summary>
        /// colorMult is applied against particle's diffuse color 
        /// </summary>
        public float ColorMult;
        /// <summary>
        ///alphaMult is applied against particle's opacity. 
        /// </summary>
        public float AlphaMult;

        /// <summary>
        /// Initializes a new instance of the <see cref="EXPTEntry"/> class.
        /// </summary>
        public EXPTEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EXPTEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public EXPTEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                ZSource = br.ReadSingle();
                ColorMult = br.ReadSingle();
                AlphaMult = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a animation file id entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 12;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(ZSource);
                    bw.Write(ColorMult);
                    bw.Write(AlphaMult);
                }
                return ms.ToArray();
            }
        }
    }
}