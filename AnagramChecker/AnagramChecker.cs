using System.Linq;

namespace AnagramChecker
{
    public interface IAnagramChecker
    {
        bool IsAnagram(string string1, string string2);
    }

    public class AnagramChecker : IAnagramChecker
    {
        public bool IsAnagram(string string1, string string2)
        {
            if (string1.Length != string2.Length)
                return false;
            return string1.ToLower().All(c => string2.ToLower().IndexOf(c) != -1);
        }
    }
}
