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

			const int requiredDimensionLength = 9;
			var dimensionZeroLength = values.GetLength(0);
			var dimensionOneLength = values.GetLength(1);

			if (dimensionZeroLength != requiredDimensionLength || dimensionOneLength != requiredDimensionLength)
				throw new ArrayLengthException();
		}
	}
}