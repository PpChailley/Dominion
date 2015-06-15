using System;
using System.Collections.Generic;
using System.Linq;

namespace org.gbd.Dominion.Tools
{
    public static class Collections
    {

        private static readonly Random Rnd = new Random();

        /// <summary>
        /// http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static T Random<T>(this ICollection<T> me)
        {
            return me.Random(1).Single();
        }

        public static ICollection<T> Random<T>(this ICollection<T> me, int count)
        {
            var l = new List<T>(count);

            for (var i = 0; i < count; i++)
            {
                l.Add(me.ElementAt(Rnd.Next(me.Count)));
            }

            return l;
        }


        /// <summary>
        /// Return the last n elements of collection
        /// http://stackoverflow.com/questions/3453274/using-linq-to-get-the-last-n-elements-of-a-collection
        /// See also Take() method
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n)
        {
            return source.Skip(Math.Max(0, source.Count() - n));
        }

    }
}
