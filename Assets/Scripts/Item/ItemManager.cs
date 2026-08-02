using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        Instance = this;
    }

    public void DropItem(Vector2 pos)
    {
        int totalWeight = 0;

        foreach (ItemData item in items)
        {
            totalWeight += item.dropRate;
        }

        if (totalWeight == 0)
            return;

        int random = Random.Range(0, totalWeight);

        foreach (ItemData item in items)
        {
            if (random < item.dropRate)
            {
                Instantiate(item.prefab, pos, Quaternion.identity);
                return;
            }

            random -= item.dropRate;
        }
    }
}