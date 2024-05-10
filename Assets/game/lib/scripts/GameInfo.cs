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
		codename,
		version,
		unityVersion,
		platform,
		server,
		room,
		username,
		url
	;

	private string getPlatform() {
		
		switch (Application.platform)
		{
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXPlayer:
				return "Mac";
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
				return "Windows";
			case RuntimePlatform.Android:
				return "Android";
			case RuntimePlatform.LinuxPlayer:
			case RuntimePlatform.LinuxEditor:
				return "Linux";
			default:
			return "Unknown";
		}
	}

	private void OnEnable() {
		codename.text = Application.productName;
		version.text ="Version: " + Application.version;
		unityVersion.text = "Unity: "+Application.unityVersion;
		platform.text = "Platform: "+getPlatform();
		server.text = "Server:"+InstanceFinder.NetworkManager.ClientManager.Connection.GetAddress();

		username.text = "Username: "+GameManager.GetUsername(InstanceFinder.NetworkManager.ClientManager.Connection.ClientId);
		url.text = "url:"+Application.absoluteURL;


	}
}
