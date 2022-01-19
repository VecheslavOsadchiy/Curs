using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Curs
{

    public partial class Form1 : Form

    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter; // добавим поле для эмиттера

        TeleportOUT point1; // добавил поле под первую точку
        TeleportIN point2; // добавил поле под вторую точку
        int mouseClick;
        public Form1()
        {
            InitializeComponent();
            // привязал изображение
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
            };
           
            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся

            point1 = new TeleportOUT
            {
                
            };
            point2 = new TeleportIN
            {
               
            };

            // привязываем поля к эмиттеру
            emitter.impactPoints.Add(point1);
            emitter.impactPoints.Add(point2);
           
            
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            point2.coord(point1.X, point1.Y);
            if ((GetAsyncKeyState(1) != 0) || (GetAsyncKeyState(2) != 0)){
                if(GetAsyncKeyState(1) != 0)
                {
                    mouseClick = 1;
                }
                else
                {
                    mouseClick = 2;
                }
            }
            emitter.UpdateState(); // каждый тик обновляем систему
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black); // добавил очистку              
                emitter.Render(g); // рендерим систему
            }
            // обновить picDisplay
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {// в обработчике заносим положение мыши в переменные для хранения положения мыши
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
            if (mouseClick == 1) { 
               point1.X = e.X;
               point1.Y = e.Y;
            }else if (mouseClick == 2)
            {
                point2.X = e.X;
                point2.Y = e.Y;
            }
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value; // направлению эмиттера присваиваем значение ползунка
            lblDirection.Text = $"{tbDirection.Value}°"; // добавил вывод значения
        }

        private void tbGraviton_Scroll(object sender, EventArgs e)
        {
            point1.Power = tbGraviton1.Value;
        }
        private void tbGraviton2_Scroll(object sender, EventArgs e)
        {
            point2.Power = tbGraviton2.Value;
        }

       






















        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void picDisplay_Click(object sender, EventArgs e)
        {

        }

        private void picDisplay_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
