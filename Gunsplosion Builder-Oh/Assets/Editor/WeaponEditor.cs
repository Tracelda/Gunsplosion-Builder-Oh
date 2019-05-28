using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    

    public override void OnInspectorGUI()
    {
        var weapon = target as Weapon;
        base.OnInspectorGUI();
        if(GUILayout.Button("fireWeapon"))
        {
            weapon.fire();
        }
    }
}
