using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour
{

    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;


  
    private void SetUpItemVisual()
    {
        if (itemData == null) return;
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item-" + itemData.name;
    }

    private void Update()
    {
        
    }

    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;


        rb.velocity = _velocity;
        SetUpItemVisual();

    }


    public void PickUpItem()
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
