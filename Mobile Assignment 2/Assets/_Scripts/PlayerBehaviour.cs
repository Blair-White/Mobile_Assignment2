using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private bool dying;
    private int count;
    public GameObject life1, life2, life3;
    private int lives = 3;
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public bool isJumping;
    public bool isCrouching;
    public Transform spawnPoint;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _Move();
        if(dying)
        {
            count++;
            if(count > 10)
            {
                dying = false; count = 0;
            }
        
        }
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (!isJumping && !isCrouching)
            {
                if (joystick.Horizontal > joystickHorizontalSensitivity)
                {
                    // move right
                    m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                    m_spriteRenderer.flipX = false;
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.Run);
                }
                else if (joystick.Horizontal < -joystickHorizontalSensitivity)
                {
                    // move left
                    m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                    m_spriteRenderer.flipX = true;
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.Run);
                }
                else
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.Idle);
                }

            }
            
            if ((joystick.Vertical > joystickVerticalSensitivity) && (!isJumping))
            {
                // jump
                m_rigidBody2D.AddForce(Vector2.up * verticalForce);
                m_animator.SetInteger("AnimState", (int)PlayerAnimationType.Jump);
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }

            if ((joystick.Vertical < -joystickVerticalSensitivity) && (!isCrouching))
            {
                
                m_animator.SetInteger("AnimState", (int)PlayerAnimationType.Crouch);
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void Die()
    {
        Debug.Log("die");
        lives--;
        if (lives == 0)
        {
            SceneManager.LoadScene(4);
        }
        if (lives == 2)
        { life1.gameObject.SetActive(false); }
        if (lives == 1)
        { life2.gameObject.SetActive(false); }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dying) return;
        // respawn
        if (other.gameObject.CompareTag("DeathPlane"))
        {
            Die();
            transform.position = spawnPoint.position;
            dying = true;
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            Die();
            transform.position = spawnPoint.position;
            dying = true;
        }
    }
}
