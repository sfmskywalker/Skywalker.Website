using System;
using Skywalker.OrchardCore.ContentExtensions.Services.Contracts;

namespace Skywalker.OrchardCore.ContentExtensions.Services
{
    public class WordCounter : IWordCounter
    {
        public int CountWords(string text)
        {
            var delimiters = new[] {' ', '\r', '\n'};
            return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}