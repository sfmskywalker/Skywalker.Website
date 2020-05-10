using Skywalker.OrchardCore.ContentExtensions.Services.Contracts;

namespace Skywalker.OrchardCore.ContentExtensions.Services
{
    public class ReadTimeCalculator : IReadTimeCalculator
    {
        private readonly IWordCounter _wordCounter;

        public ReadTimeCalculator(IWordCounter wordCounter)
        {
            _wordCounter = wordCounter;
        }

        public int CalculateReadTime(string text)
        {
            var wordCount = _wordCounter.CountWords(text);
            var value = wordCount / 200d;
            var minutes = (int) value;
            var seconds = ((value % 2) * 0.6d) * 100;

            if (seconds >= 30)
                minutes++;

            return minutes;
        }
    }
}