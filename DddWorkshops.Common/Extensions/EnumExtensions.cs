using System;
using System.Collections.Generic;
using System.Linq;

namespace DddWorkshops.Common.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<TEnum> GetEnumerationValues<TEnum>()
            where TEnum : Enum
            => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
    }
}