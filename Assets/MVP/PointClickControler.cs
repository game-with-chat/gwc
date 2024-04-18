using UnityEngine;

public class Player : MonoBehaviour {
	public string username = "p0";

	public GameObject appearence;
	public GameObject nametag;

	private void Start() {
		nametag.GetComponent<	TextMesh>().text = username;
	}

	private void Update() {
		if(Input.GetButtonDown("GoHere")) {
			Debug.Log("Mouse clicked");
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit mouseClickImpact;
			if(Physics.Raycast(mouseRay,out mouseClickImpact)) {
				Debug.Log("mouse impact");
				Vector3 newPosition = mouseClickImpact.point;
				//transform.position = newPosition;
				 GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(newPosition);

				 Vector3 direction = (newPosition - transform.position).normalized;
				appearence.rotation = Quaternion.LookRotation(direction);
			}
		}
	}
}