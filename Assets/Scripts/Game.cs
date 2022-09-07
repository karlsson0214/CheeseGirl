using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject grassPrefab;
    public GameObject sandPrefab;
    public GameObject crystalPrefab;
    public GameObject playerPrefab;
    public GameObject doorPrefab;
    public GameObject trollPrefab;

    private int score = 0;
    private Text scoreDisplay;
    private Door door;
    private int level = 1;
    private int scoreGoal = 10;
    private int xMax;
    private int yMax;
    // Start is called before the first frame update
    void Start()
    {

        InitLevelOne();
        //score = 10;
        //InitLevelTwo();




    }

    private void InitLevelOne()
    {
        level = 1;
        scoreGoal = 10;
        scoreDisplay = GameObject.Find("ScoreDisplay").GetComponent<Text>();
        scoreDisplay.text = "Score: " + score;

        // cell size is suposed to be 1.0
        float cellSize = wallPrefab.GetComponent<Renderer>().bounds.size.x;
        int x = 0;
        int y = 0;
        yMax = (int)Camera.main.orthographicSize;
        xMax = (int)(Camera.main.orthographicSize * Screen.width / Screen.height);
        Debug.Log("xMax: " + xMax);
        Debug.Log("yMax: " + yMax);

        // player
        Instantiate(playerPrefab, new Vector3(-xMax + 1, -yMax + 1, 0), Quaternion.identity);
        playerPrefab.GetComponent<SpriteRenderer>().sortingOrder = 10;

        // door
        GameObject doorObject = Instantiate(doorPrefab, new Vector3(-xMax + 1, -yMax + 1, 0), Quaternion.identity);
        // get door script object
        door = doorObject.GetComponent<Door>();
        doorPrefab.GetComponent<SpriteRenderer>().sortingOrder = 3;

        // Wall
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

        // sand
        sandPrefab.GetComponent<SpriteRenderer>().sortingOrder = 0;
        for (int col = -xMax + 1; col < xMax; col++)
        {
            for (int row = -yMax + 1; row < yMax; row++)
            {
                Instantiate(sandPrefab, new Vector3(col, row, 0), Quaternion.identity);
            }
        }
        List<Vector2> noGrassPoints = new List<Vector2>();
        noGrassPoints.Add(new Vector2(-xMax + 1, -yMax + 1));
        AddGrass(noGrassPoints);

        // crystal
        crystalPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        for (int i = 0; i < 10; ++i)
        {
            x = Random.Range(-xMax + 1, xMax - 1);
            y = Random.Range(-yMax + 1, yMax - 1);
            Instantiate(crystalPrefab, new Vector3(x, y, 0), Quaternion.identity);

        }
    }
    private void AddGrass(List<Vector2> noGrassList = null)
    {
        // grass on top of sand
        grassPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;
        for (int col = -xMax + 1; col < xMax; col++)
        {
            for (int row = -yMax + 1; row < yMax; row++)
            {
                // no grass in lower left corner
                if (noGrassList != null)
                {
                    bool doAddPoint = true;
                    foreach(Vector2 point in noGrassList)
                    {
                        if (point.x == col && point.y == row)
                        {
                            // no grass
                            doAddPoint = false;
                            break;
                        }
                    }
                    if (doAddPoint)
                    {
                        Instantiate(grassPrefab, new Vector3(col, row, 0), Quaternion.identity);
                    }
                }
                /*
                if (!(row == -yMax + 1 && col == -xMax + 1))
                {
                    
                }
                */
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int points)
    {
        score += points;
        scoreDisplay.text = "Score: " + score;
        if (score >= scoreGoal)
        {
            door.Open();
        }
    }

    public void NextLevel()
    {
        if (level == 1)
        {
            level = 2;
            InitLevelTwo();
        }
    }

    private void InitLevelTwo()
    {
        scoreGoal = 20;
        // remove all grass
        var grassObjects = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject grassObject in grassObjects)
        {
            Destroy(grassObject);
        }
        
        // close door
        door.Close();
        List<Vector2> noGrassPoints = new List<Vector2>();
        noGrassPoints.Add(new Vector2(-xMax + 1, -yMax + 1));
        // add crystals
        // crystal
        crystalPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        trollPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        for (int i = 0; i < 10; ++i)
        {
            int x = Random.Range(-xMax + 2, xMax - 2);
            int y = Random.Range(-yMax + 2, yMax - 2);
            Instantiate(crystalPrefab, new Vector3(x, y, 0), Quaternion.identity);
            // dice 4 sides
            int trollSide = Random.Range(0, 3);
            if (trollSide == 0)
            {
                x = x + 1;
            }
            else if (trollSide == 1)
            {
                x = x - 1;
            }
            else if (trollSide == 2)
            {
                y = y + 1;
            }
            else if (trollSide == 3)
            {
                y = y - 1;
            }
            // add garding troll
            Instantiate(trollPrefab, new Vector3(x, y, 0), Quaternion.identity);
            noGrassPoints.Add(new Vector2(x, y));

        }
        // add grass
        AddGrass(noGrassPoints);

    }

}
