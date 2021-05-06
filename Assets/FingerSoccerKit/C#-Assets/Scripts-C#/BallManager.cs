using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {
		
	//*****************************************************************************
	// Main Ball Manager.
	// This class controls ball collision with Goal triggers and gatePoles, 
	// and also stops the ball when the spped is too low.
	//*****************************************************************************

	private GameObject gameController;			//Reference to main game controller
	public AudioClip ballHitPost;				//Sfx for hitting the poles

	//summary information
	private bool canCheck;
	private float threshold = 8.0f;
	private GameObject playerGate;
	private GameObject opponentGate;
	private float distanceToPlayerGate;			//ball's distance to player gate
	private float distanceToOpponentGate;		//ball's distance to opponent gate

	void Awake (){
		gameController = GameObject.FindGameObjectWithTag("GameController");
		playerGate = GameObject.FindGameObjectWithTag("playerGoalTrigger");
		opponentGate = GameObject.FindGameObjectWithTag("opponentGoalTrigger");
		canCheck = true;
	}

	void Update (){

		manageBallFriction();

		//get distances
		distanceToPlayerGate = Vector3.Distance(transform.position, playerGate.transform.position);
		distanceToOpponentGate = Vector3.Distance(transform.position, opponentGate.transform.position);

		if(canCheck && !GlobalGameManager.goalHappened)
			StartCoroutine (checkForShootToGoal());

		//debug
		//if(distanceToPlayerGate <= threshold || distanceToOpponentGate <= threshold)
		//	print("Ball's distanceToPlayerGate: " + distanceToPlayerGate + "\n\r" + "Ball's distanceToOpponentGate: " + distanceToOpponentGate);
	}

	void LateUpdate (){
		//we restrict rotation and position once again to make sure that ball won't has an unwanted effect.
		transform.position = new Vector3(transform.position.x,
			                             transform.position.y,
			                             -0.5f);

		//if you want a fixed ball with no rotation, uncomment the following line:
		//transform.rotation = Quaternion.Euler(90, 0, 0);
	}


	/// <summary>
	/// Checks for shoot to goal event every 5 seconds
	/// </summary>
	IEnumerator checkForShootToGoal() {

		if (distanceToOpponentGate <= threshold && GlobalGameManager.playersTurn) {
			print ("[LEFT] Shoot to goal happened!");
			GlobalGameManager.playerShootToGate++;
			canCheck = false;
			StartCoroutine (reactiveCheck ());
			yield break;
		}

		if (distanceToPlayerGate <= threshold && GlobalGameManager.opponentsTurn) {
			print ("[RIGHT] Shoot to goal happened!");
			GlobalGameManager.opponentShootToGate++;
			canCheck = false;
			StartCoroutine (reactiveCheck ());
			yield break;
		}
			
	}

	//*****************************************************************************
	// Check ball's speed at all times.
	//*****************************************************************************
	private float ballSpeed;
	void manageBallFriction (){
		ballSpeed = GetComponent<Rigidbody>().velocity.magnitude;
		//print("Ball Speed: " + rigidbody.velocity.magnitude);
		if(ballSpeed < 0.5f) {
			//forcestop the ball
			//rigidbody.velocity = Vector3.zero;
			//rigidbody.angularVelocity = Vector3.zero;
			GetComponent<Rigidbody>().drag = 2;
		} else {
			//let it slide
			GetComponent<Rigidbody>().drag = 0.9f;
		}
	}

	void OnCollisionEnter ( Collision other  ){
		switch(other.gameObject.tag) {
			case "gatePost":
				playSfx(ballHitPost);
				break;
		}
	}

	void OnTriggerEnter ( Collider other  ){
		switch(other.gameObject.tag) {
			case "opponentGoalTrigger":
				StartCoroutine(gameController.GetComponent<GlobalGameManager>().managePostGoal("Player"));
				break;
				
			case "playerGoalTrigger":
				StartCoroutine(gameController.GetComponent<GlobalGameManager>().managePostGoal("Opponent"));
				break;
		}
	}

	//*****************************************************************************
	// Play sound clips
	//*****************************************************************************
	void playSfx ( AudioClip _clip  ){
		GetComponent<AudioSource>().clip = _clip;
		if(!GetComponent<AudioSource>().isPlaying) {
			GetComponent<AudioSource>().Play();
		}
	}


	/// <summary>
	/// Enables to check for shoot to goal event again
	/// </summary>
	IEnumerator reactiveCheck() {
		yield return new WaitForSeconds (5.0f);
		canCheck = true;
	}

}