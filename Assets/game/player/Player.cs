using UnityEngine;
using FishNet.Object;
using FishNet.Component.Transforming;
using System;
using TMPro;
using FishNet.Connection;
using FishNet;
using FishNet.Broadcast;
using FishNet.Transporting;
using UnityEditor.MemoryProfiler;

[RequireComponent(typeof(PointClickControler))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NetworkTransform))]
public class Player : NetworkBehaviour  {


	[SerializeField]
	private TextMeshProUGUI usernameText;

	private ChatBalloon chatBubble;


	private void UpdateUsername() {
		string username = GameManager.GetUsername(base.OwnerId);
		usernameText.SetText(username);
		gameObject.name = "player_"+username;
	}
	private void OnUsernameChange(int id, string username)
	{
		if(id==base.OwnerId) {
			UpdateUsername();
		}
	}


	private void OnChat(GameManager.ChatMessage chat, Channel channel)
	{
		GetComponentInChildren<ChatBalloon>().Chat(chat.message);
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		GetComponent<PointClickControler>().enabled = base.IsOwner;
		GetComponent<AudioListener>().enabled = base.IsOwner;
		
		chatBubble = GetComponentInChildren<ChatBalloon>();

		UpdateUsername();


		//Events
		GameManager.OnUsernameChange += OnUsernameChange;
		base.ClientManager.RegisterBroadcast<GameManager.ChatMessage>(OnChat);
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
		GameManager.OnUsernameChange -= OnUsernameChange;
	}


}
