using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float constantSpeed = 10;

	private Paddle paddle;

	private bool hasStarted = false;

	// declare rigi for the rigidbody of the ball
	private Rigidbody2D rigi;

	private Vector3 paddleToBallVector;
	private AudioSource[] sounds;
	private AudioSource Click;
	private AudioSource Crack;

	// Use this for initialization
	void Start () {
	// initialize the rigidbody
		rigi  = GetComponent<Rigidbody2D>();
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
		sounds = GetComponents<AudioSource>();
		Click = sounds[0];
		Crack = sounds[1];
	}
	
	// Update is called once per frame
	void Update() {
		if (!hasStarted) {
			// Lock the ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;

			// Wait for a mouse press to launch
			if (Input.GetMouseButtonDown(0)) {
				//Debug.Log("mouse clicked");
				hasStarted = true;
				rigi.velocity = new Vector2 (0, 10f);
			}
		}
		// normalize ball speed
		rigi.velocity = constantSpeed * (rigi.velocity.normalized);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (hasStarted) {
			if (coll.gameObject.tag == "Breakable") {
				Crack.Play();
			} else {
				Click.Play();
			}
		}
	}
}
