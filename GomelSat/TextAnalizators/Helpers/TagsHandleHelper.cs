using System;
using System.Text.RegularExpressions;

namespace TextAnalizators.Helpers
{
    public static class TagsHandleHelper
    {
        public static string RemoveTags(string textWithTags)
        {
            return Regex.Replace(textWithTags, "<[^>]*>", " ");
        }
    }
}
