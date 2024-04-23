using UnityEngine;
using FishNet.Object;
using FishNet.Component.Transforming;
using System;
using TMPro;
using FishNet.Connection;

[RequireComponent(typeof(PointClickControler))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NetworkTransform))]
public class Player : NetworkBehaviour  {

	[SerializeField]
	private TextMeshProUGUI usernameText;


	private void UpdateUsername() {
		string username = UsernameManager.GetUsername(base.OwnerId);
		usernameText.SetText(username);
		gameObject.name = "player_"+username;
	}
	private void OnUsernameChange(int id, string username)
	{
		if(id==base.OwnerId) {
			UpdateUsername();
		}
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		GetComponent<PointClickControler>().enabled = base.IsOwner;
		GetComponent<AudioListener>().enabled = base.IsOwner;
		UpdateUsername();
		UsernameManager.OnUsernameChange += OnUsernameChange;
	}


	public override void OnStopClient()
	{
		base.OnStopClient();
		UsernameManager.OnUsernameChange -= OnUsernameChange;
	}


}
