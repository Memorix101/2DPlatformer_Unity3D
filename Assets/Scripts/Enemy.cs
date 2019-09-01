using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool killed = false;
    private Rigidbody2D rb2d;
    public AudioClip dead_snd;
    private bool walkLeft = true;
    private float speed = 4.5f;
    private SpriteRenderer spriteRenderer;
    public Sprite killed_gfx;
    private Animator anim;
    public GameObject score_go;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(3).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (walkLeft)
        {
            rb2d.AddForce(Vector2.left * speed);
        }
        else
        {
            rb2d.AddForce(-Vector2.left * speed);
        }

        RaycastHit2D leftHit2D = Physics2D.Raycast(transform.position, Vector2.left, 0.2f);
        RaycastHit2D rightHit2D = Physics2D.Raycast(transform.position, -Vector2.left, 0.2f);
        RaycastHit2D upHit2D = Physics2D.Raycast(transform.position, Vector2.up, 0.3f);

        // If it hits something...
        if (upHit2D.collider != null)
        {
            //Debug.Log("leftHit2D: " + upHit2D.collider.transform.name);

            if (upHit2D.collider.transform.tag == "Player")
            {
                KillMe();
            }
        }

        // If it hits something...
        if (leftHit2D.collider != null)
        {
            //Debug.Log("leftHit2D: " + leftHit2D.collider.transform.name);

            if (leftHit2D.collider.transform.tag == "World")
            {
                walkLeft = false;
                spriteRenderer.flipX = true;
            }
        }

        // If it hits something...
        if (rightHit2D.collider != null)
        {
            //Debug.Log("rightHit2D: " + rightHit2D.collider.transform.name);

            if (rightHit2D.collider.transform.tag == "World")
            {
                walkLeft = true;
                spriteRenderer.flipX = false;
            }
        }
    }

    public void KillMe()
    {
        killed = true;

        Instantiate(score_go, transform.position, Quaternion.identity);

        // play sound
        GameObject soundObj = new GameObject("Enemy_Kill_Sound");
        soundObj.AddComponent<AudioSource>();
        soundObj.GetComponent<AudioSource>().playOnAwake = true;
        soundObj.GetComponent<AudioSource>().spread = 360f;
        soundObj.GetComponent<AudioSource>().clip = dead_snd;
        soundObj.GetComponent<AudioSource>().Play();
        Destroy(soundObj, 3f);

        rb2d.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;

        anim.enabled = false; //stops animation

        spriteRenderer.sprite = killed_gfx;
        spriteRenderer.sortingOrder = 100;

        Transform[] sub = new Transform[transform.childCount - 1];

        for (int i = 0; i < transform.childCount; i++)
        {
            if( i < 3)
                sub[i] = transform.GetChild(i);
        }

        foreach (var t in sub)
        {
           Destroy(t.gameObject);
        }
    }
}
