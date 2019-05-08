using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTemplate
{
    public partial class Form1 : Form
    {
        private Bitmap _backBuffer;
        private Timer _timer;

        public Form1()
        {
            InitializeComponent();

            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += new EventHandler(GameTimer_Tick);
            _timer.Start();

            ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            Load += new EventHandler(Form1_CreateBackBuffer);
            Paint += new PaintEventHandler(Form1_Paint);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Process KeyDown here.
        }

        void Draw()
        {
            if (_backBuffer != null)
            {
                using (var g = Graphics.FromImage(_backBuffer))
                {
                    g.Clear(Color.White);
                    // Draw to buffer here.
                }

                Invalidate();
            }
        }

        void GameTimer_Tick(object sender, EventArgs e)
        {
            // Process timer tick here.

            Draw();
        }

        #region Canvas painting infrastructure
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_backBuffer != null)
            {
                e.Graphics.DrawImageUnscaled(_backBuffer, Point.Empty);
            }
        }

        void Form1_CreateBackBuffer(object sender, EventArgs e)
        {
            if (_backBuffer != null)
                _backBuffer.Dispose();

            _backBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        #endregion
    }
}
