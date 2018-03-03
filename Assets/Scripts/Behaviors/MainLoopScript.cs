using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoopScript : MonoBehaviour
{
    private GameMap[] gameMapList;
    private Cell[,] grid;
    private int curLevel = 0;

    private void Awake()
    {
        gameMapList = MapLoader.loadAllMaps();

        MapLoader.boardBlock.SetActive(false);
        MapLoader.boardCrate.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;

        newGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void newGame()
    {
        gameMapList[curLevel].spawnBoard();
    }
}
