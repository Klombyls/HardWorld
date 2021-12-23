using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    string path;
    Save sv = new Save();
    public void Savegame()
    {
        int[,] world = LoadWorld.world;
        int[,] fonWorld = LoadWorld.fonWorld;
        var save = new string[200];
        Save sv = new Save();
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
        sv.x = (int)LoadWorld.spawn.x;
        sv.y = (int)LoadWorld.spawn.y;

        // Сохранение инвентаря

        path = Path.Combine(Application.dataPath, "Save.json");
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
}
