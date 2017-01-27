using UnityEngine;
using System.Collections;

public class sunLightController : MonoBehaviour
{
	#region variables
//	public int phase;
//	public int maxPhase = 8;
//	public int numDaysInPhase;
//	public int currentDaysInPhase;
	Light sunLight;
	public float maxIntensity;
	public float dawnOffset;
	public float duskOffset;
	public float twisunLightOffset;
	public float morningLightSpeed = 0.2f;
	public float eveningLightSpeed = 0.1f;

	public Color dayColor = Color.cyan;
	public Color nightColor = Color.black;
	public float morningColorSpeed = 0.2f;
	public float eveningColorSpeed = 0.1f;

	timeController timeController;
	Camera sunCamera;
//	int currentDay;
	#endregion

	// Use this for initialization
	void Start()
	{
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();
		sunCamera = GameObject.Find("sunCamera").GetComponent<Camera>();
		sunLight = transform.GetComponent<Light>();
		sunLight.enabled = false;
//		currentDay = timeController.day;

//		Debug.Log("dayColor = " + dayColor + ", nightColor = " + nightColor);
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.LookAt(Vector3.zero);

		if (transform.position.y > dawnOffset)
		{
//			if (!sunLight.enabled)
//			{
//				Debug.Log(timeController.hour + ":" + timeController.minute);
//			}
//
			sunLight.enabled = true;
	
			if (sunLight.intensity < maxIntensity && timeController.hour < 13)
			{
				sunLight.intensity += morningLightSpeed * Time.smoothDeltaTime;

				if (sunCamera.backgroundColor != dayColor)
				{
					sunCamera.backgroundColor = Color.Lerp(sunCamera.backgroundColor, dayColor, morningColorSpeed * Time.smoothDeltaTime);
				}
			}

			if (timeController.hour > 12 && transform.position.y <= twisunLightOffset)
			{
				sunLight.intensity -= eveningLightSpeed * Time.smoothDeltaTime;

				if (sunCamera.backgroundColor != nightColor)
				{
					sunCamera.backgroundColor = Color.Lerp(sunCamera.backgroundColor, nightColor, eveningColorSpeed * Time.smoothDeltaTime);
				}
			}

//			Debug.Log("currentY = " + transform.position.y);
		}
		else if (transform.position.y < duskOffset)
		{
//			if (sunLight.enabled)
//			{
//				Debug.Log(timeController.hour + ":" + timeController.minute + ", "
//						+ Time.smoothDeltaTime);
//			}
//
			sunLight.enabled = false;

			if (sunCamera.backgroundColor != nightColor)
			{
				sunCamera.backgroundColor = Color.Lerp(sunCamera.backgroundColor, nightColor, morningColorSpeed * Time.smoothDeltaTime);
			}
		}

//		Debug.Log("bgColor = " + sunCamera.backgroundColor);
	}
}