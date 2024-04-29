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
        public Color Color;
        public int i = 0;
        public int radius = 100;
        

        public Color getColor() { return Color.Red; }
        // абстрактный метод с помощью которого будем изменять состояние частиц
        // например притягивать
        public abstract void ImpactParticle(Particle particle, Color color);

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
            // а сюда по сути скопировали с минимальными правками то что было в UpdateState
            public override void ImpactParticle(Particle particle, Color color)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;

                double r = Math.Sqrt(gX * gX + gY * gY);

                if (r + particle.Radius < radius / 2)
                {
                    particle.Color = Color;
                }
            }
            public override void Render(Graphics g)
            {
                g.DrawEllipse(
                       new Pen(Color),
                       X - radius/2,
                       Y - radius/2,
                       radius,
                       radius
                   );
            }
        }
    }
}
