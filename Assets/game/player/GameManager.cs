using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Connection;
using System;
using NUnit.Framework;
using TMPro;
using FishNet.Broadcast;
using FishNet.Transporting;
using System.Text.RegularExpressions;


public class GameManager : NetworkBehaviour {
	public static event Action OnReady;
	private static GameManager gameManager;


	private void Awake() {
		gameManager = this;
		usernames.OnChange += InvokeUsernameChange;
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		OnReady?.Invoke();
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		base.ServerManager.RegisterBroadcast<ChatMessage>(SERVER_OnChat);
	}
	public override void OnStopServer()
	{
		base.OnStopServer();
		base.ServerManager.UnregisterBroadcast<ChatMessage>(SERVER_OnChat);
	}

	#region Usernames
	public static event Action<int, string> OnUsernameChange;
	private readonly SyncDictionary<int, string> usernames = new SyncDictionary<int, string>();


	private void InvokeUsernameChange(SyncDictionaryOperation op, int key, string value, bool asServer)
	{
		if(op == SyncDictionaryOperation.Add || op == SyncDictionaryOperation.Set) {
			OnUsernameChange?.Invoke(key,value);
		}
	}

	[ServerRpc(RequireOwnership =false)]
	private void SetUsername(string username=null, NetworkConnection sender = null) {
		int id = sender.ClientId;
		if(username==null) {
			usernames.Remove(id);
			return;
		}			
		usernames[sender.ClientId] = username;
	}

	[Client]
	public static void SetUsername(string username=null) {
		gameManager.SetUsername(username);
	}

	public static string GetUsername(int id) {
		if(gameManager.usernames.TryGetValue(id,out string result))
			return result;
		else
			return "player";
	} 
	#endregion

	#region Chat


	public struct ChatMessage: IBroadcast {
		public int id;
		public string room,message;
	}
	public void HUD_SendChat(TMP_InputField playerMessage) {
		string message = playerMessage.text;
		message = Regex.Replace(message,@"[0-9]","");

		if(message.Length == 0) return;

		base.ClientManager.Broadcast(new ChatMessage {
			message = message
		});

		playerMessage.text = "";
	}

	public void SERVER_OnChat(NetworkConnection connection, ChatMessage message, Channel channel) {
		NetworkObject networkObject = connection.FirstObject;
		message.id = connection.ClientId;
		if(networkObject == null) return;
		base.ServerManager.Broadcast<ChatMessage>(networkObject,message,true);
	}
	#endregion
}