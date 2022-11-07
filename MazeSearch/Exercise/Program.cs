using System;
using System.Numerics;

namespace Exercise
{
    static class Graph
    {
        public static int[,] adj = new int[6, 6]
        {
            { 0, 1, 0, 1, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 1, 0 },
        };

        static List<int>[] adjList = new List<int>[]
        {
            new List<int>() { 1, 3 },
            new List<int>() { 0, 2, 3 },
            new List<int>() { 1 },
            new List<int>() { 0, 1, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 4 },
        };

        public static int[,] adjDijikstra = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 05, 10, -1, -1 },
            { -1, 05, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 05, -1 },
            { -1, -1, -1, 05, -1, 05 },
            { -1, -1, -1, -1, 05, -1 },
        };
    }

    class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        public TreeNode()
        {

        }

        public TreeNode(T data, List<TreeNode<T>> children)
        {
            Data = data;
            Children = children;
        }

        public TreeNode(T data)
        {
            Data = data;
        }
    }

    class Goni : IComparable<Goni>
    {
        public int Id { get; set; }

        public int CompareTo(Goni other)
        {
            if (Id == other.Id)
                return 0;

            return Id > other.Id ? 1 : -1;
        }
    }

    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> heap = new List<T>();

        public void Push(T data)
        {
            // 힙의 맨 끝에 새로운 데이터를 삽입한다.
            heap.Add(data);

            // 힙의 맨 끝에 데이터를 넣어줬기 때문에 마지막 인덱스에서 시작한다.
            int now = heap.Count - 1;

            // 등반신 도장깨기 시작
            while (now > 0)
            {
                // 도장깨기 시도
                int next = (now - 1) / 2;

                // 자식의 노드가 부모의 노드보다 작다 == 본래의 위치다
                if (heap[now].CompareTo(heap[next]) < 0)
                    break;

                // 자식의 노드가 부모의 노드보다 크다
                // 두 값을 교체한다
                T temp = heap[now];
                heap[now] = heap[next];
                heap[next] = temp;

                // 검사 위치를 이동한다.
                now = next;
            }
        }

        public T Pop()
        {
            T ret = heap[0];

            // 마지막 데이터를 루트로 이동한다.
            int last = heap.Count - 1;
            heap[0] = heap[last];
            heap.RemoveAt(last);
            last--;

            // 역으로 내려가는 도장깨기 시작
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;

                // 왼쪽값이 현재값보다 크면, 왼쪽으로 이동
                if (left <= last && heap[next].CompareTo(heap[left]) < 0)
                    next = left;
                // 오른쪽값이 현재값(왼쪽 이동 포함)보다 크면, 오른쪽으로 이동
                if (right <= last && heap[next].CompareTo(heap[right]) < 0)
                    next = right;

                // 탐색을 노드 마지막까지 왔으면 루프 종료
                if (next == now)
                    break;

                // 두 값을 교체한다.
                T temp = heap[now];
                heap[now] = heap[next];
                heap[next] = temp;

                // 검사 위치를 이동한다.
                now = next;

            }

            return ret;
        }

        public int Count()
        {
            return heap.Count;
        }
    }


    class Parogram
    {
        static void Main(string[] args)
        {
            PriorityQueue<Goni> priorityQueue = new PriorityQueue<Goni>();

            priorityQueue.Push(new Goni() { Id = 20 });
            priorityQueue.Push(new Goni() { Id = 10 });
            priorityQueue.Push(new Goni() { Id = 30 });
            priorityQueue.Push(new Goni() { Id = 90 });
            priorityQueue.Push(new Goni() { Id = 40 });

            while (priorityQueue.Count() > 0)
                Console.WriteLine(priorityQueue.Pop().Id);
        }

        private static TreeNode<string> MakeTree()
        {
            TreeNode<string> root = new TreeNode<string>() { Data = "R1 개발실" };
            {
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "디자인팀" };

                    TreeNode<string> child1 = new TreeNode<string>() { Data = "전투" };
                    child1.Children.Add(new TreeNode<string>() { Data = "1" });
                    child1.Children.Add(new TreeNode<string>() { Data = "2" });
                    node.Children.Add(child1);

                    node.Children.Add(new TreeNode<string>() { Data = "전투" });
                    node.Children.Add(new TreeNode<string>() { Data = "경제" });
                    node.Children.Add(new TreeNode<string>() { Data = "스토리" });
                    node.Children.Add(new TreeNode<string>("제페토", new List<TreeNode<string>>()
                    {
                        new TreeNode<string>() { Data ="박고니" }
                    }));

                    root.Children.Add(node);
                }
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "프로그래밍 팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "서버" });
                    node.Children.Add(new TreeNode<string>() { Data = "클라" });
                    node.Children.Add(new TreeNode<string>() { Data = "엔진" });
                    root.Children.Add(node);
                }
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "아트팀" };
                    //node.Children.Add(new TreeNode<string>() { Data = "배경" });
                    //node.Children.Add(new TreeNode<string>() { Data = "캐릭터" });
                    //root.Children.Add(node);
                }
            }

            return root;
        }

        private static void PrintTree(TreeNode<string> root)
        {
            Console.WriteLine(root.Data);

            foreach (TreeNode<string> child in root.Children)
                PrintTree(child);
        }

        static int GetHeight(TreeNode<string> root)
        {
            int height = 0;

            foreach (TreeNode<string> child in root.Children)
            {
                int newHeight = GetHeight(child) + 1;

                if (height < newHeight)
                    height = newHeight;
            }

            return height;
        }




        public static void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];
            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                // 가장 유력한 후보의 거리와 노드번호를 저장한다.
                int closest = Int32.MaxValue;
                int now = -1;

                // 제일 좋은 후보를 찾는다 (가장 가까이에 있는)
                for (int i = 0; i < 6; i++)
                {
                    // 이미 방문한 노드는 PASS
                    if (visited[i])
                        continue;

                    // 한번도 계산된 적이 없다 또는 현재 가장 가까운 노드보다 멀다
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;

                    // 여태껏 발견한 후보라는 의미, 정보를 갱신
                    closest = distance[i];
                    now = i;
                }

                // 다음 후보가 하나도 없다 -> 종료
                if (now == -1)
                    break;

                // 제일 좋은 후보를 찾았으니까 방문한다.
                visited[now] = true;

                // 방문한 노드와 인접한 노드들을 조사해서
                // 상황에 따라 발견한 최단거리를 갱신한다.
                for (int next = 0; next < 6; next++)
                {
                    // 이미 방문한 노드는 PASS
                    if (visited[next])
                        continue;

                    // 연결되지 않은 노드 스킵
                    if (Graph.adjDijikstra[now, next] == -1)
                        continue;

                    // 새로 조사된 노드의 최단거리를 계산한다.
                    int nextDistance = distance[now] + Graph.adjDijikstra[now, next];
                    // 만약에 기존에 발견한 최단거리가 새로 조사된 최단거리보다 크면, 정보를 갱신
                    if (nextDistance < distance[next])
                    {
                        distance[next] = nextDistance;
                        parent[next] = now;
                    }
                }

            }


        }

        static bool[] visited = new bool[6];
        // 1) 우선 now부터 방문을 한다.
        // 2) now와 연결된 정점들을 하나씩 확인하고, 방문하지 않았다면 방문한다.

        private static void DFS(int now)
        {
            // now 노드를 방문했다.
            Console.WriteLine(now);
            visited[now] = true;

            // now 노드와 인접한 노드를 탐색한다.
            for (int next = 0; next < 6; next++)
            {
                // 이미 방문한 노드면 PASS
                if (visited[next])
                    continue;

                // 연결되어있지 않으면 PASS
                if (Graph.adj[now, next] == 0)
                    continue;

                // now 노드와 연결되어 있으면서, 방문하지 않은 노드를 방문한다.
                DFS(next);
            }
        }

        private static void DFS_Stack(int start)
        {
            Stack<int> stack = new Stack<int>();

            // start노드를 시작점으로 넣어준다.
            stack.Push(start);
            visited[start] = true;

            // 모든 탐색을 마칠 때까지 반복한다.
            while (stack.Count > 0)
            {
                // 예정되어있던 노드를 방문한다. (now 노드)
                int now = stack.Pop();

                Console.WriteLine(now);

                // now 노드와 인접한 모든 노드를 찾아낸다.
                for (int next = 6 - 1; next >= 0; next--)
                {
                    // 간선으로 연결이 되어있지 않으면 PASS
                    if (Graph.adj[now, next] == 0)
                        continue;

                    // 다음 노드가 방문이 되어있으면 PASS
                    if (visited[next])
                        continue;

                    // now 노드와 연결되어 있으면서, 방문하지 않은 노드를 예약한다.
                    stack.Push(next);
                    visited[next] = true;
                }
            }
        }

        public static void SearchAll()
        {
            visited = new bool[6];

            // 모든 노드를 돌면서 DFS를 실행시킨다.
            // why? 모든 노드가 연결되어있지 않으면, 탐색이 안될 수 있기 때문에
            // 모든 노드가 방문되어 있는지 다시금 확인해준다.
            for (int now = 0; now < 6; now++)
            {
                if (visited[now] == false)
                    DFS(now);
            }
        }

        public static void BFS(int start)
        {
            bool[] visited = new bool[6];
            Queue<int> queue = new Queue<int>();

            // 노드의 움직임을 저장하는 배열 (현재 방문하는 노드의 부모는 내가 걸어온 길이기 때문)
            int[] parent = new int[6];
            // 시작점으로부터 각 노드까지의 최단거리를 저장하는 배열
            int[] distance = new int[6];

            // start노드를 시작점으로 넣어준다.
            visited[start] = true;
            queue.Enqueue(start);
            parent[start] = start;
            distance[start] = 0;

            // 모든 노드를 탐색할 때까지 반복
            while (queue.Count > 0)
            {
                // 예정되어있던 노드를 방문한다. (now 노드)
                int now = queue.Dequeue();
                Console.WriteLine(now);

                // now 노드와 연결된 접점을 찾는다.
                for (int next = 0; next < 6; next++)
                {
                    // now노드와 연결되어있지 않으면 PASS
                    if (Graph.adj[now, next] == 0)
                        continue;

                    // 이미 방문된 노드면 PASS
                    if (visited[next])
                        continue;

                    // now 노드와 연결되어있고, 방문하지 않은 노드면 Queue에 방문 예약을 한다.
                    queue.Enqueue(next);
                    visited[next] = true;
                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }
            }
        }
    }
}