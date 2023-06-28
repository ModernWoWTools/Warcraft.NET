using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Warcraft.Extensions
{
    /// <summary>
    /// Extension methods used internally in the library for graphics classes.
    /// </summary>
    public static class ExtendedGraphics
    {
        /// <summary>
        /// Determines whether or not a given bitmap has any alpha values, and thus if it requires an alpha channel in
        /// other formats.
        /// </summary>
        /// <param name="map">The map to inspect.</param>
        /// <returns><value>true</value> if the bitmap has any alpha values; otherwise, <value>false</value>.</returns>
        public static bool HasAlpha(this Image<Rgba32> map)
        {
            for (var y = 0; y < map.Height; ++y)
            {
                for (var x = 0; x < map.Width; ++x)
                {
                    var pixel = map[x, y];
                    if (pixel.A != 255)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Span<T> GetPixelSpan<T>(this Image<T> imageRef) where T : unmanaged, IPixel<T>
        {
            var memoryFootprint = new T[imageRef.Width * imageRef.Height];
            var pixelSpan = new Span<T>(memoryFootprint);
            imageRef.CopyPixelDataTo(pixelSpan);
            return pixelSpan;
        }
    }
}
