using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Warcraft.NET.Extensions
{
    public static class ExtendedArray
    {
        /// <summary>
        /// Remove elements from array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] array, int start, int count)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            List<T> arrayList = array.ToList();
            arrayList.RemoveRange(start, count);
            return arrayList.ToArray();
        }
        
        /// <summary>
        /// Sets all elements of the array to a value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Fill<T>(this T[] array, T value)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        public static float[] ToArray(this Vector3 vector) => [vector.X, vector.Y, vector.Z];
    }
}
