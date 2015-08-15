using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public Text scoreText;
	public Text highscoreText;

	private int score;
	private int highScore;

	private string highScoreKey = "highScore";

	private void Initialize()
	{
		score = 0;

		highScore = PlayerPrefs.GetInt (highScoreKey, 0);
	}

	public void AddPoint(int point)
	{
		score += point;
	}

	public void Save()
	{
		PlayerPrefs.SetInt (highScoreKey, highScore);
		PlayerPrefs.Save ();

		Initialize ();
	}

	// Use this for initialization
	void Start () {
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		if (highScore < score) {
			highScore=score;
		}

		scoreText.text = score.ToString ();
		highscoreText.text = highScore.ToString ("0000");
	}
}
