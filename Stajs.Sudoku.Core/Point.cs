using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stajs.Sudoku.Core
{
	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}