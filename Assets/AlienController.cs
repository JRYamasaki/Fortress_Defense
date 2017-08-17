using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienController : MonoBehaviour 
{
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
		
	}
	
	// Update is called once per frame
	void Update () 
	{
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


}
