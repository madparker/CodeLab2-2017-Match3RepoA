using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	GameEvent: Abstract class for Game Events for the GameEventsManager				    */
/*																						*/
/*--------------------------------------------------------------------------------------*/
public abstract class GameEvent 
{
    public delegate void Handler(GameEvent e);      //  Delegate for GameEvents
}


public class ButtonPressed : GameEvent
{
    public string button;
    public int playerNum;
    public ButtonPressed(string _button, int _playerNum)
    {
        button = _button;
        playerNum = _playerNum;
    }
}

public class Reset : GameEvent { }