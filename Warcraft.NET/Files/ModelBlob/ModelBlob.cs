using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ModelBlob.Chunks;

namespace Warcraft.NET.Files.ModelBlob
{
    [AutoDocFile("blob")]
    public class ModelBlob : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the model blob version.
        /// </summary>
        [ChunkOrder(1)]
        public MBVR Version { get; set; }

        /// <summary>
        /// Gets or sets the blob model.
        /// </summary>
        [ChunkOrder(2)]
        public MBBB BlobModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBlob"/> class.
        /// </summary>
        public ModelBlob()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBlob"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public ModelBlob(byte[] inData) : base(inData)
        {
        }
    }
}
