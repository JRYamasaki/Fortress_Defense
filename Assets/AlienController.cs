using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienController : MonoBehaviour 
{
	//Flat to indicate whether the alien controlled by this script has reached the goal end point as defined in Gameplay.
	private bool reachedEndPoint;
	//Scene in which this alien is:
	private SceneState scene;
	//Animator in charge of this game object's animations:
	private Animator animator;
	//CharacterController used to move this game object around based on user input and environment:
	private CharacterController charController;
	//Multiplier to find the speed at which rotation must occur by mulplying translation speed by it:
	private const float TIMES_ROTATION_SPEED_IS_OF_TRANSLATION_SPEED = 16; //"A const object is always static"


	/**The fields below are temporary and used only for the initial stages of this script or for debugging purposes.*/
	//Used for determining what constant value to used to the speed of this game object.
	public float speed;
	//Used to display messages on the screen for debugging purposes:
	public Text label;

	// Use this for initialization
	void Start () 
	{
		this.animator = GetComponent<Animator> ();
		this.charController = GetComponent<CharacterController> ();
		this.reachedEndPoint = false;
		this.scene = null;
		
	}

	// Update is called once per frame
	void Update () 
	{

		//Generate a random number on [0, 1]
		int option = Random.Range(0, 1); //TODO Make upper range 2 again.
		//Based on the random number, make the alien do something:
		switch(option)
		{
			//Make the alien walk toward the end point for enemies, as defined in Gameplay:
			case 0: 
				//Make this alien move only if it has not reached the end point:
				if(!(this.hasReachedGoalPoint()))
					this.walkToward (Gameplay.getInstance().getGoalEndPointForEnemies());
				break;
			//Make the alien idle in its current position:
			case 1:
			//TODO: Implement idle method
				//this.idle ();
				break;
		}

		//Check if the alien reached the goal end point:
		this.updateStatusOfReachingEndPoint();

		/**TODO: Probably get rid of this code.
		//Control the game object based on user input:
		//If user wants to displace vertically (forward or backward), do so:
		if (Input.GetKey ("up") || Input.GetKey ("w") || Input.GetKey ("down") || Input.GetKey ("s")) {
			verticalDisplace ();
		} else 
		{
			this.animator.SetBool("isFlying", false);
		}

		//If user wants to rotate, do so:
		if(Input.GetKey ("right") || Input.GetKey ("d") || Input.GetKey("left") || Input.GetKey("a"))
		{
			rotate();
		}

		//Used for debugging purposes. Testing of SceneState class. GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_GET_RID_OF_THIS_
		if(Input.GetKey("g"))
		{
			Vector3 playersPosition = SceneState.getInstance ().getPositionOfPlayer ();
			float x = playersPosition.x, y = playersPosition.y, z = playersPosition.z;
			string playersCoordinates = "[" + x + ", " + y + ", " + z + "]";
			this.label.text = "The player's current coordinates are: " + playersCoordinates;
		}

		*/

		
	}

	/**Called when user input indicates this game object should be displaced forward or backward and, using such input, 
	 * moves the game object.*/
	private void verticalDisplace()
	{
		//Read user input:
		float displacement = Input.GetAxis("Vertical");
		//Turn on the flying animation that represents movement:
		this.animator.SetBool("isFlying", true);
		//Create a vector that represents the displacement to be made (note that if displacement < 0 - because the user wants to move backwards -
		//the direction of this.transform.forward is reversed, and so indeed a backward movement is achieved):
		Vector3 displacementVector = this.transform.forward * displacement * this.speed;
		//Apply the movement to the game object's character controller:
		this.charController.Move(displacementVector);
	}

	/**Called when user input indicates this game object should be rotated and, using such input, rotates the game object.*/
	private void rotate()
	{
		this.label.text = "rotate method called.";
		//Read user input:
		float turn = Input.GetAxis("Horizontal");
		transform.Rotate (0, turn * this.speed * AlienController.TIMES_ROTATION_SPEED_IS_OF_TRANSLATION_SPEED, 0);
	}

	/// <summary>
	/// Returns a boolean value indicating whether this Alien has reached the endpoint as defined for enemies in the Gameplay class.
	/// </summary>
	/// <returns><c>true</c>, if end goal point was reached, <c>false</c> otherwise.</returns>
	public bool hasReachedGoalPoint()
	{
		return this.reachedEndPoint;
	}


	/// <summary>
	/// Updates the status of reaching end point. If the alien has reached the goal end point as defined in the Gameplay class,
	/// it notifies the scene that holds it about this so the scene state is aware of this
	/// </summary>
	private void updateStatusOfReachingEndPoint()
	{
		//TODO: Implement
		//Depending on the alien's current position, set the flag:
		//this.reachedEndPoint ;//= something ;
		
	}

	/// <summary>
	/// Sets the provided scene to the scene which holds this alien. If another scene was set previously to calling this method with
	/// another scene, the reference to this object in that scene is removed and this object's scene is the new provided scene.
	/// </summary>
	/// <param name="currentScene">Current scene.</param>
	public void setScene(SceneState currentScene)
	{
		if (this.scene != currentScene) 
		{
			if (this.scene != null) 
			{
				currentScene.removeEnemy (this.gameObject);
			}

			this.scene = currentScene;
			if (this.scene != null) {
				currentScene.addEnemy(this.gameObject);
			}
		}
	}
		
	/// <summary>
	/// Makes this object move toward the provided point.
	/// </summary>
	/// <param name="point">Point to which this alien must move.</param>
	private void walkToward(Vector3 point)
	{
		//Rotate the alien so it "looks" toward point:
		this.transform.LookAt (point);

		//Turn on the flying animation that represents movement:
		if(!(this.animator.GetBool("isFlying")))
		{
			this.animator.SetBool("isFlying", true);
		}
		//This alien is looking at the point. Translate this alien:
		this.charController.Move(this.transform.forward * speed);

		//TODO: Get rid of PROBABLY unnecessary code:
		/**
		//Find the offset between this alien's current position and the point:
		Vector3 offset = point - this.transform.position;
		//Get the direction that the alien must have:
		Vector3 displacementDirection = offset.normalized;


		//Turn on the flying animation that represents movement:
		this.animator.SetBool("isFlying", true);
		*/

	}


}
