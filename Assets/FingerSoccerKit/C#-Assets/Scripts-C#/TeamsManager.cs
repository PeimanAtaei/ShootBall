using UnityEngine;
using System.Collections;

public class TeamsManager : MonoBehaviour {


	///*************************************************************************///
	/// Main Teams manager.
	/// You can define new teams here.
	///*************************************************************************///

	public static int teams = 17;		//total number of available teams

	//Power differs from 1 (weakest) to 10 (strongest) (base power: 35)
	//additional time differs from 1 seconds (little) to 10 seconds (too much) (base time: 15 seconds)

	public static Vector2 getTeamSettings(int _teamID) {
		Vector2 settings = Vector2.zero;
		switch (_teamID) {
		case 0:
			settings = new Vector2(9, 5);
			break;
		case 1:
			settings = new Vector2(8, 6);
			break;
		case 2:
			settings = new Vector2(7, 8);
			break;
		case 3:
			settings = new Vector2(7, 6);
			break;
		case 4:
			settings = new Vector2(9, 3);
			break;
		case 5:
			settings = new Vector2(5, 10);
			break;
		case 6:
			settings = new Vector2(5, 7);
			break;
		case 7:
			settings = new Vector2(5, 9);
			break;
		case 8:
			settings = new Vector2(8, 3);
			break;
		case 9:
			settings = new Vector2(8, 6);
			break;
		case 10:
			settings = new Vector2(7, 7);
			break;
		case 11:
			settings = new Vector2(6, 7);
			break;
		case 12:
			settings = new Vector2(7, 6);
			break;
		case 13:
			settings = new Vector2(5, 7);
			break;
		case 14:
			settings = new Vector2(6, 6);
			break;







		case 15:
			settings = new Vector2(9, 9);
			break;

		case 16:
			settings = new Vector2(9, 8);
			break;

		case 17:
			settings = new Vector2(8, 8);
			break;

		case 18:
			settings = new Vector2(7, 8);
			break;

		case 19:
			settings = new Vector2(6, 7);
			break;




		case 20:
			settings = new Vector2(9, 9);
			break;

		case 21:
			settings = new Vector2(9, 8);
			break;

		case 22:
			settings = new Vector2(8, 8);
			break;

		case 23:
			settings = new Vector2(7, 8);
			break;

		case 24:
			settings = new Vector2(6, 7);
			break;


		
		case 25:
			settings = new Vector2(9, 9);
			break;

		case 26:
			settings = new Vector2(9, 8);
			break;

		case 27:
			settings = new Vector2(8, 8);
			break;

		case 28:
			settings = new Vector2(7, 8);
			break;

		case 29:
			settings = new Vector2(6, 7);
			break;




		default:
			settings = new Vector2(5, 3);
			break;
		}

		return settings;
	}

}