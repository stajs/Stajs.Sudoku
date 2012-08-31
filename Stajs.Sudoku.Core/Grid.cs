using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stajs.Sudoku.Core.Exceptions;

namespace Stajs.Sudoku.Core
{
	public class Grid
	{
		const int DimensionLength = 9;
		const int MinValue = 0;
		const int MaxValue = 9;

		internal int[,] Values;

		public Grid(int[,] values)
		{
			if (values == null)
				throw new ArgumentNullException();

			if (!AreDimensionsValid(values))
				throw new ArrayLengthException();

			if (!AreValuesValid(values))
				throw new ValueOutOfRangeException();
		}

		private bool AreValuesValid(int[,] values)
		{
			for (var i = 0; i < DimensionLength; i++)
			{
				for (var j = 0; j < DimensionLength; j++)
				{
					var value = values[i, j];
					if (value < MinValue || value > MaxValue)
						return false;
				}
			}

			return true;
		}

		private bool AreDimensionsValid(int[,] values)
		{
			var dimensionZeroLength = values.GetLength(0);
			var dimensionOneLength = values.GetLength(1);

			return dimensionZeroLength == DimensionLength && dimensionOneLength == DimensionLength;
		}
	}
}