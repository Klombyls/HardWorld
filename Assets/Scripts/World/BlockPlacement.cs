using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacement : MonoBehaviour
{
    static GameObject player;
    static Inventory inv;
    static LoadWorld myWorld;
    static SpawnMonster monsters;
    // Убрать метод Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Place();
        }
    }
    public static void Place()
    {
        if (player == null)
            player = GameObject.Find("Player");
        if (inv == null)
            inv = GameObject.Find("Main Camera").GetComponent<Inventory>();
        if (myWorld == null)
            myWorld = GameObject.Find("World").GetComponent<LoadWorld>();
        if (monsters == null)
            monsters = GameObject.Find("World").GetComponent<SpawnMonster>();
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)(pos.x + 1.5) / 3;
        x *= 3;
        int y = (int)(pos.y + 1.5) / 3;
        y *= 3;
        float dx = pos.x - player.transform.position.x;
        float dy = pos.y - player.transform.position.y;
        if (myWorld.world[x / 3, y / 3] == 0 && Math.Abs(dx) < 10.5 && dy > -5.5 && dy < 15)
        {
            if (CanPlaceBlock(inv, myWorld, x, y, dx, dy))
            {
                if (inv.items[inv.currentUseID - 1].id == 1)
                {
                    GameObject block = Instantiate(Resources.Load<GameObject>("DirtBlock"), new Vector3(x, y, 1), new Quaternion());
                    block.name = "Block" + x + "X" + y;
                    myWorld.world[x / 3, y / 3] = 1;
                }
                if (inv.items[inv.currentUseID - 1].id == 2)
                {
                    GameObject block = Instantiate(Resources.Load<GameObject>("StoneBlock"), new Vector3(x, y, 1), new Quaternion());
                    block.name = "Block" + x + "X" + y;
                    myWorld.world[x / 3, y / 3] = 2;
                }
                inv.items[inv.currentUseID - 1].count--;
                if (inv.items[inv.currentUseID - 1].count == 0)
                    inv.items[inv.currentUseID - 1].id = 0;
                inv.UpdateInventory();
            }
        }
    }

    private static bool CanPlaceBlock(Inventory inv, LoadWorld myWorld, int x, int y, float dx, float dy)
    {
        if (inv.items[inv.currentUseID - 1].id != 1 &&
                inv.items[inv.currentUseID - 1].id != 2 &&
                inv.items[inv.currentUseID - 1].id != 3 &&
                inv.items[inv.currentUseID - 1].id != 4)
            return false;
        if (!(Math.Abs(dx) > 3 || dy > 6 || dy < 0))
            return false;
        if (x == myWorld.spawn.x && y == myWorld.spawn.y || x == myWorld.spawn.x && y == myWorld.spawn.y + 3)
            return false;
        if (!(x > 0 && myWorld.world[x / 3 - 1, y / 3] != 0 ||
            x < 3000 && myWorld.world[x / 3 + 1, y / 3] != 0 ||
            y > 0 && myWorld.world[x / 3, y / 3 - 1] != 0 ||
            y < 600 && myWorld.world[x / 3, y / 3 + 1] != 0))
            return false;
        Vector3 pos = new Vector3(x, y, 1);
        for (int i = 0; i < monsters.monsters.Count; i++)
        {
            if (Vector3.Distance(monsters.monsters[i].transform.position, pos) < 3)
                return false;
        }
        return true;
    }
}
