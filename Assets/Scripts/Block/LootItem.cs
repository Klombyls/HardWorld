using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    public ItemInventory item;
    public DataBase data;
    public Transform playerTransform;
    public Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.items[item.id].img;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= 1)
        {
            GivePlayerItem();
        }
        if(item.count == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void GivePlayerItem()
    {
        int id = FindThisItemAtInventoryWithoutFullMaxStack();
        if(id != -1)
        {
            AddRightAmountAtInventory(id);
            playerInventory.Update();
        } 
        else
        {
            id = FindEmptyCellAtInventory();
            if(id != -1)
            {
                AddRightAmountAtInventory(id);
                playerInventory.Update();
            }
        }
    }

    private int FindThisItemAtInventoryWithoutFullMaxStack()
    {
        int maxInventory = playerInventory.maxCount;
        for(int i = 0; i < maxInventory; i++)
        {
            if(playerInventory.items[i].count != 999 && playerInventory.items[i].id == item.id)
            {
                return i;
            }
        }
        return -1;
    }

    private int FindEmptyCellAtInventory()
    {
        int maxInventory = playerInventory.maxCount;
        for (int i = 0; i < maxInventory; i++)
        {
            if(playerInventory.items[i].id == 0)
            {
                return i;
            }
        }
        return -1;
    }

    private void AddRightAmountAtInventory(int id)
    {
        if (playerInventory.items[id].id == 0)
        {
            playerInventory.items[id].id = item.id;
            playerInventory.items[id].count = item.count;
            item.count = 0;
            return;
        }
        int summ = playerInventory.items[id].count + item.count;
        if (summ <= 999)
        {
            playerInventory.items[id].count = summ;
            item.count = 0;
        }
        else
        {
            playerInventory.items[id].count = 999;
            item.count = summ - 999;
        }
    }
}
