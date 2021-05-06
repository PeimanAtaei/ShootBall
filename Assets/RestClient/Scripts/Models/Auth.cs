using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
	[Serializable]
	public class Auth
	{
		public string status;
		
		
		public override string ToString(){
			return JsonUtility.ToJson (this, true);
		}
	}
}