using System;
using FluentAssertions;
using Xunit;

namespace Stajs.Sudoku.Core.Tests
{
	public class ArrayExtensionsTests
	{
		private static readonly byte?[,] _uniqueGrid = new byte?[,]
		{
			{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
			{ 10, 11, 12, 13, 14, 15, 16, 17, 18 },
			{ 19, 20, 21, 22, 23, 24, 25, 26, 27 },
			{ 28, 29, 30, 31, 32, 33, 34, 35, 36 },
			{ 37, 38, 39, 40, 41, 42, 43, 44, 45 },
			{ 46, 47, 48, 49, 50, 51, 52, 53, 54 },
			{ 55, 56, 57, 58, 59, 60, 61, 62, 63 },
			{ 64, 65, 66, 67, 68, 69, 70, 71, 72 },
			{ 73, 74, 75, 76, 77, 78, 79, 80, 81 }
		};

		#region IsGridValid
		// [Fact]
		// public void IsGridValid_SolvedGrid()
		// {
		// 	var grid = new Grid(Wat.SolvedGrid);
		// 	var isValid = _uniqueGrid.IsGridValid();

		// 	isValid.Should().BeTrue();
		// }

		// [Fact]
		// public void IsGridValid_UnsolvedGrid()
		// {
		// 	var grid = new Grid(Wat.UnsolvedGrid);
		// 	var isValid = _uniqueGrid.IsGridValid();

		// 	isValid.Should().BeTrue();
		// }
		#endregion

		#region GetRow
		[Fact]
		public void GetRow()
		{
			var values = _uniqueGrid.GetRow(4);
			var expected = new byte?[9] { 37, 38, 39, 40, 41, 42, 43, 44, 45 };

			values.Should().Equal(expected);
		}
		#endregion

		#region GetColumn
		[Fact]
		public void GetColumn()
		{
			var values = _uniqueGrid.GetColumn(3);
			var expected = new byte?[9] { 4, 13, 22, 31, 40, 49, 58, 67, 76, };

			values.Should().Equal(expected);
		}
		#endregion

		#region GetBox
		[Fact]
		public void GetBox_TopLeft()
		{
			var values = _uniqueGrid.GetBox(Box.TopLeft);
			var expected = new byte?[3,3]
			{
				{ 1, 2, 3},
				{ 10, 11, 12},
				{ 19, 20, 21}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_TopCenter()
		{
			var values = _uniqueGrid.GetBox(Box.TopCenter);
			var expected = new byte?[3,3]
			{
				{ 4, 5, 6},
				{ 13, 14, 15},
				{ 22, 23, 24}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_TopRight()
		{
			var values = _uniqueGrid.GetBox(Box.TopRight);
			var expected = new byte?[3,3]
			{
				{ 7, 8, 9},
				{ 16, 17, 18},
				{ 25, 26, 27}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_CenterLeft()
		{
			var values = _uniqueGrid.GetBox(Box.CenterLeft);
			var expected = new byte?[3,3]
			{
				{ 28, 29, 30},
				{ 37, 38, 39},
				{ 46, 47, 48}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_CenterCenter()
		{
			var values = _uniqueGrid.GetBox(Box.CenterCenter);
			var expected = new byte?[3,3]
			{
				{ 31, 32, 33},
				{ 40, 41, 42},
				{ 49, 50, 51}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_CenterRight()
		{
			var values = _uniqueGrid.GetBox(Box.CenterRight);
			var expected = new byte?[3,3]
			{
				{ 34, 35, 36},
				{ 43, 44, 45},
				{ 52, 53, 54}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_BottomLeft()
		{
			var values = _uniqueGrid.GetBox(Box.BottomLeft);
			var expected = new byte?[3,3]
			{
				{ 55, 56, 57},
				{ 64, 65, 66},
				{ 73, 74, 75}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_BottomCenter()
		{
			var values = _uniqueGrid.GetBox(Box.BottomCenter);
			var expected = new byte?[3,3]
			{
				{ 58, 59, 60},
				{ 67, 68, 69},
				{ 76, 77, 78}
			};

			values.Should().Equal(expected);
		}
		
		[Fact]
		public void GetBox_BottomRight()
		{
			var values = _uniqueGrid.GetBox(Box.BottomRight);
			var expected = new byte?[3,3]
			{
				{ 61, 62, 63},
				{ 70, 71, 72},
				{ 79, 80, 81}
			};

			values.Should().Equal(expected);
		}
		#endregion
	}
}
