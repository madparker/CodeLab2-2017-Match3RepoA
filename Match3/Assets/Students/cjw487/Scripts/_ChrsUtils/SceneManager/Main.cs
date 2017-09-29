using UnityEngine;
using UnityEngine.Assertions;

#region Main.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*     Seneca starts running fdorm this script.                                                                         */
/*                                                                                                                      */
/*    Main.cs is responsible  for:                                                                                      */
/*    Instantiating the ScenePrefab Database which holds a prefab for all scenes in the game.                           */
/*    Initalizing the Game Event Manager                                                                                */
/*    Initalizing  the scene manager                                                                                    */
/*                                                                                                                      */
/*    And                                                                                                               */
/*                                                                                                                      */
/*    Loading the first scene.                                                                                          */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion

#region Quick Overview of Prefab Database
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Prefab Database is found in: ChrsUtils -> SceneManager-> SceneManager.cs                                          */
/*                                                                                                                      */
/*        This holds the prefabs for all scenes in the game.                                                            */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion

#region Quick Overview of Game Event System
/************************************************************************************************************************/
/*                                                                                                                      */
/*     Event System is found in: ChrsUtils -> EventsManager                                                             */
/*                                                                                                                      */
/*        Overview:                                                                                                     */
/*        If we want a script to respond to a thing that happens in another script without coupling                     */
/*        the scripts together, we use the Events Manager.                                                              */
/*                                                                                                                      */
/*        How it works:                                                                                                 */
/*        We register a delegate to a GameEvent  (GameEvents are found in SenecaEvent.cs). When we                      */
/*         fire that event using the Event Manager, the delegate is executed by the computer                            */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion

#region Quick Overview of SceneManagement System
/************************************************************************************************************************/
/*                                                                                                                      */
/*       Event System is found in: ChrsUtils -> SceneManager-> SceneManager.cs                                          */
/*                                                                                                                      */
/*      Overview:                                                                                                       */
/*       Loads and unloads scenes as prefabs. This is done in this project to avoid the use of singletons.              */
/*                                                                                                                      */
/*       How it works:                                                                                                  */
/*       SceneManagement system works as a stack where scene prefabs are pushed, popped, and swapped.                   */
/*       Each scene passes Transition data to the next scene. In the case of Seneca the data being passed if            */
/*       whether you've been to the scene or not.                                                                       */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class Main : MonoBehaviour
{
    private void Awake()
    {
        Assert.raiseExceptions = true;

        InitalizeServices();

        //Services.Scenes.PushScene<TitleSceneScript>();
    }

    private void InitalizeServices()
    {
        Services.Main = this;
        Services.GameManager = GetComponent<GameManagerScript>();
        Services.EventManager = new GameEventsManager();
        Services.GeneralTaskManager = new TaskManager();
        Services.Prefabs = Resources.Load<PrefabDB>("cjw487/Prefabs/PrefabDB");
        Services.Scenes = new GameSceneManager<TransitionData>(gameObject, Services.Prefabs.Scenes);
        
    }
}
