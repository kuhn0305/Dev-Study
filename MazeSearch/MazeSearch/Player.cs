using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSearch
{
    class Pos
    {
        public int Y;
        public int X;

        public Pos(int y, int x)
        {
            Y = y;
            X = x;
        }
    }

    internal class Player
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up = 0,         // 1
            Left = 1,       // 2
            Down = 2,    //2 
            Right = 3       // 4     
        }

        int[] deltaY = new int[4] { -1, 0, 1, 0 };
        int[] deltaX = new int[4] { 0, -1, 0, 1 };

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posX, int posY, Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;

            //BFS();
            Dijikstra();
        }

        private void Dijikstra()
        {
            bool[,] visited = new bool[_board.Size, _board.Size];
            int[,] distance = new int[_board.Size, _board.Size];
            int[,] parent = new int[_board.Size, _board.Size];

            Array.Fill(distance, Int32.MaxValue);
        }

        private void BFS()
        {
            // found(방문 예정), queue(BFS), parent(경로 저장)
            bool[,] found = new bool[_board.Size, _board.Size];
            Queue<Pos> queue = new Queue<Pos>();
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            
            found[PosY, PosX] = true;
            queue.Enqueue(new Pos(PosY, PosX));
            parent[PosY, PosX] = new Pos(PosY, PosX);

            int nextY = 0;
            int nextX = 0;

            while (queue.Count > 0)
            {
                Pos nowPos = queue.Dequeue();

                // 현재 노드를 기준으로 사방면을 탐색한다.
                for (int next = 0; next < 4; next++)
                {
                    // deltaY, deltaX는 (북, 서, 남, 동) 순서대로 방향을 더해준다.
                    // 현재좌표 + delta값은 북,서,남,동 순서대로 다음 좌표를 의미한다.
                    nextY = nowPos.Y + deltaY[next];
                    nextX = nowPos.X + deltaX[next];

                    // 다음 좌표가 배열의 범위 안에 있는지 Check
                    if (nextY < 0 || nextY >= _board.Size || nextX < 0 || nextX >= _board.Size)
                        continue;
                    // 다음 방문 예정 노드가 이미 방문되었는지 Check
                    if (found[nextY, nextX])
                        continue;
                    // 다음 방문 예정 노드가 벽인지 Check
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;

                    // 모든 조건을 통과 했다면 이동 가능한 노드 (길)이다!
                    // 다음 방문으로 Queue에 넣어주고
                    queue.Enqueue(new Pos(nextY, nextX));
                    // 탐색 완료 체크를 해주고
                    found[nextY, nextX] = true;
                    // 직전의 부모노드(경로)를 저장한다.
                    parent[nextY, nextX] = new Pos(nowPos.Y, nowPos.X);
                }
            }

            // BFS 탐색 완료, 경로는 부모노드에 저장되어 있음
            // ======================

            // 도착지로부터 경로를 역순으로 저장하기 위함임
            int y = _board.DestY;
            int x = _board.DestX;

            // 도착점 -> 출발점 도착할때까지 반복
            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                // 현재 위치를 저장
                _points.Add(new Pos(y, x));

                // 현재 위치의 부모위치 다음 저장 타겟으로 설정
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }

            // 시작점은 저장되지 않으니 추가로 저장
            _points.Add(new Pos(y, x));
            // 하지만 부모의 부모의 부모.......를 탐색했기 때문에 경로가 역순이다.
            // 따라서 뒤집어 준다.
            _points.Reverse();
        }

        private void RightHand()
        {
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            //목적지에 도착하기 전에는 계속 실행
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                // 1. 현대 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인.
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;

                    // 앞으로 한 보 전진
                    PosX = PosX + deltaX[_dir];
                    PosY = PosY + deltaY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //  2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인.
                else if (_board.Tile[PosY + deltaY[_dir], PosX + deltaX[_dir]] == Board.TileType.Empty)
                {
                    // 앞으로 한 보 전진
                    PosX = PosX + deltaX[_dir];
                    PosY = PosY + deltaY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    // 왼쪽 방향으로 90도 회전
                    _dir = (_dir + 1) % 4;
                }
            }
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        int _lastIndex = 0;

        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;

            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;

            }
        }
    }
}
