using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TrollGame : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject grassPrefab;
    public GameObject sandPrefab;
    public GameObject crystalPrefab;
    public GameObject playerPrefab;
    public GameObject doorPrefab;
    public GameObject trollPrefab;

    private int xMax;
    private int yMax;
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        int y = 0;
        yMax = (int)Camera.main.orthographicSize;
        xMax = (int)(Camera.main.orthographicSize * Screen.width / Screen.height);
        // Wall
        x = -xMax;
        while (x <= xMax)
        {
            //wall at bottom
            Instantiate(wallPrefab, new Vector3(x, -yMax, 0), Quaternion.identity);
            // wall at top
            Instantiate(wallPrefab, new Vector3(x, yMax, 0), Quaternion.identity);
            x = (int)(x + 1);
        }
        y = -yMax + 1;
        while (y < yMax)
        {
            // wall left
            Instantiate(wallPrefab, new Vector3(-xMax, y, 0), Quaternion.identity);
            // wall right
            Instantiate(wallPrefab, new Vector3(xMax, y, 0), Quaternion.identity);
            y = (int)(y + 1);
        }

        // sand
        sandPrefab.GetComponent<SpriteRenderer>().sortingOrder = 0;
        for (int col = -xMax + 1; col < xMax; col++)
        {
            for (int row = -yMax + 1; row < yMax; row++)
            {
                Instantiate(sandPrefab, new Vector3(col, row, 0), Quaternion.identity);
            }
        }

        // troll
        Instantiate(trollPrefab, new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
