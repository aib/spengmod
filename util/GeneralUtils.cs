using System.Collections.Generic;
using System;

public static class GeneralUtils {

public static
List<T> getList<T>(Action<List<T>> f) { var l = new List<T>(); f(l); return l; }

}
