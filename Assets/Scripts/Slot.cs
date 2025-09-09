using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public Image itemImage;
    public TextMeshProUGUI amountText;
    public SlotInfo slotInfo;

    public void SetUp(int id)
    {
        slotInfo = new SlotInfo();
        slotInfo.id = id;
        slotInfo.EmptySlot();
    }

    public void UpdateUI()
    {
        if(slotInfo.isEmpty)
        {
            itemImage.sprite = null;
            itemImage.enabled = false; 
        }
        else
        {
            itemImage.sprite = itemDatabase.FindItemInDatabase(slotInfo.itemId).itemImage;
            itemImage.enabled = true;

            if(slotInfo.amount > 1)
            {
                amountText.text = slotInfo.amount.ToString();
                amountText.gameObject.SetActive(true);
            }
            else
            {
                amountText.gameObject.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class SlotInfo
{
    public int id;
    public bool isEmpty;
    public int itemId;
    public int amount;
    public int maxAmount = 20;

    public void EmptySlot()
    {
        isEmpty = true;
        amount = 0;
        itemId = -1;
    }
}
