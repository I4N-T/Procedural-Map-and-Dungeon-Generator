using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TileMeshScript : MonoBehaviour
{

    private Mesh mesh;

    public int squareSize;
    private int xSize, ySize;

    private int mapType;

    private int xVerts;
    private int quadCount;
    private int verticesCount;

    private Vector3[] vertices;
    private Vector2[] uv;

    private int randomCounter = 0;
    private int randChance = 0;
    private int uvRand = 0;

    private int emptyCount = 0;
    private int grassCount = 0;
    private int waterCount = 0;
    private int tileUp;
    private int tileDown;
    private int tileLeft;
    private int tileRight;

    public GameObject dungeonObject;

    //for singleton; ensure only 1 GameManager object
    private static TileMeshScript _instance = null;


    Dictionary<int, string> gridDict = new Dictionary<int, string>();

    TileMeshHelperFunctions helperScript = new TileMeshHelperFunctions();


    private void Awake()
    {
        //Check if instance already exists
        if (_instance == null)

            //if not, set instance to this
            _instance = this;

        //If instance already exists and it's not this:
        else if (_instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        squareSize = MenuScript.mapSize;
        mapType = MenuScript.mapType;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Camera.main.transform.position = new Vector3(squareSize / 2, squareSize / 2, -10);
        Generate();  
    }

    private void Generate()
    {
        xSize = ySize = squareSize;

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Tile Map";

        //helperScript.AssignVertsAndTris(xVerts, xSize, ySize, quadCount, verticesCount, vertices, uv, mesh);

        //vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        //Vector2[] uv = new Vector2[vertices.Length];

        xVerts = xSize * 6;
        quadCount = xSize * ySize;
        verticesCount = xSize * ySize * 6;
        vertices = new Vector3[verticesCount];
        uv = new Vector2[vertices.Length];
        for (int i = 1, i1 = 1, ii = 0, x0 = 0, x1 = 1, y = 0; i < verticesCount; i++)
        {
            //assign vertices
            vertices[(i1 - 1) + ii] = new Vector3(x0, 0 + y);
            vertices[i1 + ii] = new Vector3(x0, 1 + y);
            vertices[(i1 + 1) + ii] = new Vector3(x1, 0 + y);
            vertices[(i1 + 2) + ii] = new Vector3(x1, 1 + y);
            vertices[(i1 + 3) + ii] = new Vector3(x1, 0 + y);
            vertices[(i1 + 4) + ii] = new Vector3(x0, 1 + y);

            //If tiletype is Random
            if (mapType == 1)
            {
                helperScript.SetTileTypeRandom(i1, ii, i, uv, gridDict);
            }

            /*if (mapType == 2)
            {
                helperScript.SetTileTypeGL(uvRand, i1, ii, i, quadCount, squareSize, uv, gridDict);
            }*/

            //update increment counters
            if (i % 6 == 0)
            {
                ii += 5;
                i1++;
                x0++;
                x1++;
            }
            if (i % xVerts == 0)
            {
                y++;
                x0 = 0;
                x1 = 1;
            }
        }

        mesh.vertices = vertices;

        int[] tri = new int[xSize * ySize * 6];
        for (int i = 0; i < vertices.Length; i++)
        {
            //Debug.Log("[" + i + "] = " + vertices[i]);
            tri[i] = i;
        }

        mesh.triangles = tri;
        mesh.RecalculateNormals();

        //riverworld here?
        if (mapType == 3)
        {
            helperScript.SetTileTypeRW(quadCount, squareSize, uv, gridDict);
        }
        
        if (mapType != 3 && mapType != 1)
        {
            //Assign UVs in random order
            for (int i = 0; i < quadCount; i++)
            {
                uvRand = Random.Range(0, quadCount);

                //check if tile type is already set for this quad
                while (gridDict.ContainsKey(uvRand) && randomCounter < quadCount)
                {
                    //uvRand = Random.Range(0, quadCount);
                    uvRand = randomCounter;
                    randomCounter++;
                }
                randomCounter = 0;

                //Debug.Log("uvRand: " + uvRand);
                //Check type of tile surrounding uvRand and store info in array
                int[] tileTypesArray = helperScript.CheckTileTypes(uvRand, squareSize, quadCount, gridDict);
                emptyCount = tileTypesArray[0];
                grassCount = tileTypesArray[1];
                waterCount = tileTypesArray[2];

                //set tile types
                //which map type chosen?
                //scattered waters
                if (mapType == 0)
                {
                    helperScript.SetTileTypeSW(emptyCount, grassCount, waterCount, uv, uvRand, gridDict);
                }
                //Random THEN DONT RUN A SCRIPT HERE; IT ALREADY HAPPENED IN FIRST LOOP
                //helperScript.SetTileTypeRandom(verticesCount, uv, gridDict, xVerts);

                //Great Lake
                if (mapType == 2)
                {
                    helperScript.SetTileTypeGL(uvRand, quadCount, squareSize, uv, gridDict);
                }
            }
        }
        mesh.uv = uv;

        //if scattered waters -> cleanup stragglers with Generate2()
        if (mapType == 0)
        {
            Generate2();
            Generate2();
        }

        //place dungeon at random tile
        helperScript.PlaceObjects(quadCount, gridDict, squareSize, dungeonObject);
    }



        //Loop back to fix stragglers
        void Generate2()
        {
            for (uvRand = 0; uvRand < quadCount; uvRand++)
            {

                int[] tileTypesArray = helperScript.CheckTileTypes(uvRand, squareSize, quadCount, gridDict);
                emptyCount = tileTypesArray[0];
                grassCount = tileTypesArray[1];
                waterCount = tileTypesArray[2];

                //all surrounding tiles grass
                if (waterCount == 0 && emptyCount == 0 && grassCount > 1)
                {
                    helperScript.TileType(uvRand, 0, 1, uv, gridDict);
                }

                //all surrounding tiles water
                else if (grassCount == 0 && emptyCount == 0 && waterCount > 1)
                {
                    helperScript.TileType(uvRand, 1, 0, uv, gridDict);
                }

                //no empties, water > grass
                else if (waterCount > grassCount && emptyCount == 0)
                {
                    //Debug.Log(6);
                    //choose tile type and add to dictionary
                    randChance = Random.Range(0, 101);

                    helperScript.TileType(uvRand, randChance, 5, uv, gridDict);
                }

                //three grass, one water
                else if (waterCount == 1 && grassCount == 3)
                {
                    //choose tile type and add to dictionary
                    randChance = Random.Range(0, 101);

                    helperScript.TileType(uvRand, randChance, 95, uv, gridDict);
                }

                emptyCount = 0;
                grassCount = 0;
                waterCount = 0;
            }
            mesh.uv = uv;
        }

    }

