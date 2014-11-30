using System;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.DataModel.Common
{
    public static class ExtensionMethods
    {
        public static List<TSource> ToSortedList<TSource>(this IEnumerable<TSource> source) where TSource : IComparable<TSource>
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "The source must not be null");
            }

            var q = source.OrderBy(item => item);
            return q.ToList();
        }
    }
}