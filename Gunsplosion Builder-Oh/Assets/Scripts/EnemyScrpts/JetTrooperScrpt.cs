using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetTrooperScrpt : MonoBehaviour
{
    public Transform leftNodeTrans, rightNodeTrans;
    public Vector2 leftNodePos, rightNodePos;
    public GameObject leftNode, rightNode;
    private CircleCollider2D detectionRing;
    private Moving_Entity Moving_Entity;
    private float Direction = 1f;

    public bool MovingRight, mirrorPatroling = false;
    // Start is called before the first frame update
    void Start()
    {
        leftNodePos = leftNode.transform.position;
        rightNodeTrans.position = rightNode.transform.position;
        detectionRing = gameObject.GetComponent<CircleCollider2D>();
        Moving_Entity = gameObject.GetComponent<Moving_Entity>();
        MovingRight = true;
        FindNodes(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (mirrorPatroling)
        {
            ReversePatrolling(gameObject.transform.position);
        }
        else
        {
            Patrolling(gameObject.transform.position);
        }
    }

    public void FindNodes(Vector2 CharacterPos)
    {
        if (leftNodeTrans.position.x > CharacterPos.x)
        {
            mirrorPatroling = true;
        }
        else
        {
            mirrorPatroling = false;
        }
    }

    public void Patrolling(Vector2 CharacterPos)
    {
        if (MovingRight) // Moving right
        {
            if (CharacterPos.x < rightNodeTrans.position.x)
            {
                Moving_Entity.Move(Direction);
            }
            else if (CharacterPos.x >= rightNodeTrans.position.x)
            {
                MovingRight = false;
                Moving_Entity.Move(-Direction);
            }
        }
        else
        { // Moving left
            if (CharacterPos.x > leftNodeTrans.position.x)
            {
                Moving_Entity.Move(-Direction);
            }
            else if (CharacterPos.x <= leftNodeTrans.position.x)
            {
                MovingRight = true;
                Moving_Entity.Move(Direction);
            }
        }
    }

    public void ReversePatrolling(Vector2 CharacterPos)
    {
        if (MovingRight) // Moving right
        {
            if (CharacterPos.x > rightNodeTrans.position.x)
            {
                Moving_Entity.Move(-Direction);
            }
            else if (CharacterPos.x <= rightNodeTrans.position.x)
            {
                MovingRight = false;
                Moving_Entity.Move(Direction);
            }
        }
        else
        { // Moving left
            if (CharacterPos.x < leftNodeTrans.position.x)
            {
                Moving_Entity.Move(Direction);
            }
            else if (CharacterPos.x >= leftNodeTrans.position.x)
            {
                MovingRight = true;
                Moving_Entity.Move(-Direction);
            }
        }
    }
}
