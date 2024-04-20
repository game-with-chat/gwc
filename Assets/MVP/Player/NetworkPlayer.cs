using UnityEngine;
using FishNet.Object;
using FishNet.Component.Transforming;

[RequireComponent(typeof(PointClickControler))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NetworkTransform))]
public class NetworkPlayer : NetworkBehaviour  {

	private Camera playerCamera;

	public override void OnStartClient() {
		base.OnStartClient();
		if(base.IsOwner) {
			playerCamera = Camera.main;
		}
		GetComponent<PointClickControler>().enabled = base.IsOwner;
	}
}
