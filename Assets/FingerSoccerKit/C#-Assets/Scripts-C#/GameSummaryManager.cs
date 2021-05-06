using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSummaryManager : MonoBehaviour {

	/// <summary>
	/// This class gathers all usefull data throughout the match and shows them when the match is finished.
	/// - Notice:
	/// if applied power to ball is less than 24, we count it as a pass. Otherwise it is a shoot.
	/// if ball gets near to any gates, we count it as a shootToGoal.
	/// - Important:
	/// player-2 and opponent share some statistics, but we need to fetch their flags in separate calls.
	/// </summary>

	//public Texture2D[] availableFlags;
	public List<Texture2D> availableFlags = new List<Texture2D> ();
	public List<Texture2D> newTeams1 = new List<Texture2D> ();
	public List<Texture2D> newTeams2 = new List<Texture2D> ();
	//list of all available flags

	//reference to UI objects
	public GameObject uiP1Flag;
	public GameObject uiP2Flag;
	//-
	public GameObject uiP1Goals;
	public GameObject uiP2Goals;
	//-
	public GameObject uiP1Shoots;
	public GameObject uiP2Shoots;
	//-
	public GameObject uiP1Pass;
	public GameObject uiP2Pass;
	//-
	public GameObject uiP1ShootsToGate;
	public GameObject uiP2ShootsToGate;



	void Start () {
		setNewTeams ();
		showSummary ();
	}


	void showSummary() {

		///flags for player-1 and (player-2 or AI)
		//P1
		uiP1Flag.GetComponent<Renderer>().material.mainTexture = availableFlags[PlayerPrefs.GetInt("PlayerFlag")];
		//if this is a normal match (single or two player) and not a tournament game
		if(PlayerPrefs.GetInt("IsTournament") == 0)
			uiP2Flag.GetComponent<Renderer>().material.mainTexture = availableFlags[PlayerPrefs.GetInt("Player2Flag")];
		else
			uiP2Flag.GetComponent<Renderer>().material.mainTexture = availableFlags[PlayerPrefs.GetInt("OpponentFlag")];

		//goals
		uiP1Goals.GetComponent<TextMesh>().text = GlobalGameManager.playerGoals.ToString();
		uiP2Goals.GetComponent<TextMesh>().text = GlobalGameManager.opponentGoals.ToString();

		//passes
		uiP1Pass.GetComponent<TextMesh>().text = GlobalGameManager.playerPasses.ToString();
		uiP2Pass.GetComponent<TextMesh>().text = GlobalGameManager.opponentPasses.ToString();

		//shoots
		uiP1Shoots.GetComponent<TextMesh>().text = GlobalGameManager.playerShoots.ToString();
		uiP2Shoots.GetComponent<TextMesh>().text = GlobalGameManager.opponentShoots.ToString();

		//shoots to gate
		uiP1ShootsToGate.GetComponent<TextMesh>().text = GlobalGameManager.playerShootToGate.ToString();
		uiP2ShootsToGate.GetComponent<TextMesh>().text = GlobalGameManager.opponentShootToGate.ToString();

	}

	public void setNewTeams()
	{
		if(PlayerPrefs.GetInt("shopItem-13") == 1)
		{
			Debug.Log ("Shop1");
			foreach (Texture2D item in newTeams1) {
				availableFlags.Add (item);
			}


		}

		if(PlayerPrefs.GetInt("shopItem-12") == 1)
		{
			Debug.Log ("Shop2");
			foreach (Texture2D item in newTeams2) {
				availableFlags.Add (item);
			}
		}
	}

}
