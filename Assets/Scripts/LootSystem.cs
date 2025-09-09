using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemToSpawn
{
    public int ID;
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;
}

public class LootSystem : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public Inventory inventory;
    public Image itemImage;
    public GameObject nextButton;
    public GameObject aceptButton;
    public ItemToSpawn[] itemToSpawn;

    private int count = 0;

    void Start()
    {
        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            itemToSpawn[i].ID = itemDatabase.FindItemInDatabase(i).id;
            itemToSpawn[i].spawnRate = itemDatabase.FindItemInDatabase(i).spawnRate;
        }

        for (int i = 0; i < itemToSpawn.Length; i++)
        {

            if (i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1;
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1;
            }
        }

        Spawnner();
    }

    void Spawnner()
    {
        float randomNum = Random.Range(0, 100);

        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (randomNum >= itemToSpawn[i].minSpawnProb && randomNum <= itemToSpawn[i].maxSpawnProb)
            {
                int itemId = itemDatabase.FindItemInDatabase(itemToSpawn[i].ID).id;

                itemImage.enabled = true;

                itemImage.sprite = itemDatabase.FindItemInDatabase(itemId).itemImage;

                inventory.AddItem(itemId);

                break;
            }
        }
    }

    public void NextItem()
    {
        count++; print(count);

        if (count > 0 && count < 2)
        {
            Spawnner();
        }
        else
        {
            Spawnner();
            nextButton.SetActive(false);
            aceptButton.SetActive(true);
        }
    }
}
