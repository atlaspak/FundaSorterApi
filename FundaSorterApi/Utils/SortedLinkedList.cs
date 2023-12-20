using FundaSorterApi.Models;
using System.Collections.Generic;

namespace FundaSorterApi.Utils
{
    //average seek time complexity is O(1), worst case O(N)
    //worst case insert complexity is O(1)
    //worst case arithmetical addition complexity is O(N)
    //That's why this algorithm efficient when incremental or decremental is 1 (eg. FundaSorterApi)
    public class SortedLinkedList<T>
    {
        private struct ElementWithCount
        {
            public T First;
            public int Count;

            public ElementWithCount(T first, int count)
            {
                this.First = first;
                this.Count = count;
            }
        }
        private Dictionary<T, LinkedListNode<ElementWithCount>> elementMap;
        private LinkedList<ElementWithCount> sortedLList;
        public SortedLinkedList()
        {
            elementMap = new Dictionary<T, LinkedListNode<ElementWithCount >>();
            sortedLList = new LinkedList<ElementWithCount>();
        }

        public void increaseElement(T key, int increaseBy = 1)
        {
            if(elementMap.ContainsKey(key)) 
            {
                // arrange the necessary increments
                LinkedListNode<ElementWithCount> currentNode = elementMap[key];
                currentNode.ValueRef.Count += increaseBy;

                //swap it if current value is bigger than it's neighbour
                while(currentNode.Previous != null &&
                    currentNode.ValueRef.Count > currentNode.Previous.ValueRef.Count)
                {
                    //swap
                    ElementWithCount temp = currentNode.Value;
                    currentNode.Value = currentNode.Previous.Value;
                    currentNode.Previous.Value = temp;

                    //traverse
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                //create a new element
                elementMap[key] = 
                    sortedLList.AddLast(new ElementWithCount(key, increaseBy));
            }
        }

        public void decreaseElement(T key, int decreaseBy = 1) 
        {
            if (elementMap.ContainsKey(key))
            {
                // arrange the necessary increments
                LinkedListNode<ElementWithCount> currentNode = elementMap[key];
                currentNode.ValueRef.Count -= decreaseBy;

                //if it has a non positive value, it's redundant to this data structure
                if(currentNode.ValueRef.Count >= 0)
                {
                    sortedLList.Remove(elementMap[key]);
                }

                //swap it if current value is smaller than it's neighbour
                while (currentNode.Next != null &&
                    currentNode.ValueRef.Count < currentNode.Next.ValueRef.Count)
                {
                    //swap
                    ElementWithCount temp = currentNode.Value;
                    currentNode.Value = currentNode.Next.Value;
                    currentNode.Next.Value = temp;

                    //traverse
                    currentNode = currentNode.Next;
                }
            }
            else
            {
                //This should be impossible
            }
        }
    }
}
