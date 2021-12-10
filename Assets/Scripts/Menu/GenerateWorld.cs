using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

public class GenerateWorld : MonoBehaviour
{
    public void Generate()
    {
        int[,] world = new int[1000, 200];
        int maxHeight = 150;
        int minHeight = 50;
        int currHeight = 100;
        var rand = new System.Random((int)DateTime.Now.Ticks);
        int[] height = new int[1000];
        Save sv = new Save();
        string path;
        path = Path.Combine(Application.dataPath, "Save.json");

        // ��������� ���������
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

        // ����������� ���������
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

        // ���������� ����
        for (int i = 0; i < 1000; i++)
        {
            for (int k = 0; k < height[i]; k++)
                world[i, k] = 2;
            world[i, height[i]] = 1;
        }

        // ���������� ������ ����
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
        sv.backgroundWorld = save;
        File.WriteAllText(path, JsonUtility.ToJson(sv));
        Debug.Log("File saved");
    }
}

[Serializable]
public class Save
{
    [SerializeField] private string[] _world;
    [SerializeField] private string[] _backgroundWorld;
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
}
