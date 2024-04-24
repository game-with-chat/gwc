using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using FishNet.Object;
using JamesFrowen.SimpleWeb;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ChatBroadcast : NetworkBehaviour
{
	// public Transform chatHolder;
	// public GameObject msgElement;
	public TMP_InputField playerMessage;

	public override void OnStartClient()
	{
		base.OnStartClient();
	}

	// private void Update()
	// {
	// 	//If owner and space bar is pressed.
	// 	if (base.IsOwner && Input.GetKeyDown(KeyCode.Space))
	// 		RpcSendChat(playerMessage.text);
	// }

	public void SEND_CHAT() {
		RpcSendChat(playerMessage.text);
	}

	[ServerRpc(RequireOwnership =false)]
	private void RpcSendChat(string msg, NetworkConnection connection=null)
	{

		// need to update the chatlog box with the message

		string name = UsernameManager.GetUsername(connection.ClientId);
		
		Debug.Log($"Received {msg} on the server by ${name}");
	}
}

