using UnityEngine;
using System.Collections;

public class sunController : MonoBehaviour
{
	#region variables
	timeController timeController;

//	public Transform risePos;
//	public Transform setPos;
	public int riseHour;
//	public int riseMinute;
//	public int setHour;
//	public int setMinute;
	public float speed;
	public float timeScaleMultiplier;
	Quaternion originalRotation;
//	public float rotateSpeed;
//	public float riseTime;
//	public float fracComplete;
//	public int twilightOffset = 20;
//	public float maxIntensity = 2.5f;
//	public float minIntensity = 0.5f;
//
//	Light light;
	#endregion

	// Use this for initialization
	void Start()
	{
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();
		originalRotation = transform.rotation;
//		light = transform.GetComponent<Light>();
//		light.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		speed = timeController.currentTimeScale / timeScaleMultiplier;

		if (timeController.hour >= riseHour)
		{
			transform.Rotate(Vector3.right * speed * Time.smoothDeltaTime);
		}
		else
		{
			transform.rotation = originalRotation;
		}

//		moveSpeed = timeController.currentTimeScale * timeScaleMultiplier;
//		if (timeController.hour == riseHour && timeController.minute == riseMinute)
//		{
//			light.enabled = true;
//			riseTime = Time.time;
//		}
//		if (timeController.hour == setHour && timeController.minute == setMinute)
//		{
//			light.enabled = false;
//		}
//		if (timeController.hour >= setHour && timeController.minute >= (setMinute - twilightOffset))
//		{
//			if (light.intensity > minIntensity)
//			{
//				light.intensity -= 0.15f * Time.smoothDeltaTime;
//			}
//		}
//
//		if (light.enabled && transform.position != setPos.position)
//		{
//			Vector3 center = (risePos.position + setPos.position) * 0.5F;
//			center -= new Vector3(0, 1, 0);
//			Vector3 riseRelCenter = risePos.position - center;
//			Vector3 setRelCenter = setPos.position - center;
//			 fracComplete = (Time.time - riseTime) / moveSpeed;
//			transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
//			transform.position += center;
//
//			transform.LookAt(Vector3.zero);
//
//			if (light.intensity < maxIntensity && timeController.hour < setHour)
//			{
//				light.intensity += 0.1f * Time.smoothDeltaTime;
//			}
//		}
//
//		if (!light.enabled && transform.position != risePos.position)
//		{
//			transform.position = risePos.position;
//			transform.rotation = risePos.rotation;
//		}
	}
}