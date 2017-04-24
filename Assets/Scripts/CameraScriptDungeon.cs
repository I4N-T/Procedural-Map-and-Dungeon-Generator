using UnityEngine;
using System.Collections;

public class CameraScriptDungeon : MonoBehaviour {

    GameObject theobj;
    DungeonTileScript tileMeshScript;

    void Awake()
    {
        theobj = GameObject.Find("GameManager");
        tileMeshScript = theobj.GetComponent<DungeonTileScript>();
    }

    void Update()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");

        if (d > 0)
        {
            if (Camera.main.orthographicSize > 4f)
            {
                Camera.main.orthographicSize -= 1f;
            }
        }

        else if (d < 0)
        {
            if (Camera.main.orthographicSize < (tileMeshScript.squareSize / 2))
            {
                Camera.main.orthographicSize += 1f;
            }
        }
    }
}
