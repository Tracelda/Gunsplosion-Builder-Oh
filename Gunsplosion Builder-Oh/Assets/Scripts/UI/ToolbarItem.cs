using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarItem : MonoBehaviour
{
    public int id;
    public Image image;

    public void Click() {
        LevelEditor.instance.SelectBlock(id);
    }
}
