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
        //hàm xét điều kiện cho điểm từng trường hợp
        private int getBoradEval(int valuename, List<int> whotick, List<int> opponent)
        {
            int score = 0;
            if (fiveWin(valuename, whotick))
                score += 100000;
            if (liveFour(valuename, whotick, opponent))
                score += 15000;
            if (liveThreeSize(valuename, whotick, opponent) >= 2 || deadFourSize(valuename, whotick, opponent) == 2 ||
                    deadFourSize(valuename, whotick, opponent) == 1 && liveThreeSize(valuename, whotick, opponent) == 1)
                score += 10000;
            if (liveThreeSize(valuename, whotick, opponent) + jLiveThreeSỉze(valuename, whotick, opponent) == 2)
                score += 5000;
            if (deadFour(valuename, whotick, opponent))
                score += 1000;
            if (jDeadFour(valuename, whotick, opponent))
                score += 300;
            return score;
        }

    }
}
