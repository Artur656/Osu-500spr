using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osu_500spr
{
    public partial class Form1 : Form
    {
        public Bitmap HandlerTexture = Resource1.Target,
                      TargetTexture = Resource1.Handler;
        private Point _targetPosition = new Point(300, 300);
        private Point _direction = Point.Empty;
        private int _score = 0;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            UpdateStyles();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var localPosition = this.PointToClient(Cursor.Position);

            _targetPosition.X += _direction.X * 10;
            _targetPosition.Y += _direction.Y * 10;

            if (_targetPosition.X < 0 || _targetPosition.X > 500)
            {
                _direction.X *= -1;
            }
            if (_targetPosition.Y < 0 || _targetPosition.Y > 500)
            {
                _direction.Y *= -1;
            }

            Point between = new Point(localPosition.X - _targetPosition.X, localPosition.Y - _targetPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y - between.Y));

            if (distance < 20)
            {
                AddScore(1);
            }

            var handlerRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);

            g.DrawImage(TargetTexture, handlerRect);
            g.DrawImage(HandlerTexture, targetRect);

        }
    

        private void timer2_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            timer1.Interval = r.Next(25, 1000);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);
        }
        private void AddScore(int score)
        {
            _score += score;
            scoreLabel.Text = _score.ToString();
        }
    }
}
