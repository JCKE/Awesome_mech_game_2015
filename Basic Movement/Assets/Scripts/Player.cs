using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = 0.2f;
	public float accelerationTimeGrounded = 0.1f;
	public float currentFuel, maxFuel = 100;

	public float moveSpeed = 6;
	public float flyVelocity = 6;
	public float fuelUse = 1f;
	public float fuelRefill = 0.5f;

	public Slider fuelSlider;

	float gravity;
	float jumpVelocity;
	bool isFlying;
	Vector3 velocity;

	float velocityXSmoothing;

	Controller2D controller;
	
	void Start() {
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		currentFuel = maxFuel;
	}
	
	void Update() {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below){
			velocity.y = jumpVelocity;
		}

		if (Input.GetKey(KeyCode.LeftShift)){
			isFlying = true;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift)){
			isFlying = false;
		}

		if (isFlying){
			velocity.y = flyVelocity;
			currentFuel -= fuelUse*Time.deltaTime;
			if (currentFuel<=0){
				currentFuel = 0;
				velocity.y = gravity;
				isFlying = false;
			}
		}

		else if (currentFuel < maxFuel && velocity.y == 0){
			currentFuel += fuelRefill*Time.deltaTime;
		}

		fuelSlider.value = currentFuel;
		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
}