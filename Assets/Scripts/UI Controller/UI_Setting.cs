using UnityEngine;

public class UI_Setting : MonoBehaviour
{

	private static UI_Setting instance;

	private void Start()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{

	}
}
