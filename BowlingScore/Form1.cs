using System;
using System.Windows.Forms;

namespace BowlingScore
{
    public partial class BowlingForm : Form
    {

        private BowlGame game { get; set; }
        public static BowlingForm bForm;
        private int click;
        private int btnval;
        private int btnValTmp;
        public BowlingForm()
        {
            InitializeComponent();
            bForm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new BowlGame();
        }

        private void ScoreButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            if (!Int32.TryParse(button.Text, out btnval)) return;
            game.Roll(btnval);
            btnValTmp = btnval;
            HideShowButtons();
            TotalScoreLbl.Text = game.Score.ToString();
            GameOver();
        }

        public void SetTotalForFrame(int Score, int Frame)
        {
            SetTableLayoutLabel(Frame, Score.ToString(), "FrameTotal_" + Frame);
        }

        public void SetFrameDisplay(int Frame, string value)
        {
            SetTableLayoutLabel(Frame, value, "FrameDisplay_" + Frame);
        }

        public void HideShowButtons()
        {
            click++;
            if (click % 2 == 0 || btnval == 10)
            {
                if (btnval == 10)
                    click++;
                btnval = 0;
            }
            
            foreach (var b in bForm.Controls)
            {
                if (!(b is Button)) continue;
                Button btn = b as Button;
                int tmpVal;
                if (int.TryParse(btn.Text, out tmpVal) && tmpVal + btnval > 10)
                {
                    btn.Hide();
                }
                else
                {
                    btn.Show();
                }
            }
            
        }

        private void SetTableLayoutLabel(int Frame, string value, string lblName)
        {
            foreach (var l in bForm.TableLayout.Controls)
            {
                if (!(l is Label)) continue;
                Label lbl = l as Label;
                if (lbl.Name == lblName)
                {
                    lbl.Text = value;
                }
            }
        }
        private void Reset_Click(object sender, EventArgs e)
        {
            bForm.game = new BowlGame();
            bForm.click = 0;
            bForm.btnval = 0;
            for (int i = 1; i < 11; i++)
            {
                SetTotalForFrame(0,i);
                SetFrameDisplay(i,"");
            }
            NameTxtBox.Text = "Enter Name";
            TotalScoreLbl.Text = "0";
            GameLbl.Text = "Select Score For Roll";
            foreach (var b in bForm.Controls)
            {
                if (!(b is Button)) continue;
                Button btn = b as Button;
                if (btn.Name != "Reset")
                {
                    btn.Enabled = true;
                }

            }
        }

        private void GameOver()
        {
            if (!(click < 20 ||(click >= 20 && click <= 23 && bForm.btnValTmp == 10)))
            {
                GameLbl.Text = "Game Over";
                foreach (var b in bForm.Controls)
                {
                    if (!(b is Button)) continue;
                    Button btn = b as Button;
                    if (btn.Name != "Reset")
                    {
                        btn.Enabled = false;
                    }

                }
            }
           

        }

        private void NameTxtBox_MouseClick(object sender, MouseEventArgs e)
        {
            NameTxtBox.Text = "";
        }

    }
}
