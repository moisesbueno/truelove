using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    int extraJumps = 1;
    bool isJumping = false;
    bool isOnFloor = false;

    Rigidbody2D body;
    SpriteRenderer sprite;
    Animator anim;

    [Header("Movement Variables")]
    public float speed = 10f;
    public float jumpForce = 200f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float radius = 0.2f;

    [Header("Attack Variables")]
    public Transform attackCheck;
    public float radiusCheck;
    public LayerMask layerEnemy;
    float timeNextAttack = 0;

    public float forcaHorizontal = 15;
    public float forcaVertical = 10;
    public float tempoDestruicao = 1;
    public float forcaHorizontalPadrao;

    public int health;
    public bool invunerable = false;
    public bool isAlive = true;




    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        forcaHorizontalPadrao = forcaHorizontal;
    }

    // Update is called once per frame
    void Update()
    {
        //isOnFloor = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);

        // isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);

        isOnFloor = body.IsTouchingLayers(whatIsGround);

        //if (Input.GetButtonDown("Jump") && isOnFloor == true)
        //{
        //    isJumping = true;
        //}

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            isJumping = true;
            extraJumps--;
        }
        if (isOnFloor) extraJumps = 1;

        if (timeNextAttack <= 0f)
        {
            if (Input.GetButtonDown("Fire1") && body.velocity == new Vector2(0, 0))
            {
                anim.SetTrigger("Attack");
                timeNextAttack = 0.2f;
                PlayerAttack();
            }

        }
        else
        {
            timeNextAttack -= Time.deltaTime;
        }

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    anim.SetTrigger("Attack");
        //}

        PlayerAnimation();

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {

            float move = Input.GetAxis("Horizontal");

            body.velocity = new Vector2(move * speed, body.velocity.y);

            if ((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
            {
                Flip();
            }

            if (isJumping)
            {
                body.velocity = new Vector2(body.velocity.x, 0f);
                body.AddForce(new Vector2(0f, jumpForce));

                isJumping = false;
            }

            if (body.velocity.y > 0f && !Input.GetButton("Jump"))
            {
                body.velocity += Vector2.up * -0.8f;
            }
        }


    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        attackCheck.localPosition = new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(attackCheck.position, radiusCheck);
    }

    void PlayerAttack()
    {
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusCheck, layerEnemy);

        for (int i = 0; i < enemiesAttack.Length; i++)
        {
            Debug.Log(enemiesAttack[i].name);

            Destroy(enemiesAttack[i]);

            //Destroy(enemiesAttack[i].name, tempoDestruicao);
            //enemiesAttack[i].SendMessage("EnemyHit","-5");
        }
    }
    void PlayerAnimation()
    {
        anim.SetFloat("VelX", Mathf.Abs(body.velocity.x));
        anim.SetFloat("VelY", Mathf.Abs(body.velocity.y));


    }

    //void OnTriggerEnter2D(Collider2D other) {
    //    if(other.gameObject.CompareTag("Enemy")){
    //        other.gameObject.GetComponent<Enemy>().enabled = false;

    //        BoxCollider2D[] boxes = other.gameObject.GetComponents<BoxCollider2D>();

    //        foreach(BoxCollider2D box in boxes){
    //            box.enabled = false;
    //        }

    //        if(other.transform.position.x < transform.position.x){
    //            forcaHorizontal *= - 1;
    //        }

    //        other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaHorizontal,forcaVertical),ForceMode2D.Impulse);

    //        Destroy(other.gameObject,tempoDestruicao);

    //        forcaHorizontal = forcaHorizontalPadrao;
    //    }
    //}

    IEnumerator Damage()
    {
        invunerable = true;

        for (float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invunerable = false;
    }

    public void DamagePlayer()
    {
        invunerable = true;
        health--;

        StartCoroutine(Damage());

        if (health < 1)
        {
            isAlive = false;
            anim.SetTrigger("Death");
            Invoke("ReloadLevel", 1f);
        }


    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene("GameOver");
    }
}
