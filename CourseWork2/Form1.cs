using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork2
{
    public partial class Form1 : Form
    {
        Color picColor = Color.Black;
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter; // добавим поле для эмиттера

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = trackBar2.Value,
                Spreading = trackBar3.Value,
                SpeedMin = trackBar5.Value,
                SpeedMax = trackBar5.Value+3,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = trackBar1.Value,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
                RadiusMin = trackBar6.Value * 4,
                RadiusMax = trackBar6.Value * 4+8,
            };
            emitters.Add(this.emitter); // все равно добавляю в список emitters, чтобы он рендерился и обновлялся
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // тут теперь обновляем эмиттер
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(picColor);
                emitter.Render(g); // а тут теперь рендерим через эмиттер
            }
            picDisplay.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int R = random.Next(255);
            int G = random.Next(255);
            int B = random.Next(255);
            emitter.ColorFrom = Color.FromArgb(R, G, B);
            emitter.ColorTo = picColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int R = random.Next(255);
            int G = random.Next(255);
            int B = random.Next(255);
            var g = Graphics.FromImage(picDisplay.Image);
            picColor = Color.FromArgb(R, G, B);
            emitter.ColorTo = picColor;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            emitter.ParticlesPerTick = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = trackBar3.Value;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value==0) {
                emitter.SpeedMin = trackBar5.Value;
                emitter.SpeedMax = trackBar5.Value ;
            }
            else { 
                emitter.SpeedMin = trackBar5.Value;
                emitter.SpeedMax= trackBar5.Value+3;
            }
            
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (trackBar4.Value==0) {
                emitter.LifeMax = 0;
                emitter.LifeMin = 0;
            }
            else if (trackBar4.Value>70) {
                emitter.LifeMax = 100;
                emitter.LifeMin = trackBar4.Value;
            }
            else  {
                emitter.LifeMax = trackBar4.Value+30;
                emitter.LifeMin = trackBar4.Value;
            }
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            if (trackBar6.Value == 0)
            {
                emitter.RadiusMax = trackBar6.Value;
                emitter.RadiusMin = trackBar6.Value;
            }
            else
            {
                emitter.RadiusMax = trackBar6.Value*4+8;
                emitter.RadiusMin = trackBar6.Value*4;
            }
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            emitter.X = e.X;
            emitter.Y = e.Y;
        }
    }
}
