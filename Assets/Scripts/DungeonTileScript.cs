using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DungeonTileScript : MonoBehaviour {

    private Mesh mesh;

    public int squareSize;
    private int xSize, ySize;

    private int xVerts;
    private int quadCount;
    private int verticesCount;

    private Vector3[] vertices;
    private Vector2[] uv;

    private int randomCounter = 0;
    private int randChance = 0;
    private int roomQuad = 0;
    private int roomCount = 5;

    private int emptyCount = 0;
    private int grassCount = 0;
    private int waterCount = 0;
    private int tileUp;
    private int tileDown;
    private int tileLeft;
    private int tileRight;

    public GameObject ladderObject;


    //List<Rooms> roomList = new List<Rooms>();


    Dictionary<int, string> dunDict = new Dictionary<int, string>();
    Dictionary<int, bool> isConnectedDict = new Dictionary<int, bool>();
    List<List<int>> roomList = new List<List<int>>();
    //List<float> roomDistList = new List<float>();

    DungeonHelperScript helperScript = new DungeonHelperScript();


    private void Awake()
    {
        roomCount = Random.Range(5, 9);
        squareSize = 50;
    }

    private void Start()
    {
        //Camera.main.transform.position = new Vector3(squareSize / 2, squareSize / 2, -10);
        Generate();
    }

    private void Generate()
    {
        xSize = ySize = squareSize;

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Dungeon Tile Map";

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

       
            //Assign UVs in random order
            for (int i = 0; i < roomCount; i++)
            {
            int roomQuad = helperScript.AcceptableRoomQuad(quadCount, dunDict);
               
            //decide room dimensions
            int roomXSize = Random.Range(3, 8);
            int roomYSize = Random.Range(3, 8);

            //add edge quads to dictionary
            helperScript.ForbidEdgeQuads(quadCount, dunDict, squareSize);

            //check if room can be built
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
            bool isAcceptable = helperScript.AcceptableForDim(roomQuad, quadCount, dunDict, roomXSize, roomYSize, squareSize);
            
            while (!isAcceptable && randomCounter < quadCount)
            {
                roomQuad = helperScript.AcceptableRoomQuad(quadCount, dunDict);
                isAcceptable = helperScript.AcceptableForDim(roomQuad, quadCount, dunDict, roomXSize, roomYSize, squareSize);
                randomCounter++;
            }
            randomCounter = 0;

            //store this room's centerQuad, xDim, and yDim in roomList  
            //for this data structure, roomList[index] will refer to room ID starting with 0
            //and roomList[index][0] is roomQuad, roomList[index][1] is roomXSize, and roomList[index][2] is roomYsize
            List<int> subList = new List<int>() { roomQuad, roomXSize, roomYSize };
            roomList.Add(subList);            

            Debug.Log("roomList contents at index " + i + ": " + roomList[i][0] + ", " + roomList[i][1] + ", " + roomList[i][2]);
            // Debug.Log("roomQuad: " + roomQuad);
            //Debug.Log("X: " + roomXSize + " Y: " + roomYSize);
            //set tiles
            helperScript.setTiles(roomQuad, quadCount, dunDict, roomXSize, roomYSize, squareSize, uv);

            

        }

        //method 1
        /*//create path
        for (int i = 0; i < roomCount; i++)
        {
            helperScript.CreatePath(roomList, i, squareSize, uv, dunDict);
        }

        helperScript.WallsForPaths(quadCount, squareSize, uv, dunDict);

        for (int i = 0; i < roomCount; i++)
        {
            //roomsConnected();
            helperScript.CreatePath2(roomList, i, squareSize, uv, dunDict);
        }
        helperScript.WallsForPaths(quadCount, squareSize, uv, dunDict);
        for (int i = 0; i < roomCount; i++)
        {
            helperScript.CreatePath(roomList, i, squareSize, uv, dunDict);
        }

        helperScript.WallsForPaths(quadCount, squareSize, uv, dunDict);*/


        //method 2
        //start at room 0, then create path to closest room
        //then create path from that room to the next closest room that isn't the room that just pathed into it
        //continue til all rooms are connected
        
        List<int> ConnectedRooms = new List<int>();
        for (int i = 0, index = 0; i < (roomCount-1); i++)
        {
            //here, i is the index of room to start path and nearest is index of room to receive path
            
            int nearest = helperScript.ClosestRoom(roomList, index, squareSize, ConnectedRooms);
            Debug.Log("creating connection from room " + index + " to room " + nearest);

            helperScript.CreatePathAlt(roomList, index, nearest, squareSize, uv, dunDict);
            ConnectedRooms.Add(index);
            ConnectedRooms.Add(nearest);
            index = nearest;  
        }

        //add one more path between two random rooms
        int randomStart = Random.Range(0, roomCount);
        int randomEnd = Random.Range(0, roomCount);
        while (randomEnd == randomStart)
        {
            randomEnd = Random.Range(0, roomCount);
        }
        Debug.Log("creating RANDOM connection from room " + randomStart + " to room " + randomEnd);
        helperScript.CreatePathAlt(roomList, randomStart, randomEnd, squareSize, uv, dunDict);

        //build walls around paths
        helperScript.WallsForPaths(quadCount, squareSize, uv, dunDict);

        //clear ConnectedRooms list for next iteration
        ConnectedRooms.Clear();

        //apply the mesh uv
        mesh.uv = uv;

        //place objects (ladder and starting position)
        helperScript.PlaceObjects(quadCount, squareSize, uv, dunDict, ladderObject);
    }



    /*private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }*/
}
