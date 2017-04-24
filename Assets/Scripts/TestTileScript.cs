using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestTileScript : MonoBehaviour {
    private Mesh mesh;

    public int squareSize;
    private int xSize, ySize;

    private Vector3[] vertices;
    private Vector2[] uv;

    private int randomCounter = 0;

    Dictionary<int, string> gridDict = new Dictionary<int, string>();

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        xSize = ySize = squareSize;

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Tile Map";

        //vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        //Vector2[] uv = new Vector2[vertices.Length];

        int xVerts = xSize * 6;
        int quadCount = xSize * ySize;
        int verticesCount = xSize * ySize * 6;
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

            //choose tile type and add to dictionary
            int randChance = Random.Range(0, 101);

            //grass
            if (randChance <= 75)
            {
                uv[(i1 - 1) + ii] = new Vector2(0, (float).5);
                uv[i1 + ii] = new Vector2(0, 1);
                uv[(i1 + 1) + ii] = new Vector2((float).5, (float).5);
                uv[(i1 + 2) + ii] = new Vector2((float).5, 1);
                uv[(i1 + 3) + ii] = new Vector2((float).5, (float)1 / 2);
                uv[(i1 + 4) + ii] = new Vector2(0, 1);
                gridDict[i-1] = "grass";
            }
            //water
            else if (randChance > 75)
            {
                uv[(i1 - 1) + ii] = new Vector2((float).5, (float).5);
                uv[i1 + ii] = new Vector2((float).5, 1);
                uv[(i1 + 1) + ii] = new Vector2(1, (float).5);
                uv[(i1 + 2) + ii] = new Vector2(1, 1);
                uv[(i1 + 3) + ii] = new Vector2(1, (float).5);
                uv[(i1 + 4) + ii] = new Vector2((float).5, 1);
                gridDict[i-1] = "water";
            }

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
        mesh.uv = uv;

        int[] tri = new int[xSize * ySize * 6];
        for (int i = 0; i < vertices.Length; i++)
        {
            //Debug.Log("[" + i + "] = " + vertices[i]);
            tri[i] = i;
        }

        mesh.triangles = tri;
        mesh.RecalculateNormals();
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