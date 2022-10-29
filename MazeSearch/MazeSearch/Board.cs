using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSearch
{
    internal class Board
    {
        public TileType[,] Tile { get; private set; } // 배열
        public int Size { get; private set; }

        public int DestY { get; private set; }
        public int DestX { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }

        const char CIRCLE = '\u25cf';

        public void Initialize(int size, Player player)
        {
            Tile = new TileType[size, size];
            Size = size;
            _player = player;

            DestY = Size - 2;
            DestX = Size - 2;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            };

            GenerateBySideWinder();

        }

        private void GenerateBySideWinder()
        {
            Random rand = new Random();

            for (int y = 0; y < Size; y++)
            {
                int count = 1;

                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 랜덤 값이 0일때는 오른쪽으로 길 뚫기
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    // 랜덤 값이 1일 경우, 아래로 길 뚫기
                    else
                    {
                        int randomIndex = rand.Next(0, count);

                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }

        private void GenerateByBinaryTree()
        {
            // Binary Tree Algorithm
            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업

            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }


                    // 랜덤 값이 0일때는 오른쪽으로 길 뚫기
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    // 랜덤 값이 1일 경우, 아래로 길 뚫기
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    // 플레이어 좌표를 가지고 와서, 그 좌표랑 현재 y,x가 일치하면 플레이어 전용 색상으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }

        private ConsoleColor GetTileColor(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Wall:
                    return ConsoleColor.White;

                case TileType.Empty:
                    return ConsoleColor.Black;

                default:
                    return ConsoleColor.Black;
            }
        }
    }
}
