using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void Use(int id)
    {
        switch (id)
        {
            case 1:
                Debug.Log("Я блок земли");
                break;
            case 2:
                Debug.Log("Я блок камня");
                break;
            case 5:
                Debug.Log("Я кирка");
                break;
            case 6:
                Debug.Log("Я ржав");
                break;
            case 7:
                Debug.Log("Я жел");
                break;
            case 8:
                break;
            case 9:
                break;
            case 15:
                HealthBar.RecoveryPotion();
                break;
            case 16:
                break;
        }
    }
}
