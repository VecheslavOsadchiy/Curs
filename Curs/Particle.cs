﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curs
{
    internal class Particle
    {
        public int Radius; // радуис частицы
        public float X; // X координата положения частицы в пространстве
        public float Y; // Y координата положения частицы в пространстве
        public float Direction; // направление движения
        public float Speed; // скорость перемещения

        public static Random rand = new Random();

        public Particle()//создает рандомные характеристики частице
        {
            Direction = rand.Next(360);
            Speed = 1 + rand.Next(10);
            Radius = 2 + rand.Next(10);
        }
        public void Draw(Graphics g)
        {
            // создали кисть для рисования
            var b = new SolidBrush(Color.Black);

            // нарисовали залитый кружок радиусом Radius с центром в X, Y
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            // удалили кисть из памяти, вообще сборщик мусора рано или поздно это сам сделает
            // но документация рекомендует делать это самому
            b.Dispose();
        }
    }
}