using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseCraft : MonoBehaviour
{
    public List<CraftItem> craftItems = new List<CraftItem>();
}

[System.Serializable]

public class CraftItem
{
    public int idCell;
    public int id;
    public int count;
    public GameObject itemGameObj;
    public List<ItemInventory> needItems = new List<ItemInventory>();

}
