using System;
using System.Numerics;

namespace Exercise
{
    static class Graph
    {
        static public int[,] adj = new int[6, 6]
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
    }

    class Parogram
    {
        static void Main(string[] args)
        {
            // DFS (Depth First Search 깊이 우선 탐색)
            // 재귀, Stack 사용
            // BFS (Breadth First Search 너비 우선 탐색)
            // Queue 사용

            BFS(0);
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
            for(int next = 0; next < 6; next++)
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
            while(stack.Count > 0)
            {
                // 예정되어있던 노드를 방문한다. (now 노드)
                int now = stack.Pop();

                Console.WriteLine(now);

                // now 노드와 인접한 모든 노드를 찾아낸다.
                for(int next = 6 - 1; next >= 0; next--)
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
            for(int now = 0; now < 6; now++)
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
            while(queue.Count > 0)
            {
                // 예정되어있던 노드를 방문한다. (now 노드)
                int now = queue.Dequeue();
                Console.WriteLine(now);

                // now 노드와 연결된 접점을 찾는다.
                for(int next = 0; next < 6; next++)
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