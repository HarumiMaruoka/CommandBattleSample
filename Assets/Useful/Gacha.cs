// 日本語対応
using System.Collections.Generic;
using UnityEngine;

public class Gacha
{
    private IGachaItem GetRandomGachaData(IEnumerable<IGachaItem> items, int totalWeight)
    {
        IGachaItem result = null;

        var randomValue = Random.Range(1, totalWeight + 1);

        var currentCount = 0;
        foreach (var item in items)
        {
            var old = currentCount;
            currentCount += item.Rate;

            if (randomValue > old && randomValue <= currentCount)
            {
                result = item; break;
            }
        }

        return result;
    }
}

public interface IGachaItem
{
    int Rate { get; }
}