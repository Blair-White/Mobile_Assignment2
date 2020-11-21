using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : MonoBehaviour
{
    private enum States { Idle, Flipping}
    [SerializeField]
    private States State = States.Idle;
    private bool isSafe;
    private int flipCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case States.Idle:
                flipCount++;
                if (flipCount > 1200)
                {
                    State = States.Flipping;
                    flipCount = 0;
                }
                break;
            case States.Flipping:
                
                if (!isSafe)
                {

                    if (this.transform.rotation.eulerAngles.z < 180)
                    {
                        transform.Rotate(0.0f, 0.0f, 0.1f, Space.Self);
                    }
                    if(this.transform.rotation.eulerAngles.z >= 180)
                    {
                        this.transform.rotation = new Quaternion(0.0f, 0.0f, 180, 0.0f);
                        isSafe = true;
                        State = States.Idle;
                    }
                }

                if (isSafe)
                {
                    if (this.transform.rotation.eulerAngles.z > 0)
                    {
                        transform.Rotate(0.0f, 0.0f, -0.1f, Space.Self);
                    }
                    if (this.transform.rotation.eulerAngles.z <=0)
                    {
                        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                        isSafe = false;
                        State = States.Idle;
                    }
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
            if(!this.isSafe)
            {
                col.gameObject.SendMessage("Die");
            }
        }
    }
}
