using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows.Forms;

namespace Snake.Views
{
    public partial class About : Form
    {
        SoundPlayer player;

        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            player = new SoundPlayer("Media/trololo.wav");
            player.PlayLooping();
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Stop();
        }




    }
}
