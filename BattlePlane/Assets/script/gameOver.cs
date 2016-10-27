using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameOver : MonoBehaviour {
	public static gameOver Instance;

	public Text highScoreUI;
	public Text lastScoreUI;

	void Awake() {
		Instance = this;
		this.gameObject.SetActive (false);
		}

	public void show(int score){  //利用传参方式而不是直接获取，减少耦合
		float highScore = PlayerPrefs.GetFloat("highScore",0);
		if (score > highScore) {
			highScore = score;
		}
		PlayerPrefs.SetFloat ("highScore",highScore);
		highScoreUI.text =  highScore.ToString();
		lastScoreUI.text = score.ToString();
		this.gameObject.SetActive (true);
	}
}
