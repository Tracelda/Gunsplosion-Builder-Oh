using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScrpt : MonoBehaviour
{
    public Vector2 leftNodePos, rightNodePos;
    public GameObject leftNode, rightNode;
    private CircleCollider2D detectionRing;
    private Moving_Entity Moving_Entity;
    private float Direction = 1f;

    public bool MovingRight;
    // Start is called before the first frame update
    void Start()
    {
        leftNodePos = leftNode.transform.position;
        rightNodePos = rightNode.transform.position;
        detectionRing = gameObject.GetComponent<CircleCollider2D>();
        Moving_Entity = gameObject.GetComponent<Moving_Entity>();
        MovingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Patrolling(gameObject.transform.position);
    }

    public void Patrolling( Vector2 GoombaPos)
    {
        if (MovingRight) // Moving right
        {
            if (GoombaPos.x < rightNodePos.x)
            {
                Moving_Entity.Move(Direction);
            }
            else if  (GoombaPos.x >= rightNodePos.x)
            {
                MovingRight = false;
                Moving_Entity.Move(-Direction);
            }
        }
        else
        { // Moving left
            if (GoombaPos.x > leftNodePos.x)
            {
                Moving_Entity.Move(-Direction);
            }
            else if (GoombaPos.x <= leftNodePos.x)
            {
                MovingRight = true;
                Moving_Entity.Move(Direction);
            }
        }
    }
}
