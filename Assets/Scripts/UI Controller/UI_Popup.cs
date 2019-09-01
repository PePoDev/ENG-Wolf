using System;
using TMPro;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
	public TextMeshProUGUI Title;
	public TextMeshProUGUI Message;

	private Action OnClickOK;
	private Action OnClickCancel;

	public bool IsShown { get; private set; }

	private static UI_Popup instance = null;
	public static UI_Popup Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Instantiate(Resources.Load("Popup", typeof(UI_Popup))) as UI_Popup;
			}
			return instance;
		}
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		gameObject.SetActive(false);
	}

	public void Show(string title, string message, Action ok, Action no)
	{
		if (IsShown == false)
		{
			Title.text = title;
			Message.text = message;

			OnClickOK = ok;
			OnClickCancel = no;

			IsShown = true;
			gameObject.SetActive(true);
		}
	}

	public void Hide()
	{
		IsShown = false;
		gameObject.SetActive(false);
	}

	public void OnButtonClickOK()
	{
		Hide();
		OnClickOK?.Invoke();
	}

	public void OnButtonClickCancel()
	{
		Hide();
		OnClickCancel?.Invoke();
	}
}
