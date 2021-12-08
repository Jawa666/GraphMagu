using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Kursovaya.VisualGraphClasses;

using static Kursovaya.VisualGraphClasses.GraphModel;

namespace Kursovaya
{
    public partial class Graph : Form       //размер формы 685; 536
    {
        GraphModel graph;
        int R = 15;
        private Bitmap bitmap;

        private Pen black;
        private Pen arrow;

        private Graphics graphic;
        private Font font;

        private Brush brush;
        private Brush fill;

        int[,] graphMatrix;

        public Graph(int[,] graphMatrix)            
        {
            InitializeComponent();
            this.graphMatrix = graphMatrix;
            graph = new GraphModel(graphMatrix, R, pictureBox1.Width, pictureBox1.Height);

            black = new Pen(Color.Black);
            arrow = new Pen(Color.Black);

            AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
            arrow.CustomEndCap = bigArrow;

            black.Width = 2;
            arrow.Width = 2;

            font = new Font("Times New Roman", 10);

            fill = Brushes.White;
            brush = Brushes.Black;

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphic = Graphics.FromImage(bitmap);
            graphic.Clear(Color.White);

            foreach (var edge in graph.Edges)
			{
                DrawEdge(edge);
			}

            foreach (var vertex in graph.Vertexes)
			{
                DrawVertex(vertex);
			}

            pictureBox1.Image = bitmap;
        }

        private void DrawEdge(Edge edge)
        {
            PointF p1, p2, pCenter;

            int xStart, yStart, xEnd, yEnd;
            int xCenter, yCenter;

            if (edge.IsSecond)
            {
                xStart = edge.Start.X + (int)Math.Ceiling((R-2) * Math.Cos(7));
                yStart = edge.Start.Y + (int)Math.Ceiling((R - 2) * Math.Sin(7));

                xEnd = edge.End.X + (int)Math.Ceiling((R - 2) * Math.Cos(7));
                yEnd = edge.End.Y + (int)Math.Ceiling((R - 2) * Math.Sin(7));
            }
            else
            {
                xStart = edge.Start.X - (int)Math.Ceiling((R - 2) * Math.Cos(7));
                yStart = edge.Start.Y - (int)Math.Ceiling((R - 2) * Math.Sin(7));

                xEnd = edge.End.X - (int)Math.Ceiling((R - 2) * Math.Cos(7));
                yEnd = edge.End.Y - (int)Math.Ceiling((R - 2) * Math.Sin(7));
            }

            xCenter = (xStart + xEnd) / 2;
            yCenter = (yStart + yEnd) / 2;

            p1 = new PointF(xStart, yStart);
            p2 = new PointF(xEnd, yEnd);
            pCenter = new PointF(xCenter, yCenter);

            graphic.DrawLine(black, p1, p2);
            graphic.DrawLine(arrow, p1, pCenter);
        }
        private void DrawVertex(Vertex v)
        {
            PointF point = new PointF(v.X, v.Y);
            graphic.FillEllipse(fill, (v.X - R), (v.Y - R), 2 * R, 2 * R);
            graphic.DrawEllipse(black, (v.X - R), (v.Y - R), 2 * R, 2 * R);
            graphic.DrawString(v.Name, font, brush, point);
        }

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
