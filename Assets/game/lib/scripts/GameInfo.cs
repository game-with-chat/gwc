using System;
using System.Collections;
using System.Collections.Generic;
using FishNet;
using TMPro;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI
		url
	;

	

	private void OnEnable() {
		url.text = "url: "+Application.absoluteURL;


	}
}
