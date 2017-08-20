using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all the rules governing this game. It provides a significant part of what game objects in the scene need to know
/// to interact with other game objects and how. Because the set of rules for the game is always one and the same, this class follows the
/// singleton pattern.
/// </summary>
public class Gameplay : MonoBehaviour 
{
	/// <summary>
	/// Variable used to store a reference to the only instance of this class that can ever exist in a given program.
	/// </summary>
	private static Gameplay instance;
	/// <summary>
	/// If the player's position is within this radius of the enemy's position, the enemy will go after the player and attack them.
	/// Then, if the enemy's position is (x, y, z), it will go after the player is the player's position is (x + 50, y + 50, z + 50)
	/// or closer.
	/// </summary>
	public const int RADIUS_AT_WHICH_ENEMY_GOES_AFTER_PLAYER = 50;
	/// <summary>
	/// Determines the number of enemies that will be present at any given point of the game. 
	/// </summary>
	public const int NUM_OF_ENEMIES_AT_ANY_GIVEN_POINT = 4;
	/// <summary>
	/// Enemies will try to get to this point in the map. If any of them does, the game should be terminated and the player has lost.
	/// </summary>
	public Vector3 GOAL_END_POINT_FOR_ENEMY = SceneState.getInstance().getCentreOfTheScene();
	/// <summary>
	/// If any of the enemies' positions is within this much radius of the goal end point's position, defined in this class,
	/// the enemy is considered to have reached the goal position.
	/// </summary>
	public const int RADIUS_AT_WHICH_ENEMY_IS_CONSIDERED_TO_HAVE_REACHED_END_POINT = 10;

	/// <summary>
	/// Initializes the only instance of the <see cref="Gameplay"/> class.
	/// </summary>
	private Gameplay()
	{
		instance = this;
	}

	/// <summary>
	/// Gets the instance of this class following the singleton pattern.
	/// </summary>
	/// <returns>The only instance of this class.</returns>
	public static Gameplay getInstance()
	{
		if (instance == null)
			instance = new Gameplay ();
		return instance;
	}

	/// <summary>
	/// Returns a boolean value that indicates whether the game is over. If true is returned, the player has lost.
	/// </summary>
	/// <returns>True is the game is over. False, otherwise.</returns>
	private bool gameOver()
	{
		//TODO: this is a stub. Implement it;
	}

	/// <summary>
	/// Ends the game.
	/// </summary>
	private void endGame()
	{
		//TODO: Implement.
	}

	/// <summary>
	/// Gets the position for an enemy that is about to be inserted into the game.
	/// </summary>
	/// <returns>The position for enemy. The position is on the ground of the game (i.e. never outside).</returns>
	public Vector3 getAnEnemyPosition()
	{
		//The enemy position will be inserted at least this far away from the border of the scene (the border of the ground):
		const int UNITS_AWAY_FROM_SCENE_BORDER = 10;
		//Declare variables used for creating the enemy's position:
		float zCoordinate = 0.0f, xCoordinate = 0.0f; //y value of position is always 0. Default values provided because compiler was complaining.
		//Visualize a view of the ground from above. You can see a plane. Imagine a smaller plane contained in it.
		//Enemies will be inserted on the edge of the smaller plane. To simply the calculation of the enemy's position,
		//pick one of two situations: the enemy's position is either on the top or bottom side of the plane, OR on the
		//right or left side of it. If the randomly generated value is on [0, 5], the enemy position is either on the
		//top or the bottom; if it is on [5, 10], it is on either the left or the right.
		int positionChoice = Random.Range(0, 11);

		//Find coordinates of a position on the top or bottom of the smaller plane:
		if (positionChoice <= 5) 
		{
			//Determine randomly whether the enemy should be on the top or on the bottom. 0 is top; 1 is bottom:
			int topOrBottomChoice = Random.Range(0, 2);
			//Find the z coordinate:
			switch (topOrBottomChoice) 
			{
				case 0: //Enemy is on the top.
					zCoordinate = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Vertical, AssemblyCSharp.BoundaryEnd.MAXIMUM);
					zCoordinate -= UNITS_AWAY_FROM_SCENE_BORDER; //The z coordinate of the enemy's position is UNITS_AWAY_FROM_SCENE_BORDER:
					break;
				case 1: //Enemey is on the bottom.
					zCoordinate = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Vertical, AssemblyCSharp.BoundaryEnd.MINIMUM);
					zCoordinate += UNITS_AWAY_FROM_SCENE_BORDER;//The z coordinate of the enemy's position is UNITS_AWAY_FROM_SCENE_BORDER:
					break;
			}
			//Find the x coordinate:
			float minimumX = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Horizontal, AssemblyCSharp.BoundaryEnd.MINIMUM),
			maximumX = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Horizontal, AssemblyCSharp.BoundaryEnd.MAXIMUM);
			xCoordinate = Random.Range(minimumX + UNITS_AWAY_FROM_SCENE_BORDER, maximumX - UNITS_AWAY_FROM_SCENE_BORDER);
		}
		//Find coordinates of a position on the left or right of the smaller plane:
		else 
		{
			//Determine randomly whether the enemy should be on the right or left of the plane. 0 is right; 1 is left:
			int rightOfLeftChoice = Random.Range(0, 2);
			//Find the x coordinate:
			switch (rightOfLeftChoice) 
			{
				case 0: //Enemy is on the right.
					xCoordinate = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Horizontal, AssemblyCSharp.BoundaryEnd.MAXIMUM);
					xCoordinate -= UNITS_AWAY_FROM_SCENE_BORDER; //The x coordinate of the enemy's position is UNITS_AWAY_FROM_SCENE_BORDER:
					break;
				case 1: //Enemy is on the left.
					xCoordinate = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Horizontal, AssemblyCSharp.BoundaryEnd.MINIMUM);
					xCoordinate += UNITS_AWAY_FROM_SCENE_BORDER;//The x coordinate of the enemy's position is UNITS_AWAY_FROM_SCENE_BORDER:
					break;
			}
			//Find the z coordinate:
			float minimumZ = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Vertical, AssemblyCSharp.BoundaryEnd.MINIMUM),
			maximumZ = SceneState.getInstance ().getBoundaryValue (RectTransform.Axis.Vertical, AssemblyCSharp.BoundaryEnd.MAXIMUM);
			zCoordinate = Random.Range(minimumZ + UNITS_AWAY_FROM_SCENE_BORDER, maximumZ - UNITS_AWAY_FROM_SCENE_BORDER);
			
		}
		//Return the position with the found coordinates:
		return new Vector3(xCoordinate, 0, zCoordinate);
	}

	/// <summary>
	/// Gets the initial position for the player.
	/// </summary>
	/// <returns>The initial position of the player.</returns>
	public Vector3 getPlayerInitialPosition()
	{
		//The following coordinates were taken from the current position value of the player's transform, as of August 19th, 2017.
		//This function could generate a random position in the scene to make the game more challenging.
		return new Vector3(0.0f, 0.5f, 30.0f);
	}


	/// <summary>
	/// Checks whether the current game has a valid state and things are flowing as expected. If, for instance, the number of 
	/// enemies is not the one expected, returns false.
	/// </summary>
	/// <returns></returns>
	private bool hasValidState()
	{
		//TODO: Implement
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

}
