using UnityEngine;
using FishNet.Object;
using FishNet.Component.Transforming;
using TMPro;
using FishNet.Transporting;
using JetBrains.Annotations;
using FishNet.Object.Synchronizing;

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
			Instantiate(testItem,headSlot);
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
		GameManager.OnUsernameChange -= OnUsernameChange;
	}

	public void Wear(Transform toWear) {
		toWear.SetParent(headSlot);
	}

	private void Update() {
		// if(Input.GetButtonDown("Submit")) {
		// 	Debug.Log("FDFDSFDSF");
		// 	// testItem.transform.SetParent(headSlot);
		// }
	}
}
