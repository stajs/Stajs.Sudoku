using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		#endregion
	}
}