using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5;
    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        /*
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Score");
        foreach(var obj in objects)
        {
            Destroy(obj);
        }
        */
        

    }

    // Update is called once per frame
    void Update()
    {
        
        // bug fix
        // background
        // I created a UI Text element, named Score, in Hierarchy
        // is somehow disappeared. Maby it was a prefab first.
        // I made a new UI Text, named ScoreDisplay
        // I cannot get rid of Score. It is no longer i Hierarchy but appeare in Game.
        GameObject obj = GameObject.Find("Score");
        if (obj != null)
        {
            Destroy(obj);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // move left
            SnapToGridY();
            rb.velocity = new Vector2(-speed, 0);
            Debug.Log("move left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // move right
            SnapToGridY();
            rb.velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // move down
            SnapToGridX();
            rb.velocity = new Vector2(0, -speed);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            // move up
            SnapToGridX();
            rb.velocity = new Vector2(0, speed);
        }
        else
        {
            // stop
            rb.velocity = Vector2.zero;
        }
        
    }
    private void SnapToGridX()
    {
        rb.position = new Vector2((float)Math.Round(rb.position.x), rb.position.y);
    }
    private void SnapToGridY()
    {
        rb.position = new Vector2(rb.position.x, (float)Math.Round(rb.position.y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Grass"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("player hit trigger object");
        
        if (collider.gameObject.name.StartsWith("Crystal"))
        {
            Destroy(collider.gameObject);

            // score
            // get the Game script object but only once
            if (game == null)
            {
                game = GameObject.Find("Game").GetComponent<Game>();
                Debug.Log(game.GetType().Name);
            }
            game.AddScore(1);
        }
        else if (collider.gameObject.name.StartsWith("Door"))
        {
            // get Door script object
            Door door = collider.gameObject.GetComponent<Door>();
            if (door.IsOpen)
            {
                Debug.Log("next level");
                game.NextLevel();
            }
        }
    }
}
