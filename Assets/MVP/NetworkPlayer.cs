using UnityEngine;
using FishNet.Object;

[RequireComponent(typeof(PointClickControler))]
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
