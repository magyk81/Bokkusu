using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    private GameObject boardBlock;
    private GameObject crateBlock;

    /* Width and height in blocks */
    private int width, height;
    private Element._[,] map;
    private int leastMoves = 100;
    private float fastestTime = 600; //seconds
    private string levelName;
    private string player1Name = "Morgan";
    private string player2Name = "";

    public GameMap(string mapString, GameObject block, GameObject crate)
    {
        string[] lines = mapString.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);

        height = lines.Length - 1;
        width = 0;

        for (int i = 0; i < height; i++)
        {
            if (lines[i].Length > width) width = lines[i].Length;
        }

        generateMap(width, height, lines, Random.Range(0, 2) == 1, Random.Range(0, 4));

        levelName = lines[height].Substring(1, lines[height].Length - 1);

        //Debug.Log("Level: " + levelName);

        /* Have access to the original block and crate */
        boardBlock = block;
        crateBlock = crate;
    }

    public Element._[,] getMap() { return map; }
    public string getLevelName() { return levelName; }
    public int getLeastMoves() { return leastMoves; }
    public float getFastestTime() { return fastestTime; }

    public string getPlayerNames()
    {
        if (player1Name.Length > 0 && player2Name.Length > 0)
        {
            return player1Name + " & " + player2Name;
        }
        return player1Name + player2Name;
    }

    public string getPlayerName(int playerNum)
    {
        if (playerNum == 1) return player1Name;
        return player2Name;
    }

    public void setLeader(int score, float seconds, string name1, string name2)
    {
        leastMoves = score;
        fastestTime = seconds;
        setPlayerName(1, name1);
        setPlayerName(2, name2);
    }

    private void setPlayerName(int playerNum, string str)
    {
        str = str.Trim();
        if (playerNum == 1) player1Name = str;
        else player2Name = str;
    }

    private void generateMap(int width, int height, string[] lines, bool rotate, int mirror)
    {
        int[] posPlayer1, posPlayer2;
        posPlayer1 = new int[2];
        posPlayer2 = new int[2];

        if (rotate && height * 1.5 < width)
        {
            map = new Element._[height, width];

            //fill wih empty
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    map[j, i] = Element._.NOTHING;
                }
            }

            for (int j = 0; j < height; j++)
            {
                string row = lines[j];
                for (int i = 0; i < row.Length; i++)
                {
                    Element._ element = Element.get(row[i]);
                    int[] mapIndex = new int[2];
                    if (mirror == 0) { mapIndex[0] = j; mapIndex[1] = i; }
                    else if (mirror == 1) { mapIndex[0] = height - 1 - j; mapIndex[1] = i; }
                    else if (mirror == 2) { mapIndex[0] = j; mapIndex[1] = row.Length - 1 - i; }
                    else if (mirror == 3) { mapIndex[0] = height - 1 - j; mapIndex[1] = row.Length - 1 - i; }

                    map[mapIndex[0], mapIndex[1]] = element;

                    if (Element.get(row[i]) == Element._.PLAYER1)
                    {
                        posPlayer1[0] = mapIndex[0];
                        posPlayer1[1] = mapIndex[1];
                    }
                    else if (Element.get(row[i]) == Element._.PLAYER2)
                    {
                        posPlayer2[0] = mapIndex[0];
                        posPlayer2[1] = mapIndex[1];
                    }
                }
            }
        }
        else
        {
            map = new Element._[width, height];

            //fill wih empty
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    map[i, j] = Element._.NOTHING;
                }
            }

            for (int j = 0; j < height; j++)
            {
                string row = lines[j];
                for (int i = 0; i < row.Length; i++)
                {
                    Element._ element = Element.get(row[i]);
                    int[] mapIndex = new int[2];
                    if (mirror == 0) { mapIndex[0] = i; mapIndex[1] = j; }
                    else if (mirror == 1) { mapIndex[0] = row.Length - 1 - i; mapIndex[1] = j; }
                    else if (mirror == 2) { mapIndex[0] = i; mapIndex[1] = height - 1 - j; }
                    else if (mirror == 3) { mapIndex[0] = row.Length - 1 - i; mapIndex[1] = height - 1 - j; }

                    map[mapIndex[0], mapIndex[1]] = element;

                    if (Element.get(row[i]) == Element._.PLAYER1)
                    {
                        posPlayer1[0] = mapIndex[0];
                        posPlayer1[1] = mapIndex[1];
                    }
                    else if (Element.get(row[i]) == Element._.PLAYER2)
                    {
                        posPlayer2[0] = mapIndex[0];
                        posPlayer2[1] = mapIndex[1];
                    }
                }
            }
        }

        // switch places if Player 2 is on the left
        if (posPlayer1[0] > posPlayer2[0])
        {
            map[posPlayer1[0], posPlayer1[1]] = Element._.PLAYER2;
            map[posPlayer2[0], posPlayer2[1]] = Element._.PLAYER1;
        }
    }

    /* Called once by the MainLoop to set up the board's gameObjects */
    public Cell[,] spawnBoard()
    {
        /*
        textEffectHighScore.Stop();
        textEffectHighScore.Clear();

        textHighScore.SetActive(false);
        textPlayerGoals1.SetActive(false);
        textPlayerGoals2.SetActive(false);

        doorToggleSeconds = -1f;
        frameCount = 0;
        timeOfLevel = 0;
        playTimeOfLevel = 0;
        firstMoveWasMade = false;
        cellAudioIdx = 0;
        
        curLevel = level;
        
        gameMap = gameMapList[level];
        startMap = gameMap.getMap();
        gridWidth = startMap.GetLength(0);
        gridHeight = startMap.GetLength(1);
        */

        int numCells = 0;
        /*
        textNameData.text = gameMap.getLevelName();
        textScoreData.text = "Leader: " + gameMap.getLeastMoves() + " moves in " + gameMap.getFastestTime() + " sec by " +
        gameMap.getPlayerNames();
        textCurrentScore.SetActive(true);
        textHighScore.SetActive(true);
        */

        Cell[,] grid = new Cell[width, height];

        /* Used to check that there is only one goal in the map */
        bool foundGoal = false;

        /* Spawn board blocks */
        //Debug.Log("Spawn Board(" + level + "): " + grid.GetLength(0) + "(" + gridWidth + ") x " + grid.GetLength(1) + "(" + gridHeight + ")");
        CloneScript boardBlockScript = boardBlock.GetComponent<CloneScript>();
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (map[x, z] == Element._.NOTHING) continue;
                //Debug.Log("startMap[" + x + ", " + z + "]=" + startMap[x, z]);

                numCells++;

                int y = Random.Range(2, 40);
                GameObject block = boardBlockScript.cloneMe(x, 0, z);
                block.SetActive(true);

                grid[x, z] = new Cell(map[x, z], block, new Material(Shader.Find("Standard")));

                if (map[x, z] == Element._.GOAL)
                {
                    if (foundGoal)
                    {
                        Debug.Log("Each level must have Exactly ONE goal.");
                    }
                    foundGoal = true;
                    //goalBlock.transform.position = new Vector3(x, 1, z);
                }
            }
        }

        return grid;

    }

    /* TODO: Might not be needed */
    public void destroyBoard() { }
}
