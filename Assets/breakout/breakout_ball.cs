using UnityEngine;

[RequireComponent(typeof (Rigidbody2D), typeof (BoxCollider2D))]
public class BreakoutBall : MonoBehaviour
{
	[SerializeField]
	private float speed=5;
	private Rigidbody2D rigidbody;

	public float startWait { get; private set; }


	// Start is called before the first frame update
	void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();

		 Reset();
		
		
    }

	public void Reset() {
		transform.position = Vector2.down*2;
		startWait = 3;
	}


    // Update is called once per frame
    void Update()
    {
		if(startWait>0) {
			rigidbody.Sleep();
			startWait-=Time.fixedDeltaTime;
		} else if (!rigidbody.IsAwake()) {
			rigidbody.WakeUp();
			rigidbody.velocity = Vector2.one*speed;
		}
    }
}
