using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase; //Referencia a la base de datos

    [SerializeField] private GameObject slotPrefab; //Referencia al prefab del slot

    [SerializeField]
    private Transform InventoryPanel; //Refencia al panel de inventario

    [SerializeField] private int capacity; //Capacidad del inventario

    [SerializeField]
    public List<SlotInfo> slotInfoList; //Lista con la información de todos los slot (Inventario propiamente dicho)


    private string jsonString; //Texto de formato Json que se usa para guardar el inventario

    private void Start()
    {
        slotInfoList = new List<SlotInfo>();
        LoadInventory();
    }

    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey("inventory") == true)
        {
            LoadSaveInventory();
        }
        else
        { 
            LoadEmptyInventory();
        }
    }

    private void LoadEmptyInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, InventoryPanel);
            Slot newSlot = slot.GetComponent<Slot>();
            newSlot.SetUp(i);
            newSlot.itemDatabase = itemDatabase;
            SlotInfo newSlotInfo = newSlot.slotInfo;
            slotInfoList.Add(newSlotInfo);
        }
    }

    private void LoadSaveInventory()
    {
        jsonString = PlayerPrefs.GetString("inventory");
        InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(jsonString);
        this.slotInfoList = inventoryWrapper.slotInfoList;

        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, InventoryPanel);
            Slot newSlot = slot.GetComponent<Slot>();
            newSlot.SetUp(i);
            newSlot.itemDatabase = itemDatabase;
            newSlot.slotInfo = slotInfoList[i];
            newSlot.UpdateUI();
        }
    }

    public SlotInfo FindItemInInventory(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo. itemId == itemId && !slotInfo.isEmpty)
            {
                return slotInfo;
            }
        }
        return null;
    }

    private SlotInfo FindSuitableSlot(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if(slotInfo.itemId == itemId && slotInfo.amount < slotInfo.maxAmount && !slotInfo.isEmpty && itemDatabase.FindItemInDatabase(itemId).isStackable)
            {
                return slotInfo;
            }
        }
        foreach(SlotInfo slotInfo in slotInfoList)
        {
            if(slotInfo.isEmpty)
            {
                slotInfo.EmptySlot();
                return slotInfo;
            }
        }
        return null;
    }

    private Slot FindSlot(int id)
    {
        return InventoryPanel.GetChild(id).GetComponent<Slot>();
    }

    public void AddItem(int itemId)
    {
        Item item = itemDatabase.FindItemInDatabase(itemId); //Buscar en la base de datos

        if(item != null)
        {
            SlotInfo slotInfo = FindSuitableSlot(itemId);

            if(slotInfo != null)
            {
                slotInfo.amount++;
                slotInfo.itemId = itemId;
                slotInfo.isEmpty = false;
                FindSlot(slotInfo.id).UpdateUI();
            }
        } 
    }
    
    public void AddItem(int itemId, int cantidad)
    {
        Item item = itemDatabase.FindItemInDatabase(itemId); //Buscar en la base de datos

        if (item != null)
        {
            SlotInfo slotInfo = FindSuitableSlot(itemId);

            if (slotInfo != null)
            {
                slotInfo.amount += cantidad;
                slotInfo.itemId = itemId;
                slotInfo.isEmpty = false;
                FindSlot(slotInfo.id).UpdateUI();
            }
        }
    }

    public void RemoveItem (int itemId, int cantidad)
    {
        SlotInfo slotInfo = FindItemInInventory(itemId);

        if(slotInfo != null)
        {
            if(slotInfo.amount == 1)
            {
                slotInfo.EmptySlot();
            }
            else
            {
                slotInfo.amount -= cantidad;
            }

            FindSlot(slotInfo.id).UpdateUI();
        }
    }

    public void SaveInventory()
    {
        InventoryWrapper inventoryWrapper = new InventoryWrapper();
        inventoryWrapper.slotInfoList = this.slotInfoList;
        jsonString = JsonUtility.ToJson(inventoryWrapper);
        PlayerPrefs.SetString("inventory", jsonString);
    }

    private struct InventoryWrapper
    {
        public List<SlotInfo> slotInfoList;
    }

    [ContextMenu("Test Add - itemId = 3, cantidad = 1")]
    public void TestAdd()
    {
        AddItem(3, 1);
    }

    [ContextMenu("Test Remove - itemId = 3, cantidad = 1")]
    public void TestRemove()
    {
        RemoveItem(3, 1);
    }

    [ContextMenu("Test Add - itemId = 7, cantidad = 1")]
    public void TestAdd2()
    {
        AddItem(7, 1);
    }

    [ContextMenu("Test Remove - itemId = 7, cantidad = 1")]
    public void TestRemove2()
    {
        RemoveItem(7, 1);
    }


}
