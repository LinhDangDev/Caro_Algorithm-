using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class frm_main : Form
    {
        const int THREE = 3;
        private bool liveThree(int valuename, List<int> whotick, List<int> opponent)
        {
            if (completeDeadTwoHeadThree(valuename, whotick, opponent))
                return false;
            //duyệt cho JdeadThree, vì điểm đã vượt qua biên
            if (valuename == -1)
                return false;
            if (liveThreeInRow(valuename, whotick, opponent) || liveThreeInCol(valuename, whotick, opponent) ||
                liveThreeInDiagonal(valuename, whotick, opponent) || liveThreeInAntiDiagonal(valuename, whotick, opponent))
                return true;
            return false;
        }
        private int liveThreeSize(int valuename, List<int> whotick, List<int> opponent)
        {
            int count = 0;
            List<int> temp = whotick;
            if (liveThreeInRow(valuename, whotick, opponent))
                count++;
            if (liveThreeInCol(valuename, whotick, opponent))
                count++;
            if (liveThreeInDiagonal(valuename, whotick, opponent))
                count++;
            if (liveThreeInAntiDiagonal(valuename, whotick, opponent))
                count++;
            return count;
        }
        private bool deadThree(int valuename, List<int> whotick, List<int> opponent)
        {
            //duyệt cho JdeadThree, vì điểm đã vượt qua biên
            if (valuename == -1)
                return false;
            if (deadThreeInRow(valuename, whotick, opponent) || deadThreeInCol(valuename, whotick, opponent) ||
                deadThreeInDiagonal(valuename, whotick, opponent) || deadFourInAntiDiagonal(valuename, whotick, opponent))
                return true;
            return false;
        }
        private int jLiveThreeSỉze(int valuename, List<int> whotick, List<int> opponent)
        {
            int count = 0;
            //điểm kề trái
            int jleft = (valuename % NUMB_COL == 0) ? -1 : (valuename - 1);
            //điểm kề phải
            int jRight = (valuename % NUMB_COL == 1) ? -1 : (valuename + 1);
            //điểm kề trên
            int jUp = (valuename - NUMB_COL <= 0) ? -1 : (valuename - NUMB_COL);
            //điểm kề dưới
            int jDown = (valuename + NUMB_COL > NUMB_COL * NUMB_ROW) ? -1 : (valuename + NUMB_COL);
            if (liveThree(jleft, whotick, opponent))
                count++;
            if (liveThree(jRight, whotick, opponent))
                count++;
            if (liveThree(jUp, whotick, opponent))
                count++;
            if (liveThree(jDown, whotick, opponent))
                count++;
            return count;
        }
        private bool jDeadThree(int valuename, List<int> whotick, List<int> opponent)
        {
            //điểm kề trái
            int jleft = (valuename % NUMB_COL == 0) ? -1 : (valuename - 1);
            //điểm kề phải
            int jRight = (valuename % NUMB_COL == 1) ? -1 : (valuename + 1);
            //điểm kề trên
            int jUp = (valuename - NUMB_COL <= 0) ? -1 : (valuename - NUMB_COL);
            //điểm kề dưới
            int jDown = (valuename + NUMB_COL > NUMB_COL * NUMB_ROW) ? -1 : (valuename + NUMB_COL);
            if (deadThree(jleft, whotick, opponent) || deadThree(jRight, whotick, opponent) ||
                deadThree(jUp, whotick, opponent) || deadThree(jDown, whotick, opponent))
                return true;
            return false;
        }
        private List<int> getAllDeadThree()
        {
            List<int> list = new List<int>();

            return list;
        }
        private bool liveThreeInRow(int valuename, List<int> list, List<int> opponent)
        {
            if (liveThreeInRowDead(valuename, list, opponent))
                return false;
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + i == valuename)
                        flag = true;
                    if (list.Contains(item + i))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - 1) || opponent.Contains(item + FOUR))
                        return false;
                    else
                        return true;
                }

            }
            return false;
        }
        private bool liveThreeInCol(int valuename, List<int> list, List<int> opponent)
        {
            if (liveThreeInColDead(valuename, list, opponent))
                return false;
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + (i * NUMB_ROW) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * NUMB_ROW)))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - NUMB_ROW) || opponent.Contains(item + (NUMB_ROW * FOUR)))
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }
        private bool liveThreeInDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            if (liveThreeInDiagonalDead(valuename, list, opponent))
                return false;
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + (i * (NUMB_ROW + 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW + 1))))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - (NUMB_ROW + 1)) || opponent.Contains(item + ((NUMB_ROW + 1) * FOUR)))
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }
        private bool liveThreeInAntiDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            if (liveThreeInAntiDiagonalDead(valuename, list, opponent))
                return false;
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + (i * (NUMB_ROW - 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW - 1))))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - (NUMB_ROW - 1)) || opponent.Contains(item + ((NUMB_ROW - 1) * FOUR)))
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }
        private bool deadThreeInRow(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FIVEWIN; i++)
                {
                    if (item + i == valuename)
                        flag = true;
                    if (list.Contains(item + i))
                        count++;
                    // xét đối thủ có chặn trên nước đi này hay không ?
                    else if (opponent.Contains(item + i))
                        break;
                }
                if (count == THREE && flag)
                    return true;

            }
            return false;
        }
        private bool deadThreeInCol(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FIVEWIN; i++)
                {
                    if (item + (i * NUMB_ROW) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * NUMB_ROW)))
                        count++;
                    // xét đối thủ có chặn trên nước đi này hay không ?
                    else if (opponent.Contains(item + (i * NUMB_ROW)))
                        break;
                }
                if (count == THREE && flag)
                    return true;
            }
            return false;
        }
        private bool deadThreeInDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FIVEWIN; i++)
                {
                    if (item + (i * (NUMB_ROW + 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW + 1))))
                        count++;
                    // xét đối thủ có chặn trên nước đi này hay không ?
                    else if (opponent.Contains(item + (i * (NUMB_ROW + 1))))
                        break;
                }
                if (count == THREE && flag)
                    return true;
            }
            return false;
        }
        private bool deadThreeInAntiDiagonal(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= FIVEWIN; i++)
                {
                    if (item + (i * (NUMB_ROW - 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW - 1))))
                        count++;
                    // xét đối thủ có chặn trên nước đi này hay không ?
                    else if (opponent.Contains(item + (i * (NUMB_ROW - 1))))
                        break;
                }
                if (count == THREE && flag)
                    return true;
            }
            return false;
        }
        private bool completeDeadTwoHeadThree(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flagLeft = false;
                bool flagRight = false;

                // Check left side
                for (int i = 0; i <= THREE; i++)
                {
                    if (item - i == valuename)
                        flagLeft = true;
                    if (list.Contains(item - i))
                        break;
                    if (opponent.Contains(item - i))
                        break;
                }

                // Check right side
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + i == valuename)
                        flagRight = true;
                    if (list.Contains(item + i))
                        break;
                    if (opponent.Contains(item + i))
                        break;
                }

                // If both ends are blocked, it's a complete dead two head
                if (flagLeft && flagRight)
                    return true;
            }

            return false;
        }
        ///////////////////////////////////////////////////////////////////
        private bool liveThreeInRowDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + i == valuename)
                        flag = true;
                    if (list.Contains(item + i))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - 1) && opponent.Contains(item + FOUR))
                        
                        return true;
                }

            }
            return false;
        }
        private bool liveThreeInColDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + (i * NUMB_ROW) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * NUMB_ROW)))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - NUMB_ROW) && opponent.Contains(item + (NUMB_ROW * FOUR)))
                        
                        return true;
                }
            }
            return false;
        }
        private bool liveThreeInDiagonalDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + (i * (NUMB_ROW + 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW + 1))))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - (NUMB_ROW + 1)) && opponent.Contains(item + ((NUMB_ROW + 1) * FOUR)))
                        
                        return true;
                }
            }
            return false;
        }
        private bool liveThreeInAntiDiagonalDead(int valuename, List<int> list, List<int> opponent)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= THREE; i++)
                {
                    if (item + (i * (NUMB_ROW - 1)) == valuename)
                        flag = true;
                    if (list.Contains(item + (i * (NUMB_ROW - 1))))
                        count++;
                    else
                        break;
                }
                if (count == THREE && flag)
                {
                    //xét xem có chặn 1 trong 2 đầu hay không
                    if (opponent.Contains(item - (NUMB_ROW - 1)) && opponent.Contains(item + ((NUMB_ROW - 1) * FOUR)))
                        return true;
                }
            }
            return false;
        }
    }
}
