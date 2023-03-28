using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float timeBetweenSpawns;
    private GameManager gm;
    public float timer;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 9)
        {
            Destroy(gameObject);
        }
        rb.velocity = Vector2.left * speed;

    }
}
