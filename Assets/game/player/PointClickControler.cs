using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent),typeof(PlayerInput))]
public class PointClickControler : MonoBehaviour
{
	public float speed = 5;
	private NavMeshAgent navMesh;

	private void Start() {
		navMesh = GetComponent<NavMeshAgent>();
		navMesh.speed = speed;
		navMesh.angularSpeed = 0;
		navMesh.acceleration = 9999;
		navMesh.stoppingDistance =  0;
		navMesh.autoBraking = false;
	}

	private bool mouseHit(out Vector3 mouseClickPos)
	{
		Ray mouseRay = Camera.main.ScreenPointToRay( Pointer.current.position.ReadValue());
		RaycastHit mouseClickImpact;
		if (!EventSystem.current.IsPointerOverGameObject()&&Physics.Raycast(mouseRay, out mouseClickImpact))
		{
			mouseClickPos = mouseClickImpact.point;
			return true;
		}
		mouseClickPos = Vector3.zero;
		return false;

	}

	private void Update()
	{
	}

	public void OnGoHere(InputAction.CallbackContext context) {		
		
		if (mouseHit(out Vector3 newPosition))
		{
			newPosition.y = transform.position.y; // No Pecking at the ground

			navMesh.SetDestination(newPosition);
			//transform.position = newPosition;

			Vector3 direction = (newPosition - transform.position).normalized;
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}

}