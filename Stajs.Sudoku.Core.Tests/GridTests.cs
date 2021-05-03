// using System;
// using FluentAssertions;
// using Xunit;

// namespace Stajs.Sudoku.Core.Tests
// {
// 	public class GridTests
// 	{
// 		private static readonly byte?[,] _grid = new byte?[,]
// 		{
// 			{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
// 			{ 10, 11, 12, 13, 14, 15, 16, 17, 18 },
// 			{ 19, 20, 21, 22, 23, 24, 25, 26, 27 },
// 			{ 28, 29, 30, 31, 32, 33, 34, 35, 36 },
// 			{ 37, 38, 39, 40, 41, 42, 43, 44, 45 },
// 			{ 46, 47, 48, 49, 50, 51, 52, 53, 54 },
// 			{ 55, 56, 57, 58, 59, 60, 61, 62, 63 },
// 			{ 64, 65, 66, 67, 68, 69, 70, 71, 72 },
// 			{ 73, 74, 75, 76, 77, 78, 79, 80, 81 }
// 		};

// 		#region IsGridValid
// 		[Fact]
// 		public void IsGridValid_SolvedGrid()
// 		{
// 			var grid = new Grid(Wat.SolvedGrid);
// 			var isValid = grid.IsGridValid();

// 			isValid.Should().BeTrue();
// 		}

// 		[Fact]
// 		public void IsGridValid_UnsolvedGrid()
// 		{
// 			var grid = new Grid(Wat.UnsolvedGrid);
// 			var isValid = grid.IsGridValid();

// 			isValid.Should().BeTrue();
// 		}	
// 		#endregion

// 		#region GetRow
// 		[Fact]
// 		public void GetRow()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetRow(new Point(3, 4));

// 			var expected = new byte?[9] { 37, 38, 39, 40, 41, 42, 43, 44, 45 };

// 			values.Should().Equal(expected);
// 		}
// 		#endregion

// 		#region GetColumn
// 		[Fact]
// 		public void GetColumn()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetColumn(new Point(3, 4));

// 			var expected = new byte?[9] { 4, 13, 22, 31, 40, 49, 58, 67, 76, };

// 			values.Should().Equal(expected);
// 		}
// 		#endregion

// 		#region GetBoxValues
// 		[Fact]
// 		public void GetBoxValues_TopLeft()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.TopLeft);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 1, 2, 3},
// 				{ 10, 11, 12},
// 				{ 19, 20, 21}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_TopCenter()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.TopCenter);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 4, 5, 6},
// 				{ 13, 14, 15},
// 				{ 22, 23, 24}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_TopRight()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.TopRight);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 7, 8, 9},
// 				{ 16, 17, 18},
// 				{ 25, 26, 27}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_CenterLeft()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.CenterLeft);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 28, 29, 30},
// 				{ 37, 38, 39},
// 				{ 46, 47, 48}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_CenterCenter()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.CenterCenter);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 31, 32, 33},
// 				{ 40, 41, 42},
// 				{ 49, 50, 51}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_CenterRight()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.CenterRight);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 34, 35, 36},
// 				{ 43, 44, 45},
// 				{ 52, 53, 54}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_BottomLeft()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.BottomLeft);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 55, 56, 57},
// 				{ 64, 65, 66},
// 				{ 73, 74, 75}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_BottomCenter()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.BottomCenter);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 58, 59, 60},
// 				{ 67, 68, 69},
// 				{ 76, 77, 78}
// 			};

// 			values.Should().Equal(expected);
// 		}
		
// 		[Fact]
// 		public void GetBoxValues_BottomRight()
// 		{
// 			var grid = new Grid(_grid);
// 			var values = grid.GetBoxValues(Box.BottomRight);

// 			var expected = new byte?[3,3]
// 			{
// 				{ 61, 62, 63},
// 				{ 70, 71, 72},
// 				{ 79, 80, 81}
// 			};

// 			values.Should().Equal(expected);
// 		}
// 		#endregion

// 		#region GetBox
// 		[Theory]
// 		[InlineData(0, 0)]
// 		[InlineData(0, 1)]
// 		[InlineData(0, 2)]
// 		[InlineData(1, 0)]
// 		[InlineData(1, 1)]
// 		[InlineData(1, 2)]
// 		[InlineData(2, 0)]
// 		[InlineData(2, 1)]
// 		[InlineData(2, 2)]
// 		public void GetBox_TopLeft(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.TopLeft);
// 		}

// 		[Theory]
// 		[InlineData(3, 0)]
// 		[InlineData(3, 1)]
// 		[InlineData(3, 2)]
// 		[InlineData(4, 0)]
// 		[InlineData(4, 1)]
// 		[InlineData(4, 2)]
// 		[InlineData(5, 0)]
// 		[InlineData(5, 1)]
// 		[InlineData(5, 2)]
// 		public void GetBox_TopCenter(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.TopCenter);
// 		}

// 		[Theory]
// 		[InlineData(6, 0)]
// 		[InlineData(6, 1)]
// 		[InlineData(6, 2)]
// 		[InlineData(7, 0)]
// 		[InlineData(7, 1)]
// 		[InlineData(7, 2)]
// 		[InlineData(8, 0)]
// 		[InlineData(8, 1)]
// 		[InlineData(8, 2)]
// 		public void GetBox_TopRight(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.TopRight);
// 		}
		
// 		[Theory]
// 		[InlineData(0, 3)]
// 		[InlineData(0, 4)]
// 		[InlineData(0, 5)]
// 		[InlineData(1, 3)]
// 		[InlineData(1, 4)]
// 		[InlineData(1, 5)]
// 		[InlineData(2, 3)]
// 		[InlineData(2, 4)]
// 		[InlineData(2, 5)]
// 		public void GetBox_CenterLeft(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.CenterLeft);
// 		}

// 		[Theory]
// 		[InlineData(3, 3)]
// 		[InlineData(3, 4)]
// 		[InlineData(3, 5)]
// 		[InlineData(4, 3)]
// 		[InlineData(4, 4)]
// 		[InlineData(4, 5)]
// 		[InlineData(5, 3)]
// 		[InlineData(5, 4)]
// 		[InlineData(5, 5)]
// 		public void GetBox_CenterCenter(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.CenterCenter);
// 		}

// 		[Theory]
// 		[InlineData(6, 3)]
// 		[InlineData(6, 4)]
// 		[InlineData(6, 5)]
// 		[InlineData(7, 3)]
// 		[InlineData(7, 4)]
// 		[InlineData(7, 5)]
// 		[InlineData(8, 3)]
// 		[InlineData(8, 4)]
// 		[InlineData(8, 5)]
// 		public void GetBox_CenterRight(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.CenterRight);
// 		}
		
// 		[Theory]
// 		[InlineData(0, 6)]
// 		[InlineData(0, 7)]
// 		[InlineData(0, 8)]
// 		[InlineData(1, 6)]
// 		[InlineData(1, 7)]
// 		[InlineData(1, 8)]
// 		[InlineData(2, 6)]
// 		[InlineData(2, 7)]
// 		[InlineData(2, 8)]
// 		public void GetBox_BottomLeft(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.BottomLeft);
// 		}

// 		[Theory]
// 		[InlineData(3, 6)]
// 		[InlineData(3, 7)]
// 		[InlineData(3, 8)]
// 		[InlineData(4, 6)]
// 		[InlineData(4, 7)]
// 		[InlineData(4, 8)]
// 		[InlineData(5, 6)]
// 		[InlineData(5, 7)]
// 		[InlineData(5, 8)]
// 		public void GetBox_BottomCenter(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.BottomCenter);
// 		}

// 		[Theory]
// 		[InlineData(6, 6)]
// 		[InlineData(6, 7)]
// 		[InlineData(6, 8)]
// 		[InlineData(7, 6)]
// 		[InlineData(7, 7)]
// 		[InlineData(7, 8)]
// 		[InlineData(8, 6)]
// 		[InlineData(8, 7)]
// 		[InlineData(8, 8)]
// 		public void GetBox_BottomRight(byte x, byte y)
// 		{
// 			var point = new Point(x, y);
// 			var grid = new Grid(_grid);
// 			var box = grid.GetBox(point);

// 			box.Should().Be(Box.BottomRight);
// 		}
// 		#endregion
// 	}
// }
