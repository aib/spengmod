using System.Collections.Generic;
using System;

public static class DictionaryUtils {

public static
Dictionary<Key, Vleft> loJoin<Key, Vleft, Vright>(Func<Vleft, Vright, Vleft> f, Dictionary<Key, Vleft> left, Dictionary<Key, Vright> right) {
	Dictionary<Key, Vleft> merged = new Dictionary<Key, Vleft>();
	foreach (var lkv in left) {
		if (right.ContainsKey(lkv.Key)) {
			merged[lkv.Key] = f(lkv.Value, right[lkv.Key]);
		} else {
			merged[lkv.Key] = lkv.Value;
		}
	}
	return merged;
}

}
