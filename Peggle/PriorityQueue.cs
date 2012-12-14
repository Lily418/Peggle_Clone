using System.Collections.Generic;
using System.Linq;

namespace Peggle
{
    class PriorityQueue<TCompare,TValue>
    {
        

        List<KeyValuePair<TCompare, TValue>> baseStorage = new List<KeyValuePair<TCompare, TValue>>();
        IComparer<TCompare> comparer;

        public PriorityQueue(IComparer<TCompare> comparer)
        {
            this.comparer = comparer;
        }

        public KeyValuePair<TCompare, TValue> this[int index]
        {
            get
            {
                return baseStorage[index];
            }
        }



        public void enqueue(KeyValuePair<TCompare, TValue> element)
        {
            bool added = false;
            int index = 0;
            while (!added)
            {
                if (index >= baseStorage.Count)
                {
                    baseStorage.Add(element);
                    added = true;
                }
                else
                {
                    if (comparer.Compare(element.Key, baseStorage.ElementAt(index).Key) <= 0)
                    {
                        baseStorage.Insert(index, element);
                        added = true;
                    }
                    else
                    {
                        index++;
                    }
                }
            }

        }

        public KeyValuePair<TCompare, TValue> first()
        {
            return baseStorage.First();
        }

        public KeyValuePair<TCompare, TValue> last()
        {
            return baseStorage.Last();
        }

        public int count()
        {
            return baseStorage.Count;
        }

    }
}
