using System.Collections.Generic;

namespace EmojiPower.Core.Models
{
    public abstract class BaseUnicodeDescriptor
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

        public abstract IEnumerable<string> GetSymbols();
    }
}
