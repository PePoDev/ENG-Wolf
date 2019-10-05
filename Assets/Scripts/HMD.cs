using UnityEngine;

public class HMD : MonoBehaviour
{
	private float xRotation;
	private float yRotation;
	private float zRotation;

	private void Awake()
	{
		if (SystemInfo.supportsGyroscope)
		{
			Input.gyro.enabled = true;
		}
		else
		{
			Debug.LogWarning("This device dont support gyro");
		}
	}

	private void Update()
	{
		if (float.IsNaN(Input.gyro.attitude.x))
		{
			xRotation += -Input.gyro.rotationRateUnbiased.x;
			yRotation += -Input.gyro.rotationRateUnbiased.y;
			//zRotation += -Input.gyro.rotationRateUnbiased.z;

			transform.eulerAngles = new Vector3(xRotation, yRotation, zRotation);
			Debug.Log("unbiased " + transform.rotation);
		}
		else
		{
			transform.rotation = GyroToUnity(Input.gyro.attitude);
			Debug.Log("attitude " + transform.rotation);
		}
	}

	private static Quaternion GyroToUnity(Quaternion q)
	{
		return new Quaternion(q.x, q.y, -q.z, -q.w);
	}
}