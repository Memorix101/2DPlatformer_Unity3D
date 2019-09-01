using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

// https://unity3d.com/de/learn/tutorials/topics/2d-game-creation/creating-basic-platformer-game
public class Player : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float moveForce = 5f;
    public float maxSpeed = 1f;
    public float jumpForce = 1f;
    private int doubleJump = 0; //rest if 1
    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    public AudioClip coin_snd;
    public AudioClip kill_snd;
    public AudioClip jump_snd;
    public Sprite jump_gfx;
    public TextMeshProUGUI Score_TMP;
    int score = 0;
    private Vector2 startPos;
    private bool killed = false;
    private bool finished = false;
    private SpriteRenderer spriteRenderer;
    public GameObject stage_clear_ui;
    public GameObject you_died_ui;


    public Vector2 getStartPos
    {
        get { return startPos; }
    }

    // Use this for initialization
    void Awake () 
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update () 
    {
        if (Input.GetButtonDown("Jump") && doubleJump <= 1) //&& grounded
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && killed || Input.GetKeyDown(KeyCode.Return) && finished)
        {
            SceneManager.LoadScene("Game1");
        }

        Score_TMP.text = score.ToString("0000");
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.2f);

        // If it hits something...
        if (hit.collider != null)
        {
            //Debug.Log("HIT: " + hit.transform.name);
            grounded = true;
            anim.enabled = true;

            //reset
            doubleJump = 0;
        }
        else
        {
            grounded = false;
            anim.enabled = false; //stops animation
            spriteRenderer.sprite = jump_gfx;
        }

        /*
        // level start
        if (transform.position.x <= 0)
        {
            transform.position = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
        */

        // if player falls off the level
        if (transform.position.y <= -5 && !killed)
        {
            // game over
            KillMe();
        }

        if (!killed && !finished)
        {
            float h = Input.GetAxis("Horizontal");

            if (grounded && h <= 0.5f || grounded && h >= -0.5f)
            {
                rb2d.velocity = Vector2.zero;
            }

            //anim.SetFloat("Speed", Mathf.Abs(h));

            if (Mathf.Abs(h) > 0)
            {
                anim.Play("Player_Walking");
            }
            else
            {
                anim.Play("Player_Idle");
            }

            if (h * rb2d.velocity.x < maxSpeed)
                rb2d.AddForce(Vector2.right * h * moveForce);

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();

            if (jump)
            {
                doubleJump++;
                anim.SetTrigger("Jump");
                rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jump = false;

                // play sound
                GameObject soundObj = new GameObject("Player_Jump_Sound");
                soundObj.AddComponent<AudioSource>();
                soundObj.GetComponent<AudioSource>().playOnAwake = true;
                soundObj.GetComponent<AudioSource>().spread = 360f;
                soundObj.GetComponent<AudioSource>().clip = jump_snd;
                soundObj.GetComponent<AudioSource>().Play();
                Destroy(soundObj, 3f);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!killed)
        {
            // Coin
            if (col.gameObject.tag == "Coin")
            {
                // play sound
                GameObject soundObj = new GameObject("Coin_Sound");
                soundObj.AddComponent<AudioSource>();
                soundObj.GetComponent<AudioSource>().playOnAwake = true;
                soundObj.GetComponent<AudioSource>().spread = 360f;
                soundObj.GetComponent<AudioSource>().clip = coin_snd;
                soundObj.GetComponent<AudioSource>().Play();
                Destroy(soundObj, 3f);

                // add score
                score++;

                Destroy(col.gameObject);
            }

            // Exit Trigger
            if (col.gameObject.tag == "Exit")
            {
                // do stuff here
                anim.Play("Player_Idle"); //maybe a win animation?
                stage_clear_ui.SetActive(true);
                finished = true;
                //rb2d.bodyType = RigidbodyType2D.Kinematic;
            }

            // Hit Enemy HitZone
            if (col.gameObject.tag == "HitZone")
            {
                // do stuff here
                score += 100;
                Debug.Log("HitZone " + col.gameObject.name);
                col.transform.parent.GetComponent<Enemy>().KillMe();
            }

            // Hit Enemy Spike
            if (col.gameObject.tag == "Spike")
            {
                // do stuff here
                Debug.Log("Spike " + col.gameObject.name);
                KillMe();
            }
        }
    }

    void KillMe()
    {
        killed = true;
        you_died_ui.SetActive(true);
        rb2d.velocity = Vector2.zero;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 100;

        // play sound
        GameObject soundObj = new GameObject("Player_Kill_Sound");
        soundObj.AddComponent<AudioSource>();
        soundObj.GetComponent<AudioSource>().playOnAwake = true;
        soundObj.GetComponent<AudioSource>().spread = 360f;
        soundObj.GetComponent<AudioSource>().clip = kill_snd;
        soundObj.GetComponent<AudioSource>().Play();
        Destroy(soundObj, 3f);

        //respawn
        /*
        rb2d.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = true;
        transform.position = startPos;
        killed = false;
        */
    }

    /*
    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 24), "doubleJump: " + doubleJump);
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), testTexture2D, ScaleMode.StretchToFill, false);
        //GUI.DrawTexture(new Rect(10, 10, 320, 200), pix.PixMaps[0].Texture);
    }
    */
}