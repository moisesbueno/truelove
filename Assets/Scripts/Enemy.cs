using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    bool facingRight = false;
    bool noChao = false;
    Transform groundCheck;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("EnemyGroundCheck");


    }
    private void Update()
    {
        noChao = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (!noChao)
        {
            speed *= -1;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (speed > 0 && !facingRight)
        {
            Flip();
        }
        if (speed < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
