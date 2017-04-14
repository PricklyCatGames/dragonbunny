using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
	#region variables
	float v;
	float h;
	bool jumpButton;
	bool actionButton;
	bool cancelButton;

	public bool isControllable;
	public bool isTalking;

	public bool canJump;
	public float deadzone = 0.15f;
	public float maxSpeed;
	public float minSpeed;
	public float currentSpeed;
	public float horizontalSpeed;
	public float shimmySpeed;
	public float climbSpeed;
	public float acceleration;
	public float turnSpeed;
	public float jumpSpeed;
	public bool isJumping;
	float lastJumpTime;
	public float jumpTimeout = 0.05f;
	public float groundedTimeout = 0.1f;
	float lastGroundTime;
	public bool isGrounded;
	public bool isClimbing;
//	bool wasClimbing;
	public bool isHanging;
//	bool wasHanging;
	public Vector3 climbDirection;
	public float gravity;
	public float maxFallSpeed;
	float yVelocity;
	public Vector3 velocity;
	Rigidbody rigidBody;
	cameraController cameraController;
	gameController gameController;
	shopMenuController shopMenu;
	Animator animController;
	float elapsedTime = 0.0f;
	#endregion

	// Use this for initialization
	void Start()
	{
		currentSpeed = 0;
		rigidBody = GetComponent<Rigidbody>();
		gameController = GameObject.Find("gameController").GetComponent<gameController>();
		shopMenu = GameObject.Find("shopMenu").GetComponent<shopMenuController>();
		cameraController = GameObject.Find("Camera").GetComponent<cameraController>();
		animController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
//		if (rigidBody.velocity != Vector3.zero)
//		{
//			Debug.Log("velocity: " + rigidBody.velocity + ", normal: " + rigidBody.velocity.normalized + 
//					", speed: " + currentSpeed);
//		}
//		Debug.Log("isJumping: " + isJumping + ", isGrounded: " + isGrounded);
		
		if(elapsedTime >= 7.0f)
		{
			animController.SetTrigger("Idle");
			elapsedTime = 0.0f;
		}
		


		if (isControllable)
		{
			v = Input.GetAxis("Vertical");
			h = Input.GetAxis("Horizontal");
			jumpButton = Input.GetButton("Jump");
			actionButton = Input.GetButtonDown("Submit");
			cancelButton = Input.GetButtonDown("Cancel");

			animController.SetFloat("Forward", v);
			if(v > 0.1f || jumpButton || actionButton || cancelButton)
			{
				elapsedTime = 0.0f;
			}
			else if(!animController.GetCurrentAnimatorStateInfo(0).IsName("stretch"))
			{
				elapsedTime += Time.deltaTime;
			}

			if(animController.GetBool("IsAttacking"))
			{
				animController.SetBool("IsAttacking", false);
			}
		}
		else
		{
			v = 0;
			h = 0;
			jumpButton = false;
			actionButton = false;
		}
		velocity = rigidBody.velocity;
		yVelocity = velocity.y;
		Vector3 hVelocity = Vector3.zero;
		Vector3 vVelocity = Vector3.zero;
		Vector3 goalVelocity = Vector3.zero;
//		Vector3 newVelocity = Vector3.zero;

//		Debug.Log("forward: " + transform.forward + ", velocity: " + 
//			rigidBody.velocity + ", grounded: " + isGrounded);
//
//		applyGravity();

//		Debug.Log("forward: " + transform.forward + ", velocity: " + 
//			rigidBody.velocity + ", grounded: " + isGrounded);
	
//		if (wasClimbing || wasHanging && !isGrounded && !isJumping)
//		{
//			
//		}

		if (!isClimbing && !isHanging)
		{
			if (Mathf.Abs(h) > deadzone)
			{
				if (!isJumping)
				{
					transform.Rotate(0, h * turnSpeed, 0);
				}
				else
				{
					hVelocity = transform.right * horizontalSpeed * Mathf.Sign(h);
				}

//				Debug.Log("forward: " + transform.forward + ", right:" + transform.right
//						+ ", h: " + h + ", velocity: " + rigidBody.velocity + ", hVelocity: " + 
//						hVelocity + ", grounded: " + isGrounded);
			}

			if (v > deadzone)
			{
				if (currentSpeed < maxSpeed)
				{
					if (!isJumping)
					{
						currentSpeed += acceleration;
					}
					else
					{
						currentSpeed += acceleration / 3;
					}
				}
	//			rigidBody.velocity = Vector3.Slerp(rigidBody.velocity, 
	//				(transform.forward * currentSpeed), acceleration * Time.smoothDeltaTime);
			}
			else if (v < -deadzone)
			{
				if (currentSpeed > 0)
				{
					currentSpeed = 0;
	//				rigidBody.velocity = Vector3.Slerp(rigidBody.velocity, 
	//					(transform.forward * currentSpeed), acceleration * Time.smoothDeltaTime);
				}
				else
				{
					if (currentSpeed > minSpeed)
					{
						if (!isJumping)
						{
							currentSpeed -= acceleration;
						}
						else
						{
							currentSpeed -= acceleration / 3;
						}
					}
	//				rigidBody.velocity = Vector3.Slerp(rigidBody.velocity, 
	//					(transform.forward * currentSpeed), acceleration * Time.smoothDeltaTime);
				}
			}
			else
			{
				if (currentSpeed > 0)
				{
					currentSpeed -= acceleration * 2;
					if (currentSpeed < 0)
					{
						currentSpeed = 0;
					}
				}
				if (currentSpeed < 0)
				{
					currentSpeed += acceleration * 2;
					if (currentSpeed > 0)
					{
						currentSpeed = 0;
					}
				}
			}	
			vVelocity = transform.forward * currentSpeed;
		}

		if (isClimbing)
		{
			if (Mathf.Abs(v) > deadzone)
			{
				vVelocity = climbDirection * climbSpeed * Mathf.Sign(v);
			}
		}

		if (isHanging)
		{
			if (Mathf.Abs(h) > deadzone)
			{
				hVelocity = transform.right * shimmySpeed * Mathf.Sign(h);
			}
		}

		goalVelocity = vVelocity + hVelocity;

//		newVelocity = Vector3.Slerp(velocity, goalVelocity,
//			 acceleration * Time.smoothDeltaTime);

//		Debug.Log("forward: " + transform.forward + ", goalVelocity: " + goalVelocity + ", velocity: " + 
//				velocity + ", newVelocity: " + newVelocity + ", grounded: " +
//				isGrounded);

		rigidBody.velocity = goalVelocity;// newVelocity;
//			Vector3.Slerp(velocity, (transform.forward * 
//				currentSpeed), acceleration * Time.smoothDeltaTime);

		if (canJump && jumpButton)
		{
			jump();
		}

		applyGravity();

//		Debug.Log("forward: " + transform.forward + ", velocity: " + 
//				rigidBody.velocity + ", grounded: " + isGrounded);
	}

	void jump()
	{
//		Debug.Log("time: " + Time.time + ", lastJumpTime: " + lastJumpTime + ", " + (Time.time - lastJumpTime));
		if (!isJumping)
		{
			if (isGrounded && //(Time.time - lastJumpTime > jumpTimeout) && 
				(Time.time - lastGroundTime > Time.smoothDeltaTime * 2))
			{
				rigidBody.velocity += new Vector3(0, jumpSpeed, 0);
				isJumping = true;
				isGrounded = false;
				cameraController.isJumping = true;
				lastJumpTime = Time.time;
			}
		}
		else
		{
			if (Time.time - lastJumpTime < jumpTimeout)
			{
				rigidBody.velocity += new Vector3(0, jumpSpeed / 6, 0);
			}
		}
		
	}

	void applyGravity()
	{
		if (!isGrounded && !isClimbing && !isHanging)
		{
			if (rigidBody.velocity.y > -maxFallSpeed)
			{
				rigidBody.velocity += new Vector3(0, yVelocity + gravity * Time.smoothDeltaTime, 0);
//				Debug.Log("gravity = " + gravity * Time.smoothDeltaTime + ", yVelocity = " 
//					+ (yVelocity + gravity * Time.smoothDeltaTime));
			}
			else
			{
				rigidBody.velocity = new Vector3(rigidBody.velocity.x, -maxFallSpeed, 
									rigidBody.velocity.z);
			}
		}
	}

	public void moveToTarget()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "event")
		{
			int eventID = other.gameObject.GetComponent<eventData>().eventID;
			gameController.loadEvent(eventID);
		}

		if (other.tag == "climbable")
		{
			if (actionButton)
			{
				isClimbing = true;
				climbDirection = other.transform.up;
			}
		}

		if (other.tag == "hangArea")
		{
			if (!isGrounded && yVelocity > -3)
			{
				isHanging = true;
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "townNPC")
		{
			if (actionButton)
			{
				isTalking = true;
				other.gameObject.GetComponent<npcController>().talk();
				cameraController.isControllable = false;
			}

			if (cancelButton)
			{
				other.gameObject.GetComponent<npcController>().silence();
				cameraController.isControllable = true;
			}
		}

		if (other.gameObject.tag == "shopNPC")
		{
			npcController npc = other.gameObject.GetComponent<npcController>();
			if (actionButton)
			{
				if (!isTalking)
				{
					isTalking = true;
	//				shopMenu.shopItemList = npc.shopItems;
					npc.openShop();
					cameraController.isControllable = false;
				}
				else
				{
					shopMenu.closeMenu();
				}
			}

			if (cancelButton)
			{
				if (shopMenu.subMenuOpen)
				{
					shopMenu.closeMenu();
				}
				else if (shopMenu.selectMenu.activeInHierarchy)
				{
					shopMenu.exit();
					npc.closeShop();
				}
				else
				{
					npc.silence();
					cameraController.isControllable = true;
				}
			}
		}

		if (other.gameObject.tag == "questNPC")
		{
			if (actionButton)
			{
				isTalking = true;
				other.gameObject.GetComponent<npcController>().talk();
				cameraController.isControllable = false;
			}

			if (cancelButton)
			{
				other.gameObject.GetComponent<npcController>().silence();
				cameraController.isControllable = true;
			}
		}

		if (other.tag == "Enemy" && actionButton)
		{
			gameController.startBattle(other.gameObject);
			// Initiate anim variable
			animController.SetBool("IsAttacking", true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "event")
		{
			
		}

		if (other.tag == "climbable" && isClimbing)
		{
//			wasClimbing = true;
			isClimbing = false;
		}

		if (other.tag == "hangArea" && isHanging)
		{
//			wasHanging = true;
			isHanging = false;
		}

		if (other.tag == "questNPC" || other.tag == "shopNPC" || 
			other.tag == "townNPC")
		{
			other.gameObject.GetComponent<npcController>().silence();
			cameraController.isControllable = true;
			if (isTalking)
			{
				isTalking = false;
				other.gameObject.GetComponent<npcController>().jackassRating++;
				GameObject.FindWithTag("data").GetComponent<dataContainer>().jackassMeter++;
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
//		Debug.Log("collided with: " + collision.gameObject.name + ", tag: " + collision.gameObject.tag);
//		print("Points colliding: " + collision.contacts.Length);
//		for (int i = 0; i < collision.contacts.Length; i++)
		{
			int i = 0;
			if (collision.contacts[i].normal.y <= 1.1f && collision.contacts[i].normal.y >= 0.9f)
//				&& rigidBody.velocity.y >= -0.2f && rigidBody.velocity.y <= 0.2f)
			{
				if (!isGrounded)
				{
					lastGroundTime = Time.time;
				}
				isGrounded = true;
				isJumping = false;
			}
//			print("point that collided: " + collision.contacts[i].point + ", normal: " + 
//				collision.contacts[i].normal + ", Y velocity = " + rigidBody.velocity.y + 
//				", yDiff: " + (transform.position.y - collision.contacts[i].point.y));
		}

		if (collision.gameObject.tag == "Enemy")
		{
			rigidBody.velocity = Vector3.zero;
			gameController.startBattle(collision.gameObject);
			// Initiate anim variable
			animController.SetBool("IsAttacking", true);
			animController.SetFloat("Forward", 0.0f);
		}
	}

	void OnCollisionStay(Collision collision)
	{
		
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "ground")
		{
			isGrounded = false;
		}

//		if (collision.gameObject.tag == "questNPC" || collision.gameObject.tag == "shopNPC" || 
//			collision.gameObject.tag == "townNPC")
//		{
//			collision.gameObject.GetComponent<npcController>().silence();
//		}
	}
}