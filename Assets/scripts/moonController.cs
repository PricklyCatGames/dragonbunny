using UnityEngine;
using System.Collections;

public class moonController : MonoBehaviour
{
	#region variables
	public float speed;
	public float timeScaleMultiplier;
	timeController timeController;
	#endregion

	// Use this for initialization
	void Start()
	{
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		speed = timeController.currentTimeScale / timeScaleMultiplier;
		transform.Rotate(Vector3.right * speed * Time.smoothDeltaTime);
	}
}