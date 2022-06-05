using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eight_queens_puzzle
{
    public class ChessBoard
    {
        const int queenChessParam = 8;

        /// <summary>
        /// 棋盤空格
        /// </summary>
        public List<int> Slots { get; set; }

        /// <summary>
        /// 所有皇后棋子位置
        /// </summary>
        public List<int> QueenPositions { get; set; }

        /// <summary>
        /// 產生8 * 8 座標陣列空白棋盤
        /// </summary>
        public ChessBoard()
        {
            // 產生 8 * 8 座標陣列
            Slots = new List<int>(
                    Enumerable.Range(1, queenChessParam)
                        .SelectMany(x => Enumerable.Range(1, queenChessParam)
                        .Select(y => x * 10 + y))
                    );

            // 初始化皇后位置
            QueenPositions = new List<int>();
        }

        /// <summary>
        /// 放置皇后棋子後產生新棋盤
        /// </summary>
        /// <param name="chessBoard">現有棋盤</param>
        /// <param name="queenPostion">皇后位置</param>
        public ChessBoard(ChessBoard chessBoard, int queenPostion)
        {
            Slots = new List<int>(chessBoard.Slots.Except(new int[] { queenPostion }));
            QueenPositions = new List<int>(chessBoard.QueenPositions.Concat(new int[] { queenPostion }));

            // 皇后棋子放置的個別 x, y 軸座標
            int queenPositionX = Int32.Parse(queenPostion.ToString().Substring(0, 1));
            int queenPositionY = Int32.Parse(queenPostion.ToString().Substring(1, 1));

            Action<int, int> removePosition = (x, y) =>
            {
                //超出範圍時不做任何處理
                if (x < 1 || x > queenChessParam || y < 1 || y > queenChessParam)
                {
                    return;
                }
                else
                {
                    var removePosition = x * 10 + y;
                    if (Slots.Contains(removePosition)) Slots.Remove(removePosition);
                }
            };

            // 計算 x 軸
            for (int x = 1; x <= queenChessParam; x++)
            {
                // 移除放置皇后棋子的 x 軸上的可放置位置
                removePosition(x, queenPositionY);

                // 移除放置皇后棋子位置 "左上右下斜線" 的可放置位置
                int calcX = x + (queenPositionX - queenPositionY);
                removePosition(calcX, x);

                // 移除放置皇后棋子位置 "右上左下斜線" 的可放置位置
                calcX = queenPositionX + queenPositionY - x;
                removePosition(calcX, x);
            }

            // 計算 y 軸
            for (int y = 1; y <= queenChessParam; y++)
            {
                // 移除放置皇后棋子的 y 軸上的可放置位置
                removePosition(queenPositionX, y);
            }
        }

        /// <summary>
        /// 判斷是否無解 (可放置的空格數 < 未放皇后棋子的數量)
        /// </summary>
        public bool NoSolution => Slots.Count < queenChessParam - QueenPositions.Count;

        /// <summary>
        /// 判斷皇后棋子數量是否足夠
        /// </summary>
        public bool QueenChessQualityEnough => queenChessParam == QueenPositions.Count;

        /// <summary>
        /// 印出棋盤
        /// </summary>
        public void PrintChessBoard()
        {
            for (var y = 1; y <= queenChessParam; y++)
            {
                for (var x = 1; x <= queenChessParam; x++)
                {
                    int queenPostion = x * 10 + y;

                    if (QueenPositions.Contains(queenPostion))
                    {
                        Console.Write("Q");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
