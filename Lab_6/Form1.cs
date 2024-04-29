using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab_6.Emitter;
using static Lab_6.IImpactPoint;
using static Lab_6.Particle;

namespace Lab_6
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;

        public GravityPoint point1;
        public GravityPoint point2;
        public GravityPoint point3;
        public GravityPoint point4;
        public GravityPoint point5;
        public GravityPoint point6;
        public GravityPoint point7;
        public Color[] color = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Blue, Color.Purple, Color.Pink, Color.White, Color.SaddleBrown, Color.Salmon, Color.Snow, Color.Turquoise };
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };


            emitters.Add(this.emitter);
            

            point1 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 300,
                Y = picDisplay.Height / 2 - 60,
                Color = color[i]
            };

            emitter.impactPoints.Add(point1);

            point2 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 200,
                Y = picDisplay.Height / 2 - 40,
                Color = color[i + 1]
            };

            emitter.impactPoints.Add(point2);

            point3 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2 - 20,
                Color = color[i + 2]
            };

            emitter.impactPoints.Add(point3);

            point4 = new GravityPoint
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
                Color = color[i + 3]
            };

            emitter.impactPoints.Add(point4);

            point5 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 100,
                Y = picDisplay.Height / 2 - 20,
                Color = color[i + 4]
            };

            emitter.impactPoints.Add(point5);

            point6 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 200,
                Y = picDisplay.Height / 2 - 40,
                Color = color[i + 5]
            };

            emitter.impactPoints.Add(point6);

            point7 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 300,
                Y = picDisplay.Height / 2 - 60,
                Color = color[i + 6]
            };

            emitter.impactPoints.Add(point7);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // тут теперь обновляем эмиттер

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g); // а тут теперь рендерим через эмиттер
            }

            picDisplay.Invalidate();
        }
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // это не трогаем
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {

        }

        private void tbGraviton1_Scroll(object sender, EventArgs e)
        {
            point1.radius = tbGraviton1.Value;
            point2.radius = tbGraviton1.Value;
            point3.radius = tbGraviton1.Value;
            point4.radius = tbGraviton1.Value;
            point5.radius = tbGraviton1.Value;
            point6.radius = tbGraviton1.Value;
            point7.radius = tbGraviton1.Value;

        }

        private void tbGraviton2_Scroll(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            i = trackBar1.Value;
            point1.Color = color[i];
            point2.Color = color[i + 1];
            point3.Color = color[i + 2];
            point4.Color = color[i + 3];
            point5.Color = color[i + 4];
            point6.Color = color[i + 5];
            point7.Color = color[i + 6];

        }
    }
}
