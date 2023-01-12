using System;
using System.Collections.Generic;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random random = new Random();

        for (var i = 0; i < list.Count; i++)
            list.Swap(i, random.Next(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        T temp = list[i];

        list[i] = list[j];
        list[j] = temp;
    }
}