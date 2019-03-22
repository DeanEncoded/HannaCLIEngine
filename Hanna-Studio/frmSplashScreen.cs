/*
HELLO HUMANS! DeanEncoded here!

This "Hanna" project was started from an idea about making a CLI based game.
You would essentially be making choices in the game and it would have alternate paths according to how you decide.

So instead of doing the game stand-alone..... I decided to make an "engine"... which gave the idea the power of custom games.
By building the project as an engine anyone could make their own CLI choice based game (or better known as Choose Your Own Adventure game).

I gave thought to the idea and made this "engine" in c++....
This engine would take in a JSON file that contained all the data for a single game.

At first I just thought if someone was to write a game they'd just take a "template" JSON file and edit it to make their game... but then that
would be kind of hard and time consuming.

So I decided to make a Studio for the engine as well. Where one can simply create a game for the engine with a GUI!

WELL... that's how Hanna came to be.....
bye...
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hanna_Studio
{
    public partial class frmSplashScreen : Form
    {

        frmHub hub;
        public frmSplashScreen()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        
            hub = new frmHub();
        }

        private void SplashTimer_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
            hub.Closed += (s, args) => this.Close();
            hub.Show();
            splashTimer.Enabled = false;
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.02;

            if (this.Opacity >= 1)
            {
                fadeInTimer.Enabled = false;
                splashTimer.Enabled = true;
            }
        }
    }
}
