using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMonster : MonoBehaviour
{
    SpawnMonster monsters;
    GameObject player;
    public int distance;
    void Start()
    {
        monsters = GameObject.Find("World").GetComponent<SpawnMonster>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (monsters.monsters.Count != 0)
            for (int i = 0; i < monsters.monsters.Count; i++)
            {
                GameObject monster = monsters.monsters[i];
                if (Vector3.Distance(player.transform.position, monster.transform.position) > distance)
                {
                    monsters.monsters.Remove(monster);
                    Destroy(monster);
                }
            }
    }
}
