using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDL.Chunks;
using Warcraft.NET.Types;

namespace Warcraft.NET.Files.WDL
{
    public abstract class WorldDataLodBase : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the WDL version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; } = new MVER(18);

        /// <summary>
        /// Gets the map area offsets.
        /// </summary>
        [ChunkOrder(6)]
        public MAOF MapAreaOffsets { get; set; } = MAOF.CreateEmpty();

        /// <summary>
        /// Gets or sets the map areas.
        /// </summary>
        public SynchronizedList<MARE?> MapAreas { get; set; } = new SynchronizedList<MARE?>(new List<MARE?>(4096));


        /// <summary>
        /// Gets or sets the map area holes.
        /// </summary>
        public SynchronizedList<MAHO?> MapAreaHoles { get; set; } = new SynchronizedList<MAHO?>(new List<MAHO?>(4096));

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        public WorldDataLodBase()
        {
            // Set up the two area lists with default values
            for (var i = 0; i < 4096; ++i)
            {
                MapAreas.Add(null);
                MapAreaHoles.Add(null);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldDataLodBase(byte[] inData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the LOD world has an entry at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>true if the LOD world has an entry at the given coordinate; otherwise, false.</returns>
        public bool HasEntry(int x, int y)
        {
            if (x < 0 || y < 0 || x > 63 || y > 63)
                return false;

            var index = x + (y * 64);
            return MapAreas[index] != null;
        }

        /// <summary>
        /// Gets the entry at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The entry.</returns>
        public MARE GetEntry(int x, int y)
        {
            if (x < 0 || y < 0 || x > 63 || y > 63)
                throw new ArgumentException();

            var index = x + (y * 64);
            var entry = MapAreas[index];
            if (entry is null)
                throw new InvalidOperationException();

            return entry;
        }

        /// <inheritdoc/>
        public abstract byte[] Serialize();
    }
}
