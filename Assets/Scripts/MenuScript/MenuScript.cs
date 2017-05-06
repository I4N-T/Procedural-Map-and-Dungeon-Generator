using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public static int mapSize;
    public static int mapType;

    public InputField input;
    public Dropdown dropdown;

    List<string> mapTypeStrings = new List<string>() { "Scattered Waters", "Random", "Great Lake", "River World" };

    void Start()
    {
        PopulateMenu();
        mapType = 0;
    }

    //INPUT FIELD
    public void Input_onValueChanged(string text)
    {
        //only accept ints 5-100
        if (Int32.Parse(text) >= 5 && Int32.Parse(text) <= 100)
        {
            mapSize = Int32.Parse(text);
            Debug.Log(mapSize);
        }
    }

    //DROPDOWN MENU
    public void Dropdown_onValueChanged(int index)
    {
        mapType = index;
        Debug.Log(index);
    }

    void PopulateMenu()
    {
        dropdown.AddOptions(mapTypeStrings);
    }

    public void onGenerate()
    {
        if (mapSize > 5 && mapSize <= 100)
        {
            SceneManager.LoadScene(1);
        }
    }
	
}
