using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
	private void Start()
	{

	}

	private void Update()
	{
		
	}

	#region OnButtonClick_Event

	public void GameStart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	#endregion
}
