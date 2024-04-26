

using System.Collections.Generic;
using FishNet;
using FishNet.Managing.Scened;
using FishNet.Object;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{

	[SerializeField]
	private string sceneName;


	private enum TriggerType:byte
	{
		Room,
		Minigame
	};

	[SerializeField]
	private TriggerType type;

	[Server]
	private void OnTriggerEnter(Collider other) {
		NetworkObject networkObject = other.GetComponent<NetworkObject>();
		if (networkObject != null) {
			ActivateTrigger(networkObject);
		}
	}



	private void LoadScene(NetworkObject networkObject,string sceneName) {
		if(!networkObject.Owner.IsActive) return;
		SceneLoadData loadData = new SceneLoadData(sceneName);
		switch (type)
		{
			case TriggerType.Room:
			loadData.MovedNetworkObjects = new NetworkObject[] {networkObject};
			loadData.ReplaceScenes = ReplaceOption.OnlineOnly;
			break;
			case TriggerType.Minigame:
			loadData.Options.AllowStacking = true;
			loadData.Options.LocalPhysics = LocalPhysicsMode.Physics2D;

			break;

			default:
			break;
		}
		InstanceFinder.SceneManager.LoadConnectionScenes(networkObject.Owner,loadData);


		SceneUnloadData unloadData = new SceneUnloadData(gameObject.scene.name);
		InstanceFinder.SceneManager.UnloadConnectionScenes(networkObject.Owner,unloadData);

		

	}

	private void ActivateTrigger(NetworkObject networkObject)
	{
		if(!networkObject.Owner.IsActive) return;
		LoadScene(networkObject,sceneName);
	}
}
