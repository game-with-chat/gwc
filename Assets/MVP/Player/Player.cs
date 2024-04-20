using UnityEngine;


[RequireComponent(typeof(NetworkPlayer))]
[RequireComponent(typeof(PointClickControler))]
public class Player : MonoBehaviour {
	public string username = "p0";
	public TextMesh nametag;
	private NetworkPlayer networkPlayer;
	private PointClickControler controller;


	private void Start() {
		nametag.text = username;
		networkPlayer = GetComponent<NetworkPlayer>();
		controller = GetComponent<PointClickControler>();
	}
}