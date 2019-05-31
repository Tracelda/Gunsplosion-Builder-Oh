using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDrop : MonoBehaviour
{
    public float speed;
    public float Ypoint;

    private void Update() {
        if (transform.position.y > Ypoint) {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else {
            transform.position = new Vector3(transform.position.x, Ypoint);
        }
    }
}
