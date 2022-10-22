using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace EasyGames.Sources.Utils
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable) where T: class
        {
            return enumerable.Where(e => e != null);
        }

        /// <summary>Adds a collection to a hashset.</summary>
        /// <param name="hashSet">The hashset.</param>
        /// <param name="range">The collection.</param>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> range)
        {
            foreach (T obj in range)
                hashSet.Add(obj);
        }
        #region Pick

        public static T Pick<T>(this IEnumerable<T> enumerable)
        {
            return Pick(enumerable.ToList());
        }

        public static T Pick<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count - 1)];
        }

        public static T PickOrDefault<T>(this IList<T> list)
        {
            return list.Count == 0 ? default(T) : Pick(list);
        }

        public static T PickOrDefault<T>(this IEnumerable<T> enumerable)
        {
            return PickOrDefault(enumerable.ToArray());
        }

        #endregion
    }
}