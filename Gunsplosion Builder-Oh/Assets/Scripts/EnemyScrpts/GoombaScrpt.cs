using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScrpt : MonoBehaviour
{
    private Vector2 leftNodePos, rightNodePos;
    public GameObject leftNode, rightNode;
    private CircleCollider2D detectionRing;
    // Start is called before the first frame update
    void Start()
    {
        leftNodePos = leftNode.transform.position;
        rightNodePos = rightNode.transform.position;
        detectionRing = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
