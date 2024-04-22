using System;
using FishNet.Managing;
using FishNet.Transporting;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;
using FishNet.Transporting.Bayou;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class MainMenu : MonoBehaviour
{
	#region Defaults

	public string username = "p0";
	public string ip = "localhost",
	server = "0.0.0.0";
	public ushort port = 7770;
	#endregion

#region GameObjects
	public TMP_InputField nicknameField, addressField, portField;
	public Button hostButton;
	public Canvas canvas;
	private NetworkManager networkManager;
	private Multipass multipass;

	#endregion

    // Start is called before the first frame update
    void Awake()
    {
        networkManager = GetComponent<NetworkManager>();

		multipass = GetComponent<Multipass>();

		#if UNITY_WEBGL && !UNITY_EDITOR
		multipass.SetClientTransport<Bayou>();
		hostButton.gameObject.SetActive(false);
		#else
		multipass.SetClientTransport<Tugboat>();
		#endif

		//multipass = (Multipass) networkManager.TransportManager.Transport;
		// transport.SetClientAddress(ip);
		// transport.SetPort(port,0);
		// transport.SetPort(port,1);
		// transport.SetServerBindAddress(server,IPAddressType.IPv4);
		addressField.text = ip;
		portField.text = port.ToString();

		networkManager.ClientManager.OnClientConnectionState += OnClientState;
		networkManager.ServerManager.OnServerConnectionState += OnServerState;
		UsernameManager.OnReady += SetUsername;


    }

	private void SetUsername()
	{
		UsernameManager.SetUsername(username);
	}

	private void OnClientState(ClientConnectionStateArgs args)
	{
		switch (args.ConnectionState)
		{
			case LocalConnectionState.Stopped:
				canvas.enabled = true;
				Debug.Log("CLIENT HAS DISCONNECTED");
			break;
			case LocalConnectionState.Started:
				canvas.enabled = false;
				Debug.Log("CLIENT HAS CONNECTED");
			break;
		}
	}
	private void OnServerState(ServerConnectionStateArgs args)
	{
		switch (args.ConnectionState)
		{
			case LocalConnectionState.Stopped:
				Debug.Log("SERVER HAS STOPPED");
			break;
			case LocalConnectionState.Started:
				addressField.text = "localhost";
				Debug.Log("SERVER HAS STARTED");
				Click_Join();
			break;
		}
	}

	private void GatherFieldInfo() {
		ip = addressField.text;
		username = nicknameField.text;
		ushort.TryParse(portField.text,out port);
	}

	public void Click_Join(){
		GatherFieldInfo();
		multipass.SetClientAddress(ip);
		for (int i = 0; i < multipass.Transports.Count; i++)
		{
			multipass.SetPort(port,i);
		}
		networkManager.ClientManager.StartConnection();
	}

	public void Click_Host(){
		GatherFieldInfo();
		for (int i = 0; i < multipass.Transports.Count; i++)
		{
			multipass.SetPort(port,i);
			multipass.SetServerBindAddress(server,IPAddressType.IPv4,i);
		}
		networkManager.ServerManager.StartConnection();
	}
}
