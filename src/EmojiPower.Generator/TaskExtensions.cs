using System.Threading.Tasks;

namespace EmojiPower.Generator
{
    public static class TaskExtensions
    {
        public static T GetResult<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}
