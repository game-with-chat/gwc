
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// [RequireComponent(typeof(Collider))]
public class Handle : MonoBehaviour, IBeginDragHandler, IDragHandler {
	[SerializeField]
	private Transform thingToMove;

	private Vector3 offsetFromMouse ;

	private void Awake() {
		if(thingToMove == null)
		thingToMove = transform;
	}

	private Vector3 getMouse() {
		return Pointer.current.position.ReadValue();
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		offsetFromMouse = thingToMove.position - getMouse();
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		thingToMove.position = getMouse() + offsetFromMouse;
	}




	// private void OnMouseDown() {
	// 	Debug.Log("HELLO");
	// 	offsetFromMouse = thingToMove.position - getMouse();
	// }

	// private void OnMouseDrag() {

	// }

	// private void OnMouseUp() {

	// }
}