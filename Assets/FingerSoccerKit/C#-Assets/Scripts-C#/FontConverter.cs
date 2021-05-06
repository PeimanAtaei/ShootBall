using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontConverter : MonoBehaviour {

	public TextMesh[] Texts;
	public string[] Strings;

	void Start () {
		for (int i = 0; i < Texts.Length-1; i++) {
			Texts [i].text = Fa.faConvert ("خرید سکه ");
		}
	}
	

}
