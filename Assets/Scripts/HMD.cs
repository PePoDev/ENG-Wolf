using UnityEngine;

public class HMD : MonoBehaviour
{

	private void Start()
	{
		Input.gyro.enabled = true;
	}

	private void Update()
	{
		transform.rotation = GyroToUnity(Input.gyro.attitude);

		Debug.Log("Before : " + transform.rotation.eulerAngles);

		var rotation = transform.rotation.eulerAngles;
		rotation.x -= 90f;
		var q = Quaternion.Euler(rotation);
		transform.rotation = q;

		Debug.Log("After : " + transform.rotation.eulerAngles);
	}

	private static Quaternion GyroToUnity(Quaternion q)
	{
		return new Quaternion(q.x, q.y, -q.z, -q.w);
	}
}
