using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Common
{
    public class TypeComparer : Comparer<Type>
    {
        public override int Compare(Type x, Type y)
        {
            if (x == null) return -1;
            if (y == null) return 1;

            return string.Compare(x.FullName, y.FullName, StringComparison.OrdinalIgnoreCase);
        }
    }
}