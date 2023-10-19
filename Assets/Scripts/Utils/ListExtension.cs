using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static TSource GetRandom<TSource>(this IList<TSource> list) => list[Random.Range(0, list.Count)];
}
