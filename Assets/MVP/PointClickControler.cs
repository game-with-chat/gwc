using UnityEngine;

public class PointClickControler : MonoBehaviour {
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
				transform.rotation = Quaternion.LookRotation(direction);
			}
		}
	}
}