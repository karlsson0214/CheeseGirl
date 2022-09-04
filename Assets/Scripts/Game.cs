using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject wallPrefab;
        // Start is called before the first frame update
    void Start()
    { 

        // cell size is suposed to be 1.0
        float cellSize = wallPrefab.GetComponent<Renderer>().bounds.size.x;
        int x = 0;
        int y = 0;
        int yMax = (int)Camera.main.orthographicSize;
        int xMax = (int)(Camera.main.orthographicSize * Screen.width / Screen.height);
        Debug.Log("xMax: " + xMax);
        Debug.Log("yMax: " + yMax);
        x = -xMax;
        while (x <= xMax)
        {
            //wall at bottom
            Instantiate(wallPrefab, new Vector3(x, -yMax, 0), Quaternion.identity);
            // wall at top
            Instantiate(wallPrefab, new Vector3(x, yMax, 0), Quaternion.identity);
            x = (int)(x + cellSize);
        }
        y = -yMax + 1;
        while (y < yMax)
        {
            // wall left
            Instantiate(wallPrefab, new Vector3(-xMax, y, 0), Quaternion.identity);
            // wall right
            Instantiate(wallPrefab, new Vector3(xMax, y, 0), Quaternion.identity);
            y = (int)(y + cellSize);
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
