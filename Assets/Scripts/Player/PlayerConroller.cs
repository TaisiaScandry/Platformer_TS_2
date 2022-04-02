using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    bool isGrounded;
    public float velocity;
    public float jumpHeight;
    public Transform groundCheck;
    public int HealthPoints = 5;
    int CurrentHealthPoints;
    bool isHit = false;
    public Main main;

    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CurrentHealthPoints = HealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            flip();
            if (isGrounded)
            {
                animator.SetInteger("State", 2);
            }
        }
        flip();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }
        GroundCheck();
    }

    // Ratation of player
    void flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocity, rb.velocity.y);
    }

    void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
        {
            animator.SetInteger("State", 3);
        }
    }

    public void RecountHealthPoints(int deltaHealthPoints)
    {
        CurrentHealthPoints += deltaHealthPoints;
        if (deltaHealthPoints < 0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        }
        if (CurrentHealthPoints <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 2f);
        }
    }

    IEnumerator OnHit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (isHit)
        {
            sr.color = new Color(1f, sr.color.g - 0.02f, sr.color.b - 0.02f);
        } else {
            sr.color = new Color(1f, sr.color.g + 0.02f, sr.color.b + 0.02f);
        }
        if (sr.color.g ==1f)
        {
            StopCoroutine(OnHit());
        } 
        if (sr.color.g <= 0)
        {
            isHit = false;
        }
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }
}
