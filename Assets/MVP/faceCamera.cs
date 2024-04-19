using UnityEngine;

public class alwaysFaceCamera : MonoBehaviour
{
	public bool reverse = false;
    // Update is called once per frame
    void Update()
    {
		Vector3 direction = Camera.main.transform.position-transform.position;
		if(reverse) direction *=-1;
		direction.x = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
