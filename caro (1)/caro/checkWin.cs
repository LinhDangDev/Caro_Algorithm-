using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class frm_main : Form
    {
        
        private void checkWin()
        {
            if (checkWinRow() || checkWinCol() || checkAllDiagonal() || checkAllAntiDiagonal())
            {
                MessageBox.Show("The game end", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isGameOver = true;
                Application.Exit();
            }
        }
        private string whoWin(string check) //kiem tra xem ai là người chiến thắng
        {
            if (check == "O")
                return "Player win!!";
            else
                return "Machine win!!";
        }
        private bool checkWinRow()// kiểm tra hàng
        {
            string check = "";
            int count;
            for (int row = 0; row < NUMB_ROW; row++)
            {
                count = 1;
                for(int col = 0; col < NUMB_COL; col++)
                {
                    string checkNext = valueOfPoint(row, col);
                    if (count == 5)
                    {
                        MessageBox.Show(whoWin(check));
                        return true;
                    }
                    if (check == checkNext && check != "")
                        ++count;
                    else
                    {
                        check = checkNext;
                        count = 1;
                    }
                }
            }
            return false;
        }
        private bool checkWinCol()// kiểm tra cột
        {
            string check = "";
            int count;
            for (int row = 0; row < NUMB_ROW; row++)
            {
                count = 1;
                for (int col = 0; col < NUMB_COL; col++)
                {
                    string checkNext = valueOfPoint(col, row);
                    if (count == 5)
                    {
                        MessageBox.Show(whoWin(check));
                        return true;
                    }
                    if (check == checkNext && check != "")
                        ++count;
                    else
                    {
                        check = checkNext;
                        count = 1;
                    }
                }
            }
            return false;
        }
        //check các đường chéo thuận
        private bool checkAllDiagonal()
        {
            for(int row = 0; row < NUMB_COL; row++)
            {
                for(int col = 0; col < NUMB_COL; col++)
                {
                    int count = 1;
                    string check = valueOfPoint(row, col);
                    int checkNumb = int.Parse(numbOfPoint(row, col));
                    for(int i = row + 1; i < NUMB_ROW; ++i)
                    {                                                        
                        string checkNext = findNextCellDiagnal(checkNumb);
                        if (count == 5)
                            return true;
                        if (checkNext == check && check != "")
                            count++;
                        else
                        {
                            count = 1;
                            check = checkNext;
                        }
                        checkNumb += NUMB_ROW + 1;
                    }
                }
            }
            return false;
        }
        //check đường chéo nghịch
        private bool checkAllAntiDiagonal()
        {
            for (int row = 0; row < NUMB_COL; row++)
            {
                for (int col = NUMB_COL - 1; col >= 0; col--)
                {
                    int count = 1;
                    string check = valueOfPoint(row, col);
                    int checkNumb = int.Parse(numbOfPoint(row, col));
                    for (int i = row + 1; i < NUMB_ROW; ++i)
                    {
                        string checkNext = findNextCellAntiDiagnal(checkNumb);
                        if (count == 5)
                            return true;
                        if (checkNext == check && check != "")
                            count++;
                        else
                        {
                            count = 1;
                            check = checkNext;
                        }
                        checkNumb += NUMB_ROW - 1;
                    }
                }
            }
            return false;
        }
        private string findNextCellDiagnal(int valuename)
        {
            foreach (Button btn in groupTic.Controls)
                if (int.Parse(btn.Name) == valuename + NUMB_COL + 1)
                    return btn.Text;
            return null;
        }
        private string findNextCellAntiDiagnal(int valuename)
        {
            foreach (Button btn in groupTic.Controls)
                if (int.Parse(btn.Name) == valuename + NUMB_COL - 1)
                    return btn.Text;
            return null;
        }

    }
}
