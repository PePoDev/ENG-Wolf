using UnityEngine;
using UnityEngine.UI;

public class DeviceCamera : MonoBehaviour
{
	public RawImage CameraDisplay;

	private WebCamTexture mCamera;

	private void Awake()
	{
		mCamera = new WebCamTexture();
		CameraDisplay.material.mainTexture = mCamera;
		mCamera.Play();
	}

	public void StopCamera()
	{
		mCamera.Stop();
	}
}
