using System;

namespace Stajs.Sudoku.Core
{
	internal static class ArrayExtensions
	{
		private enum Dimension
		{
			Row,
			Column
		}

		internal static byte?[] GetRow(this byte?[,] grid, byte index)
		{
			return Slice(Dimension.Row, grid, index);
		}

		internal static byte?[] GetColumn(this byte?[,] grid, byte index)
		{
			return Slice(Dimension.Column, grid, index);
		}

		private static byte?[] Slice(Dimension dimension, byte?[,] grid, byte index)
		{
			var length = grid.GetLength(dimension == Dimension.Row ? 0 : 1);
			var ret = new byte?[length];

			for (byte i = 0; i < length; i++)
				ret[i] = dimension == Dimension.Row
					? grid[index, i]
					: grid[i, index];

			return ret;
		}
		
		internal static byte?[,] GetBox(this byte?[,] grid, Point point)
		{
			return grid.GetBox(point.ToBox());
		}

		internal static byte?[,] GetBox(this byte?[,] grid, Box box)
		{
			var start = box switch
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
				_ => throw new ArgumentOutOfRangeException(nameof(box), $"Can't find Box {box}")
			};

			var ret = new byte?[3, 3];

			for (var i = 0; i < 3; i++)
			{
				var col = start.X + i;
				for (var j = 0; j < 3; j++)
				{
					var row = start.Y + j;
					ret[j, i] = grid[row, col];
				}
			}

			return ret;
		}

		internal static byte?[,] Copy(this byte?[,] source)
		{
			var width = source.GetLength(0);
			var height = source.GetLength(1);
			var ret = new byte?[width, height];

			Array.Copy(source, ret, width * height);

			return ret;
		}
	}
}