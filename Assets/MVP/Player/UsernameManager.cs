using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Connection;
using System;
using NUnit.Framework;


public class UsernameManager : NetworkBehaviour {

	public static event Action OnReady;
	public static event Action<int, string> OnUsernameChange;
	private readonly SyncDictionary<int, string> usernames = new SyncDictionary<int, string>();
	private static UsernameManager usernameManager;

	private void Awake() {
		usernameManager = this;
		usernames.OnChange += InvokeUsernameChange;
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		OnReady?.Invoke();
	}

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
		usernameManager.SetUsername(username);
	}

	public static string GetUsername(int id) {
		if(usernameManager.usernames.TryGetValue(id,out string result))
			return result;
		else
			return "player";
	} 
}