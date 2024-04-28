using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.LinkLabel;

namespace Lab_6
{
    public abstract class IImpactPoint
    {
        public float X; // ну точка же, вот и две координаты
        public float Y;

        // абстрактный метод с помощью которого будем изменять состояние частиц
        // например притягивать
        public abstract void ImpactParticle(Particle particle);

        // базовый класс для отрисовки точечки
        public virtual void Render(Graphics g)
        {
            g.FillEllipse(
                    new SolidBrush(Color.Red),
                    X - 5,
                    Y - 5,
                    10,
                    10
                );
        }

        public class GravityPoint : IImpactPoint
        {
            public int Power = 100; // сила притяжения
            public int radius = 100;
            public Color[] color = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Blue, Color.Purple, Color.Pink, Color.White, Color.SaddleBrown, Color.Salmon, Color.Snow, Color.Turquoise};

            Random random = new Random();
            public int i = 0;
            public int color1 = 0;
            public int color2 = 1;
            public int color3 = 2;
            public int color4 = 3;
            public int color5 = 4;
            public int color6 = 5;
            public int color7 = 6;

            // а сюда по сути скопировали с минимальными правками то что было в UpdateState
            public override void ImpactParticle(Particle particle)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;

                double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
                if (r + particle.Radius < Power / 2) // если частица оказалось внутри окружности
                {
                    // то притягиваем ее
                    float r2 = (float)Math.Max(100, gX * gX + gY * gY);
                    particle.SpeedX += gX * Power / r2;
                    particle.SpeedY += gY * Power / r2;
                }
            }
            public override void Render(Graphics g)
            {
                

                // буду рисовать окружность с диаметром равным Power
                // буду рисовать окружность с диаметром равным Power
                g.DrawEllipse(
                       new Pen(color[color7 + i]),
                       X - 430,
                       Y - 100,
                       radius,
                       radius
                   );

                g.DrawEllipse(
                       new Pen(color[color1 + i]),
                       X - 338,
                       Y - 60,
                       radius,
                       radius
                   );

                g.DrawEllipse(
                       new Pen(color[color2 + i]),
                       X - 245,
                       Y - 20,
                       radius,
                       radius
                   );

                g.DrawEllipse(
                       new Pen(color[color3 + i]),
                       X - 147,
                       Y + 8,
                       radius,
                       radius
                   );

                g.DrawEllipse(
                       new Pen(color[color4 + i]),
                       X - 50,
                       Y - 20,
                       radius,
                       radius
                   );

                g.DrawEllipse(
                       new Pen(color[color5 + i]),
                       X + 42,
                       Y - 60,
                       radius,
                       radius
                   );

                g.DrawEllipse(
                       new Pen(color[color6 + i]),
                       X + 135,
                       Y - 100,
                       radius,
                       radius
                   );


            }
        }
        public class AntiGravityPoint : IImpactPoint
        {
            public int Power = 100; // сила отторжения

            // а сюда по сути скопировали с минимальными правками то что было в UpdateState
            public override void ImpactParticle(Particle particle)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;
                float r2 = (float)Math.Max(100, gX * gX + gY * gY);

                particle.SpeedX -= gX * Power / r2; // тут минусики вместо плюсов
                particle.SpeedY -= gY * Power / r2; // и тут
            }
        }


    }
}
