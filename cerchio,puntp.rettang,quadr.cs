using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Assicura che il form risponda all'evento Paint
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Crea un oggetto Graphics dal controllo
            Graphics g = e.Graphics;

            // Disegna un cerchio
            int xCircle = 50;
            int yCircle = 50;
            int circleWidth = 100;
            int circleHeight = 100;
            Color circleColor = Color.Blue;
            using (Pen pen = new Pen(circleColor, 2))
            {
                g.DrawEllipse(pen, xCircle, yCircle, circleWidth, circleHeight);
            }

            // Disegna una linea
            int x1Line = 200;
            int y1Line = 50;
            int x2Line = 350;
            int y2Line = 50;
            Color lineColor = Color.Red;
            using (Pen pen = new Pen(lineColor, 2))
            {
                g.DrawLine(pen, x1Line, y1Line, x2Line, y2Line);
            }

            // Disegna un quadrato
            int xSquare = 50;
            int ySquare = 200;
            int squareSize = 100;
            Color squareColor = Color.Green;
            using (Pen pen = new Pen(squareColor, 2))
            {
                g.DrawRectangle(pen, xSquare, ySquare, squareSize, squareSize);
            }

            // Disegna un rettangolo
            int xRect = 200;
            int yRect = 200;
            int rectWidth = 150;
            int rectHeight = 80;
            Color rectColor = Color.Orange;
            using (Pen pen = new Pen(rectColor, 2))
            {
                g.DrawRectangle(pen, xRect, yRect, rectWidth, rectHeight);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Puoi gestire l'inizializzazione qui se necessario
        }
    }
}
