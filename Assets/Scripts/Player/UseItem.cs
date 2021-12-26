using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public HealthBar healthBar;
    public SwordStrike swordStrike;
    public Inventory inv;
    public SummonBoss boss;
    public void Use(int id)
    {
        switch (id)
        {
            case 1:
                BlockPlacement.Place();
                break;
            case 2:
                BlockPlacement.Place();
                break;
            case 5:
                BlockMining.Mining();
                break;
            case 6:
                swordStrike.dmg = 10f;
                swordStrike.attackHitBox.GetComponent<SpriteRenderer>().sprite = inv.data.items[inv.items[inv.currentUseID - 1].id].img;
                swordStrike.Attack();
                break;
            case 7:
                swordStrike.dmg = 20f;
                swordStrike.attackHitBox.GetComponent<SpriteRenderer>().sprite = inv.data.items[inv.items[inv.currentUseID - 1].id].img;
                swordStrike.Attack();
                break;
            case 8:
                swordStrike.dmg = 30f;
                swordStrike.attackHitBox.GetComponent<SpriteRenderer>().sprite = inv.data.items[inv.items[inv.currentUseID - 1].id].img;
                swordStrike.Attack();
                break;
            case 9:
                swordStrike.dmg = 50f;
                swordStrike.attackHitBox.GetComponent<SpriteRenderer>().sprite = inv.data.items[inv.items[inv.currentUseID - 1].id].img;
                swordStrike.Attack();
                break;
            case 15:
                healthBar.RecoveryPotion();
                break;
            case 16:
                boss.Summon();
                break;
        }
    }
}
