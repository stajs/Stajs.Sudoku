using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stajs.Sudoku.Core.Tests
{
	[TestClass]
	public class ArrayExtensionsTests
	{
		private readonly int[,] _array = new[,]
		{
			{ 1, 2, 3 },
			{ 4, 5, 6 },
			{ 7, 8, 9 }
		};

		#region GetRow

		[TestMethod]
		public void GetRow()
		{
			int[] row0 = { 1, 2, 3 };
			int[] row1 = { 4, 5, 6 };
			int[] row2 = { 7, 8, 9 };

			CollectionAssert.AreEqual(row0, _array.GetRow(0));
			CollectionAssert.AreEqual(row1, _array.GetRow(1));
			CollectionAssert.AreEqual(row2, _array.GetRow(2));
		}

		#endregion

		#region GetColumn

		[TestMethod]
		public void GetColumn()
		{
			int[] col0 = { 1, 4, 7 };
			int[] col1 = { 2, 5, 8 };
			int[] col2 = { 3, 6, 9 };

			CollectionAssert.AreEqual(col0, _array.GetColumn(0));
			CollectionAssert.AreEqual(col1, _array.GetColumn(1));
			CollectionAssert.AreEqual(col2, _array.GetColumn(2));
		}

		#endregion
	}
}