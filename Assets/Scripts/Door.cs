using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openDoorSprite;
    private Sprite closedDoorSprite;
    private bool isOpen = false;

    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        closedDoorSprite = GetComponent<SpriteRenderer>().sprite;
        Debug.Log("closed door sprite: " + closedDoorSprite.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Open()
    {
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        isOpen = true;
    }
    public void Close()
    {
        GetComponent <SpriteRenderer>().sprite = closedDoorSprite;
        isOpen = false;
    }
}
