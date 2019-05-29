using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockResetBase : MonoBehaviour
{
    protected virtual void Start()
    {
        GameManager.instance.levelObjects.Add(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.instance.levelObjects.Remove(gameObject);
    }
}
