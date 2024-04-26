using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class BreakoutWalls : MonoBehaviour
{
	private new Camera camera;
	private EdgeCollider2D walls;

    // Start is called before the first frame update
    void Start()
    {
		camera = Camera.main;
        walls = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		walls.points = new Vector2[] {
			camera.ViewportToWorldPoint(Vector2.zero),
			camera.ViewportToWorldPoint(Vector2.up),
			camera.ViewportToWorldPoint(Vector2.one),
			camera.ViewportToWorldPoint(Vector2.right)
		};
        
    }
}
