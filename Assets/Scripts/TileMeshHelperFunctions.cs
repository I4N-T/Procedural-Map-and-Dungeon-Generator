using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class TileMeshHelperFunctions {

	/*public void AssignVertsAndTris(int xVerts, int xSize, int ySize, int quadCount, int verticesCount, Vector3[] vertices, Vector2[] uv, Mesh mesh)
    {
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
    }

    public int UVAssignRandom(int uvRand, int randomCounter, int verticesCount, int quadCount, Dictionary<int, string> gridDict)
    {
        //Assign UVs in random order
        for (int i = 0; i <= verticesCount + 1; i++)
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
        }
    }*/

    public int[] CheckTileTypes(int uvRand, int squareSize, int quadCount, Dictionary<int, string> gridDict)
    {
        //init types
        int emptyCount = 0;
        int grassCount = 0;
        int waterCount = 0;

        //check surrounding tiles types
        int tileLeft = uvRand - 1;
        int tileUp = uvRand + squareSize;
        int tileRight = uvRand + 1;
        int tileDown = uvRand - squareSize;


        //CORNERS
        //corner bottom left
        if (uvRand == 0)
        {
            //Debug.Log("check corner BL");
            //check tile above
            if (!gridDict.ContainsKey(tileUp))
            {
                emptyCount++;
            }
            else if (gridDict[tileUp] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileUp] == "water")
            {
                waterCount++;
            }

            //check tile right
            if (!gridDict.ContainsKey(tileRight))
            {
                emptyCount++;
            }
            else if (gridDict[tileRight] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileRight] == "water")
            {
                waterCount++;
            }
        }

        //corner bottom right
        else if (uvRand == squareSize - 1)
        {
            //Debug.Log("check corner BR");
            //check tile left
            if (!gridDict.ContainsKey(tileLeft))
            {
                emptyCount++;
            }
            else if (gridDict[tileLeft] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileLeft] == "water")
            {
                waterCount++;
            }

            //check tile above
            if (!gridDict.ContainsKey(tileUp))
            {
                emptyCount++;
            }
            else if (gridDict[tileUp] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileUp] == "water")
            {
                waterCount++;
            }
        }

        //corner top right
        else if (uvRand == quadCount - 1)
        {
            //Debug.Log("check corner TR");
            //check tile below
            if (!gridDict.ContainsKey(tileDown))
            {
                emptyCount++;
            }
            else if (gridDict[tileDown] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileDown] == "water")
            {
                waterCount++;
            }

            //check tile left
            if (!gridDict.ContainsKey(tileLeft))
            {
                emptyCount++;
            }
            else if (gridDict[tileLeft] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileLeft] == "water")
            {
                waterCount++;
            }
        }

        //corner top left
        else if (uvRand == quadCount - squareSize)
        {
            //Debug.Log("check corner TL");
            //check tile right
            if (!gridDict.ContainsKey(tileRight))
            {
                emptyCount++;
            }
            else if (gridDict[tileRight] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileRight] == "water")
            {
                waterCount++;
            }

            //check tile below
            if (!gridDict.ContainsKey(tileDown))
            {
                emptyCount++;
            }
            else if (gridDict[tileDown] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileDown] == "water")
            {
                waterCount++;
            }
        }

        //EDGES

        //bottom edge
        else if (uvRand > 0 && uvRand < (squareSize - 1))
        {
            //Debug.Log("check edge B");
            //check tile left
            if (!gridDict.ContainsKey(tileLeft))
            {
                emptyCount++;
            }
            else if (gridDict[tileLeft] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileLeft] == "water")
            {
                waterCount++;
            }

            //check tile above
            if (!gridDict.ContainsKey(tileUp))
            {
                emptyCount++;
            }
            else if (gridDict[tileUp] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileUp] == "water")
            {
                waterCount++;
            }

            //check tile right
            if (!gridDict.ContainsKey(tileRight))
            {
                emptyCount++;
            }
            else if (gridDict[tileRight] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileRight] == "water")
            {
                waterCount++;
            }
        }

        //top edge
        else if (uvRand > (quadCount - squareSize) && uvRand < (quadCount - 1))
        {
            //Debug.Log("check edge T");
            //check tile left
            if (!gridDict.ContainsKey(tileLeft))
            {
                emptyCount++;
            }
            else if (gridDict[tileLeft] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileLeft] == "water")
            {
                waterCount++;
            }

            //check tile right
            if (!gridDict.ContainsKey(tileRight))
            {
                emptyCount++;
            }
            else if (gridDict[tileRight] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileRight] == "water")
            {
                waterCount++;
            }

            //check tile below
            if (!gridDict.ContainsKey(tileDown))
            {
                emptyCount++;
            }
            else if (gridDict[tileDown] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileDown] == "water")
            {
                waterCount++;
            }
        }

        //left edge
        else if (uvRand != 0 && uvRand != (quadCount - squareSize) && uvRand % squareSize == 0)
        {
            //Debug.Log("check edge L");
            //check tile above
            if (!gridDict.ContainsKey(tileUp))
            {
                emptyCount++;
            }
            else if (gridDict[tileUp] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileUp] == "water")
            {
                waterCount++;
            }

            //check tile right
            if (!gridDict.ContainsKey(tileRight))
            {
                emptyCount++;
            }
            else if (gridDict[tileRight] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileRight] == "water")
            {
                waterCount++;
            }

            //check tile below
            if (!gridDict.ContainsKey(tileDown))
            {
                emptyCount++;
            }
            else if (gridDict[tileDown] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileDown] == "water")
            {
                waterCount++;
            }
        }

        //right edge
        else if (uvRand != (squareSize - 1) && uvRand != (quadCount - 1) && (uvRand + 1) % squareSize == 0)
        {
            //Debug.Log("check edge R");
            //check tile below
            if (!gridDict.ContainsKey(tileDown))
            {
                emptyCount++;
            }
            else if (gridDict[tileDown] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileDown] == "water")
            {
                waterCount++;
            }

            //check tile left
            if (!gridDict.ContainsKey(tileLeft))
            {
                emptyCount++;
            }
            else if (gridDict[tileLeft] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileLeft] == "water")
            {
                waterCount++;
            }

            //check tile above
            if (!gridDict.ContainsKey(tileUp))
            {
                emptyCount++;
            }
            else if (gridDict[tileUp] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileUp] == "water")
            {
                waterCount++;
            }
        }

        //MIDDLE
        else
        {
            //Debug.Log("check MIDDLE");
            //check tile left
            if (!gridDict.ContainsKey(tileLeft))
            {
                emptyCount++;
            }
            else if (gridDict[tileLeft] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileLeft] == "water")
            {
                waterCount++;
            }

            //check tile above
            if (!gridDict.ContainsKey(tileUp))
            {
                emptyCount++;
            }
            else if (gridDict[tileUp] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileUp] == "water")
            {
                waterCount++;
            }

            //check tile right
            if (!gridDict.ContainsKey(tileRight))
            {
                emptyCount++;
            }
            else if (gridDict[tileRight] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileRight] == "water")
            {
                waterCount++;
            }

            //check tile below
            if (!gridDict.ContainsKey(tileDown))
            {
                emptyCount++;
            }
            else if (gridDict[tileDown] == "grass")
            {
                grassCount++;
            }
            else if (gridDict[tileDown] == "water")
            {
                waterCount++;
            }
        }
        return new[] { emptyCount, grassCount, waterCount };
    }

    public void SetTileTypeSW(int emptyCount, int grassCount, int waterCount, Vector2[] uv, int uvRand, Dictionary<int, string> gridDict)
    {
        int randChance;

        //all surrounding tiles empty
        if (grassCount == 0 && waterCount == 0 && emptyCount > 1)
        {
            //Debug.Log(1);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 90, uv, gridDict);
        }

        //all surrounding tiles grass
        else if (waterCount == 0 && emptyCount == 0 && grassCount > 1)
        {
            TileType(uvRand, 0, 1, uv, gridDict);
        }

        //all surrounding tiles water
        else if (grassCount == 0 && emptyCount == 0 && waterCount > 1)
        {
            TileType(uvRand, 1, 0, uv, gridDict);

        }

        //no empties, grass and water equal
        else if (grassCount == waterCount && emptyCount == 0)
        {
            //Debug.Log(4);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 10, uv, gridDict);
        }

        //no empties, grass > water
        else if (grassCount > waterCount && emptyCount == 0)
        {
            //Debug.Log(5);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 95, uv, gridDict);
        }

        //no empties, water > grass
        else if (waterCount > grassCount && emptyCount == 0)
        {
            //Debug.Log(6);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 5, uv, gridDict);
        }

        //empty exists, grass = water
        else if (grassCount == waterCount && emptyCount > 0)
        {
            //Debug.Log(6);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 10, uv, gridDict);
        }

        //empty exists, grass > water
        else if (grassCount > waterCount && emptyCount > 0)
        {
            //Debug.Log(7);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 85, uv, gridDict);
        }

        //empty exists, water > grass
        else if (waterCount > grassCount && emptyCount > 0)
        {
            //Debug.Log(8);
            //choose tile type and add to dictionary
            randChance = Random.Range(0, 101);

            TileType(uvRand, randChance, 85, uv, gridDict);
        }
    }

    public void SetTileTypeRandom(int i1, int ii, int i, Vector2[] uv, Dictionary<int, string> gridDict)
    {
  
            //choose tile type and add to dictionary
            int randChance = Random.Range(0, 101);

            //grass
            if (randChance <= 85)
            {
                uv[(i1 - 1) + ii] = new Vector2(0, (float).5);
                uv[i1 + ii] = new Vector2(0, 1);
                uv[(i1 + 1) + ii] = new Vector2((float).5, (float).5);
                uv[(i1 + 2) + ii] = new Vector2((float).5, 1);
                uv[(i1 + 3) + ii] = new Vector2((float).5, (float)1 / 2);
                uv[(i1 + 4) + ii] = new Vector2(0, 1);
                gridDict[i - 1] = "grass";
            }
            //water
            else if (randChance > 85)
            {
                uv[(i1 - 1) + ii] = new Vector2((float).5, (float).5);
                uv[i1 + ii] = new Vector2((float).5, 1);
                uv[(i1 + 1) + ii] = new Vector2(1, (float).5);
                uv[(i1 + 2) + ii] = new Vector2(1, 1);
                uv[(i1 + 3) + ii] = new Vector2(1, (float).5);
                uv[(i1 + 4) + ii] = new Vector2((float).5, 1);
                gridDict[i - 1] = "water";
            } 

        }

    //Great Lake
    public void SetTileTypeGL(int uvRand, int quadCount, int squareSize, Vector2[] uv, Dictionary<int, string> gridDict)
    {

        int factor = squareSize / 4;
        int factorUp = factor + 1;
        int factorVert = factor * squareSize;
        int factorVertUp = factorUp * squareSize;
        int centerQuad = (squareSize * (squareSize / 2)) + (squareSize / 2);

        //for odd squarSize
        if ((squareSize % 2) != 0)
        {
            //if quad is horizontal strip through center quad
            if (uvRand >= (centerQuad - factor) && uvRand <= (centerQuad + factor))
            {
                //this loop is vertical strip at each point in horizontal center strip
                for (int uvCoord = (uvRand - factorVert); uvCoord <= (uvRand + (factorVert)); uvCoord += squareSize)
                {
                    //Debug.Log(uvCoord);
                    //choose tile type and add to dictionary
                    int randChance = Random.Range(0, 101);


                    //if this quad is left or right edge AND large map
                    if ((uvRand == (centerQuad - factor) || uvRand == (centerQuad + factor)) && squareSize > 11)
                    {
                        TileType(uvRand, randChance, 100, uv, gridDict);

                    }
                    //if this quad is left or right edge AND small map
                    else if ((uvRand == (centerQuad - factor) || uvRand == (centerQuad + factor)) && squareSize <= 11)
                    {
                        TileType(uvCoord, randChance, 20, uv, gridDict);
                    }

                    //if this quad is top or bottom edge AND large map
                    else if ((uvCoord == (uvRand - factorVert) || uvCoord == (uvRand + factorVert)) && squareSize > 11)
                    {
                        TileType(uvCoord, randChance, 100, uv, gridDict);
                    }
                    //if this quad is top or bottom edge AND small map
                    else if ((uvCoord == (uvRand - factorVert) || uvCoord == (uvRand + factorVert)) && squareSize <= 11)
                    {
                        TileType(uvCoord, randChance, 20, uv, gridDict);
                    }

                    //if second to top or bottom edge AND medium to large map
                    else if ((uvCoord == (uvRand - (factorVert - squareSize)) || uvCoord == (uvRand + (factorVert - squareSize))) && squareSize > 11)
                    {
                        int cornerOffset = 2;
                        if (squareSize > 37)
                        {
                            cornerOffset = 3;
                        }

                        //quad IS corner or near corner
                        if (uvRand <= ((centerQuad - factor) + cornerOffset) || (uvRand >= ((centerQuad + factor) - cornerOffset)))
                        {
                            TileType(uvCoord, randChance, 100, uv, gridDict);
                        }
                        //NOT corners or near corners
                        else
                        {
                            TileType(uvCoord, randChance, 0, uv, gridDict);
                        }
                    }

                    //if second to left or right edge AND medium to large map
                    else if ((uvRand == (centerQuad - (factor - 1)) || uvRand == (centerQuad + (factor - 1))) && squareSize > 11)
                    {
                        int cornerOffset = 2 * squareSize;
                        if (squareSize > 37 && squareSize <= 51)
                        {
                            cornerOffset = 3 * squareSize;
                        }
                        else if (squareSize > 51)
                        {
                            cornerOffset = 5 * squareSize;
                        }

                        //quad IS corner or near corner
                        if (uvCoord <= (uvRand - factorVert) + cornerOffset || uvCoord >= (uvRand + factorVert) - cornerOffset)
                        {
                            TileType(uvCoord, randChance, 100, uv, gridDict);
                        }
                        //NOT corner or near corner
                        else
                        {
                            TileType(uvCoord, randChance, 0, uv, gridDict);
                        }
                    }

                    else
                    {
                        TileType(uvCoord, randChance, 1, uv, gridDict);
                    }
                }
            }

            else
            {
                TileType(uvRand, 0, 100, uv, gridDict);
            }
        }

        //if EVEN squareSize
        else if ((squareSize % 2) == 0)
        {
            //if quad is horizontal strip through center quad
            if (uvRand >= (centerQuad - factorUp) && uvRand <= (centerQuad + factor))
            {
                //this loop is vertical strip at each point in horizontal center strip
                for (int uvCoord = (uvRand - factorVertUp); uvCoord <= (uvRand + (factorVert)); uvCoord += squareSize)
                {
                    //Debug.Log(uvCoord);
                    //choose tile type and add to dictionary
                    int randChance = Random.Range(0, 101);

                    //if this quad is left or right edge AND large map
                    if ((uvRand == (centerQuad - factorUp) || uvRand == (centerQuad + factor)) && squareSize > 10)
                    {
                        TileType(uvRand, randChance, 100, uv, gridDict);

                    }
                    //if this quad is left or right edge AND small map
                    else if ((uvRand == (centerQuad - factorUp) || uvRand == (centerQuad + factor)) && squareSize <= 10)
                    {
                        TileType(uvCoord, randChance, 20, uv, gridDict);
                    }

                    //if this quad is top or bottom edge AND large map
                    else if ((uvCoord == (uvRand - factorVertUp) || uvCoord == (uvRand + factorVert)) && squareSize > 10)
                    {
                        TileType(uvCoord, randChance, 100, uv, gridDict);
                    }
                    //if this quad is top or bottom edge AND small map
                    else if ((uvCoord == (uvRand - factorVertUp) || uvCoord == (uvRand + factorVert)) && squareSize <= 10)
                    {
                        TileType(uvCoord, randChance, 20, uv, gridDict);
                    }

                    //if second to top or bottom edge AND medium to large map
                    else if ((uvCoord == (uvRand - (factorVertUp - squareSize)) || uvCoord == (uvRand + (factorVert - squareSize))) && squareSize > 10)
                    {
                        int cornerOffset = 2;
                        if (squareSize > 36)
                        {
                            cornerOffset = 3;
                        }

                        //quad IS corner or near corner
                        if (uvRand <= ((centerQuad - factorUp) + cornerOffset) || (uvRand >= ((centerQuad + factor) - cornerOffset)))
                        {
                            TileType(uvCoord, randChance, 100, uv, gridDict);
                        }
                        //NOT corners or near corners
                        else
                        {
                            TileType(uvCoord, randChance, 0, uv, gridDict);
                        }
                    }

                    //if second to left or right edge AND medium to large map
                    else if ((uvRand == (centerQuad - (factorUp - 1)) || uvRand == (centerQuad + (factor - 1))) && squareSize > 10)
                    {
                        int cornerOffset = 2 * squareSize;
                        if (squareSize > 36 && squareSize >=50)
                        {
                            cornerOffset = 3 * squareSize;
                        }
                        else if (squareSize > 50)
                        {
                            cornerOffset = 5 * squareSize;
                        }

                        //quad IS corner or near corner
                        if (uvCoord <= (uvRand - factorVertUp) + cornerOffset || uvCoord >= (uvRand + factorVert) - cornerOffset)
                        {
                            TileType(uvCoord, randChance, 100, uv, gridDict);
                        }
                        //NOT corner or near corner
                        else
                        {
                            TileType(uvCoord, randChance, 0, uv, gridDict);
                        }
                    }

                    else
                    {
                        TileType(uvCoord, randChance, 1, uv, gridDict);
                    }
                }
            }

            else
            {    
                TileType(uvRand, 0, 100, uv, gridDict);
            }
        }
    }

    //River World ERROR: RIVER CAN OVERFLOW TO OTHER SIDE OF MAP
    public void SetTileTypeRW( int quadCount, int squareSize, Vector2[] uv, Dictionary<int, string> gridDict)
    {
        int centerBottom = squareSize / 2;
        int normal = (int)Mathf.Sqrt(squareSize);
        int narrow = (int)Mathf.Sqrt(squareSize) - (((int)Mathf.Sqrt(squareSize)) / 2);
        int wide = (int)Mathf.Sqrt(squareSize) + (((int)Mathf.Sqrt(squareSize)) / 2);
        int width = normal;
        int offset = -(width/2);

        
            //Start at bottom center quad, increment up to top row
            for (int uvCoord = centerBottom; uvCoord < (centerBottom + (squareSize * squareSize)); uvCoord += squareSize)
            {
                //increment across width
                for (int i = 0; i < width; i++)
                {
                    int uvHere = uvCoord + i + offset;
                    TileType(uvHere, 1, 0, uv, gridDict);
                }

                //width change?
                int widthRand = Random.Range(0, 101);
                //grow
                if (widthRand < 20)
                {
                    width += 1;
                    offset -= 1;
                }
                //remain same
                else if (widthRand >= 20 && widthRand <= 80)
                {
                    
                }
                else if (widthRand > 80)
                {
                    width -= 1;
                    offset += 1;
                }

                //offset change?
                int offRand = Random.Range(0, 101);
                if (offRand < 33)
                {
                    offset -= 1;
                }
                else if (offRand >= 33 && offRand <= 66)
                {

                }
                else if (offRand > 66)
                {
                    offset += 1;
                }

                //boundaries
                if (width < narrow)
                {
                    width += 1;
                }
                if (width > wide)
                {
                    width -= 1;
                }

            }

        //set remaining tiles to grass
        for (int i = 0; i < quadCount; i++)
        {
            if (!gridDict.ContainsKey(i))
            {
                TileType(i, 0, 1, uv, gridDict);
            } 
        }
    }

    public void TileType(int uvCoord, int randChance, int grassChance, Vector2[] uv, Dictionary<int, string> gridDict)
    {
        //grass
        if (randChance <= grassChance)
        {
            uv[0 + (uvCoord * 6)] = new Vector2(0, (float).5);
            uv[1 + (uvCoord * 6)] = new Vector2(0, 1);
            uv[2 + (uvCoord * 6)] = new Vector2((float).5, (float).5);
            uv[3 + (uvCoord * 6)] = new Vector2((float).5, 1);
            uv[4 + (uvCoord * 6)] = new Vector2((float).5, (float)1 / 2);
            uv[5 + (uvCoord * 6)] = new Vector2(0, 1);
            gridDict[uvCoord] = "grass";
        }
        //water
        else if (randChance > grassChance)
        {
            uv[0 + (uvCoord * 6)] = new Vector2((float).5, (float).5);
            uv[1 + (uvCoord * 6)] = new Vector2((float).5, 1);
            uv[2 + (uvCoord * 6)] = new Vector2(1, (float).5);
            uv[3 + (uvCoord * 6)] = new Vector2(1, 1);
            uv[4 + (uvCoord * 6)] = new Vector2(1, (float).5);
            uv[5 + (uvCoord * 6)] = new Vector2((float).5, 1);
            gridDict[uvCoord] = "water";
        }
    }

    }

                           /* uv[0 + (uvCoord * 6)] = new Vector2(0, 0);
                            uv[1 + (uvCoord * 6)] = new Vector2(0, .5f);
                            uv[2 + (uvCoord * 6)] = new Vector2((float).5, 0);
                            uv[3 + (uvCoord * 6)] = new Vector2((float).5, .5f);
                            uv[4 + (uvCoord * 6)] = new Vector2((float).5, 0);
                            uv[5 + (uvCoord * 6)] = new Vector2(0, .5f);
                            gridDict[uvCoord] = "stone";*/




