namespace Skywalker.OrchardCore.ContentExtensions.Extensions
{
    public static class StringExtensions
    {
        public static string StripHtml(this string text)
        {
            var stringValue = text;
            var chArray = new char[stringValue!.Length];
            var length = 0;
            var flag = false;
            
            foreach (var ch in stringValue)
            {
                switch (ch)
                {
                    case '<':
                        flag = true;
                        break;
                    case '>':
                        flag = false;
                        break;
                    default:
                        if (!flag) 
                            chArray[length++] = ch;
                        break;
                }
            }
            
            return new string(chArray, 0, length);
        }
    }
}