using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class frm_main : Form
    {
        BoardGame boardGame;
        bool isMachineFirst;
        bool isGameOver = false; // kiểm tra xem đã kết thúc trận hay chưa, nếu đã kết thúc thì unclickable
        const int NUMB_ROW = 15;
        const int NUMB_COL = 15;
        List<int> tickedListPlayer; // danh sách các điểm được tích của người chơi
        List<int> tickedListMachine; // danh sách các điểm được tích của máy
        List<int> allticked; // danh sách các điểm được chọn
        List<int> potential; // danh sách các điểm tiềm năng
        public frm_main()
        {
            InitializeComponent();
            boardGame = new BoardGame();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            allticked = new List<int>();
            tickedListPlayer = new List<int>();
            tickedListMachine = new List<int>();
            isMachineFirst = true;
            createBox();
        }
        private void createBox()
        {
            for (int row = 0; row < NUMB_ROW; row++)
            {
                for (int col = 0; col < NUMB_COL; col++)
                {
                    int name = row * NUMB_ROW + col + 1;
                    var tick = new Button
                    {
                        Name = name + "",
                        Size = new Size(30, 30),
                        Location = new Point(col * 30, row * 30),
                        Tag = new Point(row, col),
                        Margin = new Padding(0, 0, 0, 0),
                        FlatStyle = FlatStyle.Flat,
                        Enabled = false,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                    };
                    tick.FlatAppearance.BorderColor = Color.Black;
                    tick.FlatAppearance.BorderSize = 1;
                    tick.Click += new EventHandler(button_Click);
                    groupTic.Controls.Add(tick);
                }
            }
        }
        private string valueOfPoint(int row, int col) // trả về giá trị O, X của ô đang xét tại 1 điểm
        {
            int numb = row * NUMB_ROW + col + 1;
            foreach (Button btn in groupTic.Controls)
                if (btn.Name == numb.ToString())
                    return btn.Text;
            return null;
        }
        private string numbOfPoint(int row, int col) // trả về thứ tự id của ô đang xét tại 1 điểm
        {
            int numb = row * NUMB_ROW + col + 1;
            foreach (Button btn in groupTic.Controls)
                if (btn.Name == numb.ToString())
                    return btn.Name;
            return null;
        }
        private string findPointByValueName(string value) // trả về giá trị O,X tại thứ tự n
        {
            foreach(Button btn in groupTic.Controls)
                if(btn.Name == value.ToString()) 
                    return btn.Text;
            return null;
        }
        private void button_Click(object sender, EventArgs e) // nước đi của người chơi luôn là O
        {
            Button btn = sender as Button;
            if (isGameOver)
                return;
            if (btn.Text != "")
                return;
            else
            {
                btn.Text = "O";
                btn.ForeColor = Color.Blue;
                tickedListPlayer.Add(int.Parse(btn.Name)); //thêm các điểm mới chọn vào danh sách
                allticked.Add(int.Parse(btn.Name));
            }
            checkWin();
            findPotentialPoints();
            machinePlay();
            findPotentialPoints();
            checkWin();
        }

        private void menuNewGame_Click(object sender, EventArgs e)
        {
            resetGame();
            MessageBox.Show("Good luck");
            foreach (Button btn in groupTic.Controls)
            {
                btn.Text = "";
                btn.Enabled = true;
            }
            machinePlay();
        }
        private void resetGame()
        {
            foreach (Button btn in groupTic.Controls)
                btn.Text = "";
        }
        private void machinePlay()
        {
            int count = 0;
            int rand;
            if (isMachineFirst)
            {
                rand = 8 * NUMB_ROW + 8;
                isMachineFirst = false;
            }
            else
            {
                string name = "";
                foreach (var item in potential)
                    name += item + ", ";
                //MessageBox.Show(name + "The total is : " + potential.Count);
                //int alpha = int.MinValue, beta = int.MaxValue;
                int bestmove = FindBestMove(potential);
               // MessageBox.Show("the ID of best move is: " + bestmove);

                clearColor();
                
                rand = potential[bestmove];
            }
            tickedListMachine.Add(rand);
            allticked.Add(rand);
            foreach (Button btn in groupTic.Controls)
            {
                if (btn.Name == rand.ToString())
                {
                    btn.ForeColor = Color.Red;
                    btn.Text = "X";
                }
            }
        }
        // check màu để biết nước đi nào
        private void clearColor()
        {
            foreach(Button btn in groupTic.Controls)
                btn.BackColor = Color.White;
        }
        
    }
}
