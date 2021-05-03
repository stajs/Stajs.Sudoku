using System;
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

		internal Box GetBox(Point p) => p switch
		{
			Point { X: <= 2, Y: <= 2} => Box.TopLeft,
			Point { X: <= 5, Y: <= 2} => Box.TopCenter,
			Point { X: <= 8, Y: <= 2} => Box.TopRight,
			Point { X: <= 2, Y: <= 5} => Box.CenterLeft,
			Point { X: <= 5, Y: <= 5} => Box.CenterCenter,
			Point { X: <= 8, Y: <= 5} => Box.CenterRight,
			Point { X: <= 2, Y: <= 8} => Box.BottomLeft,
			Point { X: <= 5, Y: <= 8} => Box.BottomCenter,
			Point { X: <= 8, Y: <= 8} => Box.BottomRight,
			_ => throw new ArgumentOutOfRangeException(nameof(p), $"Can't find Box for Point({p.X}, {p.Y})")
		};

		internal Point GetBoxStart(Box b) => b switch
		{
			Box.TopLeft => new Point(0, 0),
			Box.TopCenter => new Point(3, 0),
			Box.TopRight => new Point(6, 0),
			Box.CenterLeft => new Point(0, 3),
			Box.CenterCenter => new Point(3, 3),
			Box.CenterRight => new Point(6, 3),
			Box.BottomLeft => new Point(0, 6),
			Box.BottomCenter => new Point(3, 6),
			Box.BottomRight => new Point(6, 6),
			_ => throw new ArgumentOutOfRangeException(nameof(b), $"Can't find Box {b}")
		};

		internal byte?[,] GetBoxValues(Point point)
		{
			var box = GetBox(point);
			return GetBoxValues(box);
		}
		
		internal byte?[,] GetBoxValues(Box box)
		{
			var start = GetBoxStart(box);
			var ret = new byte?[3, 3];

			for (var i = 0; i < 3; i++)
			{
				var col = start.X + i;
				for (var j = 0; j < 3; j++)
				{
					var row = start.Y + j;
					ret[j, i] = _grid[row, col];
				}
			}

			return ret;
		}

		internal byte?[] GetRow(Point p)
		{
			var ret = new byte?[9];
			
			for (int i = 0; i < 9; i++)
			{
				ret[i] = _grid[p.Y, i];
			}

			return ret;
		}

		internal byte?[] GetColumn(Point p)
		{
			var ret = new byte?[9];
			
			for (int i = 0; i < 9; i++)
			{
				ret[i] = _grid[i, p.X];
			}

			return ret;
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
