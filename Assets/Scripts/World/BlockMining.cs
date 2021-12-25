using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMining : MonoBehaviour
{
    // Убрать метод Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Mining();
        }
    }
    public static void Mining() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)(pos.x + 1.5)/3;
        x *= 3;
        int y = (int)(pos.y + 1.5) / 3;
        y *= 3;
        GameObject player = GameObject.Find("Player");
        Inventory inv = GameObject.Find("Main Camera").GetComponent<Inventory>();
        float dx = pos.x - player.transform.position.x;
        float dy = pos.y - player.transform.position.y;
        if (Math.Abs(dx) < 10 && dy > -5.5 && dy < 12)
        {
            GameObject block = GameObject.Find("Block" + x + "X" + y);
            if (block != null)
            {
                int id = block.GetComponent<BlockInformation>().ID;
                LootItem.CreateLootItem(block.transform, id, 1, player, inv, inv.data, inv.world, inv.lootItem);
                Destroy(block);
                LoadWorld myWorld = GameObject.Find("World").GetComponent<LoadWorld>();
                myWorld.world[x / 3, y / 3] = 0;
            }
        }
    }
}
