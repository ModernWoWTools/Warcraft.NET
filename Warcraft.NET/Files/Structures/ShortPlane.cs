using System;
using System.Collections.Generic;

namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a world Z-aligned plane with nine coordinates.
    /// </summary>
    public struct ShortPlane
    {
        /// <summary>
        /// The 3x3 grid of coordinates in the plane.
        /// </summary>
        public List<List<short>> Coordinates;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortPlane"/> struct from a jagged list of coordinates.
        /// </summary>
        /// <param name="inCoordinates">A list of coordinates.</param>
        /// <exception cref="ArgumentException">
        /// An <see cref="ArgumentException"/> will be thrown if the input list is not a 3x3 jagged list of coordinates.
        /// </exception>
        public ShortPlane(List<List<short>> inCoordinates)
        {
            if (inCoordinates.Count != 3)
            {
                throw new ArgumentException("The input coordinate list must be a 3x3 grid of coordinates.", nameof(inCoordinates));
            }

            for (var i = 0; i < 3; ++i)
            {
                if (inCoordinates[i].Count != 3)
                {
                    throw new ArgumentException("The input coordinate list must be a 3x3 grid of coordinates.", nameof(inCoordinates));
                }
            }

            Coordinates = inCoordinates;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortPlane"/> struct from a single short value, which is applied to all nine coordinates.
        /// </summary>
        /// <param name="inAllCoordinates">The short to use for all coordinates.</param>
        public ShortPlane(short inAllCoordinates)
        {
            Coordinates = new List<List<short>>();

            for (var y = 0; y < 3; ++y)
            {
                var coordinateRow = new List<short>();
                for (var x = 0; x < 3; ++x)
                {
                    coordinateRow.Add(inAllCoordinates);
                }

                Coordinates.Add(coordinateRow);
            }
        }
    }
}