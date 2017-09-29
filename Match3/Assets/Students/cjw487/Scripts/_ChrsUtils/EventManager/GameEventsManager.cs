using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	GameEventsManager: The program pattern for Event Management							*/
/*																						*/
/*		Functions:																		*/
/*			public:																		*/
/*				void Register<T>(GameEvent.Handler handler) where T : GameEvent 		*/
/*				void Unregister<T>(GameEvent.Handler handler) where T : GameEvent 		*/
/*				void Fire(GameEvent e) 													*/
/*																						*/
/*			private:																	*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class GameEventsManager 
{

    static private GameEventsManager instance;          //  Instance of GameEventsManager
    static public GameEventsManager Instance 
    { 
        get 
        {
            if (instance == null) 
            {
                instance = new GameEventsManager();
            }
            return instance;
        }
    }
    
    //  Dictionary of all GameEvents
    private Dictionary<Type, GameEvent.Handler> registeredHandlers = new Dictionary<Type, GameEvent.Handler>();
    
    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Register<T>: Registers script for a GameEvent          								*/
    /*				T : a GameEvent															*/
    /*			param:																		*/
    /*				GameEvent.Handler handler - Handler for the GameEvent       			*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public void Register<T>(GameEvent.Handler handler) where T : GameEvent 
    {
        Type type = typeof(T);
        if (registeredHandlers.ContainsKey(type))
        {
            registeredHandlers[type] += handler;
        } 
        else 
        {
            registeredHandlers[type] = handler;
        }
    }
    
    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Unregister<T>: Unregisters script for a GameEvent          							*/
    /*				T : a GameEvent															*/
    /*			param:																		*/
    /*				GameEvent.Handler handler - Handler for the GameEvent       			*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public void Unregister<T>(GameEvent.Handler handler) where T : GameEvent 
    {
        Type type = typeof(T);
        GameEvent.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers)) 
        {
            handlers -= handler;
            if (handlers == null) 
            {
                registeredHandlers.Remove(type);
            } 
            else 
            {
                registeredHandlers[type] = handlers;
            }
        }
    }
    
    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Fire: Fires the event          								                        */
    /*			param:																		*/
    /*				GameEvent e - The current GameEvent                          			*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public void Fire(GameEvent e) 
    {
        Type type = e.GetType();
        GameEvent.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers)) 
        {
            handlers(e);
        }
    }
 }

