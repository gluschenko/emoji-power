using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using EmojiPower.Core.Models;

namespace EmojiPower.Core
{
    public class EmojiExtractor
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public EmojiExtractor(
            HttpClient httpClient,
            string baseUrl = "https://unicode.org/Public/emoji/latest/emoji-sequences.txt"
        )
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BaseUnicodeDescriptor>> ExtractAsync()
        {
            var result = new List<BaseUnicodeDescriptor>();

            var response = await _httpClient.GetStringAsync(_baseUrl);

            var lines = response.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();

            foreach (var line in lines)
            {
                var lineArray = line.Split(new[] { '#' }, 2);
                if (lineArray.Length > 0)
                {
                    var content = lineArray[0];
                    var comment = lineArray.Length > 1 ? lineArray[1] : null;

                    var contentArray = content.Split(new[] { ';' });

                    var value = contentArray.Length > 0 ? contentArray[0].Trim() : null;
                    var typeString = contentArray.Length > 1 ? contentArray[1].Trim() : null;
                    var descriptionString = contentArray.Length > 2 ? contentArray[2].Trim() : null;

                    if (value is null || string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    var rangeArray = value.Split(new[] { ".." }, 2, StringSplitOptions.RemoveEmptyEntries);
                    var rangeStart = rangeArray.Length > 0 ? rangeArray[0].Trim() : null;
                    var rangeEnd = rangeArray.Length > 1 ? rangeArray[1].Trim() : null;

                    if (rangeStart is null)
                    {
                        continue;
                    }

                    if (typeString is null)
                    {
                        continue;
                    }

                    if (rangeEnd is null)
                    {
                        result.Add(new SingleUnicodeDescriptor
                        {
                            Value = rangeStart,
                            Type = typeString,
                            Description = descriptionString,
                            Comment = comment,
                        });
                    }
                    else
                    {
                        result.Add(new RangeUnicodeDescriptor
                        {
                            Start = rangeStart,
                            End = rangeEnd,
                            Type = typeString,
                            Description = descriptionString,
                            Comment = comment,
                        });
                    }
                }
            }

            return result;
        }
    }

}
