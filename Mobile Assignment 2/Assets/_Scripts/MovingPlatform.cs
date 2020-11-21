using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private GameObject mPlayer;
    private Transform PointA, PointB;
    private Vector3 posA, posB, oldPos, moveVector;
    private enum States { left, right};
    private States State = States.right;
    public bool isRiding;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        PointA = this.transform;
        PointB = this.transform.GetChild(0);
        posA = PointA.transform.position;
        posB = PointB.transform.position;
        oldPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        switch (State)
        {
            case States.left:
                this.transform.position = Vector3.MoveTowards(this.transform.position, posA, 3.0f * Time.deltaTime);
                if(Vector3.Distance(this.transform.position, posA) < 0.01f)
                {
                    State = States.right;
                }
                break;
            case States.right:
                this.transform.position = Vector3.MoveTowards(this.transform.position, posB, 3.0f * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, posB) < 0.01f)
                {
                    State = States.left;
                }
                break;
            default:
                break;
        }

        oldPos = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

    }

    private void OnCollisionExit2D(Collision2D col)
    {
   
    }
}
