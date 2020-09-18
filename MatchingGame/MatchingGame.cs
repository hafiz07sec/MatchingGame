using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class MatchingGame : Form
    {

        Label firstClicked = null;
        Label secondClicked = null;
        Random random = new Random();
        List<String> icons = new List<string>() { 
        "!","!" ,"N","N", ",",",", "k","k", "b","b", "v","v", "w","w", "z","z"
        };

        private void AssignIconsToSquares()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                iconLabel.ForeColor = iconLabel.BackColor;
                if(iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public MatchingGame()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled == true)
            {
                return;
            }
            if(secondClicked != null)
            {
                return;
            }
            Label clickLabel = sender as Label;
            if(clickLabel !=null)
            {
                if(clickLabel.ForeColor == Color.Black)
                {
                    return;
                }
                //clickLabel.ForeColor = Color.Black;
                if(firstClicked == null)
                {
                    firstClicked = clickLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                secondClicked = clickLabel;
                secondClicked.ForeColor = Color.Black;

                checkForWinner();

                if(firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;

        }

        private void checkForWinner()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    if(iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
            }
            MessageBox.Show("You Matched All the Icons!", "Congratulations");
            Close();
        }
    }
}
