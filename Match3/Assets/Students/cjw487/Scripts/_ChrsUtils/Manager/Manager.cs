using System;
using System.Collections.Generic;
using UnityEngine;


/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	IManaged: Base  managed objects     						            			*/
/*																						*/
/*		Functions:																		*/
/*			public:																		*/
/*																						*/
/*			private:																	*/
/*			    void OnCreated()														*/
/*				void OnDestroyed()														*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public interface IManaged
{
    void OnCreated();       //  Runs when Managed object is created
    void OnDestroyed();     //  Runs when Managed object is destroyed
}

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	Manager<T>: Base Class for Managers							            			*/
/*																						*/
/*		Functions:																		*/
/*			public:																		*/
/*				abstract T Create()														*/
/*				abstract void Destroy(T o)												*/
/*				T Find(Predicate<T> predicate)											*/
/*				List<T> FindAll(Predicate<T> predicate)									*/
/*																						*/
/*			private:																	*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public abstract class Manager<T> : MonoBehaviour where T : IManaged
{
    protected static readonly List<T> ManagedObjects = new List<T>();       //  List of Managed Objects

    public abstract T Create();             //  Abstarct class to creat the managed object

    public abstract void Destroy(T o);      //  Abstract class to destroy the managed object

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Find: Finds the managed object you send in          								*/
    /*				T : A generic class														*/
    /*			param:																		*/
    /*				Predicate<T> predicate - The object to be found               			*/
    /*																						*/
    /*			return:																		*/
    /*			    T : Generic Managed object												*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public T Find(Predicate<T> predicate)
    {
        return ManagedObjects.Find(predicate);
    }

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	FindAll: Finds all the managed objects you based on a flag 							*/
    /*				T : A generic class														*/
    /*			param:																		*/
    /*				Predicate<T> predicate - The object to be found               			*/
    /*																						*/
    /*			return:																		*/
    /*			    List<T> : A list of all the managed objects with the flag				*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public List<T> FindAll(Predicate<T> predicate)
    {
        return ManagedObjects.FindAll(predicate);
    }
}


