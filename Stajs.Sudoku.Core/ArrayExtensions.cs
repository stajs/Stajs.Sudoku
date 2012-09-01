namespace Stajs.Sudoku.Core
{
	internal static class ArrayExtensions
	{
		private enum Dimension
		{
			Row,
			Column
		}

		internal static int[] GetRow(this int[,] source, int index)
		{
			return Slice(Dimension.Row, source, index);
		}

		internal static int[] GetColumn(this int[,] source, int index)
		{
			return Slice(Dimension.Column, source, index);
		}

		private static int[] Slice(Dimension dimension, int[,] source, int index)
		{
			var length = source.GetLength(dimension == Dimension.Row ? 0 : 1);
			var ret = new int[length];

			for (int i = 0; i < length; i++)
				ret[i] = dimension == Dimension.Row
					? source[index, i]
					: source[i, index];

			return ret;
		}
	}
}