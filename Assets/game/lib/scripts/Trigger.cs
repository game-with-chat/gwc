

using FishNet;
using FishNet.Managing.Scened;
using FishNet.Object;
using UnityEditor;
using UnityEngine;

public class Trigger : MonoBehaviour
{

	[SerializeField]
	private string sceneName;

	[Server]
	private void OnTriggerEnter(Collider other) {
		NetworkObject networkObject = other.GetComponent<NetworkObject>();
		if (networkObject != null) {
			ActivateTrigger(networkObject);
		}
	}



	private void JoinRoom(NetworkObject networkObject,string sceneName) {
		if(!networkObject.Owner.IsActive) return;
		SceneLoadData loadData = new SceneLoadData(sceneName);
		loadData.MovedNetworkObjects = new NetworkObject[] {networkObject};
		loadData.ReplaceScenes = ReplaceOption.OnlineOnly;
		InstanceFinder.SceneManager.LoadConnectionScenes(networkObject.Owner,loadData);


		SceneUnloadData unloadData = new SceneUnloadData(gameObject.scene.name);
		InstanceFinder.SceneManager.UnloadConnectionScenes(networkObject.Owner,unloadData);

		

	}

	private void ActivateTrigger(NetworkObject networkObject)
	{
		if(!networkObject.Owner.IsActive) return;
		JoinRoom(networkObject,sceneName);
	}
}
