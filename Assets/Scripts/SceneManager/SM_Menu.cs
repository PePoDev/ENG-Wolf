using UnityEngine;
using UnityEngine.SceneManagement;

public class SM_Menu : MonoBehaviour
{
	public Canvas Canvas_LevelSelection;
	public Canvas Canvas_Settings;

	enum Stage
	{
		Munu,
		LevelSelect,
		Setting,
		Popup
	}
	private Stage CurrentStage = Stage.Munu;

	private static UI_Popup popup;

	private void Start()
	{
		popup = UI_Popup.Instance;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			switch (CurrentStage)
			{
				case Stage.Munu:
					if (!popup.IsShown)
					{
						popup.Show(
							"Do you want to exit ?",
							"Press Yes to exit, and press no to cancel",
							() =>
							{
								OnClick_Exit();
							},
							() =>
							{
								Debug.Log("Popup Canceled");
							}
						);
					}
					CurrentStage = Stage.Popup;
					break;
				case Stage.LevelSelect:
					Canvas_LevelSelection.gameObject.SetActive(false);
					CurrentStage = Stage.Munu;
					break;
				case Stage.Setting:
					Canvas_Settings.gameObject.SetActive(false);
					CurrentStage = Stage.Munu;
					break;
				case Stage.Popup:
					popup.Hide();
					CurrentStage = Stage.Munu;
					break;
				default:
					break;
			}
		}

	}
	#region OnClick
	public void OnClick_Start()
	{
		Canvas_LevelSelection.gameObject.SetActive(true);
		CurrentStage = Stage.LevelSelect;
	}

	public void OnClick_Setting()
	{
		Canvas_Settings.gameObject.SetActive(true);
		CurrentStage = Stage.Setting;
	}

	public void OnClick_SelectLevel(string level)
	{
		PlayerPrefs.SetString("mode", level);
		SceneManager.LoadScene("Game");
	}

	public void OnClick_Exit()
	{
		Application.Quit();
	}
	#endregion
}
