using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteSaveScript : MonoBehaviour
{
    public void DeleteSave()
    {
        string path = Path.Combine(Application.dataPath, "Save.json");
        if (File.Exists(path))
            File.Delete(path);
    }
}
