using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap("256x256.png");
            pictureBox1.Image = bm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap) pictureBox1.Image,
                bm1 = new Bitmap(bm.Width, bm.Height);
            for (int y = 0; y < bm.Height; ++y)
            for (int x = 0; x < bm.Width; ++x)
            {
                Color col = bm.GetPixel(x, y);
                int r = col.R, g = col.G, b = col.B;
                int br = (r + g + b) / 3;
                col = Color.FromArgb(br, br, br);
                bm1.SetPixel(x, y, col);
            }

            pictureBox2.Image = bm1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(800, 600);
            double u = 1, v = 0;
            for (int y = 0; y < bm.Height; ++y)
            for (int x = 0; x < bm.Width; ++x)
            {
                int br = Convert.ToInt32(Math.Cos(u * x + v * y) * 50 + 128);
                Color col = Color.FromArgb(br, br, br);
                bm.SetPixel(x, y, col);
            }

            pictureBox1.Image = bm;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap) pictureBox1.Image,
                bm1 = new Bitmap(bm.Width, bm.Height);
            Random rnd = new Random();
            double s = 200;
            for (int y = 0; y < bm.Height; ++y)
            for (int x = 0; x < bm.Width; ++x)
            {
                Color col = bm.GetPixel(x, y);
                int r = col.R, g = col.G, b = col.B;
                double d = 0;
                for (int i = 0; i < 12; ++i) d += rnd.NextDouble();
                d -= 6;
                d *= s;
                r += Convert.ToInt32(d);
                g += Convert.ToInt32(d);
                b += Convert.ToInt32(d);
                if (r < 0) r = 0;
                else if (r > 255) r = 255;
                if (g < 0) g = 0;
                else if (g > 255) g = 255;
                if (b < 0) b = 0;
                else if (b > 255) b = 255;
                col = Color.FromArgb(r, g, b);
                bm1.SetPixel(x, y, col);
            }

            pictureBox2.Image = bm1;
        }

        /// <summary>
        /// добавление шума
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap) pictureBox1.Image,
                bm1 = new Bitmap(bm.Width, bm.Height);
            Random rnd = new Random();
            double s = 200;
            for (int y = 0; y < bm.Height; ++y)
            for (int x = 0; x < bm.Width; ++x)
            {
                Color col = bm.GetPixel(x, y);
                int r = col.R, g = col.G, b = col.B;
                double d = rnd.NextDouble() * 2 * s - s;
                r += Convert.ToInt32(d);
                d = rnd.NextDouble() * 2 * s - s;
                g += Convert.ToInt32(d);
                d = rnd.NextDouble() * 2 * s - s;
                b += Convert.ToInt32(d);
                if (r < 0) r = 0;
                else if (r > 255) r = 255;
                if (g < 0) g = 0;
                else if (g > 255) g = 255;
                if (b < 0) b = 0;
                else if (b > 255) b = 255;
                col = Color.FromArgb(r, g, b);
                bm1.SetPixel(x, y, col);
            }

            pictureBox2.Image = bm1;
        }

        /// <summary>
        /// naive denoiser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap) pictureBox1.Image,
                bm1 = new Bitmap(bm.Width, bm.Height);
            Random rnd = new Random();
            for (int y = 1; y < bm.Height - 1; ++y)
            for (int x = 1; x < bm.Width - 1; ++x)
            {
                Color col = bm.GetPixel(x, y);
                int r = col.R, g = col.G, b = col.B;

                Color col2 = bm.GetPixel(x - 1, y - 1);
                int r2 = col2.R, g2 = col2.G, b2 = col2.B;

                Color col3 = bm.GetPixel(x + 1, y + 1);
                int r3 = col3.R, g3 = col3.G, b3 = col3.B;

                r = (r + r2 + r3) / 3;
                g = (g + g2 + g3) / 3;
                b = (b + b2 + b3) / 3;

                if (r < 0) r = 0;
                else if (r > 255) r = 255;
                if (g < 0) g = 0;
                else if (g > 255) g = 255;
                if (b < 0) b = 0;
                else if (b > 255) b = 255;

                col = Color.FromArgb(r, g, b);
                bm1.SetPixel(x, y, col);
            }

            pictureBox2.Image = bm1;
        }

        /// <summary>
        /// box denoiser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap) pictureBox1.Image,
                bm1 = new Bitmap(bm.Width, bm.Height);
            Random rnd = new Random();
            for (int y = 0; y < bm.Height; ++y)
            for (int x = 0; x < bm.Width; ++x)
            {
                if (x > 5 && y > 5 && x < bm.Height - 6 && y < bm.Height - 6)
                {
                    bm1.SetPixel(x, y, calcAvg(bm, x, y, 5));
                }
                else
                {
                    bm1.SetPixel(x, y, bm.GetPixel(x, y));
                }
            }

            pictureBox2.Image = bm1;
        }

        /// <summary>
        /// support function
        /// </summary>
        /// <param name="bm"></param>
        /// <param name="topLeftX"></param>
        /// <param name="topLeftY"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private Color calcAvg(Bitmap bm, int topLeftX, int topLeftY, int size)
        {
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Color col = bm.GetPixel(topLeftX + j, topLeftY + i);
                    sumR += col.R;
                    sumG += col.G;
                    sumB += col.B;
                }
            }

            //if (sumR < 0) sumR = 0; else if (r > 255) r = 255;
            //if (g < 0) g = 0; else if (g > 255) g = 255;
            //if (b < 0) b = 0; else if (b > 255) b = 255;

            return Color.FromArgb(sumR / 25, sumG / 25, sumB / 25);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap) pictureBox1.Image,
                bm1 = new Bitmap(bm.Width, bm.Height);
            Random rnd = new Random();
            for (int it = 0; it < 10; it++)
            {
                for (int y = 1; y < bm.Height - 1; ++y)
                for (int x = 1; x < bm.Width - 1; ++x)
                {
                    Color col = bm.GetPixel(x, y);
                    int r = col.R, g = col.G, b = col.B;

                    Color col2 = bm.GetPixel(x - 1, y - 1);
                    int r2 = col2.R, g2 = col2.G, b2 = col2.B;

                    Color col3 = bm.GetPixel(x + 1, y + 1);
                    int r3 = col3.R, g3 = col3.G, b3 = col3.B;

                    r = (r + r2 + r3) / 3;
                    g = (g + g2 + g3) / 3;
                    b = (b + b2 + b3) / 3;

                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;

                    col = Color.FromArgb(r, g, b);
                    bm1.SetPixel(x, y, col);
                }

                pictureBox2.Image = bm1;
                bm = bm1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(256, 256);
            for (int g = 0; g < 256; ++g)
            for (int r = 0; r < 256 - g; ++r)
            {
                int b = 255 - r - g;
                Color col = Color.FromArgb(r, g, b);
                bm.SetPixel(r, g, col);
            }
        }


        //NOise
        public Bitmap ColorNoise(Bitmap bm, double s)
        {
            Bitmap bm1 = new Bitmap(bm.Width, bm.Height);
            //d - уровень шума (200)
            //double s = 10;
            int d;
            Random rnd = new Random();


            for (int y = 0; y < bm.Height; ++y)
            for (int x = 0; x < bm.Width; ++x)
            {
                Color col = bm.GetPixel(x, y);
                int r = col.R,
                    g = col.G,
                    b = col.B;

                d = (int) Math.Round(rnd.NextDouble() * 2 * s - s);
                r += d;
                d = (int) Math.Round(rnd.NextDouble() * 2 * s - s);
                g += d;
                d = (int) Math.Round(rnd.NextDouble() * 2 * s - s);
                b += d;
                if (r < 0) r = 0;
                else if (r > 255) r = 255;
                if (g < 0) g = 0;
                else if (g > 255) g = 255;
                if (b < 0) b = 0;
                else if (b > 255) b = 255;
                col = Color.FromArgb(r, g, b);
                bm1.SetPixel(x, y, col);
            }

            //pictureBox1.Image = bm1;
            return bm1;
        }

        //Фильтр
        private Bitmap Gays(double[,] m1, Bitmap bmp)
        {
            //bmp = (Bitmap)pictureBox1.Image
            Bitmap bmp1 = new Bitmap(bmp.Width, bmp.Height);
            Random rnd = new Random();
            //от 2 до w-2
            for (int y = 2; y < bmp.Height - 2; ++y)
            {
                for (int x = 2; x < bmp.Width - 2; ++x)
                {
                    int r, g, b;
                    double rs, gs, bs;
                    rs = bs = gs = 0;
                    for (int j = y - 2, k = 0; j < y + 2; ++j, ++k)
                    {
                        for (int i = x - 2, n = 0; i < x + 2; ++i, ++n)
                        {
                            Color c = bmp.GetPixel(i, j);
                            r = c.R;
                            g = c.G;
                            b = c.B;
                            rs += r * m1[n, k];
                            gs += g * m1[n, k];
                            bs += b * m1[n, k];
                        }
                    }

                    r = (int) Math.Round(rs);
                    g = (int) Math.Round(gs);
                    b = (int) Math.Round(bs);
                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;


                    Color c1 = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, c1);
                }
            }

            //pictureBox2.Image = bmp1;
            return bmp1;
        }
        
        private Bitmap GaysRecursive(double[,] m1, Bitmap bmp)
        {
            //bmp = (Bitmap)pictureBox1.Image
            Bitmap bmp1 = bmp;//new Bitmap(bmp.Width, bmp.Height);
            Random rnd = new Random();
            //от 2 до w-2
            for (int y = 2; y < bmp.Height - 2; ++y)
            {
                for (int x = 2; x < bmp.Width - 2; ++x)
                {
                    int r, g, b;
                    double rs, gs, bs;
                    rs = bs = gs = 0;
                    for (int j = y - 2, k = 0; j < y + 2; ++j, ++k)
                    {
                        for (int i = x - 2, n = 0; i < x + 2; ++i, ++n)
                        {
                            Color c = bmp1.GetPixel(i, j);
                            r = c.R;
                            g = c.G;
                            b = c.B;
                            rs += r * m1[n, k];
                            gs += g * m1[n, k];
                            bs += b * m1[n, k];
                        }
                    }

                    r = (int) Math.Round(rs);
                    g = (int) Math.Round(gs);
                    b = (int) Math.Round(bs);
                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;


                    Color c1 = Color.FromArgb(r, g, b);
                    bmp1.SetPixel(x, y, c1);
                }
            }

            //pictureBox2.Image = bmp1;
            return bmp1;
        }


        //Сравнение
        public double CheckBitmaps(Bitmap bm, Bitmap bm1)
        {
            double sumR = 0;
            double sumG = 0;
            double sumB = 0;

            for (int y = 0; y < bm.Height; ++y)
            {
                for (int x = 0; x < bm.Width; ++x)
                {
                    Color c1 = bm.GetPixel(x, y);
                    Color c2 = bm1.GetPixel(x, y);
                    sumR += Math.Abs(c1.R - c2.R);
                    sumG += Math.Abs(c1.G - c2.G);
                    sumB += Math.Abs(c1.B - c2.B);
                }
            }

            double s = (sumR + sumG + sumB) / (3 * bm.Width * bm.Height);
            return s;
        }

        public void button10_Click(object sender, EventArgs e)
        {
            // создать матрицу 6:6
            // TODO: Заполнить ее результатами проверок при разном уровне шума и разном фильтре
            //label1.Text = "2/6";
            //return; todo create progress bar
            //фильтрующие матрицы
            double w11 = 0.0;
            double w12 = 0.07;
            double w13 = 0.44;

            double w21 = 0.005;
            double w22 = 0.08;
            double w23 = 0.27;

            double w31 = 0.02;
            double w32 = 0.06;
            double w33 = 0.15;

            double w41 = 0.02;
            double w42 = 0.06;
            double w43 = 0.1;

            double w51 = 0.03;
            double w52 = 0.45;
            double w53 = 0.06;

            double w61 = 0.025;
            double w62 = 0.06;
            double w63 = 0.06;


            double[,] m1 =
            {
                {w11, w11, w11, w11, w11}, {w11, w12, w12, w12, w11}, {w11, w12, w13, w12, w11},
                {w11, w12, w12, w12, w11}, {w11, w11, w11, w11, w11}
            };
            double[,] m2 =
            {
                {w21, w21, w21, w21, w21}, {w21, w22, w22, w22, w21}, {w21, w22, w23, w22, w21},
                {w21, w22, w22, w22, w21}, {w21, w21, w21, w21, w21}
            };
            double[,] m3 =
            {
                {w31, w31, w31, w31, w31}, {w31, w32, w32, w32, w31}, {w31, w32, w33, w32, w31},
                {w31, w32, w32, w32, w31}, {w31, w31, w31, w31, w31}
            };
            double[,] m4 =
            {
                {w41, w41, w41, w41, w41}, {w41, w42, w42, w42, w41}, {w41, w42, w43, w42, w41},
                {w41, w42, w42, w42, w41}, {w41, w41, w41, w41, w41}
            };
            double[,] m5 =
            {
                {w51, w51, w51, w51, w51}, {w51, w52, w52, w52, w51}, {w51, w52, w53, w52, w51},
                {w51, w52, w52, w52, w51}, {w51, w51, w51, w51, w51}
            };
            double[,] m6 =
            {
                {w61, w61, w61, w61, w61}, {w61, w62, w62, w62, w61}, {w61, w62, w63, w62, w61},
                {w61, w62, w62, w62, w61}, {w61, w61, w61, w61, w61}
            };

            label1.Text = "filters loaded";


            //уровни шума
            int[] s = {10, 60, 110, 160, 210, 260};

            //массив с ответами
            double[,] answer = new double[6, 6];
            int rows = answer.GetUpperBound(0) + 1;
            int columns = answer.Length / rows;


            Bitmap bm = new Bitmap("128.png");
            label1.Text = "image loaded";
            label1.Text = "1/6";
            for (int i = 0; i < 6; i++)
            {
                Bitmap bm2 = (Bitmap) bm.Clone();
                bm2 = ColorNoise(bm2, s[i]);
                bm2 = GaysRecursive(m1, bm2);
                answer[0, i] = CheckBitmaps(bm, bm2);
            }
            
            label1.Text = "2/6";
            for (int i = 0; i < 6; i++)
            {
                Bitmap bm2 = (Bitmap) bm.Clone();
                bm2 = ColorNoise(bm2, s[i]);
                bm2 = GaysRecursive(m2, bm2);
                answer[1, i] = CheckBitmaps(bm, bm2);
            }
            label1.Text = "3/6";
            for (int i = 0; i < 6; i++)
            {
                Bitmap bm2 = (Bitmap) bm.Clone();
                bm2 = ColorNoise(bm2, s[i]);
                bm2 = GaysRecursive(m3, bm2);
                answer[2, i] = CheckBitmaps(bm, bm2);
            }
            label1.Text = "4/6";
            for (int i = 0; i < 6; i++)
            {
                Bitmap bm2 = (Bitmap) bm.Clone();
                bm2 = ColorNoise(bm2, s[i]);
                bm2 = GaysRecursive(m4, bm2);
                answer[3, i] = CheckBitmaps(bm, bm2);
            }
            label1.Text = "5/6";
            for (int i = 0; i < 6; i++)
            {
                Bitmap bm2 = (Bitmap) bm.Clone();
                bm2 = ColorNoise(bm2, s[i]);
                bm2 = GaysRecursive(m5, bm2);
                answer[4, i] = CheckBitmaps(bm, bm2);
            }
            label1.Text = "6/6";
            for (int i = 0; i < 6; i++)
            {
                Bitmap bm2 = (Bitmap) bm.Clone();
                bm2 = ColorNoise(bm2, s[i]);
                bm2 = GaysRecursive(m6, bm2);
                answer[5, i] = CheckBitmaps(bm, bm2);
            }

            //и в циклы добавить клонирование зашумленной фотки и использование рекурсивного фильтра
            //сделать 12 столбцов : 6 для обычного и 6 для рекрсивного фильтра

            //вывод таблички
            for (int i = 0; i < 6; i++)
            {
                textBox1.AppendText(Convert.ToString(i));
                textBox1.AppendText(" :");
                for (int j = 0; j < 6; j++)
                {
                    textBox1.AppendText(Convert.ToString(answer[i, j]));
                    textBox1.AppendText(" ");
                }

                textBox1.AppendText("\n");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}