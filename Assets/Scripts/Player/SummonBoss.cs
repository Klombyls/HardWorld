using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBoss : MonoBehaviour
{
    static int distance = 150;
    static int height = 60;
    static LoadWorld myWorld;
    static Pause pause;
    static SpawnMonster monsters;
    static Inventory inv;
    public GameObject golem;

    public void Summon()
    {
        GameObject player = GameObject.Find("Player");
        inv = GameObject.Find("Main Camera").GetComponent<Inventory>();
        System.Random rand = new System.Random((int)DateTime.Now.Ticks);
        if (myWorld == null)
            myWorld = GameObject.Find("World").GetComponent<LoadWorld>();
        if (pause == null)
            pause = GameObject.Find("Main Camera").GetComponent<Pause>();
        if (monsters == null)
            monsters = GameObject.Find("World").GetComponent<SpawnMonster>();
        // Определяем сторону где появится босс
        int dx = distance;
        if (rand.Next(2) == 1)
            dx = -dx;
        if (player.transform.position.x + dx < 0 || player.transform.position.x + dx > 3000)
            dx = -dx;
        int x = (int)(player.transform.position.x / 3);
        x = x * 3 + dx;
        int y = (int)(player.transform.position.y / 3);
        y = y * 3 + height;
        if (CanSpawn(x, y))
        {
            GameObject monster = Instantiate(golem, new Vector3(x, y, 1), new Quaternion());
            monster.GetComponent<MonsterMovement>().playerPosition = player.transform;
            monster.name = "Boss";
            monster.GetComponent<MonsterTakingDamage>().hp = 1000 + myWorld.difficult * 200;
            monster.GetComponent<PlayerTakingDamage>().damage = 25 + myWorld.difficult * 5;
            monsters.monsters.Add(monster);
            inv.items[inv.currentUseID - 1].count--;
            if (inv.items[inv.currentUseID - 1].count == 0)
                inv.items[inv.currentUseID - 1].id = 0;
            inv.UpdateInventory();
        }

    }
    bool CanSpawn(int x, int y)
    {
        if (pause.pauseActive)
            return false;
        if (inv.items[inv.currentUseID - 1].id != 16)
            return false;
        foreach (var e in monsters.monsters)
        {
            if (e.name == "Boss")
                return false;
        }
        if (x < 0 || x > 3000 || y < 0 || y > 600)
            return false;
        for (int i = -1; i <= 1; i++)
            for (int k = -1; k <= 1; k++)
            {
                if (x + i * 3 < 0 || x + i * 3 > 3000 ||
                    y + i * 3 < 0 || y + i * 3 > 600)
                    return false;
                if (myWorld.world[x / 3 + i, y / 3 + k] != 0)
                    return false;
            }
        return true;
    }
}
