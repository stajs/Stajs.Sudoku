﻿using System;
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

		#endregion
	}
}