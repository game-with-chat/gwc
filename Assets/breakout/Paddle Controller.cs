using System.ComponentModel;
using FishNet.Utility.Extension;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    
    void Update()
    {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition),
		position = transform.position;
		position.x = mousePos.x;
		transform.position = position;
    }
}
