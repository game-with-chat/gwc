using System.Runtime.ConstrainedExecution;
using FishNet.Managing;
using FishNet.Transporting;
using TMPro;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class TestIP : MonoBehaviour
{
	#region Defaults
	public string ip = "localhost",
	server = "0.0.0.0";
	public ushort port = 7770;
	#endregion

#region GameObjects
	public TMP_InputField nicknameField, addressField, portField;
	public Button hostButton;
	public Canvas canvas;
	private NetworkManager networkManager;
	private Transport transport;

	#endregion

    // Start is called before the first frame update
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();

		transport = networkManager.TransportManager.Transport;
		transport.SetClientAddress(ip);
		transport.SetPort(port);
		transport.SetServerBindAddress(server,IPAddressType.IPv4);
		addressField.text = ip;
		portField.text = port.ToString();


		#if UNITY_WEBGL
		hostButton.gameObject.SetActive(false);
		#endif

    }

	public void ToggleMenu() {
		canvas.enabled = !canvas.enabled;
	}

	private void GatherFieldInfo() {
		ip = addressField.text;
		ushort.TryParse(portField.text,out port);
		
	}

	public void Click_Join(){
		GatherFieldInfo();
		transport.SetClientAddress(ip);
		transport.SetPort(port);
		networkManager.ClientManager.StartConnection(ip,port);
		ToggleMenu();
	}

	public void Click_Host(){
		GatherFieldInfo();
		transport.SetPort(port);
		transport.SetServerBindAddress(server,IPAddressType.IPv4);
		networkManager.ServerManager.StartConnection(port);
		addressField.text = "localhost";
		Click_Join();
		
	}
}
