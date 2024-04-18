using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {
	public string username = "p0";

	public GameObject nametag;

	private void Start() {
		nametag.GetComponent<TextMesh>().text = username;
	}

	private void Update() {
		if(Input.GetButtonDown("GoHere")) {
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit mouseClickImpact;
			if(Physics.Raycast(mouseRay,out mouseClickImpact)) {
				Vector3 newPosition = mouseClickImpact.point;
				newPosition.y = transform.position.y;
				//transform.position = newPosition;
				 GetComponent<NavMeshAgent>().SetDestination(newPosition);

				 Vector3 direction = (newPosition - transform.position).normalized;
				transform.rotation = Quaternion.LookRotation(direction);
			}
		}
	}
}