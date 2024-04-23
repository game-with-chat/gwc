using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using JamesFrowen.SimpleWeb;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ChatBroadcast : MonoBehaviour
{
	public Transform chatHolder;
	public GameObject msgElement;
	public TMP_InputField playerUsername, playerMessage;

	private void Update()
	{
		//If owner and space bar is pressed.
		if (base.IsOwner && Input.GetKeyDown(KeyCode.Space))
			RpcSendChat(playerMessage);
	}

	[ServerRpc]
	private void RpcSendChat(string msg)
	{

		// need to update the chatlog box with the message
		
		Debug.Log($"Received {msg} on the server.");
	}
}

