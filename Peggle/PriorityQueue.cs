using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peggle
{
    class PriorityQueue<TCompare,TValue>
    {
        public KeyValuePair<TCompare, TValue> this[int index]
        {
            get
            {
                return baseStorage[index];
            }
        }

        List<KeyValuePair<TCompare, TValue>> baseStorage = new List<KeyValuePair<TCompare, TValue>>();
        IComparer<TCompare> comparer;

        public PriorityQueue(IComparer<TCompare> comparer)
        {
            this.comparer = comparer;
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

        public TValue first()
        {
            return baseStorage.First().Value;
        }

        public TValue last()
        {
            return baseStorage.Last().Value;
        }

        public int count()
        {
            return baseStorage.Count;
        }

    }
}
