using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
   [SerializeField] private Image itemImage;
   [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;


   

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;
        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.data.icon;


            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item.data.ItemType == ItemType.Equipment) {
            Inventory.Instance.EquipItem(item.data);
        }

    }

    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }
}
