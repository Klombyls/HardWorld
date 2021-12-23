using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadWorld : MonoBehaviour
{
    public GameObject dirtBlock;
    public GameObject stoneBlock;
    public GameObject ironOreBlock;
    public GameObject goldOreBlock;
    public GameObject dirtFonBlock;
    public GameObject stoneFonBlock;
    public GameObject player;
    private Save sv = new Save();
    public static int[,] world = new int[1000, 200];
    public static int[,] fonWorld = new int[1000, 200];
    public static Vector3 spawn;

    void Start()
    {
        string path = Path.Combine(Application.dataPath, "Save.json");
        if (File.Exists(path))
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        
        for (int i = 0; i < 200; i++)
            for (int k = 0; k < 1000; k++)
                world[k, i] = int.Parse(sv.world[i][k].ToString());
        for (int i = 0; i < 200; i++)
            for (int k = 0; k < 1000; k++)
                fonWorld[k, i] = int.Parse(sv.backgroundWorld[i][k].ToString());

        for (int i = 0; i < 1000; i++)
        {
            for (int k = 0; k < 200; k++)
            {
                if (world[i, k] != 0)
                {
                    Vector3 pos = new Vector3();
                    pos.x = i * 3;
                    pos.y = k * 3;
                    pos.z = 1;
                    if (world[i, k] == 1)
                    {
                        GameObject block = Instantiate(dirtBlock, pos, new Quaternion());
                    }
                    if (world[i, k] == 2)
                    {
                        GameObject block = Instantiate(stoneBlock, pos, new Quaternion());
                    }
                    if (world[i, k] == 3)
                    {
                        GameObject block = Instantiate(ironOreBlock, pos, new Quaternion());
                    }
                    if (world[i, k] == 4)
                    {
                        GameObject block = Instantiate(goldOreBlock, pos, new Quaternion());
                    }
                }
            }
        }
        for (int i = 0; i < 1000; i++)
        {
            for (int k = 0; k < 200; k++)
            {
                if (fonWorld[i, k] != 0)
                {
                    Vector3 pos = new Vector3();
                    pos.x = i * 3;
                    pos.y = k * 3;
                    pos.z = 2;
                    if (fonWorld[i, k] == 1)
                    {
                        GameObject fonblock = Instantiate(dirtFonBlock, pos, new Quaternion());
                    }
                    if (fonWorld[i, k] == 2)
                    {
                        GameObject fonblock = Instantiate(stoneFonBlock, pos, new Quaternion());
                    }
                }
            }
        }
        
        spawn.x = sv.x;
        spawn.y = sv.y;
        spawn.z = 1;
        player.transform.position = spawn;
    }
}
