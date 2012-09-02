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

			if (!IsGridValid(values))
				throw new ArgumentException();

			Values = values;
		}

		private bool AreDimensionsValid(int[,] values)
		{
			var dimensionZeroLength = values.GetLength(0);
			var dimensionOneLength = values.GetLength(1);

			return dimensionZeroLength == DimensionLength && dimensionOneLength == DimensionLength;
		}

		private static bool IsValueValid(int value)
		{
			return value >= MinValue && value <= MaxValue;
		}

		internal static bool IsSliceValid(int[] slice)
		{
			var list = new List<int>();

			foreach (var i in slice)
			{
			   if (!IsValueValid(i))
			      throw new ValueOutOfRangeException();

			   if (i != 0 && list.Contains(i))
			      return false;

			   list.Add(i);
			}

			return true;
		}

		internal static bool IsQuadrantValid(int[,] quadrant)
		{
			var list = new List<int>();

			foreach (var i in quadrant)
			{
			   if (!IsValueValid(i))
					throw new ValueOutOfRangeException();

				if (i != 0 && list.Contains(i))
					return false;

				list.Add(i);
			}

			return true;
		}

		internal static bool IsPointValid(int[,] values, int x, int y)
		{
			if (!IsSliceValid(values.GetRow(x)))
				return false;

			if (!IsSliceValid(values.GetColumn(y)))
				return false;

			if (!IsQuadrantValid(GetQuadrantForPoint(values, x, y)))
				return false;

			return true;
		}

		internal static bool IsGridValid(int[,] values)
		{
			for (var x = 0; x < DimensionLength; x++)
			{
				for (var y = 0; y < DimensionLength; y++)
				{
					if (!IsPointValid(values, x, y))
						return false;
				}
			}

			return true;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			for (var i = 0; i < DimensionLength; i++)
			{
				sb.Append("{");

				for (var j = 0; j < DimensionLength; j++)
					sb.AppendFormat(" {0},", Values[i, j]);

				sb.Remove(sb.Length - 1, 1)
					.AppendLine(" },");
			}

			sb.Remove(sb.Length - 3, 3);

			return sb.ToString();
		}
		
		internal static int[,] GetQuadrantForPoint(int[,] values, int x, int y)
		{
			Quadrant quadrant;

			if (x >= 0 && x <= 2)
			{
				if (y >= 0 && y <= 2)
					quadrant = Quadrant.TopLeft;
				else if (y >= 3 && y <= 5)
					quadrant = Quadrant.TopCenter;
				else
					quadrant = Quadrant.TopRight;
			}
			else if (x >= 3 && x <= 5)
			{
				if (y >= 0 && y <= 2)
					quadrant = Quadrant.CenterLeft;
				else if (y >= 3 && y <= 5)
				   quadrant = Quadrant.CenterCenter;
				else
				   quadrant = Quadrant.CenterRight;
			}
			else
			{
				if (y >= 0 && y <= 2)
					quadrant = Quadrant.BottomLeft;
				else if (y >= 3 && y <= 5)
				   quadrant = Quadrant.BottomCenter;
				else
					quadrant = Quadrant.BottomRight;
			}

			return GetQuadrant(values, quadrant);
		}

		internal static int[,] GetQuadrant(int[,] values, Quadrant quadrant)
		{
			int startCol;
			int startRow;

			switch (quadrant)
			{
				default:
					startCol = 0;
					startRow = 0;
					break;
				case Quadrant.TopCenter:
					startCol = 3;
					startRow = 0;
					break;
				case Quadrant.TopRight:
					startCol = 6;
					startRow = 0;
					break;
				case Quadrant.CenterLeft:
					startCol = 0;
					startRow = 3;
					break;
				case Quadrant.CenterCenter:
					startCol = 3;
					startRow = 3;
					break;
				case Quadrant.CenterRight:
					startCol = 6;
					startRow = 3;
					break;
				case Quadrant.BottomLeft:
					startCol = 0;
					startRow = 6;
					break;
				case Quadrant.BottomCenter:
					startCol = 3;
					startRow = 6;
					break;
				case Quadrant.BottomRight:
					startCol = 6;
					startRow = 6;
					break;
			}

			var ret = new int[3,3];

			for (var i = 0; i < 3; i++)
			{
				var col = startCol + i;
				for (var j = 0; j < 3; j++)
				{
					var row = startRow + j;
					ret[j, i] = values[row, col];
				}
			}

			return ret;
		}

		internal static List<int> GetValidValuesForPoint(int[,] grid, int x, int y)
		{
			var availableValues = new List<int>();

			if (grid[x, y] != 0)
				return availableValues;

			availableValues.AddRange(Enumerable.Range(1, MaxValue));

			foreach (var i in grid.GetRow(x))
				availableValues.Remove(i);

			foreach (var i in grid.GetColumn(y))
				availableValues.Remove(i);

			foreach (var i in GetQuadrantForPoint(grid, x, y))
				availableValues.Remove(i);

			return availableValues;
		}

		internal static bool HasGaps(int[,] grid)
		{
			foreach (var i in grid)
				if (i == 0)
					return true;

			return false;
		}

		internal static bool IsSolved(int[,] grid)
		{
			return !HasGaps(grid) && IsGridValid(grid);
		}
	}
}