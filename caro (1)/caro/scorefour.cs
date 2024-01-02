using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class frm_main : Form
    {
        
        const int FOUR = 4;
        const int FIVEWIN = 5;

        private bool liveFour(int valuename, List<int> whotick, List<int> opponent)
        {
            return liveFourInRow(valuename, whotick, opponent) ||
                   liveFourInCol(valuename, whotick, opponent) ||
                   liveFourInDiagonal(valuename, whotick, opponent) ||
                   liveFourInAntiDiagonal(valuename, whotick, opponent);
        }

        private bool deadFour(int valuename, List<int> whotick, List<int> opponent)
        {
            return deadFourInRow(valuename, whotick, opponent) ||
                   deadFourInCol(valuename, whotick, opponent) ||
                   deadFourInDiagonal(valuename, whotick, opponent) ||
                   deadFourInAntiDiagonal(valuename, whotick, opponent);
        }

        private int deadFourSize(int valuename, List<int> whotick, List<int> opponent)
        {
            int count = 0;
            if (deadFourInRow(valuename, whotick, opponent)) count++;
            if (deadFourInCol(valuename, whotick, opponent)) count++;
            if (deadFourInDiagonal(valuename, whotick, opponent)) count++;
            if (deadFourInAntiDiagonal(valuename, whotick, opponent)) count++;
            return count;
        }

        private bool jDeadFour(int valuename, List<int> whotick, List<int> opponent)
        {
            int jLeft = (valuename % NUMB_COL == 0) ? -1 : (valuename - 1);
            int jRight = (valuename % NUMB_COL == 1) ? -1 : (valuename + 1);
            int jUp = (valuename - NUMB_COL <= 0) ? -1 : (valuename - NUMB_COL);
            int jDown = (valuename + NUMB_COL > NUMB_COL * NUMB_ROW) ? -1 : (valuename + NUMB_COL);

            if (deadFour(jLeft, whotick, opponent) || deadFour(jRight, whotick, opponent) ||
                deadFour(jUp, whotick, opponent) || deadFour(jDown, whotick, opponent))
            {
                return true;
            }

            return false;
        }

        private bool liveFourInRow(int valuename, List<int> list, List<int> opponent)
        {
            if (liveFourInRowDead(valuename, list, opponent))
                return false;
            return hasConsecutivePiecesFourLive(valuename, list, 0, 1, opponent);
        }
        private bool liveFourInCol(int valuename, List<int> list, List<int> opponent)
        {
            if (liveFourInColDead(valuename, list, opponent))
                return false;
            return hasConsecutivePiecesFourLive(valuename, list, 1, 0, opponent);
        }
        private bool liveFourInDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            if (liveFourInDiagonalDead(valuename, list, opponent))
                return false;
            return hasConsecutivePiecesFourLive(valuename, list, 1, 1, opponent);
        }
        private bool liveFourInAntiDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            if (liveFourInAntiDiagonalDead(valuename, list, opponent))
                return false;
            return hasConsecutivePiecesFourLive(valuename, list, 1, -1, opponent);
        }
        private bool deadFourInRow(int valuename, List<int> list, List<int> opponent)
        {
            return hasConsecutivePiecesFourDead(valuename, list, 0, 1, opponent);
        }

        private bool deadFourInCol(int valuename, List<int> list, List<int> opponent)
        {
            return hasConsecutivePiecesFourDead(valuename, list, 1, 0, opponent);
        }

        private bool deadFourInDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            return hasConsecutivePiecesFourDead(valuename, list, 1, 1, opponent);
        }

        private bool deadFourInAntiDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            return hasConsecutivePiecesFourDead(valuename, list, 1, -1, opponent);
        }

        // Add similar methods for liveFourInCol, liveFourInDiagonal, liveFourInAntiDiagonal, 
        // deadFourInRow, deadFourInCol, deadFourInDiagonal, deadFourInAntiDiagonal

        private bool hasConsecutivePiecesFourDead(int valuename, List<int> list, int rowIncrement, int colIncrement, List<int> opponent)
        {
            foreach (var item in list)
            {
                int count = 0;
                bool flag = false;
                for (int i = 0; i <= FIVEWIN; i++)
                {
                    int position = item + i * (rowIncrement * NUMB_ROW + colIncrement);
                    if (position == valuename)
                    {
                        flag = true;
                    }
                    if (list.Contains(position))
                    {
                        count++;
                    }
                    else if (opponent.Contains(position))
                    {
                        break;
                    }
                }
                if (count == FOUR && flag)
                {
                    
                    return true;
                }
            }
            return false;
        }
        private bool hasConsecutivePiecesFourLive(int valuename, List<int> list, int rowIncrement, int colIncrement, List<int> opponent)
        {
            foreach (var item in list)
            {
                int count = 0;
                bool flag = false;
                for (int i = 0; i <= FOUR; i++)
                {
                    int position = item + i * (rowIncrement * NUMB_ROW + colIncrement);
                    if (position == valuename)
                    {
                        flag = true;
                    }
                    if (list.Contains(position))
                    {
                        count++;
                    }
                    else
                        break;
                }
                if (count == FOUR && flag)
                {
                    return true;
                }
            }
            return false;
        }
        /////////////////////////////////////////////////////////////////
        private bool liveFourInRowDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FOUR; i++)
                {
                    if (item + 1 == valuename)
                        flag = true;
                    if (list.Contains(item + i))
                        count++;
                    else
                        break;
                }
                if (count == FOUR && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - 1) && opponent.Contains(item + FIVEWIN))
                        return true;
                }

            }
            return false;
        }
        private bool liveFourInColDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FOUR; i++)
                {
                    if (item + (i * NUMB_ROW) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * NUMB_ROW)))
                        count++;
                    else
                        break;
                }
                if (count == FOUR && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - NUMB_ROW) && opponent.Contains(item + (NUMB_ROW * FIVEWIN)))
                        return true;
                }
            }
            return false;
        }
        private bool liveFourInDiagonalDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FOUR; i++)
                {
                    if (item + (i * (NUMB_ROW + 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW + 1))))
                        count++;
                    else
                        break;
                }
                if (count == FOUR && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - (NUMB_ROW + 1)) && opponent.Contains(item + ((NUMB_ROW + 1) * FIVEWIN)))
                        return true;
                }
            }
            return false;
        }
        private bool liveFourInAntiDiagonalDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FOUR; i++)
                {
                    if (item + (i * (NUMB_ROW - 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW - 1))))
                        count++;
                    else
                        break;
                }
                if (count == FOUR && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - (NUMB_ROW - 1)) && opponent.Contains(item + ((NUMB_ROW - 1) * FIVEWIN)))
                        return true;
                }
            }
            return false;
        }

    }
}
