using UnityEngine;
using FishNet.Object;
using FishNet.Component.Transforming;
using TMPro;
using FishNet.Transporting;
using JetBrains.Annotations;
using FishNet.Object.Synchronizing;
using System;
using GameKit.Dependencies.Utilities;

[RequireComponent(typeof(PointClickControler))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NetworkTransform))]
public class Player : NetworkBehaviour  {


	[SerializeField]
	private TextMeshProUGUI usernameText;

	private ChatBalloon chatBubble;
	

	[SerializeField]
	private Transform headSlot;
	[SerializeField]
	private GameObject testItem;

	public static Player current {get; private set; }

	private readonly SyncVar<bool> wearingHat = new SyncVar<bool>();


	public override void OnStartClient()
	{
		base.OnStartClient();
		GetComponent<PointClickControler>().enabled = base.IsOwner;
		GetComponent<AudioListener>().enabled = base.IsOwner;
		if(base.IsOwner) {
			Player.current = this;
		}
		
		chatBubble = GetComponentInChildren<ChatBalloon>();

		UpdateUsername();


		//Events
		GameManager.OnUsernameChange += OnUsernameChange;
		base.ClientManager.RegisterBroadcast<GameManager.ChatMessage>(OnChat);
		wearingHat.OnChange += OnHatChange;
	}


	public override void OnStopClient()
	{
		base.OnStopClient();
		GameManager.OnUsernameChange -= OnUsernameChange;
		wearingHat.OnChange -= OnHatChange;
	}

	private void OnHatChange(bool prev, bool next, bool asServer)
	{
		Debug.Log("OnHatChange");
		if(next!=prev) {
			if(next) {
				Instantiate(testItem,headSlot);
			}else {
				headSlot.DestroyChildren();
			}
		}
	}

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

		if(chat.id == base.OwnerId) GetComponentInChildren<ChatBalloon>().Chat(chat.message);
	}



	[ServerRpc(RequireOwnership =false)]
	public void WearHat() {
		Debug.Log("WearHAT");
		wearingHat.Value = !wearingHat.Value;
	}

	private void Update() {
		// if(Input.GetButtonDown("Submit")) {
		// 	Debug.Log("FDFDSFDSF");
		// 	// testItem.transform.SetParent(headSlot);
		// }
	}
}
