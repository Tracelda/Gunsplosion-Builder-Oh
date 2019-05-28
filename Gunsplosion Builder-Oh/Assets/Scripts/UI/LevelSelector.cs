using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public Transform levelSelectContent;
    public GameObject levelSelectPrefab;

    private void Start() {
        LoadItems();
    }

    public void LoadItems() {
        foreach (Transform transform in levelSelectContent) {
            Destroy(transform.gameObject);
        }

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/levels");
        FileInfo[] info = dir.GetFiles("*.dat");
        foreach (FileInfo f in info) {
            LevelMenuItem newItem = Instantiate(levelSelectPrefab, levelSelectContent).GetComponent<LevelMenuItem>();
            newItem.levelName = f.Name.Replace(".dat", "");
            newItem.UpdateUI();
        }
    }

    public void OpenFolder() {
        string path = Application.persistentDataPath + "/levels/";
        path = path.Replace(@"/", @"\");
        System.Diagnostics.Process.Start("explorer.exe", path);
    }
}
