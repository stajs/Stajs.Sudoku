using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stajs.Sudoku.Core
{
	public class Grid
	{
		internal int[,] Values;

		public Grid(int[,] values)
		{
			if (values == null) throw new ArgumentNullException();
		}
	}
}