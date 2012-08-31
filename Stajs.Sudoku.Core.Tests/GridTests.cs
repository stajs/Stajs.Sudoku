using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stajs.Sudoku.Core.Exceptions;

namespace Stajs.Sudoku.Core.Tests
{
	[TestClass]
	public class GridTests
	{
		private int[,] _solvedGrid = new[,]
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

		#region Constructor

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Ctor_Null_ThrowsArgumentNullException()
		{
			new Grid(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayLengthException))]
		public void Ctor_0x9_ThrowsArrayLengthException()
		{
			new Grid(new int[0,9]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayLengthException))]
		public void Ctor_9x0_ThrowsArrayLengthException()
		{
			new Grid(new int[9,0]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayLengthException))]
		public void Ctor_0x0_ThrowsArrayLengthException()
		{
			new Grid(new int[0,0]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayLengthException))]
		public void Ctor_10x9_ThrowsArrayLengthException()
		{
			new Grid(new int[10,9]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayLengthException))]
		public void Ctor_9x10_ThrowsArrayLengthException()
		{
			new Grid(new int[9,10]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayLengthException))]
		public void Ctor_10x10_ThrowsArrayLengthException()
		{
			new Grid(new int[10,10]);
		}

		private int GetValueOutOfRangeExceptionCountForValue(int value)
		{
			var exceptionCount = 0;

			for (var i = 0; i < 9; i++)
			{
				for (var j = 0; j < 9; j++)
				{
					var array = new int[9,9];
					array[i, j] = value;

					try
					{
						new Grid(array);
					}
					catch (ValueOutOfRangeException)
					{
						exceptionCount++;
					}
				}
			}

			return exceptionCount;
		}
		
		[TestMethod]
		public void Ctor_ValuesBelowZero_ThrowsValueOutOfRangeException()
		{
			var expectedExceptionCount = 81; // 9x9
			var exceptionCount = GetValueOutOfRangeExceptionCountForValue(-1);

			Assert.AreEqual(expectedExceptionCount, exceptionCount);
		}
		
		[TestMethod]
		public void Ctor_ValuesAboveNine_ThrowsValueOutOfRangeException()
		{
			var expectedExceptionCount = 81; // 9x9
			var exceptionCount = GetValueOutOfRangeExceptionCountForValue(10);

			Assert.AreEqual(expectedExceptionCount, exceptionCount);
		}

		[TestMethod]
		public void Ctor_ValidValues_ValuesPropertyIsSet()
		{
			var grid = new Grid(_solvedGrid);
			CollectionAssert.AreEqual(_solvedGrid, grid.Values);
		}

		#endregion
	}
}