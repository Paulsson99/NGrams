using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Dictionaries;
using System.Text.RegularExpressions;

namespace NaturalLanguageProcessing.TextData
{
    public class Sentence
    {
        private string text;
        private List<string> tokenList;
        private List<int> tokenIndexList;
        private DictionaryItemComparer comparer;

        public Sentence()
        {
            tokenList = new List<string>();
            tokenIndexList = new List<int>();
            comparer = new DictionaryItemComparer();
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
            string processedText = text.ToLower();
            // Remove the punctuation from the end of the string
            processedText = Regex.Replace(processedText, @"[^a-z0-9]+$", "");
            // Split the text into words and ignore empty entries
            string[] wordArray = processedText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // Do some final tweaks on each word
            foreach (string word in wordArray)
            {
                // Remove all special characters except a dot from the end a string (a dot is a abreviation as the end of sentence dot is already striped away)
                string processedWord = Regex.Replace(word, @"[^a-z0-9.]+$", "");
                // Remove all special characters from the start of a word
                processedWord = Regex.Replace(processedWord, @"^[^a-z0-9]+", "");
                // Add the word if it is not an empty string
                if (!string.IsNullOrEmpty(processedWord))
                {
                    tokenList.Add(processedWord);
                }
            }
        }

        public void BuildSentenceIndex(Dictionary dict)
        {
            // Loop over all tokens and find the corresponding index in the (sorted) dictionary and add that to the index list
            foreach (string token in this.tokenList)
            {
                int index = dict.ItemList.BinarySearch(new DictionaryItem(token), comparer);
                this.TokenIndexList.Add(index);
            }
        }

        public List<List<string>> NGrams(int n)
        {
            // Return a list of all possible n-grams in the sentence
            List<List<string>> nGrams = new List<List<string>>();
            // Loop over the start word in the n-gram
            for (int i=0; i + n - 1 < tokenList.Count; i++)
            {
                List<string> nGram = new List<string>();
                // Add n entris to the n-gram
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
