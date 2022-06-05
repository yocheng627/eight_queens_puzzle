using System;
using System.Linq;

namespace eight_queens_puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            // 初始化棋盤
            var chessBoard = new ChessBoard();

            // 執行八皇后問題
            ExecuteQueenPuzzle(chessBoard);
            Console.ReadLine();
        }

        // 初始化解法編號
        static int solutionNum = 1;

        static void ExecuteQueenPuzzle(ChessBoard chessBoard)
        {
            // 放皇后的順序一律由左上到右下，排除重複組合
            var minPosition = chessBoard.QueenPositions.Any() ? chessBoard.QueenPositions.Max() : 0;

            foreach (var queenPostion in chessBoard.Slots.Where(o => o > minPosition))
            {
                var newChessBoard = new ChessBoard(chessBoard, queenPostion);

                // 判斷皇后棋子數量是否足夠
                if (newChessBoard.QueenChessQualityEnough)
                {
                    // 印出解法
                    Console.WriteLine($"// Solution {solutionNum++} ");
                    newChessBoard.PrintChessBoard();
                    Console.WriteLine();
                }
                // 判斷是否無解
                else if (!newChessBoard.NoSolution)
                {
                    ExecuteQueenPuzzle(newChessBoard);
                }
            }
        }
    }
}
