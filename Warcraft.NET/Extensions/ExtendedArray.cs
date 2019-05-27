using System.Collections.Generic;
using System.Linq;

namespace Warcraft.NET.Extensions
{
    public static class ExtendedArray
    {
        /// <summary>
        /// Remove elemets from array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] array, int start, int count)
        {
            List<T> arrayList = array.ToList();
            arrayList.RemoveRange(start, count);
            return arrayList.ToArray();
        }
    }
}
