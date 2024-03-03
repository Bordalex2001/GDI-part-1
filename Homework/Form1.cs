using System;
using System.Drawing;
using System.Windows.Forms;

namespace Homework
{
    public partial class Form1 : Form
    {
        private int width;
        private int height;
        public Form1()
        {
            InitializeComponent();
            width = pictureBox1.Width; 
            height = pictureBox1.Height;
            Activated += new EventHandler(Form1_Load);
            button1.Click += new EventHandler(button1_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Pen graphPen = new Pen(Color.Red, 3);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            string chosenFunction = comboBox1.SelectedItem.ToString();
            switch (chosenFunction)
            {
                case "Лінійна":
                    BuildLinearFunction(g, graphPen); break;
                case "Пряма пропорціональність":
                    BuildProportionalFunction(g, graphPen); break;
                case "Квадратична":
                    BuildQuadraticFunction(g, graphPen); break;
            }
            graphPen.Dispose();
            g.Dispose();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            int x_axis = pictureBox1.Height / 2;
            int y_axis = pictureBox1.Width / 2;
            Pen axis_line = new Pen(Color.Black, 2);
            g.DrawLine(axis_line, 0, x_axis, pictureBox1.Width, x_axis);
            g.DrawLine(axis_line, y_axis, 0, y_axis, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x += 20)
            {
                g.DrawLine(Pens.SlateGray, x, 0, x, pictureBox1.Height);
            }
            for (int y = 0; y < pictureBox1.Height; y += 20)
            {
                g.DrawLine(Pens.SlateGray, 0, y, pictureBox1.Width, y);
            }
        }
        private void BuildLinearFunction(Graphics g, Pen graphPen)
        {
            for (int x = 100; x < width - 50; x++)
            {
                int y = height - 50 - (x - 100); //Приклад лінійної функції
                g.DrawLine(graphPen, x, y, x + 1, y + 1);
            }
        }
        private void BuildProportionalFunction(Graphics g, Pen graphPen)
        {
            int k = 3;
            for (int x = 100; x < width - 50; x++)
            {
                int y = height - 50 - (k * (x - 100)); //Приклад прямої пропорціональності 
                g.DrawLine(graphPen, x, y, x + 1, y + 1);
            }
        }
        private void BuildQuadraticFunction(Graphics g, Pen graphPen)
        {
            PointF p1 = new PointF(0, height - 50);
            PointF p2 = new PointF(width - 50, height - 50);
            PointF[] curvePoints = GenerateQuadraticPoints(p1, p2);
            g.DrawCurve(graphPen, curvePoints);
        }
        private PointF[] GenerateQuadraticPoints(PointF p1, PointF p2)
        {
            int numPoints = 50; // Кількість точок параболи
            PointF[] curvePoints = new PointF[numPoints];
            float a = 2f;
            float b = 0f;
            float c = 0f;
            //double discriminant = b * b - 4 * a * c;
            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (float)(numPoints - 1);
                float x = (1 - t) * p1.X + t * p2.X;
                double y = a * Math.Pow(x, 2) + b * x + c; // Приклад квадратичної функції
                curvePoints[i] = new PointF(x, (float)y);
            }
            return curvePoints;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Лінійна");
            comboBox1.Items.Add("Пряма пропоціональність");
            comboBox1.Items.Add("Квадратична");
            comboBox1.SelectedIndex = 0;
        }
    }
}