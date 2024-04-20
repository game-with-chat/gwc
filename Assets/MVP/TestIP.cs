using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Spawning;
using FishNet.Managing;
using FishNet.Managing.Observing;
using FishNet.Object;
using FishNet.Transporting;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class TestIP : MonoBehaviour
{

	public string ip = "localhost";
	public string server = "0.0.0.0";
	public ushort port = 7770;
	public bool hostServer = false;

	private NetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();

		Transport transport = networkManager.TransportManager.Transport;
		transport.SetClientAddress(ip);
		transport.SetPort(port);
		transport.SetServerBindAddress(server,IPAddressType.IPv4);

		if(hostServer) {
			networkManager.ServerManager.StartConnection(port);
		} if(ip!="localhost") {
			networkManager.ClientManager.StartConnection(ip,port);
		}

		Debug.Log($"Address: {transport.GetClientAddress()}");
		Debug.Log($"Port: {transport.GetPort()}");

    }

    // Update is called once per frame
    void Update()
    {
    }
}
