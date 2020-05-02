using System.Collections.Generic;
using NUnit.Framework;

public class DictionaryUtilsTests
{
	[Test]
	public void loJoinSimple()
	{
		var left = new Dictionary<string, int> { { "x", 1 }, { "y", 2 }, { "z", 3 } };
		var right = new Dictionary<string, int> { { "x", 10 }, { "y", 20 }, { "z", 30 } };

		var j = DictionaryUtils.loJoin((l, r) => l + r, left, right);
		CollectionAssert.AreEqual(new Dictionary<string, int> {
			{ "x", 11 }, { "y", 22 }, { "z", 33 }
		}, j);
	}

	[Test]
	public void loJoinWithMissing()
	{
		var left = new Dictionary<string, int> { { "x", 1 }, { "y", 2 } };
		var right = new Dictionary<string, int> { { "y", 20 }, { "z", 30 } };

		var j = DictionaryUtils.loJoin((l, r) => l + r, left, right);
		Assert.AreEqual(new Dictionary<string, int> {
			{ "x", 1 }, { "y", 22 }
		}, j);
	}
}
