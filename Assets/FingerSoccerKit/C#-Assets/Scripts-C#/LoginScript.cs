using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.Networking;

public class LoginScript : MonoBehaviour {

	//private const string Url = "http://192.168.43.174:8000/api/auth";

	// Use this for initialization
	void Start () {
		Request ();

	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void Request()
	{
		
		WWW request = new WWW ("http://192.168.43.174:8000/api/auth");
		StartCoroutine (OnResponse(request));

	}

	private IEnumerator OnResponse(WWW req)
	{
		yield return req;

		Debug.Log (req.text);
	}

}
