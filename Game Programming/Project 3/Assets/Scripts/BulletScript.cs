using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float velX = 10f;
    private float velY = 0f;
    Rigidbody2D rb;
    AudioSource bulletSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSound = GetComponent<AudioSource>();
        bulletSound.Play(0);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX, velY);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bear" || collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
