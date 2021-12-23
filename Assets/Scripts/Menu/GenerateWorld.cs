using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

public class GenerateWorld : MonoBehaviour
{
    int[,] world = new int[1000, 200];
    int[,] fonWorld = new int[1000, 200];
    int maxHeight = 150;
    int minHeight = 50;
    System.Random rand = new System.Random((int)DateTime.Now.Ticks);
    string path;
    int[] height = new int[1000];
    Save sv = new Save();
    public void Generate()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        // Генерация ландшафта
        int currHeight = 100;
        for (int i = 485; i < 516; i++)
            height[i] = 100;
        for (int i = 516; i < 1000; i++)
        {
            int newHeight = rand.Next(0, 2) == 1 ? 1 : -1;
            if (newHeight + currHeight > maxHeight || newHeight + currHeight < minHeight)
                newHeight = -newHeight;
            newHeight += currHeight;
            height[i] = newHeight;
            currHeight = newHeight;
        }
        currHeight = 100;
        for (int i = 484; i >= 0; i--)
        {
            int newHeight = rand.Next(0, 2) == 1 ? 1 : -1;
            if (newHeight + currHeight > maxHeight || newHeight + currHeight < minHeight)
                newHeight = -newHeight;
            newHeight += currHeight;
            height[i] = newHeight;
            currHeight = newHeight;
        }

        // Сглаживание ландшафта
        for (int i = 0; i < 1000; i++)
        {
            float sum = height[i];
            int count = 1;
            for (int k = 1; k <= 5; k++)
            {
                int i1 = i - k;
                int i2 = i + k;

                if (i1 >= 0)
                {
                    sum += height[i1];
                    count++;
                }

                if (i2 < 999)
                {
                    sum += height[i2];
                    count++;
                }
            }

            height[i] = (int)(sum / count);
        }

        // Заполнение мира
        for (int i = 0; i < 1000; i++)
        {
            for (int k = 0; k < height[i]; k++)
            {
                if (rand.Next(100) % 20 == 0)
                    world[i, k] = 3;
                else if (rand.Next(100) % 33 == 0)
                    world[i, k] = 4;
                else world[i, k] = 2;
            }
            world[i, height[i]] = 1;
        }
        for (int i = 0; i < 1000; i++)
        {
            for (int k = 0; k < height[i]; k++)
                fonWorld[i, k] = 2;
            fonWorld[i, height[i]] = 1;
        }

        // Сохранение нового мира
        var save = new string[200];
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
        sv.x = 1500;
        sv.y = height[500] * 3 + 3;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
}

[Serializable]
public class Save
{
    [SerializeField] private string[] _world;
    [SerializeField] private string[] _backgroundWorld;
    [SerializeField] private int[] _countItem;
    [SerializeField] private int[] _idItem;
    [SerializeField] public int x;
    [SerializeField] public int y;
    public string[] world
    {
        get { return _world; }
        set { _world = value; }
    }
    public string[] backgroundWorld
    {
        get { return _backgroundWorld; }
        set { _backgroundWorld = value; }
    }
    public int[] idItem
    {
        get { return _idItem; }
        set { _idItem = value; }
    }
    public int[] countItem
    {
        get { return _countItem; }
        set { _countItem = value; }
    }
}
