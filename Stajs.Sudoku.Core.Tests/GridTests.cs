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
		#region grid variables

		private int[,] _emptyGrid = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		private int[,] _unsolvedGrid = new[,]
		{
			{ 5, 3, 0, 0, 7, 0, 0, 0, 0 },
			{ 6, 0, 0, 1, 9, 5, 0, 0, 0 },
			{ 0, 9, 8, 0, 0, 0, 0, 6, 0 },
			{ 8, 0, 0, 0, 6, 0, 0, 0, 3 },
			{ 4, 0, 0, 8, 0, 3, 0, 0, 1 },
			{ 7, 0, 0, 0, 2, 0, 0, 0, 6 },
			{ 0, 6, 0, 0, 0, 0, 2, 8, 0 },
			{ 0, 0, 0, 4, 1, 9, 0, 0, 5 },
			{ 0, 0, 0, 0, 8, 0, 0, 7, 9 }
		};

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

		#endregion

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

		#region GetQuadrant

		private int[,] _expectedQuadrant = new[,]
		{
			{ 1, 2, 3 },
			{ 4, 5, 6 },
			{ 7, 8, 9 }
		};

		#region GetQuadrantForPoint

		#region TopLeft

		private static int[,] _arrayTopLeft = new[,]
		{
			{ 1, 2, 3, 0, 0, 0, 0, 0, 0 },
			{ 4, 5, 6, 0, 0, 0, 0, 0, 0 },
			{ 7, 8, 9, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_00_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 0, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_01_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 0, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_02_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 0, 2));
		}

		[TestMethod]
		public void GetQuadrantForPoint_10_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 1, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_11_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 1, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_12_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 1, 2));
		}

		[TestMethod]
		public void GetQuadrantForPoint_20_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 2, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_21_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 2, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_22_ReturnsTopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopLeft, 2, 2));
		}

		#endregion

		#region TopCenter

		private static int[,] _arrayTopCenter = new[,]
		{
			{ 0, 0, 0, 1, 2, 3, 0, 0, 0 },
			{ 0, 0, 0, 4, 5, 6, 0, 0, 0 },
			{ 0, 0, 0, 7, 8, 9, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_03_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 0, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_04_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 0, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_05_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 0, 5));
		}

		[TestMethod]
		public void GetQuadrantForPoint_13_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 1, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_14_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 1, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_15_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 1, 5));
		}

		[TestMethod]
		public void GetQuadrantForPoint_23_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 2, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_24_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 2, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_25_ReturnsTopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopCenter, 2, 5));
		}

		#endregion

		#region TopRight

		private static int[,] _arrayTopRight = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 1, 2, 3 },
			{ 0, 0, 0, 0, 0, 0, 4, 5, 6 },
			{ 0, 0, 0, 0, 0, 0, 7, 8, 9 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_06_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 0, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_07_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 0, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_08_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 0, 8));
		}

		[TestMethod]
		public void GetQuadrantForPoint_16_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 1, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_17_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 1, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_18_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 1, 8));
		}

		[TestMethod]
		public void GetQuadrantForPoint_26_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 2, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_27_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 2, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_28_ReturnsTopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayTopRight, 2, 8));
		}

		#endregion

		#region CenterLeft

		private static int[,] _arrayCenterLeft = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 1, 2, 3, 0, 0, 0, 0, 0, 0 },
			{ 4, 5, 6, 0, 0, 0, 0, 0, 0 },
			{ 7, 8, 9, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_30_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 3, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_31_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 3, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_32_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 3, 2));
		}

		[TestMethod]
		public void GetQuadrantForPoint_40_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 4, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_41_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 4, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_42_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 4, 2));
		}

		[TestMethod]
		public void GetQuadrantForPoint_50_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 5, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_51_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 5, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_52_ReturnsCenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterLeft, 5, 2));
		}

		#endregion

		#region CenterCenter

		private static int[,] _arrayCenterCenter = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 1, 2, 3, 0, 0, 0 },
			{ 0, 0, 0, 4, 5, 6, 0, 0, 0 },
			{ 0, 0, 0, 7, 8, 9, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_33_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 3, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_34_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 3, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_35_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 3, 5));
		}

		[TestMethod]
		public void GetQuadrantForPoint_43_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 4, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_44_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 4, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_45_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 4, 5));
		}

		[TestMethod]
		public void GetQuadrantForPoint_53_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 5, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_54_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 5, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_55_ReturnsCenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterCenter, 5, 5));
		}

		#endregion

		#region CenterRight

		private static int[,] _arrayCenterRight = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 1, 2, 3 },
			{ 0, 0, 0, 0, 0, 0, 4, 5, 6 },
			{ 0, 0, 0, 0, 0, 0, 7, 8, 9 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_36_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 3, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_37_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 3, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_38_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 3, 8));
		}

		[TestMethod]
		public void GetQuadrantForPoint_46_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 4, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_47_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 4, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_48_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 4, 8));
		}

		[TestMethod]
		public void GetQuadrantForPoint_56_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 5, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_57_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 5, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_58_ReturnsCenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayCenterRight, 5, 8));
		}

		#endregion

		#region BottomLeft

		private static int[,] _arrayBottomLeft = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 1, 2, 3, 0, 0, 0, 0, 0, 0 },
			{ 4, 5, 6, 0, 0, 0, 0, 0, 0 },
			{ 7, 8, 9, 0, 0, 0, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_60_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 6, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_61_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 6, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_62_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 6, 2));
		}

		[TestMethod]
		public void GetQuadrantForPoint_70_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 7, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_71_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 7, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_72_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 7, 2));
		}

		[TestMethod]
		public void GetQuadrantForPoint_80_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 8, 0));
		}

		[TestMethod]
		public void GetQuadrantForPoint_81_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 8, 1));
		}

		[TestMethod]
		public void GetQuadrantForPoint_82_ReturnsBottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomLeft, 8, 2));
		}

		#endregion

		#region BottomCenter

		private static int[,] _arrayBottomCenter = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 1, 2, 3, 0, 0, 0 },
			{ 0, 0, 0, 4, 5, 6, 0, 0, 0 },
			{ 0, 0, 0, 7, 8, 9, 0, 0, 0 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_63_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 6, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_64_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 6, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_65_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 6, 5));
		}

		[TestMethod]
		public void GetQuadrantForPoint_73_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 7, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_74_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 7, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_75_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 7, 5));
		}

		[TestMethod]
		public void GetQuadrantForPoint_83_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 8, 3));
		}

		[TestMethod]
		public void GetQuadrantForPoint_84_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 8, 4));
		}

		[TestMethod]
		public void GetQuadrantForPoint_85_ReturnsBottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomCenter, 8, 5));
		}

		#endregion

		#region BottomRight

		private static int[,] _arrayBottomRight = new[,]
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 1, 2, 3 },
			{ 0, 0, 0, 0, 0, 0, 4, 5, 6 },
			{ 0, 0, 0, 0, 0, 0, 7, 8, 9 }
		};

		[TestMethod]
		public void GetQuadrantForPoint_66_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 6, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_67_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 6, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_68_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 6, 8));
		}

		[TestMethod]
		public void GetQuadrantForPoint_76_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 7, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_77_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 7, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_78_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 7, 8));
		}

		[TestMethod]
		public void GetQuadrantForPoint_86_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 8, 6));
		}

		[TestMethod]
		public void GetQuadrantForPoint_87_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 8, 7));
		}

		[TestMethod]
		public void GetQuadrantForPoint_88_ReturnsBottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrantForPoint(_arrayBottomRight, 8, 8));
		}

		#endregion

		#endregion

		#region GetQuadrant

		[TestMethod]
		public void GetQuadrant_TopLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayTopLeft, Quadrant.TopLeft));
		}

		[TestMethod]
		public void GetQuadrant_TopCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayTopCenter, Quadrant.TopCenter));
		}

		[TestMethod]
		public void GetQuadrant_TopRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayTopRight, Quadrant.TopRight));
		}

		[TestMethod]
		public void GetQuadrant_CenterLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayCenterLeft, Quadrant.CenterLeft));
		}

		[TestMethod]
		public void GetQuadrant_CenterCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayCenterCenter, Quadrant.CenterCenter));
		}

		[TestMethod]
		public void GetQuadrant_CenterRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayCenterRight, Quadrant.CenterRight));
		}

		[TestMethod]
		public void GetQuadrant_BottomLeft()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayBottomLeft, Quadrant.BottomLeft));
		}

		[TestMethod]
		public void GetQuadrant_BottomCenter()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayBottomCenter, Quadrant.BottomCenter));
		}

		[TestMethod]
		public void GetQuadrant_BottomRight()
		{
			CollectionAssert.AreEqual(_expectedQuadrant, Grid.GetQuadrant(_arrayBottomRight, Quadrant.BottomRight));
		}

		#endregion

		#endregion

		#region IsSliceValid

		[TestMethod]
		[ExpectedException(typeof(ValueOutOfRangeException))]
		public void IsSliceValid_ValuesAreLessThanZero_ThrowsValueOutOfRangeException()
		{
			int[] array = { 0, -1, 0, 0, 0, 0, 0, 0, 0 };
			Grid.IsSliceValid(array);
		}

		[TestMethod]
		[ExpectedException(typeof(ValueOutOfRangeException))]
		public void IsSliceValid_ValuesAreGreaterThanNine_ThrowsValueOutOfRangeException()
		{
			int[] array = { 0, 10, 0, 0, 0, 0, 0, 0, 0 };
			Grid.IsSliceValid(array);
		}

		[TestMethod]
		public void IsSliceValid_ValuesAreRepeated_ReturnsFalse()
		{
			int[] array = { 0, 7, 0, 0, 0, 0, 0, 0, 7 };
			Assert.IsFalse(Grid.IsSliceValid(array));
		}

		[TestMethod]
		public void IsSliceValid_ValuesAreZero_ReturnsTrue()
		{
			int[] array = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			Assert.IsTrue(Grid.IsSliceValid(array));
		}

		[TestMethod]
		public void IsSliceValid_ValuesAreValid_ReturnsTrue()
		{
			int[] array = { 1, 0, 2, 3, 4, 5, 0, 7, 0 };
			Assert.IsTrue(Grid.IsSliceValid(array));
		}

		#endregion

		#region IsQuadrantValid

		[TestMethod]
		[ExpectedException(typeof(ValueOutOfRangeException))]
		public void IsQuadrantValid_ValuesAreLessThanZero_ThrowsValueOutOfRangeException()
		{
			var quadrant = new[,]
			{
				{ 0, 2, 3 },
				{ 4, 5, 6 },
				{ 7, -1, 9 }
			};

			Grid.IsQuadrantValid(quadrant);
		}

		[TestMethod]
		[ExpectedException(typeof(ValueOutOfRangeException))]
		public void IsQuadrantValid_ValuesAreGreaterThanNine_ThrowsValueOutOfRangeException()
		{
			var quadrant = new[,]
			{
				{ 0, 2, 3 },
				{ 4, 5, 6 },
				{ 7, 10, 9 }
			};

			Grid.IsQuadrantValid(quadrant);
		}

		[TestMethod]
		public void IsQuadrantValid_ValuesAreRepeated_ReturnsFalse()
		{
			var quadrant = new[,]
			{
				{ 9, 2, 3 },
				{ 4, 5, 6 },
				{ 7, 8, 9 }
			};

			Assert.IsFalse(Grid.IsQuadrantValid(quadrant));
		}

		[TestMethod]
		public void IsQuadrantValid_ValuesAreZero_ReturnsTrue()
		{
			var quadrant = new[,]
			{
				{ 0, 0, 0 },
				{ 0, 0, 0 },
				{ 0, 0, 0 }
			};

			Assert.IsTrue(Grid.IsQuadrantValid(quadrant));
		}

		[TestMethod]
		public void IsQuadrantValid_ValuesAreValid_ReturnsTrue()
		{
			var quadrant = new[,]
			{
				{ 0, 2, 3 },
				{ 4, 0, 6 },
				{ 7, 8, 0 }
			};

			Assert.IsTrue(Grid.IsQuadrantValid(quadrant));
		}

		#endregion

		#region IsPointValid

		[TestMethod]
		public void IsPointValid_RepeatedValueInRow_ReturnsFalse()
		{
			var values = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 3, 4, 5, 6, 7, 7, 9 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			Assert.IsFalse(Grid.IsPointValid(values, 2, 3));
		}

		[TestMethod]
		public void IsPointValid_ValidRow_ReturnsTrue()
		{
			var values = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 3, 4, 5, 6, 7, 0, 9 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			Assert.IsTrue(Grid.IsPointValid(values, 2, 3));
		}

		[TestMethod]
		public void IsPointValid_RepeatedValueInColumn_ReturnsFalse()
		{
			var values = new[,]
			{
				{ 0, 0, 0, 1, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 2, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 3, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 4, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 5, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 6, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 7, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 7, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 9, 0, 0, 0, 0, 0 }
			};

			Assert.IsFalse(Grid.IsPointValid(values, 2, 3));
		}

		[TestMethod]
		public void IsPointValid_ValidColumn_ReturnsTrue()
		{
			var values = new[,]
			{
				{ 0, 0, 0, 1, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 2, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 3, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 4, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 5, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 6, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 7, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 9, 0, 0, 0, 0, 0 }
			};

			Assert.IsTrue(Grid.IsPointValid(values, 2, 3));
		}

		[TestMethod]
		public void IsPointValid_RepeatedValueInQuadrant_ReturnsFalse()
		{
			var values = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 9, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 9, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			Assert.IsFalse(Grid.IsPointValid(values, 4, 4));
		}

		[TestMethod]
		public void IsPointValid_ValidQuadrant_ReturnsTrue()
		{
			var values = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 9, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 8, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			Assert.IsTrue(Grid.IsPointValid(values, 4, 4));
		}

		#endregion
		
		#region GetValidValuesForPoint

		[TestMethod]
		public void GetValidValuesForPoint_PointHasValue_ReturnsEmptyList()
		{
			var grid = _emptyGrid;
			grid[4, 5] = 3;

			var values = Grid.GetValidValuesForPoint(grid, 4, 5);

			Assert.IsFalse(values.Any());
		}

		[TestMethod]
		public void GetValidValuesForPoint_RowHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 3, 4, 5, 0, 7, 8, 9 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 1, 5);
			var expected = new List<int> { 6 };

			CollectionAssert.AreEqual(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_RowHasThreeGaps_ReturnsThreeValues()
		{
			var grid = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 0, 3, 4, 5, 0, 7, 8, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 1, 5);
			var expected = new List<int> { 6, 2, 9 };

			CollectionAssert.AreEquivalent(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_ColumnHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 1, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 2, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 4, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 5, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 6, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 8, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 9, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 2, 1);
			var expected = new List<int> { 3 };

			CollectionAssert.AreEqual(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_ColumnHasThreeGaps_ReturnsThreeValues()
		{
			var grid = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 2, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 4, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 5, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 6, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 9, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 2, 1);
			var expected = new List<int> { 3, 1, 8 };

			CollectionAssert.AreEquivalent(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_RowAndColumnHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 1, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 0, 3, 4, 5, 6, 7, 8, 9 },
				{ 0, 3, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 4, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 5, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 6, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 8, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 9, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 1, 1);
			var expected = new List<int> { 2 };

			CollectionAssert.AreEqual(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_RowHasOneGapAndColumnHasFourGaps_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 1, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 0, 3, 4, 5, 6, 7, 8, 9 },
				{ 0, 3, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 4, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 9, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 1, 1);
			var expected = new List<int> { 2 };

			CollectionAssert.AreEqual(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_RowHasFourGapAsndColumnHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 1, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 0, 0, 4, 0, 6, 7, 8, 0 },
				{ 0, 3, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 4, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 5, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 6, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 8, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 9, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 1, 1);
			var expected = new List<int> { 2 };

			CollectionAssert.AreEqual(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_QuadrantHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 3, 0, 0, 0, 0, 0, 0 },
				{ 4, 0, 6, 0, 0, 0, 0, 0, 0 },
				{ 7, 8, 9, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 4, 1);
			var expected = new List<int> { 5 };

			CollectionAssert.AreEqual(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_QuadrantHasThreeGaps_ReturnsThreeValues()
		{
			var grid = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 0, 0, 0, 0, 0, 0, 0 },
				{ 4, 0, 6, 0, 0, 0, 0, 0, 0 },
				{ 0, 8, 9, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 4, 1);
			var expected = new List<int> { 3, 7, 5 };

			CollectionAssert.AreEquivalent(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_QuadrantHasThreeGapsAndRowHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 0, 0, 0, 0, 0, 0, 0 },
				{ 4, 0, 6, 7, 8, 9, 1, 2, 3 },
				{ 0, 8, 9, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 4, 1);
			var expected = new List<int> { 5 };

			CollectionAssert.AreEquivalent(expected, values);
		}

		[TestMethod]
		public void GetValidValuesForPoint_QuadrantHasThreeGapsAndColumnHasOneGap_ReturnsOneValue()
		{
			var grid = new[,]
			{
				{ 0, 1, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 3, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 4, 0, 0, 0, 0, 0, 0, 0 },
				{ 1, 2, 0, 0, 0, 0, 0, 0, 0 },
				{ 4, 0, 6, 0, 0, 0, 0, 0, 0 },
				{ 0, 8, 9, 0, 0, 0, 0, 0, 0 },
				{ 0, 6, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 9, 0, 0, 0, 0, 0, 0, 0 }
			};

			var values = Grid.GetValidValuesForPoint(grid, 4, 1);
			var expected = new List<int> { 5 };

			CollectionAssert.AreEquivalent(expected, values);
		}

		#endregion

		#region HasGaps

		[TestMethod]
		public void HasGaps_EmptyGrid_ReturnsTrue()
		{
			Assert.IsTrue(Grid.HasGaps(_emptyGrid));
		}

		[TestMethod]
		public void HasGaps_UnsolvedGrid_ReturnsTrue()
		{
			Assert.IsTrue(Grid.HasGaps(_unsolvedGrid));
		}

		[TestMethod]
		public void HasGaps_SolvedGrid_ReturnsFalse()
		{
			Assert.IsFalse(Grid.HasGaps(_solvedGrid));
		}

		#endregion

		#region IsSolved

		[TestMethod]
		public void IsSolved_EmptyGrid_ReturnsFalse()
		{
			Assert.IsFalse(Grid.IsSolved(_emptyGrid));
		}

		[TestMethod]
		public void IsSolved_UnsolvedGrid_ReturnsFalse()
		{
			Assert.IsFalse(Grid.IsSolved(_unsolvedGrid));
		}

		[TestMethod]
		public void IsSolved_SolvedGrid_ReturnsTrue()
		{
			Assert.IsTrue(Grid.IsSolved(_solvedGrid));
		}

		#endregion

		#region GetEmptyPoints

		[TestMethod]
		public void GetEmptyPoints_EmptyGrid_Returns81()
		{
			var points = Grid.GetEmptyPoints(_emptyGrid).ToList();

			Assert.AreEqual(81, points.Count());
			Assert.IsNotNull(points.Single(p => p.X == 0 && p.Y == 0));
			Assert.IsNotNull(points.Single(p => p.X == 2 && p.Y == 6));
			Assert.IsNotNull(points.Single(p => p.X == 8 && p.Y == 8));
		}

		[TestMethod]
		public void GetEmptyPoints_GridWithTwoGaps_Returns2()
		{
			var grid = _solvedGrid;
			grid[7, 4] = 0;
			grid[3, 1] = 0;

			var points = Grid.GetEmptyPoints(grid).ToList();

			Assert.AreEqual(2, points.Count());
			Assert.IsNotNull(points.Single(p => p.X == 7 && p.Y == 4));
			Assert.IsNotNull(points.Single(p => p.X == 3 && p.Y == 1));
		}

		[TestMethod]
		public void GetEmptyPoints_SolvedGrid_Returns0()
		{
			var points = Grid.GetEmptyPoints(_solvedGrid).ToList();

			Assert.AreEqual(0, points.Count());
		}


		#endregion
	}
}