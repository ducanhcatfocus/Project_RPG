using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Inventory instance;

    public static Inventory Instance  => instance;
    public List<ItemData> startingItem = new List<ItemData>();
    public ItemData flask;

    public GameObject itemSlotPrefab;


    public List<InventoryItem> inventoryItems;
    public List<InventoryItem> stashItems;
    public List<InventoryItem> equipment;


    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    public Dictionary<ItemData, InventoryItem> stashDictionary;
    public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;



    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;


    private ItemSlotUI[] inventoryItemSlot;
    private ItemSlotUI[] stashItemSlot;
    private EquipmentSlotUI[] equipmentSlot;


   
    private float lastTimeUseFlask;
    public float flaskDefautlCd;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        stashItems = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<ItemSlotUI>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<EquipmentSlotUI>();

        AddStartingItem();

    }

    private void AddStartingItem()
    {
        for (int i = 0; i < startingItem.Count; i++)
        {
            AddItem(startingItem[i]);
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemDataEquipment newEquipment = _item as ItemDataEquipment;
        InventoryItem newItem = new InventoryItem(newEquipment);
        ItemDataEquipment oldEquipment = null;

        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }
        if(oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifier();
        RemoveItem(_item);
        UpdateSlotUI();
    }

    public void UnequipItem(ItemDataEquipment itemTodelete)
    {
        if (equipmentDictionary.TryGetValue(itemTodelete, out InventoryItem value))
        {
            
            equipment.Remove(value);
            equipmentDictionary.Remove(itemTodelete);
            itemTodelete.RemoveModifier();
        }
    }

    private void UpdateSlotUI()
    {
   

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
         

            foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }
        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }


        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventoryItems[i]);
        }

        for (int i = 0; i < stashItems.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stashItems[i]);
        }
    }
    public void AddItem(ItemData item)
    {
        if(item.ItemType == ItemType.Equipment)
            AddEquipmentToInventory(item);
        else if(item.ItemType == ItemType.Material)
            AddMaterialToStash(item);
        


        UpdateSlotUI();
    }

    private void AddMaterialToStash(ItemData item)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            stashItems.Add(newItem);
            stashDictionary.Add(item, newItem);
        }
    }

    private void AddEquipmentToInventory(ItemData item)
    {
        if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            if(value.stackSize <=1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
      


        if (stashDictionary.TryGetValue(item, out InventoryItem stashValue))
        {
            if (value.stackSize <= 1)
            {
                stashItems.Remove(stashValue);
                stashDictionary.Remove(item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }
    

        UpdateSlotUI();
    }

    public ItemDataEquipment GetEquipment(EquipmentType equipmentType) {
        ItemDataEquipment equipment = null;
        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == equipmentType)
            {
                equipment = item.Key;
            }
        }

        return equipment;
    }


    public void UseFlask()
    {
        //ItemDataEquipment currentFlask = GetEquipment(EquipmentType.Flask);
        ItemDataEquipment currentFlask = flask as ItemDataEquipment;


        if (currentFlask == null) return;
        bool canUseFlask = Time.time > lastTimeUseFlask + flaskDefautlCd;

        if (canUseFlask)
        {
            flaskDefautlCd = currentFlask.itemCd;   
            currentFlask.ExeItemEffect(null);
            lastTimeUseFlask = Time.time;
        }
    }
}
