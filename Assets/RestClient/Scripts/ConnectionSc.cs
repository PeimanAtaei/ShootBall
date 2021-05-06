using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using Models;

public class ConnectionSc : MonoBehaviour {

	private readonly string basePath = "https://game.atisapp.com/api/auth";
	public  GameObject alarmBox;

	void Start()
	{

		Auth ();
		CheckDeadLine ();
	}

	private void Auth()
	{
		RestClient.Get<Auth>(basePath).Then(user => {
			//EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(user.status, true), "Ok");

			Debug.Log(user.status);
			if (user.status == "active")
			{
				PlayerPrefs.SetInt("deadLine",0);
				CheckDeadLine();
				Debug.Log("User active");
			}
			else
			{
				Debug.Log("User inactive");
				PlayerPrefs.SetInt("deadLine",1);
				CheckDeadLine();
			}
		});

	}

	private void CheckDeadLine()
	{
		if(PlayerPrefs.GetInt("deadLine") == 1)
		{
			alarmBox.SetActive(true);
		}
	}
}
