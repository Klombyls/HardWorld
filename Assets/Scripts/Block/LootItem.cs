using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    public ItemInventory item;
    public DataBase data;
    public Transform playerTransform;
    public Inventory playerInventory;

    public GameObject player;
    public GameObject world;

    private void Update()
    {
        if (player.activeSelf 
            && Vector3.Distance(transform.position, playerTransform.position) <= 1)
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

    public static GameObject CreateLootItem(Transform transform, int id, int count, GameObject player, Inventory inv, DataBase data, GameObject parent, GameObject lootItem)
    {
        GameObject obj;
        obj = Instantiate(lootItem, transform.position, transform.rotation, parent.transform);
        obj.GetComponent<LootItem>().data = data;
        obj.GetComponent<LootItem>().player = player;
        obj.GetComponent<LootItem>().playerTransform = player.transform;
        obj.GetComponent<LootItem>().playerInventory = inv;
        obj.GetComponent<LootItem>().item.id = id;
        obj.GetComponent<LootItem>().item.count = count;
        obj.GetComponent<SpriteRenderer>().sprite = data.items[id].img;
        obj.GetComponent<Transform>().localScale = new Vector2(180 / (float)data.items[id].img.texture.width, 180 / (float)data.items[id].img.texture.height);
        obj.AddComponent<BoxCollider2D>();

        return obj;
    }
}
