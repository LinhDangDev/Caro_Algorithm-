using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class frm_main : Form
    {
        const int WIN_COUNT = 5;

        private bool fiveWin(int valuename, List<int> whotick)
        {
            return fiveInRow(valuename, whotick) || fiveInCol(valuename, whotick) ||
                   fiveInDiagonal(valuename, whotick) || fiveInAntiDiagonal(valuename, whotick);
        }

        private bool hasConsecutivePieces(int valuename, List<int> list, int rowIncrement, int colIncrement)
        {
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= WIN_COUNT; i++)
                {
                    int position = item + i * (rowIncrement * NUMB_ROW + colIncrement);
                    if (position == valuename)
                        flag = true;
                    if (list.Contains(position))
                        count++;
                    else
                        break;
                }
                if (count == WIN_COUNT && flag)
                    return true;
            }
            return false;
        }

        private bool fiveInRow(int valuename, List<int> list)
        {
            return hasConsecutivePieces(valuename, list, 0, 1);
        }

        private bool fiveInCol(int valuename, List<int> list)
        {
            return hasConsecutivePieces(valuename, list, 1, 0);
        }

        private bool fiveInDiagonal(int valuename, List<int> list)
        {
            return hasConsecutivePieces(valuename, list, 1, 1);
        }

        private bool fiveInAntiDiagonal(int valuename, List<int> list)
        {
            return hasConsecutivePieces(valuename, list, 1, -1);
        }


        //////////////////////////////////////////////////////////
        private int scoreRow(int valuename, List<int> list)
        {
            return countSize(valuename, list, 0, 1) * 50;
        }
        private int scoreCol(int valuename, List<int> list)
        {
            return countSize(valuename, list, 1, 0) * 50;
        }
        private int scoreDiagonal(int valuename, List<int> list)
        {
            return countSize(valuename, list, 1, 1) * 50;
        }
        private int scoreAntiDiagonal(int valuename, List<int> list)
        {
            return countSize(valuename, list, 1, -1) * 50;
        }

        private int countSize(int valuename, List<int> list, int rowIncrement, int colIncrement)
        {
            List<int> listcount = new List<int>();
            foreach (var item in list)
            {
                bool flag = false;
                int count = 0;
                for (int i = 0; i <= WIN_COUNT; i++)
                {
                    int position = item + i * (rowIncrement * NUMB_ROW + colIncrement);
                    if (position == valuename)
                        flag = true;
                    if (list.Contains(position))
                        count++;
                    else
                    {
                        if (flag)
                            listcount.Add(valuename);
                        break;
                    }
                }
            }
            int max = listcount[0];
            foreach (var item in listcount)
                max = (item > max) ? item : max;
            return max;
        }
    }
}
