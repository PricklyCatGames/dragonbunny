using UnityEngine;
using System.Collections;

public class moonLightController : MonoBehaviour
{
	#region variables
	public int phase;
	public int maxPhase = 8;
	public int numDaysInPhase;
	public int currentDaysInPhase;
	Light moonLight;
	public float maxIntensity;
	public float dayTimeMaxIntensity;
	public float twimoonLightOffset;

	timeController timeController;
	int currentDay;
	GameObject sun;
	#endregion

	// Use this for initialization
	void Start()
	{
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();
		sun = GameObject.Find("Sun");
		moonLight = transform.GetComponent<Light>();
		moonLight.enabled = false;
		currentDay = timeController.currentDayInMonth;
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.LookAt(Vector3.zero);

		if (transform.position.y > 0)
		{
			moonLight.enabled = true;

			if (timeController.hour < 13 && sun.transform.position.y > 0)
			{
				if (moonLight.intensity > dayTimeMaxIntensity)
				{
					moonLight.intensity -= 0.1f * Time.smoothDeltaTime;
				}
			}
			if (timeController.hour >= 13 && sun.transform.position.y <= 0 + twimoonLightOffset)
			{
				if (moonLight.intensity < maxIntensity)
				{
					moonLight.intensity += 0.1f * Time.smoothDeltaTime;
				}
			}
		}
		else
		{
			moonLight.enabled = false;
		}

		if (currentDay != timeController.currentDayInMonth)
		{
			currentDaysInPhase++;
			currentDay = timeController.currentDayInMonth;
		}
		if (currentDaysInPhase >= numDaysInPhase)
		{
			phase++;
			changePhase(phase);
		}
		if (phase >= maxPhase)
		{
			phase = 0;
		}
	}

	void changePhase(int phase)
	{
		
	}
}