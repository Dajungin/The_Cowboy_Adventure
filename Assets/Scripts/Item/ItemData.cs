using UnityEngine;

[System.Serializable]
public class ItemData
{
    public ItemType itemType;

    [Range(0, 100)]
    public int dropRate;

    public GameObject prefab;

    public float duration;
}