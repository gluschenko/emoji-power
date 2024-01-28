using System.Collections.Generic;
using System.Linq;
using System;

namespace EmojiPower.Core.Models
{
    public sealed class SingleUnicodeDescriptor : BaseUnicodeDescriptor
    {
        public string Value { get; set; }

        public override IEnumerable<string> GetSymbols()
        {
            var result = new List<string>();

            var values = Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Single
            if (values.Length == 1)
            {
                var str = Convert.ToInt32(values[0], 16);
                result.Add(char.ConvertFromUtf32(str));
            }
            // Sequence
            else if (values.Length > 1)
            {
                var str = string.Join("", values.Select(x => char.ConvertFromUtf32(Convert.ToInt32(x, 16))));
                result.Add(str);
            }

            return result;
        }
    }

}
