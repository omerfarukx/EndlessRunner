using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusman : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 15;
    public float timeBetweenSpawns;
    private GameManager gm;
    public float timer;
    Animator anim;
    BoxCollider2D bc;
    void Start()
    {
        transform.Rotate(0, -180, 0);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 15)
        {
            Destroy(gameObject);
        }
        rb.velocity = Vector2.left * speed;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            anim.SetTrigger("Die");
            bc.enabled = false;
            rb.gravityScale= 0f;
            speed = 5;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
