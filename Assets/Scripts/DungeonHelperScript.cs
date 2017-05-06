using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonHelperScript : MonoBehaviour {

	//public void oddOrEven(int dimSize, )

    public int AcceptableRoomQuad(int quadCount, Dictionary<int, string> dunDict)
    {
        
        int roomQuadTest = Random.Range(0, quadCount);

        while (dunDict.ContainsKey(roomQuadTest))
        {
            Debug.Log("it's happening: " + roomQuadTest);
            roomQuadTest = Random.Range(0, quadCount);
            //AcceptableRoomQuad(quadCount, dunDict);
            
        }
            return roomQuadTest;  
    }

    public bool AcceptableForDim(int roomQuad, int quadCount, Dictionary<int, string> dunDict, int roomXSize, int roomYSize, int squareSize)
    {
        //important ints
        int xfactor = roomXSize / 2;
        int xfactorEven = xfactor + 1;

        int yPrefactor = roomYSize / 2;
        int yPrefactorEven = yPrefactor + 1;
        int yfactor = yPrefactor * squareSize;
        int yfactorEven = yPrefactorEven * squareSize;

        int testQuad = 0;

        //PROBABLY REFACTOR!!!!!!!!!!!!
        //if X dimension is ODD and Y dimension is ODD
        if (roomXSize % 2 != 0 && roomYSize % 2 != 0)
        {
             testQuad = (roomQuad - xfactor - 2) - yfactor - (2 * squareSize);
        }
        //else if x dim is ODD and Y dim is EVEN
        else if (roomXSize % 2 != 0 && roomYSize % 2 == 0)
        {
             testQuad = (roomQuad - xfactor - 2) - yfactor - (2 * squareSize);
        }
        //else if x dim is EVEN and Y dim is ODD
        else if (roomXSize % 2 == 0 && roomYSize % 2 != 0)
        {
             testQuad = (roomQuad - xfactor - 2) - yfactor - (2 * squareSize);
        }
        //else if x dim is EVEN and Y dim is EVEN
        else if (roomXSize % 2 == 0 && roomYSize % 2 == 0)
        {
             testQuad = (roomQuad - xfactor - 2) - yfactor - (2 * squareSize);
        }

        //check testQuad
        if (testQuad < 0)
        {
            return false;
        }
        else
        {
            //loop through necessary rectangular space and check if each necessary quad is not in dunDict 
            for (int i = testQuad; i < (testQuad + roomXSize + 4); i++)
            {
                for (int j = i; j < (i + (roomYSize * squareSize) + (4 * squareSize)); j += squareSize)
                {
                    if (dunDict.ContainsKey(j) || j >= quadCount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public void setTiles(int roomQuad, int quadCount, Dictionary<int, string> dunDict, int roomXSize, int roomYSize, int squareSize, Vector2[] uv)
    {
        //important ints
        int xfactor = roomXSize / 2;
        int xfactorEven = xfactor + 1;

        int yPrefactor = roomYSize / 2;
        int yPrefactorEven = yPrefactor + 1;
        int yfactor = yPrefactor * squareSize;
        int yfactorEven = yPrefactorEven * squareSize;

        int innerLowBound = 0;
        int innerUpBound = (roomQuad + xfactor) + yfactor;

        //PROBABLY REFACTOR!!!!!!!!!!!!!
        //if X dimension is ODD and Y dimension is ODD
        if (roomXSize % 2 != 0 && roomYSize % 2 != 0)
        {
            innerLowBound = (roomQuad - xfactor) - yfactor;
        }
        //else if x dim is ODD and Y dim is EVEN
        else if (roomXSize % 2 != 0 && roomYSize % 2 == 0)
        {
            innerLowBound = (roomQuad - xfactor) - yfactor;
        }
        //else if x dim is EVEN and Y dim is ODD
        else if (roomXSize % 2 == 0 && roomYSize % 2 != 0)
        {
            innerLowBound = (roomQuad - xfactor) - yfactor;
        }
        //else if x dim is EVEN and Y dim is EVEN
        else if (roomXSize % 2 == 0 && roomYSize % 2 == 0)
        {
            innerLowBound = (roomQuad - xfactor) - yfactor;
        }

        //Debug.Log("innerLowerBound: " + innerLowBound);
        //iterate through inner rectangle and set floor tiles
        for (int i = innerLowBound; i < (innerLowBound + roomXSize); i++)
        {
            for (int j = i; j < (i + ((roomYSize) * squareSize)); j += squareSize)
            {
                try
                {
                    TileType(j, 1, 0, uv, dunDict);
                }
                catch 
                {
                    Debug.Log("j: " + j);
                }

                //TileType(j, 1, 0, uv, dunDict);
            }
        }

        //set wall tiles
        //bottom walls
        for (int i = 0, quad = (innerLowBound - 1 - squareSize); i < (roomXSize + 2); i++, quad++)
        {
            TileType(quad, 0, 1, uv, dunDict);
        }
        //left walls
        for (int i = 0, quad = (innerLowBound - 1); i < (roomYSize); i++, quad += squareSize)
        {
            TileType(quad, 0, 1, uv, dunDict);
        }
        //top walls
        for (int i = 0, quad = (innerLowBound - 1 + (squareSize * roomYSize)); i < (roomXSize + 2); i++, quad++)
        {
            TileType(quad, 0, 1, uv, dunDict);
        }
        //right walls
        for (int i = 0, quad = (innerLowBound + roomXSize); i < (roomYSize); i++, quad += squareSize)
        {
            TileType(quad, 0, 1, uv, dunDict);
        }
    }

    public void ForbidEdgeQuads(int quadCount, Dictionary<int, string> dunDict, int squareSize)
    {
        //IT WORKS BUT IS IT 100% PRECISE???
        //add outer frame of squareSize^2 grid to dunDict
        //bottom
        for (int i = 0; i < squareSize; i++)
        {
            dunDict[i] = "space frame";
        }
        //left
        for (int i = 0; i < quadCount; i += squareSize)
        {
            dunDict[i] = "space frame";
        }
        //top
        for (int i = 0, quad = (quadCount - squareSize); i < squareSize; i++, quadCount++)
        {
            dunDict[quad] = "space frame";
        }
        //right 
        for (int i = 0, quad = (squareSize - 1); i < squareSize; i++, quad += squareSize)
        {
            dunDict[quad] = "space frame";
        }
    }

    public void CreatePath(List<List<int>> roomList, int index, int squareSize, Vector2[] uv, Dictionary<int, string> dunDict)
    {
        int centerQuad = roomList[index][0];
        int roomXSize = roomList[index][1];
        int roomYSize = roomList[index][2];

        //important ints
        int xfactor = roomXSize / 2;
        int yPrefactor = roomYSize / 2;   
        int yfactor = yPrefactor * squareSize;
        int lowBound = (centerQuad - xfactor) - yfactor;
        string value = "";

        //*******************************
        TileType(centerQuad, 0, 1, uv, dunDict);

        //iterate across floor tile edges
        //BOTTOM edge
        for (int i = (lowBound - squareSize); i < ((lowBound - squareSize) + roomXSize); i++)
        {

            //iterate outward at each edge tile checking dunDict at each quad for "floor"
            for (int j = i; j >= 0; j -= squareSize)
            {

                //if wall is detected
                if (dunDict.TryGetValue(j, out value))
                {
                    if (value == "wall")
                    {
                        j -= squareSize;
                        //if next tile is also wall
                        if (dunDict.TryGetValue(j, out value))
                        {
                            //if "wall" then exit
                            if (value == "wall")
                            {
                                j = -1;
                            }
                            //else if "floor" then continue
                            else if (value == "floor")
                            {
                                //set floor tiles from i to j
                                for (int k = i, jHere = j; k > jHere; k -= squareSize)
                                {

                                    TileType(k, 1, 0, uv, dunDict);
                                    //exit loop
                                    j = -1;
                                    i = lowBound + 10;
                                }
                            }
                        }
                    }
                }
            }
        }

        //LEFT edge
        int farEdge = 0;
        for (int i = (lowBound - 1); i < ((lowBound - 1) + (roomYSize * squareSize)); i += squareSize)
        {
            //if j % 100 between 0 and 49, iterate while j % 100 > 0
            if (i % 100 >= 0 && i % 100 <= 49)
            {
                farEdge = 0;
            }

            //if j % 100 between 50 and 99, iterate while j % 100 > 50
            else if (i % 100 > 49 && i % 100 <= 99)
            {
                farEdge = 50;
            }

            //iterate outward at each edge tile checking dunDict at each quad for "wall" then "floor"
            for (int j = i; j % 100 > farEdge; j -= 1)
            {
                //if wall is detected
                if (dunDict.TryGetValue(j, out value))
                {
                    if (value == "wall")
                    {
                        j -= 1;
                        //if next tile is also wall
                        if (dunDict.TryGetValue(j, out value))
                        {
                            if (value == "wall")
                            {
                                farEdge = 101;
                            }
                            else if (value == "floor")
                            {
                                //set floor tiles from i to j
                                for (int k = i, jHere = j; k > jHere; k -= 1)
                                {

                                    TileType(k, 1, 0, uv, dunDict);
                                    //exit loop
                                    //k = j - 1;
                                    farEdge = 101;
                                    i = lowBound + (10 * squareSize);
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    public void CreatePath2(List<List<int>> roomList, int index, int squareSize, Vector2[] uv, Dictionary<int, string> dunDict)
    {
        int centerQuad = roomList[index][0];
        int roomXSize = roomList[index][1];
        int roomYSize = roomList[index][2];

        //important ints
        int xfactor = roomXSize / 2;
        int yPrefactor = roomYSize / 2;
        int yfactor = yPrefactor * squareSize;
        int lowBound = (centerQuad - xfactor) - yfactor;
        string value = "";

        //*******************************
        TileType(centerQuad, 0, 1, uv, dunDict);

        //iterate across floor tile edges
        //TOP edge
        for (int i = (lowBound + (squareSize * roomYSize)); i < ((lowBound + (squareSize * roomYSize)) + roomXSize); i++)
        {
            //Debug.Log("hit it i: " + i);
            //iterate outward at each edge tile checking dunDict at each quad for "floor"
            for (int j = i; j <= (squareSize * squareSize); j += squareSize)
            {
                //Debug.Log("hit it j: " + j);
                //if wall is detected
                if (dunDict.TryGetValue(j, out value))
                {
                    if (value == "wall")
                    {
                        j += squareSize;
                        //if next tile is also wall
                        if (dunDict.TryGetValue(j, out value))
                        {
                            //Debug.Log("got here");
                            //if "wall" then exit
                            if (value == "wall")
                            {
                                j = (squareSize * squareSize) + 1;
                            }
                            //else if "floor" then continue
                            else if (value == "floor")
                            {
                                //Debug.Log("got to floor");
                                //set floor tiles from i to j
                                for (int k = i, jHere = j; k < jHere; k += squareSize)
                                {
                                    //Debug.Log("creating upward path at: " + k);
                                    TileType(k, 1, 0, uv, dunDict);
                                    //exit loop CAN MOVE OUTSIDE THIS FOR LOOP!!!
                                    j = (squareSize * squareSize) + 1;
                                    i = (lowBound + (squareSize * squareSize)) + 10;
                                }
                            }
                        }
                    }
                }
            }
        }

        //RIGHT edge
        int farEdge = 0;
        for (int i = (lowBound + roomXSize); i < ((lowBound + roomXSize) + (roomYSize * squareSize)); i += squareSize)
        {
            //if j % 100 between 0 and 49, iterate while j % 100 > 0
            if (i % 100 >= 0 && i % 100 <= 49)
            {
                farEdge = 49;
            }

            //if j % 100 between 50 and 99, iterate while j % 100 > 50
            else if (i % 100 > 49 && i % 100 <= 99)
            {
                farEdge = 99;
            }

            //iterate outward at each edge tile checking dunDict at each quad for "wall" then "floor"
            for (int j = i; j % 100 < farEdge; j += 1)
            {
                //Debug.Log("j: " + j);
                //if wall is detected
                if (dunDict.TryGetValue(j, out value))
                {
                    if (value == "wall")
                    {
                        if (j % 100 <= farEdge)
                        {
                            j += 1;
                        }
                        //Debug.Log("lets see: " + j);
                        //if next tile is also wall
                        if (dunDict.TryGetValue(j, out value))
                        {
                            if (value == "wall")
                            {
                                farEdge = -1;
                            }
                            else if (value == "floor")
                            {
                                //set floor tiles from i to j
                                for (int k = i, jHere = j; k < jHere; k += 1)
                                {

                                    TileType(k, 1, 0, uv, dunDict);
                                    //exit loop
                                    //k = j - 1;
                                    farEdge = -1;
                                    i = lowBound + roomXSize + (10 * squareSize);
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    //returns closest room that is not yet connected
    public int ClosestRoom(List<List<int>> roomList, int index, int squareSize, List<int> ConnectedRooms)
    {
        int centerQuad = roomList[index][0];
        int roomXSize = roomList[index][1];
        int roomYSize = roomList[index][2];
        int minIndex = -1;
        List<float> roomDistList = new List<float>();

        //get roomDistList index of closest room
        for (int i = 0; i < roomList.Count; i++)
        {
            int centerQuadOther = roomList[i][0];
            int vertDist = Mathf.RoundToInt(((float)centerQuad - (float)centerQuadOther) / (float)squareSize);

            //to find horizontal distance, adjust centerQuadOther to be on same line as centerQuad
            if (vertDist > 0)
            {
                //centerQuadOther is BELOW, so move it up
                centerQuadOther = centerQuadOther + (vertDist * squareSize);
            }
            else if (vertDist < 0)
            {
                //centerQuadOther is ABOVE, so move it down
                centerQuadOther = centerQuadOther - (-vertDist * squareSize);
            }

            int horDist = centerQuad - centerQuadOther;

            float distance = HypotenuseDist(vertDist, horDist);
            roomDistList.Add(distance);
        }
        minIndex = IndexAtMin(roomDistList, index, ConnectedRooms);
        return minIndex;
    }

    public float HypotenuseDist(int a, int b)
    {
        int aSquare = a * a;
        int bSquare = b * b;
        int cSquare = aSquare + bSquare;
        return Mathf.Sqrt(cSquare); 
    }

    //returns index at minimum ABSOLUTE value
    public int IndexAtMin(List<float> roomDistList, int indexAtStartRoom, List<int> ConnectedRooms)
    {
        float minValue = 2501;
        int minIndex = -1;
        int index = -1;
        
        foreach (float num in roomDistList)
        {
            index++;         

            if (!ConnectedRooms.Contains(index))
            {
                if (num < 0)
                {
                    float numsub = num * -1;

                    if (numsub <= minValue)
                    {
                        minIndex = index;
                        minValue = numsub;
                    }
                }

                else if (num > 0)
                {
                    if (num <= minValue)
                    {
                        minIndex = index;
                        minValue = num;
                    }
                }
            }
        }

        return minIndex;    
    }

    public void CreatePathAlt(List<List<int>> roomList, int startRoomIndex, int endRoomIndex, int squareSize, Vector2[] uv, Dictionary<int, string> dunDict)
    {
        //starting room info
        int centerQuadSt = roomList[startRoomIndex][0];
        int roomXSizeSt = roomList[startRoomIndex][1];
        int roomYSizeSt = roomList[startRoomIndex][2];

        int xfactorSt = roomXSizeSt / 2;
        int yPrefactorSt = roomYSizeSt / 2;
        int yfactorSt = yPrefactorSt * squareSize;
        int lowBoundSt = (centerQuadSt - xfactorSt) - yfactorSt;

        //ending room info
        int centerQuadEnd = roomList[endRoomIndex][0];
        int centerQuadEndAdjustment = centerQuadEnd;
        int roomXSizeEnd = roomList[endRoomIndex][1];
        int roomYSizeEnd = roomList[endRoomIndex][2];

        int xfactorEnd = roomXSizeEnd / 2;
        int yPrefactorEnd = roomYSizeEnd / 2;
        int yfactorEnd = yPrefactorEnd * squareSize;
        int lowBoundEnd = (centerQuadEnd - xfactorEnd) - yfactorEnd;

        int vertDist = Mathf.RoundToInt(((float)centerQuadSt - (float)centerQuadEnd) / (float)squareSize);

        //if absolute value of last 2 digits of cQSt - last 2 digits of cQEnd is > 25, then if negative subtract 1 from vertDist (so it's like having rounded down) and if positive add 1 to vertDist
        float last2St = centerQuadSt % 100;
        float last2End = centerQuadEnd % 100;
        int differenceint = (int)(last2St - last2End);
        float difference  = (float)differenceint;
        Debug.Log("ST: " + last2St);
        Debug.Log("End: " + last2End);
        Debug.Log("int dif: " + differenceint);
        Debug.Log("difference: " + difference);
        Debug.Log("absolute: " + Mathf.Abs(difference));

        if (Mathf.Abs(difference) < 49)
        {
            if (((last2St > 0 && last2St < 50) && (last2End > 0 && last2End < 50)) || ((last2St > 50 && last2St < 99) && (last2End > 50 && last2End < 99)))
            {
                if (difference > 25)
                {
                    if (((last2St > 0 && last2St < 50) && (last2End > 0 && last2End < 50)) || ((last2St > 50 && last2St < 99) && (last2End > 50 && last2End < 99)))
                    {
                            vertDist -= 1;   
                    }
                }

                else if (difference < -25)
                {
                        vertDist += 1;
                }
            }
            //different types of rows
            else if (((last2St > 0 && last2St < 50) && (last2End > 50 && last2End < 99)) || ((last2St > 50 && last2St < 99) && (last2End > 0 && last2End < 50)))
            {
                if (difference > 0 && difference <= 25)
                {
                    vertDist += 1;
                }
                if (difference < 0 && difference >= -25)
                {
                    vertDist -= 1;
                }
            }
        }

        else if (Mathf.Abs(difference) > 49)
        {
            if (((last2St > 0 && last2St < 50) && (last2End > 50 && last2End < 99)) || ((last2St > 50 && last2St < 99) && (last2End > 0 && last2End < 50)))
            {
                if (difference >= 75)
                {
                    vertDist -= 1;
                }
                else if (difference <= -75)
                {
                    vertDist += 1;   
                }
            }
        }

        //to find horizontal distance, adjust centerQuadEnd to be on same line as centerQuad
        if (vertDist > 0)
        {
            //centerQuadEnd is BELOW, so move it up
            centerQuadEndAdjustment = centerQuadEnd + (vertDist * squareSize);
        }
        else if (vertDist < 0)
        {
            //centerQuadEnd is ABOVE, so move it down
            centerQuadEndAdjustment = centerQuadEnd - (-vertDist * (squareSize));
        }

        int horDist = centerQuadSt - centerQuadEndAdjustment;
        float distance = HypotenuseDist(vertDist, horDist);

        //create paths
        //vertical path; if vertDist negative -> path up, if vertDist positive -> path down, if vertDist = 0 -> no vertical path
        if (vertDist < 0)
        {
            //convert to absolute value
            int absVertDist = -vertDist;
            for (int i = 0, d = centerQuadSt; i <= absVertDist; i ++, d += squareSize)
            {
                TileType(d, 1, 0, uv, dunDict);
            }

            //horizontal path; if horDist negative -> path right, if horDist positive -> path left, if horDist = 0 -> no horizontal path
            if (horDist < 0)
            {
                //convert to absolute value
               
                for (int i = (centerQuadSt + (squareSize * absVertDist)); i <= centerQuadEnd; i++)
                {
                    TileType(i, 1, 0, uv, dunDict);
                }
            }
            else if (horDist > 0)
            {
                for (int i = (centerQuadSt + (squareSize * absVertDist)); i >= centerQuadEnd; i--)
                {
                    TileType(i, 1, 0, uv, dunDict);
                }
            }

        }
        else if (vertDist >= 0)
        {
            for (int i = centerQuadSt; i >= centerQuadSt - (vertDist * squareSize); i -= squareSize)
            {
                TileType(i, 1, 0, uv, dunDict);
            }

            //horizontal path; if horDist negative -> path right, if horDist positive -> path left, if horDist = 0 -> no horizontal path
            if (horDist < 0)
            {
                //convert to absolute value
                int absHorDist = -horDist;
                for (int i = (centerQuadSt - (squareSize * vertDist)); i <= centerQuadEnd; i++)
                {
                    TileType(i, 1, 0, uv, dunDict);
                }
            }
            else if (horDist > 0)
            {
                for (int i = (centerQuadSt - (squareSize * vertDist)); i >= centerQuadEnd; i--)
                {
                    TileType(i, 1, 0, uv, dunDict);
                }
            }
        }
    }


    public void WallsForPaths(int quadCount, int squareSize, Vector2[] uv, Dictionary<int, string> dunDict)
    {
        string value = "";
        //fill empty tiles with walls
        for (int i = 0; i < quadCount; i++)
        {
            if (dunDict.TryGetValue(i, out value))
            {
                //if i is floor tile, check 4 adjacent tiles for empty. if empty, set to wall
                if (value == "floor")
                {
                    //check down
                    if (!dunDict.ContainsKey(i - squareSize))
                    {
                        TileType((i - squareSize), 0, 1, uv, dunDict);
                    }
                    //check left
                    if (!dunDict.ContainsKey(i - 1))
                    {
                        TileType((i-1), 0, 1, uv, dunDict);
                    }
                    //check up
                    if (!dunDict.ContainsKey(i + squareSize))
                    {
                        TileType((i + squareSize), 0, 1, uv, dunDict);
                    }
                    //check right
                    if (!dunDict.ContainsKey(i + 1))
                    {
                        TileType((i + 1), 0, 1, uv, dunDict);
                    }
                }
            }
        }
    }

    public void PlaceObjects(int quadCount, int squareSize, Vector2[] uv, Dictionary<int, string> dunDict, GameObject ladderObject)
    {
        string value = "";
        bool isSet = false;
   
        //iterate through tiles to find appropriate spot
        while (!isSet)
        {
            int surroundingFloorTiles = 0;
            int chosenQuad = Random.Range(0, quadCount);
            if (dunDict.TryGetValue(chosenQuad, out value))
            {
                //if i is floor tile, check 4 adjacent tile types, for each floor, surroundingFloorTiles +1
                if (value == "floor")
                {
                    //check down
                    if (dunDict.TryGetValue(chosenQuad - squareSize, out value))
                    {
                        if (value == "floor")
                        {
                            surroundingFloorTiles++;
                        }
                    }
                    //check left
                    if (dunDict.TryGetValue(chosenQuad - 1, out value))
                    {
                        if (value == "floor")
                        {
                            surroundingFloorTiles++;
                        }
                    }
                    //check up
                    if (dunDict.TryGetValue(chosenQuad + squareSize, out value))
                    {
                        if (value == "floor")
                        {
                            surroundingFloorTiles++;
                        }
                    }
                    //check right
                    if (dunDict.TryGetValue(chosenQuad + 1, out value))
                    {
                        if (value == "floor")
                        {
                            surroundingFloorTiles++;
                        }
                    }
                }
                if (surroundingFloorTiles >= 3)
                {
                    //instantiate ladder
                    float xPos = 0;
                    float yPos = 0;

                    //get x position
                    int last2CQ = chosenQuad % 100;
                    if (last2CQ > 50 && last2CQ < 99)
                    {
                        xPos = last2CQ - squareSize;
                    }
                    else
                    {
                        xPos = last2CQ;
                    }
                    xPos = xPos + 24;


                    //get y position
                    float cQFloat = (float)chosenQuad;
                    yPos = Mathf.Round(cQFloat / squareSize);
                    Debug.Log("yPos: " + yPos);
                    if ((last2CQ > 25 && last2CQ < 50) || (last2CQ >= 75))
                    {
                        yPos -= 1;
                    }
                    Debug.Log("yPos: " + yPos);
                    yPos = yPos + 21.5f;
                   // yPos++;

                    Debug.Log("chosen quad " + chosenQuad);
                    Instantiate(ladderObject, new Vector3(xPos, yPos, -4), Quaternion.identity);
                    //exit loop
                    isSet = true;
                }
            }
        }
    }


    public void TileType(int uvCoord, int randChance, int floorChance, Vector2[] uv, Dictionary<int, string> dunDict)
    {
        //wall
        if (randChance <= floorChance)
        {
            uv[0 + (uvCoord * 6)] = new Vector2(0, (float).5);
            uv[1 + (uvCoord * 6)] = new Vector2(0, 1);
            uv[2 + (uvCoord * 6)] = new Vector2((float).5, (float).5);
            uv[3 + (uvCoord * 6)] = new Vector2((float).5, 1);
            uv[4 + (uvCoord * 6)] = new Vector2((float).5, (float)1 / 2);
            uv[5 + (uvCoord * 6)] = new Vector2(0, 1);
            dunDict[uvCoord] = "wall";
        }
        //floor
        else if (randChance > floorChance)
        {
            uv[0 + (uvCoord * 6)] = new Vector2((float).5, (float).5);
            uv[1 + (uvCoord * 6)] = new Vector2((float).5, 1);
            uv[2 + (uvCoord * 6)] = new Vector2(1, (float).5);
            uv[3 + (uvCoord * 6)] = new Vector2(1, 1);
            uv[4 + (uvCoord * 6)] = new Vector2(1, (float).5);
            uv[5 + (uvCoord * 6)] = new Vector2((float).5, 1);
            dunDict[uvCoord] = "floor";
        }
    }

}
