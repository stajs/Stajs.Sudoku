using System;

namespace Stajs.Sudoku.Core
{
	internal record Point(byte X, byte Y);
	
	internal static class PointExtensions
	{
		public static Box ToBox(this Point p)
		{
			return p switch
			{
				Point { X: <= 2, Y: <= 2 } => Box.TopLeft,
				Point { X: <= 5, Y: <= 2 } => Box.TopCenter,
				Point { X: <= 8, Y: <= 2 } => Box.TopRight,
				Point { X: <= 2, Y: <= 5 } => Box.CenterLeft,
				Point { X: <= 5, Y: <= 5 } => Box.CenterCenter,
				Point { X: <= 8, Y: <= 5 } => Box.CenterRight,
				Point { X: <= 2, Y: <= 8 } => Box.BottomLeft,
				Point { X: <= 5, Y: <= 8 } => Box.BottomCenter,
				Point { X: <= 8, Y: <= 8 } => Box.BottomRight,
				_ => throw new ArgumentOutOfRangeException(nameof(p), $"Can't find Box for Point({p.X}, {p.Y})")
			};
		}
	}
}
