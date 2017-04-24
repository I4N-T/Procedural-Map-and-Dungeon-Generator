using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileScript : MonoBehaviour
{

    public GameObject grassTile;
    public GameObject waterTile;
    public GameObject stoneTile;
    public int sizeX = 5;
    public int sizeY = 5;
    private int randox;
    private int randoy;

    int x = 0;
    int y = 0;

    private List<int> xUsed = new List<int>();
    private List<int> yUsed = new List<int>();
    int randomCounter = 0;

    private Transform boardHolder;

    private List<Vector3> gridPositions = new List<Vector3>();
    private List<Vector3> gridPositionsUsed = new List<Vector3>();
    Dictionary<string, string> gridDict = new Dictionary<string, string>();

    private string keyString;
    private bool firstTime = true;

    void GridSetUp()
    {
        gridPositions.Clear();

        for (x = 0; x < sizeX; x++)
        {
            for (y = 0; y < sizeY; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0));
            }
        }
        Debug.Log(gridPositions.Count);
    }

    void PlaceTiles()
    {
        boardHolder = new GameObject("Board").transform;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            //Debug.Log("cycle: " + i);

            //randoms because 
            randox = Random.Range(0, x);
            randoy = Random.Range(0, y);
            int randox1 = randox;
            int randoy1 = randoy;

            //Debug.Log("randox initial: " + randox);
            //Debug.Log("randoy initial: " + randoy);

            //if this coordinate already used, get new coordinates
            if (firstTime == false)
            {
                string testString = randox.ToString() + "~" + randoy.ToString();
                
                while (gridDict.ContainsKey(randox.ToString() + "~" + randoy.ToString()) && randomCounter < x)
                {
                    randox = randomCounter;
                    randomCounter++;
                }
                randomCounter = 0;
                if (gridDict.ContainsKey(randox.ToString() + "~" + randoy.ToString()))
                {
                    randoy = 0;
                }
                TestFunc();
                
               
       

            }
            firstTime = false;

            //first test. 50% grass, 25% water, 25% stone
            int randChance = Random.Range(0, 101);

            if (randChance <= 50)
            {
                GameObject testTile = Instantiate(grassTile, new Vector3(randox, randoy, 0), Quaternion.identity, boardHolder) as GameObject;
                testTile.hideFlags = HideFlags.HideInHierarchy;
                keyString = randox.ToString() + "~" + randoy.ToString();
                gridDict[keyString] = "grass";
                //Debug.Log(keyString);
                //xUsed.Add(randox);
                //yUsed.Add(randoy);
            }
            else if (randChance > 50 && randChance <= 75)
            {
                GameObject testTile = Instantiate(waterTile, new Vector3(randox, randoy, 0), Quaternion.identity, boardHolder) as GameObject;
                testTile.hideFlags = HideFlags.HideInHierarchy;
                keyString = randox.ToString() + "~" + randoy.ToString();
                gridDict[keyString] = "water";
                //Debug.Log(keyString);
            }
            else if (randChance > 75)
            {
                GameObject testTile = Instantiate(stoneTile, new Vector3(randox, randoy, 0), Quaternion.identity, boardHolder) as GameObject;
                testTile.hideFlags = HideFlags.HideInHierarchy;
                keyString = randox.ToString() + "~" + randoy.ToString();
                gridDict[keyString] = "stone";
                //Debug.Log(keyString);
            }
        }
    }

    /*void PlaceTiles()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -49; x < 50; x++)
        {
            for (int y = -49; y < 50; y++)
            {
                GameObject testTile = Instantiate(grassTile, new Vector3(x, y, 0), Quaternion.identity, boardHolder) as GameObject;
            }
        }
    }*/


    void TestFunc()
    {
        for (int i = 0; i < gridPositions.Count; i++)
        {
            while (gridDict.ContainsKey(randox.ToString() + "~" + randoy.ToString()) && randomCounter < x)
            {
                randox = randomCounter;
                randomCounter++;
            }
            if (gridDict.ContainsKey(randox.ToString() + "~" + randoy.ToString()))
            {
                randoy++;
            }
            randomCounter = 0;
        }
    }

    void Start()
    {
        GridSetUp();
        PlaceTiles();
    }
}
