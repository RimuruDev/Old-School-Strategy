using System.Collections.Generic;

namespace RimuruDev.External.GridLogic.Utils
{
    public class PriorityQueue<T> where T : System.IComparable<T>
    {
        private readonly List<T> data;

        public int Count =>
            data.Count;

        public PriorityQueue() =>
            data = new List<T>();

        public void Enqueue(T item)
        {
            data.Add(item);

            var childIndex = data.Count - 1;

            while (childIndex > 0)
            {
                var parentindex = (childIndex - 1) / 2;

                if (data[childIndex].CompareTo(data[parentindex]) >= 0)
                {
                    break;
                }

                (data[childIndex], data[parentindex]) = (data[parentindex], data[childIndex]);

                childIndex = parentindex;
            }
        }

        public T Dequeue()
        {
            var lastindex = data.Count - 1;

            var frontItem = data[0];

            data[0] = data[lastindex];
            data.RemoveAt(lastindex);

            lastindex--;

            var parentindex = 0;

            while (true)
            {
                var childindex = parentindex * 2 + 1;

                if (childindex > lastindex)
                {
                    break;
                }

                var rightchild = childindex + 1;
                if (rightchild <= lastindex && data[rightchild].CompareTo(data[childindex]) < 0)
                {
                    childindex = rightchild;
                }

                if (data[parentindex].CompareTo(data[childindex]) <= 0)
                {
                    break;
                }

                (data[parentindex], data[childindex]) = (data[childindex], data[parentindex]);

                parentindex = childindex;
            }


            return frontItem;
        }

        public T Peek()
        {
            var frontItem = data[0];

            return frontItem;
        }

        public bool Contains(T item) =>
            data.Contains(item);

        public List<T> ToList() =>
            data;
    }
}