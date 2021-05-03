using System;
using FluentAssertions;
using Xunit;

namespace Stajs.Sudoku.Core.Tests
{
	public class PointExtensionsTests
	{
		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(0, 2)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(1, 2)]
		[InlineData(2, 0)]
		[InlineData(2, 1)]
		[InlineData(2, 2)]
		public void ToBox_TopLeft(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.TopLeft);
		}

		[Theory]
		[InlineData(3, 0)]
		[InlineData(3, 1)]
		[InlineData(3, 2)]
		[InlineData(4, 0)]
		[InlineData(4, 1)]
		[InlineData(4, 2)]
		[InlineData(5, 0)]
		[InlineData(5, 1)]
		[InlineData(5, 2)]
		public void ToBox_TopCenter(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.TopCenter);
		}

		[Theory]
		[InlineData(6, 0)]
		[InlineData(6, 1)]
		[InlineData(6, 2)]
		[InlineData(7, 0)]
		[InlineData(7, 1)]
		[InlineData(7, 2)]
		[InlineData(8, 0)]
		[InlineData(8, 1)]
		[InlineData(8, 2)]
		public void ToBox_TopRight(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.TopRight);
		}
		
		[Theory]
		[InlineData(0, 3)]
		[InlineData(0, 4)]
		[InlineData(0, 5)]
		[InlineData(1, 3)]
		[InlineData(1, 4)]
		[InlineData(1, 5)]
		[InlineData(2, 3)]
		[InlineData(2, 4)]
		[InlineData(2, 5)]
		public void ToBox_CenterLeft(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.CenterLeft);
		}

		[Theory]
		[InlineData(3, 3)]
		[InlineData(3, 4)]
		[InlineData(3, 5)]
		[InlineData(4, 3)]
		[InlineData(4, 4)]
		[InlineData(4, 5)]
		[InlineData(5, 3)]
		[InlineData(5, 4)]
		[InlineData(5, 5)]
		public void ToBox_CenterCenter(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.CenterCenter);
		}

		[Theory]
		[InlineData(6, 3)]
		[InlineData(6, 4)]
		[InlineData(6, 5)]
		[InlineData(7, 3)]
		[InlineData(7, 4)]
		[InlineData(7, 5)]
		[InlineData(8, 3)]
		[InlineData(8, 4)]
		[InlineData(8, 5)]
		public void ToBox_CenterRight(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.CenterRight);
		}
		
		[Theory]
		[InlineData(0, 6)]
		[InlineData(0, 7)]
		[InlineData(0, 8)]
		[InlineData(1, 6)]
		[InlineData(1, 7)]
		[InlineData(1, 8)]
		[InlineData(2, 6)]
		[InlineData(2, 7)]
		[InlineData(2, 8)]
		public void ToBox_BottomLeft(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.BottomLeft);
		}

		[Theory]
		[InlineData(3, 6)]
		[InlineData(3, 7)]
		[InlineData(3, 8)]
		[InlineData(4, 6)]
		[InlineData(4, 7)]
		[InlineData(4, 8)]
		[InlineData(5, 6)]
		[InlineData(5, 7)]
		[InlineData(5, 8)]
		public void ToBox_BottomCenter(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.BottomCenter);
		}

		[Theory]
		[InlineData(6, 6)]
		[InlineData(6, 7)]
		[InlineData(6, 8)]
		[InlineData(7, 6)]
		[InlineData(7, 7)]
		[InlineData(7, 8)]
		[InlineData(8, 6)]
		[InlineData(8, 7)]
		[InlineData(8, 8)]
		public void ToBox_BottomRight(byte x, byte y)
		{
			new Point(x, y).ToBox().Should().Be(Box.BottomRight);
		}
	}
}
