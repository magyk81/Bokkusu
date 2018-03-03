using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    public enum _ { FLOOR, WALL, GOAL, CRATE, PLAYER1, PLAYER2, DOOR_A, DOOR_B, NOTHING };

    private static _[] elementValues = (_[]) System.Enum.GetValues(typeof (_));

    public static _ get(char c)
    {
        if (c == '.') return _.FLOOR;
        else if (c == '#') return _.WALL;
        else if (c == '=') return _.GOAL;
        else if (c == '&') return _.CRATE;
        else if (c == '1') return _.PLAYER1;
        else if (c == '2') return _.PLAYER2;
        else if (c == 'A') return _.DOOR_A;
        else if (c == 'B') return _.DOOR_B;
        else if (c == ' ') return _.NOTHING;
        return _.FLOOR;
    }

    public static _ getElement(int idx)
    {
        if (idx == -1) return elementValues[0];
        return elementValues[idx];
    }
}
