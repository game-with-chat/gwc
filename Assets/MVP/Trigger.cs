

using FishNet;
using FishNet.Managing.Scened;
using FishNet.Object;
using UnityEditor;
using UnityEngine;

public class Trigger : MonoBehaviour
{

	#if UNITY_EDITOR
	[SerializeField]
	private SceneAsset scene;

	private void OnValidate() {
		// SceneAsset is only available in the editor
		sceneName = scene.name;
	}

	#endif
	private string sceneName;

	[Server]
	private void OnTriggerEnter(Collider other) {
		NetworkObject networkObject = other.GetComponent<NetworkObject>();
		if (networkObject != null) {
			ActivateTrigger(networkObject);
		}
	}

	private void ActivateTrigger(NetworkObject networkObject)
	{
		if(!networkObject.Owner.IsActive) return;

		SceneLoadData sceneLoadData = new SceneLoadData(sceneName);
		sceneLoadData.MovedNetworkObjects = new NetworkObject[] {networkObject};
		sceneLoadData.ReplaceScenes = ReplaceOption.OnlineOnly;
		InstanceFinder.SceneManager.LoadConnectionScenes(networkObject.Owner,sceneLoadData);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
