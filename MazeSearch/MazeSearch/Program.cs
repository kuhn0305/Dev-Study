using System;

namespace MazeSearch
{
    class Parogram
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;

            Board board = new Board();
            Player player = new Player();

            board.Initialize(25, player);
            player.Initialize(1, 1, board);

            while (true)
            {
                int currentTick = System.Environment.TickCount;
                int elapsedTick = currentTick - lastTick;

                // 만약에 경과한 Tick 30프레임 보다 작다면
                if (elapsedTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;

                // 입력


                // 로직
                player.Update(elapsedTick);

                // 렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}