using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject wallPrefab;
        // Start is called before the first frame update
    void Start()
    { 


        float cellSize = wallPrefab.GetComponent<Renderer>().bounds.size.x;
        int x = 0;
        int y = 0;
        Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
