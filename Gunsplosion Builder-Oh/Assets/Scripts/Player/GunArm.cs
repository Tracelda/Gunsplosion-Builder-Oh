using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunArm : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer sprite;

    void Update()
    {
        transform.right = target.position - transform.position;
        print(transform.rotation.eulerAngles.z);
        if (transform.localRotation.eulerAngles.z == 135 || transform.localRotation.eulerAngles.z == 225) {
            sprite.flipY = true;
        }
        else {
            sprite.flipY = false;
        }
    }
}
