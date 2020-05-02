using System.Collections.Generic;
using NUnit.Framework;

public class GeneralUtilsTests
{
	[Test]
	public void getListSimple()
	{
		void list123(List<int> ret) {
			ret.Clear();
			ret.Add(1);
			ret.Add(2);
			ret.Add(3);
		}

		var l = GeneralUtils.getList<int>(list123);
		Assert.AreEqual(new List<int> { 1, 2, 3 }, l);
	}

	private class ListGetter {
		public void get123(List<string> ret, bool flag = false) {
			ret.Clear();
			ret.Add("foo");
			ret.Add("bar");
		}
	}

	[Test]
	public void getListMethodGroup()
	{
		var lg = new ListGetter();
		var l = GeneralUtils.getList<string>(r => lg.get123(r));
		Assert.AreEqual(new List<string>{ "foo", "bar" }, l);
	}
}
