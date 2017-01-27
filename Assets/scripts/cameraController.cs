using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour
{
	#region variables
	public bool isControllable;
	public bool forceOverride;

	public Transform followTarget;
	public Transform normalPos;
	public float maxDistance = 6f;
	public float minDistance = 3f;
	public float deadzone = 0.15f;

	// Are we moving backwards (This locks the camera to not do a 180 degree spin)
//	private bool isMovingBack = false;
//	private bool isMovingForward = false;
	public bool isJumping = false;
	public float moveTimeOut = 2;
	private float lastMoveTime;

	public float minY = 1.3f;
	public float maxY = 8.5f;
	float currentMinY;
	float currentMaxY;
	public float sensitivity = 3.0f;
	public float turnSmoothing = 6;
	public float speedSmoothing = 6;
	public float normalClipDistance = 1f;
	public float maxClipDistance = 5.7f;
	public float currentClipDistance;

	public bool battleMode = false;
	public bool wasBattleMode = false;
	public Transform battlePos;
	#endregion

	// Use this for initialization
	void Start()
	{
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}

		if (!followTarget)
		{
			followTarget = GameObject.FindGameObjectWithTag("Player").transform;
			normalPos = followTarget.FindChild("normalCameraPos").transform;
		}

		transform.position = normalPos.transform.position;

		currentMinY = minY + followTarget.position.y;
		currentMaxY = maxY + followTarget.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		bool shift = Input.GetKey(KeyCode.LeftShift);
		if (battleMode && shift)
		{
			forceOverride = true;
		}
		else
		{
			forceOverride = false;
		}

		if (forceOverride || (!battleMode && isControllable))
		{
			float v = Input.GetAxis("Vertical");
			float h = Input.GetAxis("Horizontal");
			float cameraV = Input.GetAxis("Mouse Y");
			float cameraH = Input.GetAxis("Mouse X");
			bool resetButton = Input.GetButton("CameraReset");

			float currentY = transform.position.y;
			float newY = currentY;
			float currentX = transform.position.x;
			float currentZ = transform.position.z;
			float newX = currentX;
			float newZ = currentZ;
			float dx = currentX - followTarget.position.x;
			float dz = currentZ - followTarget.position.z;
			float radius = Mathf.Sqrt((dx * dx) + (dz * dz));
			float angle = Mathf.Atan2(dz, dx);

//			Debug.Log("currentDist = " + radius);

			currentMinY = minY + followTarget.position.y;
			currentMaxY = maxY + followTarget.position.y;

//			isMovingBack = v < -deadzone;
//			isMovingForward = v > deadzone;

			if (wasBattleMode || resetButton)
			{
				transform.position = Vector3.Slerp(transform.position, normalPos.position, 
					speedSmoothing * Time.smoothDeltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, normalPos.rotation,
					speedSmoothing * Time.smoothDeltaTime);

				wasBattleMode = battleMode;
			}

			if (cameraV > deadzone && currentY < currentMaxY)
			{
				newY += cameraV;

				if (newY > currentMaxY)
				{
					newY = currentMaxY;
				}
				lastMoveTime = Time.time;
			}
			else if (cameraV < -deadzone && currentY > currentMinY)
			{
				newY += cameraV;

				if (newY < currentMinY)
				{
					newY = currentMinY;
				}
				lastMoveTime = Time.time;
			}

			if (Mathf.Abs(cameraH) > deadzone)
			{
	//			Debug.Log("camH/MouseX = " + cameraH + ", currX = " + currentX + ", currZ = "
	//				+ currentZ + ", radius = " + radius);
				angle += cameraH * sensitivity;

				newX = radius * Mathf.Cos(angle) + followTarget.position.x;
				newZ = radius * Mathf.Sin(angle) + followTarget.position.z;
				lastMoveTime = Time.time;
			}

			transform.position = new Vector3(newX, newY, newZ);

			if (radius > maxDistance)
			{
//				Debug.Log("currentDist > " + radius);
				radius = Mathf.Lerp(radius, maxDistance, turnSmoothing);
				transform.position = Vector3.Slerp(transform.position, normalPos.position, 
					speedSmoothing * Time.smoothDeltaTime);
			}

			if (Time.time - lastMoveTime > moveTimeOut && (v != 0 || h != 0))
			{
				transform.position = Vector3.Slerp(transform.position, normalPos.position, 
										speedSmoothing * Time.smoothDeltaTime);
			}

			if ((radius == maxDistance) && (Time.time - lastMoveTime > moveTimeOut) &&
				(v != 0 || h != 0))
			{
				transform.position = normalPos.position;
			}

			transform.LookAt(followTarget);
			adjustClippingDistance();
		}

		if (battleMode)
		{
			wasBattleMode = battleMode;
			if (transform.position != battlePos.position)
			{
				transform.position = Vector3.Slerp(transform.position, battlePos.position,
									speedSmoothing * Time.smoothDeltaTime);
				transform.rotation = Quaternion.Slerp(transform.rotation, battlePos.rotation,
									speedSmoothing * Time.smoothDeltaTime);
			}
		}
	}

	void adjustClippingDistance()
	{
		
	}

	void adjustClippingDistance(float min, float max)
	{
		Vector3 lineStart = transform.position * currentClipDistance;
		Vector3 lineEnd = transform.position * maxClipDistance;
		Physics.Linecast(lineStart, lineEnd);
	}
}