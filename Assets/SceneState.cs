using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**This class is a representation of the game's scene, holding references to relevant game objects in it and providing accesor
methods so game objects can interact with their scene and the objects in it. Since there is only one scene and its state is always the same at
a given point in time, this class follows the singleton design pattern.
@author Ruben Gonzalez.
@lastmodified August 17th, 2017.
*/
public class SceneState : MonoBehaviour {

	//One and only instance of the class:
	private static SceneState instance;

	/**Game objects that are relevant to the clients of this class.*/
	//Game object used as the player.
	public GameObject player;


	/**Initializes this class' instance field to itself.*/
	private SceneState()
	{
		SceneState.instance = this;
	}

	/**Returns the one and only instance that exists of this class.*/
	public static SceneState getInstance()
	{
		if(SceneState.instance == null)
		{
			SceneState.instance = new SceneState ();
		}
		return SceneState.instance;
	}


	/**Returns the position of the player game object.*/
	public Vector3 getPositionOfPlayer()
	{
		return this.player.transform.position;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
