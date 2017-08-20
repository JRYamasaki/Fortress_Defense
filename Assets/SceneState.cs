using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;


/**This class is a representation of the game's scene, holding references to relevant game objects in it and providing accesor
methods so game objects can interact with their scene and the objects in it. Since there is only one scene and its state is always the same at
a given point in time, this class follows the singleton design pattern.
@author Ruben Gonzalez.
*/
public class SceneState : MonoBehaviour {

	//One and only instance of the class:
	private static SceneState instance;

	/**Game objects that are relevant to the clients of this class.*/
	//Game object used by the player.
	public GameObject player;
	//Game object used as the ground of the game on which all game object are placed and interact in.
	public GameObject ground;
	//Reference to prefab used as the model for enemies in the game:
	public GameObject enemyModel;
	//List of game objects representing the enemies that the player has to deal with:
	private List<GameObject> enemies;


	// Use this for initialization
	void Start () 
	{
		this.enemies = new List<GameObject> ();
		//Instantiate the enemies to be present in the game and as many as defined in Gameplay.cs:
		for(int i = 0; i < Gameplay.NUM_OF_ENEMIES_AT_ANY_GIVEN_POINT; i++)
		{
			//enemyModel is the prefab for the enemy. getAnEnemyPosition returns a valid position for the enemy in the scene, as defined in
			//Gameplay. Quaternion.identity returns an instante with the same rotation as the scene's axes.
			GameObject enemy = (GameObject)Instantiate (enemyModel, Gameplay.getInstance().getAnEnemyPosition(), Quaternion.identity);
			this.addEnemy (enemy);
		}
	}

	/// <summary>
	/// Adds the provided enemy to this scene.
	/// </summary>
	/// <param name="enemy">Enemy to be added to the collection of enemies that this scene holds.</param>
	private void addEnemy(GameObject enemy)
	{
		this.enemies.Add (enemy);
		enemy.GetComponent<AlienController> ().setScene(this);
	
	}

	// Update is called once per frame
	void Update () 
	{

	}

	/**Initializes this class' instance field to itself in accordance with the Singleton design pattern.*/
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
		
	/// <summary>
	/// Gets the boundary value that any game object's transform's position value (in axes x (horizontal) and z (vertical))
	/// can take without leaving the ground of the scene - which would make the game object be out of sight of the player.
	/// </summary>
	/// <param name="axis">
	/// Axis in which the boundary value must be returned. If Axis.Horizontal, the boundary value
	/// for the value of the position's x is returned. If Axis.Vertical, that of the position's y is returned.
	/// </param>
	/// <param name="end">
	/// Boundary type required: it can be either BoundaryEnd.MAXIMUM or BoundaryEnd.MINIMUM. If BoundaryEnd.MAXIMUM, the
	/// maximum possible value for the specified axis is returned. If BoundaryEnd.MINIMUM, the minimum possible value for
	/// the specified axis is returned. 
	/// </param>
	/// <returns>The boundary value for the correct property specified by the arguments.</returns>
	public float getBoundaryValue(RectTransform.Axis axis, AssemblyCSharp.BoundaryEnd end)
	{
		//Get the centre of the position of the game object used for the ground:
		Vector3 groundCentre = this.ground.transform.position;
		//Based on the axis indicated, get the value of the property and the scale in that axis:
		float valueOfCentreCoordinate;
		float scale;
		switch(axis)
		{
			case RectTransform.Axis.Horizontal:
				valueOfCentreCoordinate = groundCentre.x;
				scale = this.ground.transform.lossyScale.x;
				break;
			case RectTransform.Axis.Vertical: 
				valueOfCentreCoordinate = groundCentre.z;
				scale = this.ground.transform.lossyScale.z;
				break;
			default: throw new PlayerPrefsException("In SceneState.getBoundaryValue(RectTransform.Axis, " +
				"RectTransform.Axis): the provided axis is neither horizontal nor vertical.");
		}

		//If scale is negative, change its sign to positive so in the following switch construct the return statements 
		//apply for both cases (when scale is negative and positive):
		if (scale < 0) 
		{
			scale = scale * -1;
		}

		//Based on whether this method's client wants the maximum or the minimum value, add or subtract half of scale to the 
		//value of the centre coordinate:
		switch(end)
		{
			case AssemblyCSharp.BoundaryEnd.MAXIMUM:
				return valueOfCentreCoordinate + (scale / 2.0f);
			case AssemblyCSharp.BoundaryEnd.MINIMUM: 
				return valueOfCentreCoordinate - (scale / 2.0f);
			default: throw new PlayerPrefsException("In SceneState.getBoundaryValue(RectTransform.Axis, " +
				"RectTransform.Axis): the provided boundary end must be either MAXIMUM or MINIMUM.");
		}
	}


	/// <summary>
	/// Gets the centre of the scene.
	/// </summary>
	/// <returns>The centre of the scene.</returns>
	public Vector3 getCentreOfTheScene()
	{
		return ground.transform.position;
	}

	/// <summary>
	/// Returns a boolean value indicating whether at least one enemy has reached the end point.
	/// </summary>
	/// <returns><c>true</c>, if enemy has reached the end point of the game as specified in Gameplay; <c>false</c> otherwise.</returns>
	public void setAnEnemyHasReachedEndPoint()
	{
		foreach(GameObject enemy in this.enemies)
		{
			if(enemy.GetComponent<AlienController>().hasReachedGoalPoint())
			{
				return true;
			}
		}
		return false;
	}
}
