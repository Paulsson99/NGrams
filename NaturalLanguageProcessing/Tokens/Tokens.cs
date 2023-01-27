using NaturalLanguageProcessing.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.Tokens
{
    public class SharedToken
    {
        private const int INSTANCE_FORMAT_WIDTH = 8;

        private string token;
        private double ratio;

        public SharedToken(string tokenText, int spokenCount, int writtenCount)
        {
            token = tokenText;
            ratio = (double)writtenCount / (double)spokenCount;
        }

        public string AsString()
        {
            return ratio.ToString("0.#####").PadLeft(INSTANCE_FORMAT_WIDTH) + " " + token;
        }

        public string Token
        { 
            get { return token; } 
        }

        public double Ratio
        { 
            get { return ratio; }
        }
    }

    public class SharedTokenComparer : IComparer<SharedToken>
    {
        public int Compare(SharedToken item1, SharedToken item2)
        {
            return item1.Ratio.CompareTo(item2.Ratio);
        }
    }
}
