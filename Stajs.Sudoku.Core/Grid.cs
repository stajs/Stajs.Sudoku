using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stajs.Sudoku.Core.Exceptions;

namespace Stajs.Sudoku.Core
{
	public class Grid
	{
		internal int[,] Values;

		public Grid(int[,] values)
		{
			if (values == null)
				throw new ArgumentNullException();

			if (values.GetLength(0) < 1
			    || values.GetLength(1) < 1
			    || values.GetLength(0) > 9
			    || values.GetLength(1) > 9)
				throw new ArrayLengthException();
		}
	}
}