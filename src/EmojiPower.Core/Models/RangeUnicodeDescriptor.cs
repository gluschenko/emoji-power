using System.Collections.Generic;
using System;

namespace EmojiPower.Core.Models
{
    public sealed class RangeUnicodeDescriptor : BaseUnicodeDescriptor
    {
        public string Start { get; set; }
        public string End { get; set; }

        public override IEnumerable<string> GetSymbols()
        {
            var result = new List<string>();

            var a = Convert.ToInt32(Start, 16);
            var b = Convert.ToInt32(End, 16);

            var min = Math.Min(a, b);
            var max = Math.Max(a, b);

            for (var i = min; i <= max; i++)
            {
                result.Add(char.ConvertFromUtf32(i));
            }

            return result;
        }
    }

}
