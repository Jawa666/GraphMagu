using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.VisualGraphClasses
{
	public partial class GraphModel
	{
		public List<Edge> Edges { get; private set; }
		public List<Vertex> Vertexes { get; set; }

		public GraphModel(int[,] graphMatrix, int R, int width, int height)
		{
			Random rnd = new Random();
			int startX = 2*R, endX = width - 2*R;
			int startY = 2*R, endY = height - 2*R;


			char[] alphabeth = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

			Edges = new List<Edge>();
			Vertexes = new List<Vertex>();

			for (int i = 0; i < graphMatrix.GetLength(0); i++)
			{
				Vertex v = new Vertex
				{
					Name = alphabeth[i].ToString()
				};

				int x = 0, y = 0;

				do
				{
					x = rnd.Next(startX, endX);
					y = rnd.Next(startY, endY);
				}
				while (!CanSetVertex(x, y, R));

				v.X = x; v.Y = y;

				Vertexes.Add(v);
			}

			for (int i = 0; i < graphMatrix.GetLength(0); i++)
			{
				for (int j = 0; j < graphMatrix.GetLength(1); j++)
				{

					if (graphMatrix[i, j] == 1)
					{
						Edge e = new Edge
						{
							Start = Vertexes[i],
							End = Vertexes[j],
							IsSecond = false
						};

						if (graphMatrix[j, i] == 1)
						{
							Edge v = Edges.Where(c => c.Start.Name == alphabeth[j].ToString()
								&& c.End.Name == alphabeth[i].ToString())
								.FirstOrDefault();

							if (v != null)
							{
								e.IsSecond = true;
							}
						}

						Edges.Add(e);
					}
				}
			}
		}

		private bool CanSetVertex (int x, int y, int R)
		{
			int radius = R * 4;

			foreach (var v in Vertexes)
			{
				if ((Math.Pow((x - v.X), 2) + Math.Pow((y - v.Y), 2)) < Math.Pow(radius, 2))
				{
					return false;
				}
			}

			return true;
		}
	}
}
