using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int dropAmount;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData[] possibleDrop;
    [SerializeField] private List<ItemData> dropList = new List<ItemData>();
    public void GenerateDrop()
    {
        dropAmount = Random.Range(1, 5);
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if(Random.Range(0,100) <= possibleDrop[i].DropChance)
                dropList.Add(possibleDrop[i]);
        }

        for (int i = 1; i < dropAmount; i++)
        {
            if (dropList.Count == 0) return;
            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];
            dropList.Remove(randomItem);
            DropItem(randomItem);
        }
    }

    public void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));
        newDrop.GetComponent<ItemObj>().SetUpItem(_itemData, randomVelocity);
    }
}
