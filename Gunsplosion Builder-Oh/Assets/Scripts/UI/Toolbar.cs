using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public Transform toolbarTransform;
    public GameObject toolbarItem;
    public LevelData levelData;

    private void Start() {
        int i = 0;
        foreach(BlockData block in levelData.blocks) {
            ToolbarItem newItem = Instantiate(toolbarItem, toolbarTransform).GetComponent<ToolbarItem>();
            newItem.id = i;
            newItem.image.sprite = block.icon;
            i++;
        }
    }
}
