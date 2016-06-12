using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public bool autoPlay = false;
	private Ball ball;
	private float lastPosX;
	private float movementX;
	// coefficient to get a value that changes the speed in the x axis. increase for stronger x velocity, decrease to weaken
	public int movementCoefficient = 5000;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		print(ball);
	}
	
	// Update is called once per frame
	void Update() {
		if (!autoPlay) {
			MoveWithMouse();
		} else {
			AutoPlay();
		}


		// get x position of the paddle
		float posX = this.transform.position.x;

		// calculate the difference of the x position between the last frame and this frame
		float diffPosX = posX - lastPosX;

		// save the movement of the X axis that happened between the 2 frames
		// multiply by deltaTime (time between 2 frames) to get the same result on all pcs/devices
		// then multiply by coefficient to get a comfortable x velocity
		movementX = diffPosX * Time.deltaTime * movementCoefficient;

		// save the last x position for the calculation of the next movement difference
		lastPosX = posX;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// if the paddle collides with the ball
		if (coll.gameObject.tag == "Ball") {
			// get the x and y velocity of the ball
			float velY = coll.gameObject.GetComponent<Rigidbody2D>().velocity.y;
			float velX = coll.gameObject.GetComponent<Rigidbody2D>().velocity.x;
			// set the new velocity to the ball (x value takes the movement of the paddle and the old x velocity of the ball
			// and halves that value together to get a more realistic feeling
			coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 ((movementX + velX) / 2, velY);
		}
	}

	void MoveWithMouse() {
		Vector3 paddlePos = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);

		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;

		paddlePos.x = Mathf.Clamp(mousePosInBlocks, 0.5f, 15.5f);

		this.transform.position = paddlePos;
	}

	void AutoPlay() {
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y, 0f);

		Vector3 ballPos = ball.transform.position;

		paddlePos.x = Mathf.Clamp((ballPos.x + Random.Range(-.05f, .05f)), 0.5f, 15.5f);

		this.transform.position = paddlePos;
	}
}
