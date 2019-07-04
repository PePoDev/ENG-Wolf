using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
	public static LocationManager Instance { set; get; }
	public float latitude, longitude;
	public GameObject ui_ground;

	private void Start()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
		StartCoroutine(StartLocationService());
	}

	private IEnumerator StartLocationService()
	{
		if (!Input.location.isEnabledByUser)
		{
			Debug.Log("User has not enabled GPS");
			yield break;
		}

		Input.location.Start();
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		if (maxWait <= 0)
		{
			Debug.Log("Timed Out!");
			yield break;
		}

		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("Failed to Loaded!");
			yield break;
		}

		latitude = Input.location.lastData.latitude;
		longitude = Input.location.lastData.longitude;

		if (latitude <= 13.9f && longitude <= 100.56)
		{
			ui_ground.SetActive(true);
		}
		else
		{
			ui_ground.SetActive(false);
		}
		yield break;
	}
}
