using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScrpt : BaseEnemy
{
    public Transform leftNodeTrans, rightNodeTrans;
    public Vector2 leftNodePos, rightNodePos;
    public GameObject leftNode, rightNode;
    private CircleCollider2D detectionRing;
    private Moving_Entity Moving_Entity;
    private float Direction = 1f;
    public LineRenderer leftLineRender, rightLineRender;

    public bool MovingRight, mirrorPatroling = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        leftNodeTrans = leftNode.transform;
        rightNodeTrans = rightNode.transform;
        detectionRing = gameObject.GetComponent<CircleCollider2D>();
        Moving_Entity = gameObject.GetComponent<Moving_Entity>();
        MovingRight = true;
        FindNodes(gameObject.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        //if (active)
        //{
            if (mirrorPatroling)
            {
                ReversePatrolling(gameObject.transform.position);
            }
            else
            {
                Patrolling(gameObject.transform.position);
            }
        //}
        //else
        //{
        //    leftLineRender = leftNode.GetComponent<LineRenderer>();
        //    rightLineRender = rightNode.GetComponent<LineRenderer>();
        //    leftLineRender.SetPosition(0, leftNodeTrans.position);
        //    leftLineRender.SetPosition(1, transform.position);

        //    rightLineRender.SetPosition(0, rightNodeTrans.position);
        //    rightLineRender.SetPosition(1, transform.position);
        //}
    }

    public void FindNodes(Vector2 CharacterPos)
    {
        if (leftNodePos.x > CharacterPos.x)
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
            transform.localScale = new Vector2(-1, transform.localScale.y);
            if (CharacterPos.x < rightNodePos.x)
            {
                Moving_Entity.Move(Direction);
            }
            else if (CharacterPos.x >= rightNodePos.x)
            {
                MovingRight = false;
                Moving_Entity.Move(-Direction);
            }
        }
        else
        { // Moving left
            transform.localScale = new Vector2(1, transform.localScale.y);
            if (CharacterPos.x > leftNodePos.x)
            {
                Moving_Entity.Move(-Direction);
            }
            else if (CharacterPos.x <= leftNodePos.x)
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
            transform.localScale = new Vector2(1, transform.localScale.y);
            if (CharacterPos.x > rightNodePos.x)
            {
                Moving_Entity.Move(-Direction);
            }
            else if (CharacterPos.x <= rightNodePos.x)
            {
                MovingRight = false;
                Moving_Entity.Move(Direction);
            }
        }
        else
        { // Moving left
            transform.localScale = new Vector2(-1, transform.localScale.y);
            if (CharacterPos.x < leftNodePos.x)
            {
                Moving_Entity.Move(Direction);
            }
            else if (CharacterPos.x >= leftNodePos.x)
            {
                MovingRight = true;
                Moving_Entity.Move(-Direction);
            }
        }
    }
}
