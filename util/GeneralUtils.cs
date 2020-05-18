using System.Collections.Generic;
using System;

public static class GeneralUtils {

public static
List<T> getList<T>(Action<List<T>> f) { var l = new List<T>(); f(l); return l; }

public static
char colorChar(int r, int g, int b) { return (char)('\xe100' + (r << 6) + (g << 3) + b); }

}
