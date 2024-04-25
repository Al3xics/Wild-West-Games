using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetScenesName : MonoBehaviour
{
    void Start()
    {
        string folderPath = "Assets/Scenes";
        if (GameManager.Instance.getGamesName().Count <= 0)
        {
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string file in files)
                {
                    if (!file.EndsWith(".meta"))
                    {
                        // Obtient le nom du fichier sans son extension
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                        GameManager.Instance.setGamesName(fileNameWithoutExtension);
                    }
                }
                Destroy(this.gameObject);
            }
        }
    }
}
