using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace caro
{
    public partial class frm_main : Form
    {

        private int numbOfLeftDiagonal(string numb) // tìm điểm đường chéo thuận bên trái tại 1 điểm đang xét
        {
            int check = int.Parse(numb) - NUMB_COL - 1;
            if(check < 1 || check > NUMB_COL * NUMB_ROW)// xét nếu điểm vượt qua biên 
                return -1;
            if (int.Parse(numb) % NUMB_ROW == 1) // xét nếu điểm đầu của dãy
                return -1;
            return check;
        }
        private int numbOfRightDiagonal(string numb) // tìm điểm đường chéo thuận bên phải tại 1 điểm đang xét
        {
            int check = int.Parse(numb) + NUMB_COL + 1;
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            if (int.Parse(numb) % NUMB_ROW == 0)// xét nếu là điểm cuối của dãy
                return -1;
            return check;
        }
        private int numbOfLeftAntiDiagonal(string numb) // tìm điểm đường chéo nghịch bên trái tại 1 điểm đang xét
        {
            int check = int.Parse(numb) + (NUMB_COL - 1);
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            if (int.Parse(numb) % NUMB_ROW == 1) // xét nếu điểm đầu của dãy
                return -1;
            return check;
        }
        private int numbOfRightAntiDiagonal(string numb) // tìm điểm đường chéo nghịch bên phải tại 1 điểm đang xét
        {
            int check = int.Parse(numb) - (NUMB_COL - 1);
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            if (int.Parse(numb) % NUMB_ROW == 0)// xét nếu là điểm cuối của dãy
                return -1;
            return check;
        }
        private int numbOfUp(string numb) // tìm điểm phía trên tại 1 điểm đang xét
        {
            int check = int.Parse(numb) - NUMB_COL;
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            return check;
        }
        private int numbOfDown(string numb) // tìm điểm phía dưới tại 1 điểm đang xét
        {
            int check = int.Parse(numb) + NUMB_COL;
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            return check;
        }
        private int numbOfLeft(string numb) // tìm điểm phía dưới tại 1 điểm đang xét
        {
            int check = int.Parse(numb) - 1;
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            if (int.Parse(numb) % NUMB_ROW == 1) // xét nếu điểm đầu của dãy
                return -1;
            return check;
        }
        private int numbOfRight(string numb) // tìm điểm phía dưới tại 1 điểm đang xét
        {
            int check = int.Parse(numb) + 1;
            if (check < 1 || check > NUMB_COL * NUMB_ROW) // xét nếu điểm vượt qua biên
                return -1;
            if (int.Parse(numb) % NUMB_ROW == 0) // xét nếu điểm cuối của dãy
                return -1;
            return check;
        }
        // hàm thêm nhiều phần tử vào list
        private void addItemsInList(List<int> list, params int[] items) 
        {
            foreach (var item in items)
            {
                // nếu vượt biên bàn cờ
                if (item != -1)
                    list.Add(item);
            }
        }
        private List<int> findNearTicked(string valuename) //trả danh sách các điểm lân cận của các điểm được tích
        {
            List<int> nearticked = new List<int>();
            int x1 = numbOfLeftDiagonal(valuename);
            int x2 = numbOfUp(valuename);
            int x3 = numbOfRightAntiDiagonal(valuename);     //    x1     x2     x3
            int x4 = numbOfRight(valuename);                //     x8    tick    x4
            int x5 = numbOfRightDiagonal(valuename);       //      x7     x6     x5
            int x6 = numbOfDown(valuename);
            int x7 = numbOfLeftAntiDiagonal(valuename);
            int x8 = numbOfLeft(valuename);
            addItemsInList(nearticked, x1, x2, x3, x4, x5, x6, x7, x8); 
            return nearticked;
        }

        // hàm lưu các điểm tiềm năng
        private void findPotentialPoints()
        {
            var potentialPoints = new List<int>();

            foreach (var item in allticked)
            {
                List<int> nearby = findNearTicked(item.ToString());

                foreach (var item2 in nearby)
                {
                    // If the point is already in the potentialPoints list, or it has been ticked by either player or machine, skip it
                    if (potentialPoints.Contains(item2) || tickedListPlayer.Contains(item2) || tickedListMachine.Contains(item2))
                        continue;

                    potentialPoints.Add(item2);
                }
            }

            potential = potentialPoints;
        }
        // hàm lưu trữ các điểm lân cận của các điểm tiềm năng - list
        private List<List<int>> findNearPotentialPoint() 
        {
            var listOfTickedList = new List<List<int>>();

            // hàm tìm các nước đi
            for (int i = 0; i < potential.Count; i++)
            {
                listOfTickedList.Add(new List<int>());
                List<int> listOfEachTicked = findNearTicked(potential[i].ToString());
                listOfTickedList[i] = listOfEachTicked.GetRange(0, listOfEachTicked.Count);
            }
            return listOfTickedList;
        }
        // hàm lưu trữ các điểm kề của điểm kề các điểm tiềm năng
        //hàm lưu trữ các điểm lân cận của 1 list bên trong các list
        private List<List<List<int>>> findNearTickedTickedList()
        {
            var listOfTickedList = new List<List<List<int>>>();
            List<List<int>> nearPotentialPoints = findNearPotentialPoint();

            foreach (var potentialPoint in nearPotentialPoints)
            {
                var listOfTwice = new List<List<int>>();

                foreach (var nearPoint in potentialPoint)
                {
                    List<int> listOfEachTicked = findNearTicked(nearPoint.ToString());
                    listOfTwice.Add(listOfEachTicked);
                }

                listOfTickedList.Add(new List<List<int>>(listOfTwice));
            }

            return listOfTickedList;
        }



    }
}
