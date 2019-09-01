using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private float speed = 0.5f;
    private SpriteRenderer spriteRenderer;
    private float colourFade = 1f;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colourFade -= 1f * Time.deltaTime;
        spriteRenderer.color = new Color(1f, 1f, 1f, colourFade);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Destroy(gameObject, 5f);
    }
}
