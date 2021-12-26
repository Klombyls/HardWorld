using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public List<GameObject> monsters = new List<GameObject>();
    public int distance = 150;
    public int height = 60;
    LoadWorld myWorld;
    public Pause pause;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    System.Random rand = new System.Random((int)DateTime.Now.Ticks);
    public int minTime = 15;
    public int maxTime = 45;
    public GameObject player;
    bool canSpawn = true;

    void Start()
    {
        myWorld = GameObject.Find("World").GetComponent<LoadWorld>();
    }

    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            Invoke("Spawn", rand.Next(minTime, maxTime));
        }
    }

    void Spawn()
    {
        canSpawn = true;
        // Определяем сторону где появится монстр
        int dx = distance;
        if (rand.Next(2) == 1)
            dx = -dx;
        if (player.transform.position.x + dx < 0 || player.transform.position.x + dx > 3000)
            dx = -dx;
        int x = (int)(player.transform.position.x / 3);
        x = x * 3 + dx;
        int y = (int)(player.transform.position.y / 3);
        y = y * 3 + height;
        // Появление монстра, если он окажется в свободной клетке
        if (CanSpawn(x, y))
        {
            int chance2 = (int)((1.0 - (myWorld.difficult + 1.0) / ((myWorld.difficult + 1.0) * (myWorld.difficult + 1.0))) / 2.0 * 100.0);
            GameObject monster;
            if (rand.Next(100) < chance2)
            {
                monster = Instantiate(monster3, new Vector3(x, y, 1), new Quaternion());
                monster.GetComponent<MonsterTakingDamage>().hp = 100 + myWorld.difficult * 25;
                monster.GetComponent<PlayerTakingDamage>().damage = 20 + myWorld.difficult * 3;
            }
            else if (rand.Next(2) == 1)
            {
                monster = Instantiate(monster2, new Vector3(x, y, 1), new Quaternion());
                monster.GetComponent<MonsterTakingDamage>().hp = 40 + myWorld.difficult * 10;
                monster.GetComponent<PlayerTakingDamage>().damage = 12 + myWorld.difficult * 2;
            }
            else
            {
                monster = Instantiate(monster1, new Vector3(x, y, 1), new Quaternion());
                monster.GetComponent<MonsterTakingDamage>().hp = 20 + myWorld.difficult * 5;
                monster.GetComponent<PlayerTakingDamage>().damage = 7 + myWorld.difficult;
            }
            monster.GetComponent<MonsterMovement>().playerPosition = player.transform;
            monsters.Add(monster);
        }

    }

    bool CanSpawn(int x, int y)
    {
        if (pause.pauseActive)
            return false;
        if (monsters.Count >= 19)
            return false;
        if (x < 0 || x > 3000 || y < 0 || y > 600)
            return false;
        for (int i = -1; i <= 1; i++)
            for (int k = -1; k <= 1; k++)
            {
                if (x + i * 3 < 0 || x + i * 3 > 3000 ||
                    y + k * 3 < 0 || y + k * 3 > 600)
                    return false;
                if (myWorld.world[x / 3 + i, y / 3 + k] != 0)
                    return false;
            }
        return true;
    }
}
