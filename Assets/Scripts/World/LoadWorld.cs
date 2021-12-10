using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadWorld : MonoBehaviour
{
    public GameObject dirtBlock;
    public GameObject stoneBlock;
    private Save sv = new Save();
    private int[,] world = new int[1000, 200];

    void Start()
    {
        string path = Path.Combine(Application.dataPath, "Save.json");
        if (File.Exists(path))
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        
        for (int i = 0; i < 200; i++)
            for (int k = 0; k < 1000; k++)
                world[k, i] = int.Parse(sv.world[i][k].ToString());

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
                    Debug.Log("Блок");
                }
            }
        }
        Debug.Log("Файл загружен");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
