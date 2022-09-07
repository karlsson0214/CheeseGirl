using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class Troll : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5;
    private int direction = 0; // right
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0;
        //rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // no grass => can move in that direction
        //ResetSpeed();
        if (direction == 0)
        {
            rb.MovePosition(new Vector2(speed, 0) * Time.deltaTime + rb.position);
        }
        else if (direction == 1)
        {
            rb.MovePosition(new Vector2(0, speed) * Time.deltaTime + rb.position);
        }
        else if (direction == 2)
        {
            rb.MovePosition(new Vector2(-speed, 0) * Time.deltaTime + rb.position );
        }
        else if (direction == 3)
        {
            rb.MovePosition(new Vector2(0, -speed) * Time.deltaTime + rb.position);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // change direction
        direction = Random.Range(0, 4);
        if (direction == 0 || direction == 2)
        {
            SnapToGridY();
        }
        else
        {
            SnapToGridX();
        }
        //Debug.Log("Troll direction: " + direction);
        //ResetSpeed(); 
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // change direction
        direction = Random.Range(0, 4);
        //ResetSpeed();
    }
    private void SnapToGridX()
    {
        rb.position = new Vector2((float)System.Math.Round(rb.position.x), rb.position.y);
    }
    private void SnapToGridY()
    {
        rb.position = new Vector2(rb.position.x, (float)System.Math.Round(rb.position.y));
    }
    private void ResetSpeed()
    {
        int x = 0;
        int y = 0;
        if (direction == 0)
        {
            x = 1;
        }
        else if (direction == 1)
        {
            y = 1;
        }
        else if (direction == 2)
        {
            x = -1;
        }
        else if (direction == 3)
        {
            y = -1;
        }
        //Debug.Log("Troll speed: " + x + ", " + y);
        rb.velocity = new Vector2(x, y) * speed;
    }

}
