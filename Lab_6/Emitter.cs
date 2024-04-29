﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab_6.IImpactPoint;
using static Lab_6.Particle;
using static Lab_6.Form1;

namespace Lab_6
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;

        public float GravitationX = 0;
        public float GravitationY = 1;

        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();

        public GravityPoint point1;


        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20;
        public int LifeMax = 100;

        public int ParticlesPerTick = 1; // добавил новое поле

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц



        /* добавил метод */
        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;

            return particle;
        }

        public void UpdateState()
        {

            int particlesToCreate = ParticlesPerTick; // фиксируем счетчик сколько частиц нам создавать за тик

            foreach (var particle in particles)
            {
                if (particle.Life <= 0) // если частицы умерла
                {
                    ResetParticle(particle);
                }
                else
                {
                    /* теперь двигаю вначале */
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;

                    particle.Life -= 1;
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                    /* это уехало вверх
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY; */
                }
                
            }

            // второй цикл меняем на while, 
            // этот новый цикл также будет срабатывать только в самом начале работы эмиттера
            // собственно пока не накопится критическая масса частиц
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < 500)
                {
                    /* ну и тут чуток подкрутили */
                    var particle = new ParticleColorful();
                    particle.FromColor = Color.White;
                    particle.ToColor = Color.FromArgb(0, Color.Black);

                    ResetParticle(particle); // добавили вызов ResetParticle

                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }



        public Color[] color = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Blue, Color.Purple, Color.Pink, Color.White, Color.SaddleBrown, Color.Salmon, Color.Snow, Color.Turquoise };

        public int i = 0;
        public int color1 = 0;
        public int color2 = 1;
        public int color3 = 2;
        public int color4 = 3;
        public int color5 = 4;
        public int color6 = 5;
        public int color7 = 6;


        public void Render(Graphics g)
        {
         

            // не трогаем
            foreach (var particle in particles)
            {
                particle.Draw(g);
                if (particle.Y >= 100 && particle.X > 20 && particle.X < 100)
                {
                        particle.FirstDraw(g, color[color7 + i]);

                }
                if (particle.Y >= 140 && particle.X > 100 && particle.X < 200)
                {
                    particle.FirstDraw(g, color[color1 + i]);

                }
                if (particle.Y >= 170 && particle.X > 200 && particle.X < 300)
                {
                    particle.FirstDraw(g, color[color2 + i]);

                }
                if (particle.Y >= 200 && particle.X > 300 && particle.X < 400)
                {
                    particle.FirstDraw(g, color[color3 + i]);

                }
                if (particle.Y >= 170 && particle.X > 400 && particle.X < 500)
                {
                    particle.FirstDraw(g, color[color4 + i]);

                }
                if (particle.Y >= 140 && particle.X > 500 && particle.X < 600)
                {
                    particle.FirstDraw(g, color[color5 + i]);

                }
                if (particle.Y >= 100 && particle.X > 600 && particle.X < 700)
                {
                    particle.FirstDraw(g, color[color6 + i]);

                }
            }

            foreach (var point in impactPoints) // тут теперь  impactPoints
            {
                /* это больше не надо
                g.FillEllipse(
                    new SolidBrush(Color.Red),
                    point.X - 5,
                    point.Y - 5,
                    10,
                    10
                );
                */
                point.Render(g); // это добавили
            }
        }

        // добавил новый метод, виртуальным, чтобы переопределять можно было

        public int ParticlesCount = 500;
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 20 + Particle.rand.Next(100);
            particle.X = MousePositionX;
            particle.Y = MousePositionY;

            var direction = (double)Particle.rand.Next(360);
            var speed = 1 + Particle.rand.Next(10);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = 2 + Particle.rand.Next(10);
        }

        public class TopEmitter : Emitter
        {
            public int Width; // длина экрана

            public override void ResetParticle(Particle particle)
            {
                base.ResetParticle(particle); // вызываем базовый сброс частицы, там жизнь переопределяется и все такое

                // а теперь тут уже подкручиваем параметры движения
                particle.X = Particle.rand.Next(Width); // позиция X -- произвольная точка от 0 до Width
                particle.Y = 0;  // ноль -- это верх экрана 

                particle.SpeedY = 1; // падаем вниз по умолчанию
                particle.SpeedX = Particle.rand.Next(-2, 2); // разброс влево и вправа у частиц 
            }
        }

        

    }
}
