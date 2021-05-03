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

		internal bool IsBoxValid(byte?[,] box)
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

		internal bool IsPointValid(byte?[,] grid, Point point)
		{
			if (!IsSliceValid(grid.GetRow(point.Y)))
				return false;

			if (!IsSliceValid(grid.GetColumn(point.X)))
				return false;

			if (!IsBoxValid(grid.GetBox(point)))
				return false;

			return true;
		}

		internal bool IsGridValid(byte?[,] grid)
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

		internal bool HasGaps()
		{
			for (byte y = 0; y < 9; y++)
			{
				for (byte x = 0; x < 9; x++)
				{
					if (_grid[y, x] == null)
						return false;
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

		internal static List<byte> GetValidValuesForPoint(byte?[,] grid, byte x, byte y)
		{
			var availableValues = new List<byte>();

			if (grid[y, x] != null)
				return availableValues;

			availableValues.AddRange(Enumerable.Range(1, 9).Select(x => (byte)x));

			foreach (byte i in grid.GetRow(x))
				availableValues.Remove(i);

			foreach (byte i in grid.GetColumn(y))
				availableValues.Remove(i);

			foreach (byte i in grid.GetBox(new Point(x, y)))
				availableValues.Remove(i);

			return availableValues;
		}

		internal bool IsSolved(byte?[,] grid)
		{
			return !HasGaps() && IsGridValid(grid);
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
			var values = GetValidValuesForPoint(grid, point.X, point.Y);

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

		public override string ToString()
		{
			var sb = new StringBuilder();

			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					var val = _grid[i, j]?.ToString() ?? "-";
					sb.Append(val);

					if (j == 2 || j == 5)
						sb.Append('┃');
				}

				sb.AppendLine();

				if (i == 2 || i == 5)
					sb.AppendLine("━━━╋━━━╋━━━");
			}

			return sb.ToString();
		}
	}
}
