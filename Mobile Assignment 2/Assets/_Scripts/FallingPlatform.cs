using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private enum States { Idle, Shaking, Falling, Resetting}
    [SerializeField]
    private States State = States.Idle;
    private int countShake, countReset;
    private Transform startTransform;
    private Vector3 startPos;
    private bool leftRight;
    // Start is called before the first frame update
    void Start()
    {
        startTransform = this.transform;
        startPos = startTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case States.Idle:
                break;
            case States.Shaking:
                countShake++;
                if (countShake > 650)
                {
                    State = States.Falling;
                    countShake = 0;
                }
                if(!leftRight)
                {
                    if(this.transform.position.x > startPos.x - 0.1f)
                    {
                        this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
                    }
                    else if(this.transform.position.x < startPos.x - 0.1f)
                    {
                        leftRight = true;
                    }

                }

                if(leftRight)
                {
                    if (this.transform.position.x < startPos.x + 0.1f)
                    {
                        this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
                    }
                    else if(this.transform.position.x > startPos.x + 0.1f)
                    {
                        leftRight = false;
                    }
                }

                break;
            case States.Falling:
                countReset++;
                if (countReset > 129)
                {
                    State = States.Resetting;
                    countReset = 0;
                }
                this.transform.position = new Vector3(startPos.x, this.transform.position.y - 0.2f, startPos.z);
                break;
            case States.Resetting:
                this.transform.position = Vector3.MoveTowards(this.transform.position, startPos, 5f * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, startPos) < 0.01f)
                {
                    State = States.Idle;
                }
                break;
            default:
                break;
        }



    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(State == States.Idle)
            {
                State = States.Shaking;
            }
        }
    }
}
