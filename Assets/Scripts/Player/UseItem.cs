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
                Debug.Log("� ���� �����");
                break;
            case 2:
                Debug.Log("� ���� �����");
                break;
            case 5:
                Debug.Log("� �����");
                break;
            case 6:
                Debug.Log("� ����");
                break;
            case 7:
                Debug.Log("� ���");
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
