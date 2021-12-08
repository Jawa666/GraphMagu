using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.VisualGraphClasses
{
	public partial class GraphModel
	{
		public class Edge
		{
			public Vertex Start { get; set; }

			public Vertex End { get; set; }

			public bool IsSecond { get; set; }
		}
	}
}
