using UnityEngine;
using System.Collections;

public class starController : MonoBehaviour
{
	#region variables
	public GameObject starPrefab;
	public int numStars;
	public int twilightOffset;
	public int starSpawnMinRange;
	public int starSpawnMaxRange;
	public int starSpawnRangeY;
	Vector3 spawnPos;
	public float delay;
	public int numActiveStars;

	timeController timeController;
	GameObject sun;
	#endregion

	// Use this for initialization
	void Start()
	{
		timeController = GameObject.Find("TimeController").GetComponent<timeController>();
		sun = GameObject.Find("Sun");

		for (int i = 0; i < numStars; i++)
		{
			findSpawnPos();
			GameObject star = Instantiate(starPrefab, spawnPos, Random.rotation) as GameObject;
			star.transform.parent = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (timeController.hour < 13 && sun.transform.position.y > 0)
		{
			for (int i = 0; i < numStars; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		if (timeController.hour >= 13 && sun.transform.position.y <= 0 + twilightOffset)
		{
			for (int i = 0; i < numStars; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	void findSpawnPos()
	{
		float xPos = Random.Range(-starSpawnMaxRange, starSpawnMaxRange);
		float yPos = Random.Range(starSpawnRangeY, starSpawnMaxRange);
		float zPos = Random.Range(-starSpawnMaxRange, starSpawnMaxRange);

		spawnPos = new Vector3(xPos, yPos, zPos);

		if ((xPos > -starSpawnMinRange && xPos < starSpawnMinRange) &&
			(zPos > -starSpawnMinRange && zPos < starSpawnMinRange))
		{
			findSpawnPos();
		}
	}
}