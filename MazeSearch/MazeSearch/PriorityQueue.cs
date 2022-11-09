using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSearch
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();


        // O(logN)
        public void Push(T data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입한다.
            _heap.Add(data);

            // 힙의 맨 마지막에서 시작한다.
            int now = _heap.Count - 1;

            // now가 힙의 index내에 있다면 계속 시도함
            while (now > 0)
            {
                int next = (now - 1) / 2;

                // 부모노드의 값이 나보다 크면 break
                if (_heap[now].CompareTo(_heap[next]) < 0)
                    break;

                // 내가 부모노드보다 값이 크거나 같으면 교체한다.
                T temp = _heap[next];
                _heap[next] = _heap[now];
                _heap[now] = temp;

                // 검사 위치를 이동한다.
                now = next;
            }
        }

        // log(N)
        public T Pop()
        {
            // 가장 큰 값 Root (index : 0) 을 반환한다.
            T ret = _heap[0];

            int lastIndex = _heap.Count - 1;
            // 마지막 데이터를 루트로 이동한다
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            int now = 0;

            while (true)
            {
                int left = (now * 2) + 1;
                int right = (now * 2) + 2;
                int next = now;

                if (left <= _heap.Count - 1 && _heap[left].CompareTo(_heap[next]) > 0)
                    next = left;
                if (right <= _heap.Count - 1 && _heap[right].CompareTo(_heap[next]) > 0)
                    next = right;

                // 왼쪽 & 오른쪽 모두 현재값보다 작으면 종료
                if (next == now)
                    break;

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;

            }


            return ret;
        }

        public int Count()
        {
            int count = _heap.Count;

            return count;
        }
    }
}
