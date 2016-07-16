using System;
using System.Collections.Generic;

namespace DataParsers.Models.ModelTools
{
    public class GomelSatNewsHeaderModelEqualityComparer : IEqualityComparer<GomelSatNewsHeaderModel>
    {
        public bool Equals(GomelSatNewsHeaderModel x, GomelSatNewsHeaderModel y)
        {
            return string.Equals(x.Link, y.Link, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(GomelSatNewsHeaderModel obj)
        {
            return obj.Link.GetHashCode();
        }
    }
}
