using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GameKit.Dependencies.Utilities;
using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 rotation = transform.rotation.eulerAngles;
		Vector3 newRotation = rotation;
		newRotation.z = -15 + 30*Mathf.PingPong(Time.time,1f)-.5f;
		transform.Rotate(newRotation-rotation);
		transform.localScale *= Mathf.PingPong(Time.time,1.0f);
		// float rotation = transform.rotation.eulerAngles.z;
		// rotation = Mathf.PingPong(rotation,100);

        // transform.rotation = Quaternion.Euler(0,rotation,0);
    }
}
