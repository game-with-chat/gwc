using UnityEngine;

public class alwaysFaceCamera : MonoBehaviour
{
	public bool reverse = false;
	public bool stayUpright = false;
    // Update is called once per frame
    void Update()
    {
		Vector3 direction = Camera.main.transform.position-transform.position;
		if(reverse) direction *=-1;
		if(stayUpright) direction.x = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
