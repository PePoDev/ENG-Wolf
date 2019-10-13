using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Question
{
	public string[] choice;

	public int correctAnswer;

	public GameObject model;
}

public class GameManager : MonoBehaviour
{
	public TextMeshProUGUI m_ScoreText;
	public TextMeshProUGUI m_TimeText;

	public Button m_AnswerButton1;
	public Button m_AnswerButton2;

	public DeviceCamera deviceCamera;

	public float lifePositionOne;
	public float lifePositionTwo;
	public float lifePositionThree;

	public GameObject WinCanvas;
	public GameObject LoseCanvas;

	public AudioSource CorrectAudio;
	public AudioSource WrongAudio;
	public AudioSource Lose;
	public AudioSource Win;

	public Question[] animals;
	public Question[] Occupations;
	public Question[] Fruits;

	public Question[] worldList;

	public GameObject lastWord;

	public Image Wolf;

	public Transform Privot;

	private Question currentQuestion;

	private int life = 3;

	private int score;
	private float time;

	private bool isGameOver = false;

	private const float Capture_Time = 15.9f;

	private List<int> seed = new List<int>();
	private List<int> random = new List<int>();

	private void Awake()
	{
		// Set word mode
		switch (PlayerPrefs.GetString("mode"))
		{
			case "animals":
			{
				worldList = animals;
				break;
			}
			case "occupations":
			{
				worldList = Occupations;
				break;
			}
			case "fruits":
			{
				worldList = Fruits;
				break;
			}
		}

		// Add word to seed list
		for (int i = 0; i < worldList.Length; i++)
		{
			seed.Add(i);
		}

		// Random selected to random list
		for (int i = 0; i < worldList.Length; i++)
		{
			var rand = Random.Range(0, seed.Count);
			random.Add(seed[rand]);
			seed.RemoveAt(rand);
		}

		// Start game with first word
		NextWord();
	}

	private void Update()
	{
		if (!isGameOver)
		{
			UpdateUI();

			time += Time.deltaTime;
			if (time >= Capture_Time)
			{
				time = 0f;
				SubmitAnswer(-1);
			}
		}
	}

	private void UpdateUI()
	{
		m_TimeText.text = $"{Mathf.RoundToInt(Capture_Time - time).ToString()} s";
		m_ScoreText.text = $"Score {score.ToString()}";
	}

	public void NextWord()
	{
		var rad = Random.Range(0, 360);
		var rotation = new Vector3(0, 0, rad);

		Privot.rotation = Quaternion.Euler(rotation);

		Destroy(lastWord);

		currentQuestion = worldList[random[0]];

		m_AnswerButton1.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choice[0];
		m_AnswerButton2.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choice[1];

		Debug.Log($"{random[0]} : {currentQuestion.choice[currentQuestion.correctAnswer]}");

		lastWord = Instantiate(currentQuestion.model, Privot.GetChild(0));
		random.RemoveAt(0);
	}

	public void SubmitAnswer(int choice)
	{
		if (choice == currentQuestion.correctAnswer)
		{
			score += 100;
			CorrectAudio.Play();
		}
		else
		{
			life--;
			if (life == 2)
			{
				Wolf.rectTransform.anchoredPosition = new Vector2(lifePositionOne, 0);
			}
			else if (life == 1)
			{
				Wolf.rectTransform.anchoredPosition = new Vector2(lifePositionTwo, 0);
			}
			else
			{
				Wolf.rectTransform.anchoredPosition = new Vector2(lifePositionThree, 0);
				GameOver();
			}
			WrongAudio.Play();
		}

		time = 0;

		if (random.Count == 0)
		{
			StopCamera();
			Win.Play();
			isGameOver = true;
			WinCanvas.SetActive(true);
			return;
		}

		NextWord();
	}

	private void GameOver()
	{
		StopCamera();
		Lose.Play();
		isGameOver = true;
		LoseCanvas.SetActive(true);
	}

	public void GoToMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void StopCamera()
	{
		deviceCamera.StopCamera();
	}
}
