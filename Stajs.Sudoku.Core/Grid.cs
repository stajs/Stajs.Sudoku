using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Stajs.Sudoku.Core.Exceptions;

namespace Stajs.Sudoku.Core
{
	public class Grid
	{
		private const int DimensionLength = 9;
		private const int EmptyValue = 0;
		private const int MinValue = 1;
		private const int MaxValue = 9;

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
			return value >= EmptyValue && value <= MaxValue;
		}

		internal static bool IsSliceValid(int[] slice)
		{
			var list = new List<int>();

			foreach (var i in slice)
			{
				if (!IsValueValid(i))
					throw new ValueOutOfRangeException();

				if (i != EmptyValue && list.Contains(i))
					return false;

				list.Add(i);
			}

			return true;
		}

		internal static bool IsBoxValid(int[,] box)
		{
			var list = new List<int>();

			foreach (var i in box)
			{
				if (!IsValueValid(i))
					throw new ValueOutOfRangeException();

				if (i != EmptyValue && list.Contains(i))
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

			if (!IsBoxValid(GetBoxForPoint(values, x, y)))
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

		internal static int[,] GetBoxForPoint(int[,] values, int x, int y)
		{
			Box box;

			if (x >= 0 && x <= 2)
			{
				if (y >= 0 && y <= 2)
					box = Box.TopLeft;
				else if (y >= 3 && y <= 5)
					box = Box.TopCenter;
				else
					box = Box.TopRight;
			}
			else if (x >= 3 && x <= 5)
			{
				if (y >= 0 && y <= 2)
					box = Box.CenterLeft;
				else if (y >= 3 && y <= 5)
					box = Box.CenterCenter;
				else
					box = Box.CenterRight;
			}
			else
			{
				if (y >= 0 && y <= 2)
					box = Box.BottomLeft;
				else if (y >= 3 && y <= 5)
					box = Box.BottomCenter;
				else
					box = Box.BottomRight;
			}

			return GetBox(values, box);
		}

		internal static int[,] GetBox(int[,] values, Box box)
		{
			int startCol;
			int startRow;

			switch (box)
			{
				default:
					startCol = 0;
					startRow = 0;
					break;
				case Box.TopCenter:
					startCol = 3;
					startRow = 0;
					break;
				case Box.TopRight:
					startCol = 6;
					startRow = 0;
					break;
				case Box.CenterLeft:
					startCol = 0;
					startRow = 3;
					break;
				case Box.CenterCenter:
					startCol = 3;
					startRow = 3;
					break;
				case Box.CenterRight:
					startCol = 6;
					startRow = 3;
					break;
				case Box.BottomLeft:
					startCol = 0;
					startRow = 6;
					break;
				case Box.BottomCenter:
					startCol = 3;
					startRow = 6;
					break;
				case Box.BottomRight:
					startCol = 6;
					startRow = 6;
					break;
			}

			var ret = new int[3, 3];

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

		internal static List<int> GetValidValuesForPoint(int[,] grid, Point point)
		{
			return GetValidValuesForPoint(grid, point.X, point.Y);
		}

		internal static List<int> GetValidValuesForPoint(int[,] grid, int x, int y)
		{
			var availableValues = new List<int>();

			if (grid[x, y] != EmptyValue)
				return availableValues;

			availableValues.AddRange(Enumerable.Range(1, MaxValue));

			foreach (var i in grid.GetRow(x))
				availableValues.Remove(i);

			foreach (var i in grid.GetColumn(y))
				availableValues.Remove(i);

			foreach (var i in GetBoxForPoint(grid, x, y))
				availableValues.Remove(i);

			return availableValues;
		}

		internal static bool HasGaps(int[,] grid)
		{
			foreach (var i in grid)
				if (i == EmptyValue)
					return true;

			return false;
		}

		internal static bool IsSolved(int[,] grid)
		{
			return !HasGaps(grid) && IsGridValid(grid);
		}

		internal static IEnumerable<Point> GetEmptyPoints(int[,] grid)
		{
			for (var x = 0; x < DimensionLength; x++)
			{
				for (var y = 0; y < DimensionLength; y++)
				{
					if (grid[x, y] == EmptyValue)
						yield return new Point(x, y);
				}
			}
		}

		internal static int[,] Solve(int[,] grid)
		{
			var stack = new Stack<int[,]>();
			stack.Push(grid);

			Trace.WriteLine("Solve Init");

			var count = 0;

			return Solve(stack, ref count);
		}

		private static int[,] Solve(Stack<int[,]> stack, ref int count)
		{
			Trace.WriteLine("\nSolve count: " + ++count);
			Trace.WriteLine("stack.Count: " + stack.Count);

			if (count > 500)
				throw new TimeoutException();

			var grid = stack.Pop();

			if (IsSolved(grid))
			{
				Trace.WriteLine("Returning grid\n");
				return grid;
			}

			var point = GetEmptyPoints(grid).First();
			var values = GetValidValuesForPoint(grid, point);

			Trace.WriteLine("values.Count: " + values.Count);

			foreach (var value in values)
			{
				Trace.WriteLine("value: " + value);
				var newGrid = grid.Copy();
				newGrid[point.X, point.Y] = value;

				stack.Push(newGrid);
			}

			return Solve(stack, ref count);
		}
	}
}