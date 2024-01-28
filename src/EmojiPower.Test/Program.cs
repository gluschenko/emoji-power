using EmojiPower.Core;

namespace EmojiPower.Test;

public partial class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = nameof(EmojiPower);
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        
        var httpClient = new HttpClient();

        try
        {
            var extractor = new EmojiExtractor(new HttpClient());
            var descriptors = await extractor.ExtractAsync();
            var text = string.Join("", descriptors.SelectMany(x => x.GetSymbols()));
            Console.WriteLine($"Lazy result:\n{text}\n\n");

            descriptors = Emoji.GetEmoji();
            Console.WriteLine($"Source-generated result:\n{text}\n\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        Console.ReadKey();
    }
}
