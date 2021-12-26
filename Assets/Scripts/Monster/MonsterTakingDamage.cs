using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakingDamage : MonoBehaviour
{
    public float hp = 20f;
    Inventory inv;
    public List<GettingReward> lut = new List<GettingReward>();
    SpawnMonster monsters;
    

    private void Start()
    {
        inv = GameObject.Find("Main Camera").GetComponent<Inventory>();
        monsters = GameObject.Find("World").GetComponent<SpawnMonster>();
    }

    public void TakingDmg(float dmg)
    {
        hp -= dmg;
    }

    void Update()
    {
        if (hp <= 0)
        {
            for (int i = 0; i < lut.Count; i++)
            {
                LootItem.CreateLootItem(transform, lut[i].id, Random.Range(lut[i].min, lut[i].max), inv.player, inv, inv.data, inv.world, inv.lootItem);
            }
            monsters.monsters.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class GettingReward
{
    public int id;
    public int min;
    public int max;
}
