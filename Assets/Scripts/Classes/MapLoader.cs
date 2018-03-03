using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapLoader
{
    public static GameObject boardBlock, boardCrate;

    public static GameMap[] loadAllMaps()
    {
        /* Finds the "Maps" folder and loads all of the text files as TextAssets */
        Object[] mapTexts = Resources.LoadAll("Maps", typeof(TextAsset));

        /* Checks if the folder is empty */
        if (mapTexts.Length == 0)
        {
            Debug.Log("ERROR: MapLoader.loadAllMaps(): files not found.");
        }

        GameMap[] gameMaps = new GameMap[mapTexts.Length];

        boardBlock = GameObject.FindGameObjectWithTag("Block");
        boardCrate = GameObject.FindGameObjectWithTag("Crate");

        for (int i = 0; i < mapTexts.Length; i++)
        {
            /* Converts it back into a string */
            string text = ((TextAsset) mapTexts[i]).text;

            /* GameMap will parse the string to generate the level */
            gameMaps[i] = new GameMap(text, boardBlock, boardCrate);
        }

        /* An array of GameMaps that are generated */
        return gameMaps;
    }
}
