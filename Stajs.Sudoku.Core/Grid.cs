using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Stajs.Sudoku.Core
{
	public class Grid
	{
		internal byte?[,] _grid = new byte?[9, 9];

		public Grid(byte?[,] grid)
		{
			_grid = grid;
		}

		internal static bool IsSliceValid(byte?[] slice)
		{
			var list = new List<byte?>();

			foreach (var i in slice)
			{
				if (i.HasValue)
				{
					if (list.Contains(i))
						return false;
					else
						list.Add(i);
				}
			}

			return true;
		}

		internal static bool IsBoxValid(byte?[,] box)
		{
			var list = new List<byte?>();

			for (byte row = 0; row < 3; row++)
			{
				for (byte col = 0; col < 3; col++)
				{
					if (box[row, col].HasValue)
					{
						if (list.Contains(box[row, col]))
							return false;
						else
							list.Add(box[row, col]);
					}
				}
			}

			return true;
		}

		internal static bool IsPointValid(byte?[,] grid, Point point)
		{
			if (!IsSliceValid(grid.GetRow(point.Y)))
				return false;

			if (!IsSliceValid(grid.GetColumn(point.X)))
				return false;

			if (!IsBoxValid(grid.GetBox(point)))
				return false;

			return true;
		}

		internal static bool IsGridValid(byte?[,] grid)
		{
			for (byte y = 0; y < 9; y++)
			{
				for (byte x = 0; x < 9; x++)
				{
					if (!IsPointValid(grid, new Point(x, y)))
						return false;
				}
			}

			return true;
		}

		internal bool HasGaps(byte?[,] grid)
		{
			for (byte y = 0; y < 9; y++)
			{
				for (byte x = 0; x < 9; x++)
				{
					if (grid[y, x] == null)
						return true;
				}
			}

			return false;
		}

		internal static IEnumerable<Point> GetEmptyPoints(byte?[,] grid)
		{
			for (byte y = 0; y < 9; y++)
			{
				for (byte x = 0; x < 9; x++)
				{
					if (grid[y, x] == null)
						yield return new Point(x, y);
				}
			}
		}

		internal static List<byte> GetValidValuesForPoint(byte?[,] grid, Point point)
		{
			if (grid[point.Y, point.X] != null)
				return new List<byte>();

			var availableValues = Enumerable.Range(1, 9).Select(x => (byte)x).ToList();

			foreach (byte i in grid.GetRow(point.Y).ExcludeNulls())
				availableValues.Remove(i);

			foreach (byte i in grid.GetColumn(point.X).ExcludeNulls())
				availableValues.Remove(i);

			foreach (byte i in grid.GetBox(point).ExcludeNulls())
				availableValues.Remove(i);

			return availableValues;
		}

		internal bool IsSolved(byte?[,] grid)
		{
			return !HasGaps(grid) && IsGridValid(grid);
		}

		internal byte?[,] Solve()
		{
			var stack = new Stack<byte?[,]>();
			stack.Push(_grid);

			Trace.WriteLine("Solve Init");

			var count = 0;

			return Solve(stack, ref count);
		}

		private byte?[,] Solve(Stack<byte?[,]> stack, ref int count)
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
				newGrid[point.Y, point.X] = value;

				stack.Push(newGrid);
			}

			return Solve(stack, ref count);
		}

		public override string ToString()
		{
			return _grid.ToStringGrid();
		}
	}
}
