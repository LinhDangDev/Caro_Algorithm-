using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace caro
{
    public class BoardGame
    {
        public int[,] board_game;

        public BoardGame()
        {
            board_game = new int[15, 15];
            inital();
        }

        private void inital()
        {
            for(int i = 0; i < 15; i++) {
                for (int j = 0; j < 15; j++)
                    board_game[i, j] = 0;
            }
        }
        // check giá trị tại 1 điểm
        private int checkPoint(int row, int column) =>  board_game[row, column];
    }
}
