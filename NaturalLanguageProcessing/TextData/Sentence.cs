using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Dictionaries;

namespace NaturalLanguageProcessing.TextData
{
    public class Sentence
    {
        private string text;
        private List<string> tokenList;
        private List<int> tokenIndexList;

        public Sentence()
        {
            tokenList = new List<string>();
            tokenIndexList = new List<int>();   
        }

        // Write this method:
        //
        // First, make the text lower-case (ToLower()...)
        // Remember to handle (remove) end-of-sentence markers, as well as "," and
        // quotation marks (if any). Also, make sure *not* to split abbreviations and contractions.
        //
        // Spend some effort on this method: It should be more than just a few lines - there are
        // many special cases to deal with!
        public void Tokenize()
        {

            // Fix better later
            string stripedText = this.text.ToLower().Trim(new char[] {' ', '?', '.', '!', ',', '-'});
            string[] unprocessedTokenList = stripedText.Split(' ');
            foreach (string token in unprocessedTokenList)
            {
                tokenList.Add(token.Trim(new char[] { '"', '\'' }));
            }
            
        }

        public void Indexinize(Dictionary dict, DictionaryItemComparer comparer)
        {
            foreach (string token in this.tokenList)
            {
                int index = dict.ItemList.BinarySearch(new DictionaryItem(token), comparer);
                this.TokenIndexList.Add(index);
            }
        }

        public List<List<string>> NGrams(int n)
        {
            List<List<string>> nGrams = new List<List<string>>();
            for (int i=0; i + n < tokenList.Count; i++)
            {
                List<string> nGram = new List<string>();
                for (int j=0; j < n; j++)
                {
                    nGram.Add(tokenList[i + j]);
                }
                nGrams.Add(nGram);
            }
            return nGrams;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public List<string> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public List<int> TokenIndexList
        {
            get { return tokenIndexList; }
            set { tokenIndexList = value; }
        }
    }
}
