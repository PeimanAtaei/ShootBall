using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class PlayerAI : MonoBehaviour {
		
	///*************************************************************************///
	/// Main Player AI class.
	/// why do we need a player AI class?
	/// This class has a reference to all player-1 (and player-2) units, their formation and their position
	/// and will be used to setup new formations for these units at the start of the game or when a goal happens.
	///*************************************************************************///

	//public Texture2D[] availableFlags;			//array of all available teams
	public List<Texture2D> availableFlags = new List<Texture2D> ();
	public List<Texture2D> newTeams1 = new List<Texture2D> ();
	public List<Texture2D> newTeams2 = new List<Texture2D> ();
	public List<Texture2D> newTeams3 = new List<Texture2D> ();
	public List<Texture2D> newTeams4 = new List<Texture2D> ();

	public static GameObject[] playerTeam;		//array of all player-1 units
	public static int playerFormation;			//player-1 formation
	public static int playerFlag;				//player-1 team flag

	//for two player game
	public static GameObject[] player2Team;		//array of all player-2 units
	public static int player2Formation;			//player-2 formation
	public static int player2Flag;				//player-2 team flag

	//flags
	internal bool canChangeFormation;


	//*****************************************************************************
	// Init. 
	//*****************************************************************************
	void init() {

		setNewTeams ();
		canChangeFormation = true;	//we just change the formation at the start of the game. No more change of formation afterwards!

		//fetch player_1's formation
		if(PlayerPrefs.HasKey("PlayerFormation"))
			playerFormation = PlayerPrefs.GetInt("PlayerFormation");
		else	
			playerFormation = 0; //Default Formation

		//fetch player_1's flag
		if(PlayerPrefs.HasKey("PlayerFlag"))
			playerFlag = PlayerPrefs.GetInt("PlayerFlag");
		else	
			playerFlag = 0; //Default team
		
		//cache all player_1 units
		playerTeam = GameObject.FindGameObjectsWithTag("Player");
		//debug
		int i = 1;
		foreach(GameObject unit in playerTeam) {
			//Optional
			unit.name = "PlayerUnit-" + i;
			unit.GetComponent<playerController>().unitIndex = i;
			unit.GetComponent<Renderer>().material.mainTexture = availableFlags[playerFlag];
			i++;
		}
		
		//if this is a 2-player local game
		if(GlobalGameManager.gameMode == 1) {

			//fetch player_2's formation
			if(PlayerPrefs.HasKey("Player2Formation"))
				player2Formation = PlayerPrefs.GetInt("Player2Formation");
			else	
				player2Formation = 0; //Default Formation
			
			//fetch player_2's flag
			if(PlayerPrefs.HasKey("Player2Flag"))
				player2Flag = PlayerPrefs.GetInt("Player2Flag");
			else	
				player2Flag = 1; //Default team

			//cache all player_2 units
			player2Team = GameObject.FindGameObjectsWithTag("Player_2");
			int j = 1;
			foreach(GameObject unit in player2Team) {
				//Optional
				unit.name = "Player2Unit-" + j;
				unit.GetComponent<playerController>().unitIndex = j;
				unit.GetComponent<Renderer>().material.mainTexture = availableFlags[player2Flag];
				j++;		
			}
		}
	}


	void Start (){

		//run the starting commands
		init ();


		if(!GlobalGameManager.isPenaltyKick)
			StartCoroutine(changeFormation(playerTeam, playerFormation, 1, 1));
		else
			StartCoroutine(goToPosition(playerTeam, GlobalGameManager.playerDestination, 1));

		//For two-player mode,
		if(GlobalGameManager.gameMode == 1) {

			if(!GlobalGameManager.isPenaltyKick)
				StartCoroutine(changeFormation(player2Team, player2Formation, 1, -1));
			else
				StartCoroutine(goToPosition(player2Team, GlobalGameManager.AIDestination, 1));
		}
			
		canChangeFormation = false;

	}

	//*****************************************************************************
	// changeFormation function take all units, selected formation and side of the player (left half or right half)
	// and then position each unit on it's destination.
	// speed is used to fasten the translation of units to their destinations.
	//*****************************************************************************
	public IEnumerator changeFormation ( GameObject[] _team ,   int _formationIndex ,   float _speed ,   int _dir  ){

		//cache the initial position of all units
		List<Vector3> unitsSartingPosition = new List<Vector3>();
		foreach(GameObject unit in _team) {
			unitsSartingPosition.Add(unit.transform.position); //get the initial postion of this unit for later use.
			unit.GetComponent<MeshCollider>().enabled = false;	//no collision for this unit till we are done with re positioning.
		}
		
		float t = 0;
		while(t < 1) {
			t += Time.deltaTime * _speed;
			for(int cnt = 0; cnt < _team.Length; cnt++) {
				_team[cnt].transform.position = new Vector3(	 Mathf.SmoothStep(	unitsSartingPosition[cnt].x, 
                                                              	 FormationManager.getPositionInFormation(_formationIndex, cnt).x * _dir,
                                                              	 t),
				                                            	 Mathf.SmoothStep(	unitsSartingPosition[cnt].y, 
												                 FormationManager.getPositionInFormation(_formationIndex, cnt).y,
												                 t),
				                                            	 FormationManager.fixedZ );
			}		
			yield return 0;
		}
		
		if(t >= 1) {
			canChangeFormation = true;
			foreach(GameObject unit in _team)
				unit.GetComponent<MeshCollider>().enabled = true; //collision is now enabled.
		}
	}


	//*****************************************************************************
	// move the unit to its position
	//*****************************************************************************
	public IEnumerator goToPosition ( GameObject[] unit, Vector3 pos,   float _speed  ){

		Vector3 unitsSartingPosition = unit[0].transform.position;
		unit[0].GetComponent<MeshCollider>().enabled = false;	//no collision for this unit till we are done with re positioning.

		float t = 0;
		while(t < 1) {
			t += Time.deltaTime * _speed;
			unit[0].transform.position = new Vector3( Mathf.SmoothStep(unitsSartingPosition.x, pos.x, t),
			                                   		  Mathf.SmoothStep(unitsSartingPosition.y, pos.y, t),
			                                     	  unitsSartingPosition.z );
				
			yield return 0;
		}

		if(t >= 1) {
			unit[0].GetComponent<MeshCollider>().enabled = true;
		}
	}

	public void setNewTeams()
	{
		Debug.Log ("Set Teams");
		if(PlayerPrefs.GetInt("shopItem-12") == 1)
		{
			Debug.Log ("Shop1");
			foreach (Texture2D item in newTeams1) {
				availableFlags.Add (item);
			}


		}

		if(PlayerPrefs.GetInt("shopItem-13") == 1)
		{
			Debug.Log ("Shop2");
			foreach (Texture2D item in newTeams2) {
				availableFlags.Add (item);
			}
		}

		if(PlayerPrefs.GetInt("shopItem-14") == 1)
		{
			Debug.Log ("Shop2");
			foreach (Texture2D item in newTeams3) {
				availableFlags.Add (item);
			}
		}

		if(PlayerPrefs.GetInt("shopItem-15") == 1)
		{
			Debug.Log ("Shop2");
			foreach (Texture2D item in newTeams4) {
				availableFlags.Add (item);
			}
		}
	}

}