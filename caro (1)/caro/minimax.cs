using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    public partial class frm_main : Form
    {
        List<int> check = new List<int>();
        private int FindBestMove(List<int> values)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            int alpha = int.MinValue;
            int beta = int.MaxValue;

            // attack là 1, defense là 0
            int bestscoreDefense = int.MinValue;
            int bestMoveDefense = -1;
            
            // nước đi phòng thủ
            for (int i = 0; i < values.Count; i++)
            {
                addTick(tickedListPlayer, values[i]); // máy nếu chọn nước đi đó
                findPotentialPoints();
                int scoreDefense = Minimax(values, 2, true, alpha, beta, 0);
                removeTick(tickedListPlayer, values[i]); // hoàn trả lại ban đầu
                findPotentialPoints();
                if (scoreDefense > bestscoreDefense)
                {
                    bestscoreDefense = scoreDefense;
                    bestMoveDefense = i;
                }
            }
            foreach (Button btn in groupTic.Controls)
                if (btn.Name == potential[bestMoveDefense].ToString())
                    btn.BackColor = Color.Blue;

            int bestscoreAttack = int.MinValue;
            int bestMoveAttack = -1;


            // nước đi tấn công
            for (int i = 0; i < values.Count; i++)
            {
                addTick(tickedListMachine, values[i]); // máy nếu chọn nước đi đó
                findPotentialPoints();
                int scoreAttack = Minimax(values, 2, false, alpha, beta, 1);
                removeTick(tickedListMachine, values[i]); // hoàn trả lại ban đầu
                findPotentialPoints();
                if (scoreAttack > bestscoreAttack)
                {
                    bestscoreAttack = scoreAttack;
                    bestMoveAttack = i;
                }
            }

            foreach (Button btn in groupTic.Controls)
                if (btn.Name == potential[bestMoveAttack].ToString())
                    btn.BackColor = Color.Red;
            sw.Stop();
            textBox1.Text = sw.Elapsed.ToString();

            // khi người chơi có khả năng thắng cao thì chặn ngay nước thắng đó
            if (bestscoreAttack > bestscoreDefense)
                return bestMoveAttack;
            return bestMoveDefense;

        }
        private string read(List<int> value)
        {
            string hi = "";
            foreach (var item in value)
                hi += item.ToString() + ", ";
            return hi;
        }
        private int GetBoardEvaluate(int role)
        {
            // nhận 1 thì là nước đi tấn công, 0 là nước đi phòng thủ
            int score = 0;
            // duyệt từng phần tử được thêm vào
            foreach (var item in check)
            {
                // nếu tại nước đi này là của người chơi thì trừ
                if (tickedListPlayer.Contains(item))
                {
                    if(role == 1)
                        score -= getBoradEval(item, tickedListPlayer, tickedListMachine);
                    else
                        score += getBoradEval(item, tickedListPlayer, tickedListMachine);
                }

                // nếu tại nước đi này là của máy thì cộng
                else if (tickedListMachine.Contains(item))
                {
                    if(role == 1)
                        score += getBoradEval(item, tickedListMachine, tickedListPlayer);
                    else
                        score -= getBoradEval(item, tickedListMachine, tickedListPlayer);
                }
            }
            return score;
        }

        // Modify MinimaxAlphaBeta to pass the list of potential points to GetBoardEvaluate
        private int Minimax(List<int> potentialPoints, int depth, bool isMaximizing, int alpha, int beta, int role)
        {
            if (depth == 0 || IsGameOver())
            {
                return GetBoardEvaluate(role);
            }

            if (isMaximizing)
            {
                int maxScore = int.MinValue;
                foreach (var point in potentialPoints)
                {
                    // điểm này chưa được kiểm duyệt
                    if (!allticked.Contains(point))
                    {
                        addTick(tickedListMachine, point);
                        int score = Minimax(potentialPoints, depth - 1, false, alpha, beta, role);
                        removeTick(tickedListMachine, point);
                        maxScore = Math.Max(maxScore, score);
                        alpha = Math.Max(alpha, score);
                        if (beta <= alpha)
                            break; // Beta cut-off
                    }
                }
                return maxScore;
            }
            else
            {
                int minScore = int.MaxValue;
                foreach (var point in potentialPoints)
                {

                    if (!allticked.Contains(point))
                    {
                        addTick(tickedListPlayer, point);
                        int score = Minimax(potentialPoints, depth - 1, true, alpha, beta, role);
                        removeTick(tickedListPlayer, point);
                        minScore = Math.Min(minScore, score);
                        beta = Math.Min(beta, score);
                        if (beta <= alpha)
                            break; // Alpha cut-off
                    }
                }
                return minScore;
            }
        }

        private bool IsGameOver()
        {
            int FULL_BOARD = 225; // 15x15
            // các điểm trên bàn cờ đã được đánh dấu
            if (allticked.Count == FULL_BOARD)
                return true;
            return false;
        }

        private void addTick(List<int> tickedList, int value)
        {
            tickedList.Add(value);
            allticked.Add(value);
            check.Add(value);
            //findPotentialPoints();
        }

        private void removeTick(List<int> tickedList, int value)
        {
            tickedList.Remove(value);
            allticked.Remove(value);
            check.Remove(value);
            //findPotentialPoints();
        }
        
    }
}
