using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChatBalloon : MonoBehaviour
{
	Vector2 lastSize;

	[SerializeField]
	private TextMeshProUGUI textBox;

	[SerializeField]
	private float margin = .1f;



	[SerializeField]
	private float balloonLifespan = 5;

	private float lifetime = 0;

	private void Awake()
	{
		GetComponent<MeshFilter>().mesh = new Mesh() { name = "Chat Balloon" };
	}

	private void setEnabled(bool enabled)
	{
		GetComponent<MeshRenderer>().enabled = textBox.enabled = enabled;
	}

	private void Update()
	{

		if (lifetime == balloonLifespan)
		{
			//setup
			UpdateBalloon(new Vector2(
				textBox.renderedWidth,
				textBox.renderedHeight
			));
		}

		if (lifetime > 0)
		{
			lifetime -= Time.deltaTime;
		}
		else
		{
			//setEnabled(false);
		}
	}

	public void Chat(string message)
	{
		textBox.text = message;
		lifetime = balloonLifespan;
		setEnabled(true);
	}

	private void UpdateBalloon(Vector2 size)
	{
		if(lastSize != size) {
			lastSize = size;
		}
		RectTransform rectTransform = GetComponent<RectTransform>();

		Vector2 triangle = new Vector2(margin * 2, margin * 2);
		float trianglePostion = size.x * .10f;

		Vector2 meshSize = new Vector2(
			margin * 2 + size.x,
			triangle.y + 2 * margin + size.y
		);

		Vector3 start = -meshSize / 2;

		rectTransform.sizeDelta = meshSize;

		Mesh mesh = GetComponent<MeshFilter>().mesh;

		mesh.vertices = new Vector3[] {
				start + new Vector3(0,meshSize.y-margin),
				start + new Vector3(margin,meshSize.y-margin),
				start + new Vector3(margin,meshSize.y),
				start + new Vector3(meshSize.x-margin,meshSize.y),
				start + new Vector3(meshSize.x-margin,meshSize.y-margin),
				start + new Vector3(meshSize.x,meshSize.y-margin),
				start + new Vector3(meshSize.x,triangle.y+margin),
				start + new Vector3(meshSize.x-margin,triangle.y+margin),
				start + new Vector3(meshSize.x-margin,triangle.y),
				start + new Vector3(meshSize.x-margin-trianglePostion,triangle.y),
				start + new Vector3(meshSize.x-margin-trianglePostion-triangle.x,0),
				start + new Vector3(meshSize.x-margin-trianglePostion-triangle.x,triangle.y),
				start + new Vector3(margin,triangle.y),
				start + new Vector3(margin,triangle.y+margin),
				start + new Vector3(0,triangle.y+margin),
			};
		mesh.triangles = new int[] {
				0,2,1,
				1,2,4,
				2,3,4,
				3,5,4,
				0,13,14,
				0,1,13,
				1,7,13,
				1,4,7,
				4,6,7,
				4,5,6,
				14,13,12,
				13,8,12,
				13,7,8,
				7,6,8,
				11,9,10
			};

	}
}