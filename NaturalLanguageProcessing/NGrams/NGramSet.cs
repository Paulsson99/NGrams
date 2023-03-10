using NaturalLanguageProcessing.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.NGrams
{
    public class NGramSet
    {
        private List<NGram> itemList;
        private NGramComparer comparer;
        private List<NGram> tempItemList;

        public NGramSet()
        {
            itemList = new List<NGram>();
            comparer = new NGramComparer();
            tempItemList = new List<NGram>();
        }

        public void Append(List<string> tokenList)
        {
            NGram nGram = new NGram(tokenList);

            // Option 1: Just add 2-grams to the itemList. Then, in the end, sort them based
            // on the tokenString, then count (i.e. as in Method1 in the Dictionary.Build() method). 
            // I have not tried this method here, but it should be fine,
            // perhaps even faster than Option 1.

            // Option 2: Use binary search (on the tokenString of the nGram)
            // to find its index. If the index is negative, then this nGram is not
            // yet in the list. If so, insert it *in the appropriate location* to
            // keep the nGramList sorted (based on the tokenString). Try to figure
            // out how to do that (e.g. on StackOverflow) - if you cannot, then ask 
            // the examiner or the assistant.
            // Only a few lines of code are needed for this method... :)
            // However, it will take quite a while to run -- the binary search
            // becomes increasingly slower as the list grows.
            //
            //
            // (There may be other options too...)

            // Add code here

            // Just add the nGram to a temporary list and call Process() when all n-grams have been added
            tempItemList.Add(nGram);

            //int index = itemList.BinarySearch(nGram, comparer);
            //if (index < 0)
            //{
            //    index = ~index;
            //    itemList.Insert(index, nGram);
            //}
            //else
            //{
            //    itemList[index].NumberOfInstances += 1;
            //}
        }

        public void Process()
        {
            // Sort the temporary list and count how many items are the same in a row
            tempItemList.Sort(comparer);

            int count = 0;
            NGram previousNGram = tempItemList[0];

            foreach (NGram currentNGram in tempItemList)
            {
                if (previousNGram.TokenString != currentNGram.TokenString)
                {
                    // Add the previous item when we see a new one
                    previousNGram.NumberOfInstances = count;
                    itemList.Add(previousNGram);
                    // Reset the count and update the previous token
                    count = 0;
                    previousNGram = currentNGram;
                }
                // Update the count
                count += 1;
            }
            // Add the last item to the dictionary
            previousNGram.NumberOfInstances = count;
            itemList.Add(previousNGram);

            // Clear the temporary list to free up memory
            tempItemList.Clear();
        }

        public void DropRare(int cutoff)
        {
            // Drop all items with fewer instances then cutoff
            itemList.RemoveAll(item => item.NumberOfInstances <= cutoff);
        }

        // This method you get for free... :)
        public void SortOnFrequency()
        {
            itemList = itemList.OrderByDescending(n => n.NumberOfInstances).ToList();
        }

        public List<NGram> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
