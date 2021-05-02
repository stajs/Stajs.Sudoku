using System;
using System.Text;

namespace Stajs.Sudoku.Core
{
	public record Point(byte X, byte Y);

	public enum BoxRow
	{
		Top, Center, Bottom
	}

	public enum BoxColumn
	{
		Left, Center, Right
	}

	public class Grid
	{
		internal byte?[,] _grid = new byte?[9, 9];

		public Grid(byte?[,] grid)
		{
			_grid = grid;
		}

		internal static BoxColumn GetBoxColumn(Point p)
		{
			if (p.X < 3)
				return BoxColumn.Left;
			else if (p.X < 6)
				return BoxColumn.Center;
			else
				return BoxColumn.Right;
		}

		internal static BoxRow GetBoxRow(Point p)
		{
			if (p.Y < 3)
				return BoxRow.Top;
			else if (p.Y < 6)
				return BoxRow.Center;
			else
				return BoxRow.Bottom;
		}

		internal static (BoxRow row, BoxColumn column) GetBox(Point p)
		{
			return (GetBoxRow(p), GetBoxColumn(p));
		}

		internal static (Point start, Point end) GetBoxPoints(Point p)
		{
			var (row, column) = GetBox(p);

			if (row == BoxRow.Top)
			{
				if (column == BoxColumn.Left)
					return (new Point(0, 0), new Point(2, 2));
				else if (column == BoxColumn.Center)
					return (new Point(0, 3), new Point(5, 2));
				else
					return (new Point(0, 6), new Point(8, 2));
			}
			else
				throw new NotImplementedException("TODO");
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
