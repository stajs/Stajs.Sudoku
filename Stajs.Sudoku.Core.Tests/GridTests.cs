using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace Stajs.Sudoku.Core.Tests
{
	public class GridTests
	{
		private static readonly byte? _ = null;
		private static readonly byte?[,] _unsolvedGrid = new byte?[,]
		{
			{ 5, 3, _, _, 7, _, _, _, _ },
			{ 6, _, _, 1, 9, 5, _, _, _ },
			{ _, 9, 8, _, _, _, _, 6, _ },
			{ 8, _, _, _, 6, _, _, _, 3 },
			{ 4, _, _, 8, _, 3, _, _, 1 },
			{ 7, _, _, _, 2, _, _, _, 6 },
			{ _, 6, _, _, _, _, 2, 8, _ },
			{ _, _, _, 4, 1, 9, _, _, 5 },
			{ _, _, _, _, 8, _, _, 7, 9 }
		};

		private static readonly byte?[,] _solvedGrid = new byte?[,]
		{
			{ 5, 3, 4, 6, 7, 8, 9, 1, 2 },
			{ 6, 7, 2, 1, 9, 5, 3, 4, 8 },
			{ 1, 9, 8, 3, 4, 2, 5, 6, 7 },
			{ 8, 5, 9, 7, 6, 1, 4, 2, 3 },
			{ 4, 2, 6, 8, 5, 3, 7, 9, 1 },
			{ 7, 1, 3, 9, 2, 4, 8, 5, 6 },
			{ 9, 6, 1, 5, 3, 7, 2, 8, 4 },
			{ 2, 8, 7, 4, 1, 9, 6, 3, 5 },
			{ 3, 4, 5, 2, 8, 6, 1, 7, 9 }
		};

		private static readonly byte?[,] _invalidGrid = new byte?[,]
		{
			// 1 has been replaced with 9 invalidating column 5, row 3, box CenterCenter.
			{ 5, 3, 4, 6, 7, 8, 9, 1, 2 },
			{ 6, 7, 2, 1, 9, 5, 3, 4, 8 },
			{ 1, 9, 8, 3, 4, 2, 5, 6, 7 },
			{ 8, 5, 9, 7, 6, 9/*1*/, 4, 2, 3 },
			{ 4, 2, 6, 8, 5, 3, 7, 9, 1 },
			{ 7, 1, 3, 9, 2, 4, 8, 5, 6 },
			{ 9, 6, 1, 5, 3, 7, 2, 8, 4 },
			{ 2, 8, 7, 4, 1, 9, 6, 3, 5 },
			{ 3, 4, 5, 2, 8, 6, 1, 7, 9 }
		};

		#region Solve
		[Fact]
		public void Solve_SolvedGrid()
		{
			var grid = new Grid(_solvedGrid);
			var solution = grid.Solve();
			solution.Should().Equal(_solvedGrid);
		}
		
		[Fact]
		public void Solve_UnsolvedGrid()
		{
			var grid = new Grid(_unsolvedGrid);
			var solution = grid.Solve();

			Trace.WriteLine("_solvedGrid");
			Trace.WriteLine(_solvedGrid.ToStringGrid());
			Trace.WriteLine("solution");
			Trace.WriteLine(solution.ToStringGrid());

			solution.Should().Equal(_solvedGrid);
		}
		#endregion

		#region IsGridValid
		[Fact]
		public void IsGridValid_SolvedGrid()
		{
			var isValid = Grid.IsGridValid(_solvedGrid);

			isValid.Should().BeTrue();
		}

		[Fact]
		public void IsGridValid_UnsolvedGrid()
		{
			var isValid = Grid.IsGridValid(_unsolvedGrid);
			isValid.Should().BeTrue();
		}

		[Fact]
		public void IsGridValid_InvalidGrid()
		{
			var isValid = Grid.IsGridValid(_invalidGrid);
			isValid.Should().BeFalse();
		}
		#endregion

		#region IsSliceValid
		[Fact]
		public void IsSliceValid_ValidColumn()
		{
			var column = _invalidGrid.GetColumn(7);
			var isValid = Grid.IsSliceValid(column);
			isValid.Should().BeTrue();
		}

		[Fact]
		public void IsSliceValid_InvalidColumn()
		{
			var column = _invalidGrid.GetColumn(5);
			var isValid = Grid.IsSliceValid(column);
			isValid.Should().BeFalse();
		}

		[Fact]
		public void IsSliceValid_ValidRow()
		{
			var row = _invalidGrid.GetRow(7);
			var isValid = Grid.IsSliceValid(row);
			isValid.Should().BeTrue();
		}

		[Fact]
		public void IsSliceValid_InvalidRow()
		{
			var row = _invalidGrid.GetRow(3);
			var isValid = Grid.IsSliceValid(row);
			isValid.Should().BeFalse();
		}
		#endregion

		#region IsBoxValid
		[Fact]
		public void IsBoxValid_ValidBox()
		{
			var box = _invalidGrid.GetBox(Box.BottomCenter);
			var isValid = Grid.IsBoxValid(box);
			isValid.Should().BeTrue();
		}

		[Fact]
		public void IsBoxValid_InvalidBox()
		{
			var box = _invalidGrid.GetBox(Box.CenterCenter);
			var isValid = Grid.IsBoxValid(box);
			isValid.Should().BeFalse();
		}
		#endregion
	}
}
