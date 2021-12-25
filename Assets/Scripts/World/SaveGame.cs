using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    string path;
    Save sv = new Save();
    LoadWorld myWorld;
    public void Savegame()
    {
        myWorld = GameObject.Find("World").GetComponent<LoadWorld>();
        int[,] world = myWorld.world;
        int[,] fonWorld = myWorld.fonWorld;
        var save = new string[200];
        Save sv = new Save();
        // Сохранение блоков
        for (int i = 0; i < 200; i++)
        {
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < 1000; k++)
            {
                sb.Append(world[k, i]);
            }
            save[i] = sb.ToString();
        }
        sv.world = save;
        var save2 = new string[200];
        for (int i = 0; i < 200; i++)
        {
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < 1000; k++)
            {
                sb.Append(fonWorld[k, i]);
            }
            save2[i] = sb.ToString();
        }
        sv.backgroundWorld = save2;
        // Сохранение спавна
        sv.x = (int)myWorld.spawn.x;
        sv.y = (int)myWorld.spawn.y;

        sv.idItem = new int[37];
        sv.countItem = new int[37];
        // Сохранение инвентаря
        Inventory inv = GameObject.Find("Main Camera").GetComponent<Inventory>();
        for (int i = 0; i < inv.maxCount; i++)
        {
            sv.idItem[i] = inv.items[i].id;
            sv.countItem[i] = inv.items[i].count;
        }
        // Сохранение сложности игры
        sv.difficult = myWorld.difficult;
        path = Path.Combine(Application.dataPath, "Save.json");
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
}