using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum GameState{
	Running,
	Pause
}

public class gameManager : MonoBehaviour {

	public static gameManager Instance;

	public GameState gameState = GameState.Running;

	private int score = 0;
	private Text ScoreUI;

	private int bombNum = 0;
	private Text BombUI;

	public float gameTimes = 1f;				//敌机随时间加速
	void Awake() {
		Instance = this;
		ScoreUI = GameObject.Find("Canvas/score").GetComponent<Text>();
		//ScoreUI = GameObject.Find("score").GetComponent<Text>(); 不加 Canvas 也可
		//ScoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>(); 找不到 tag??

		BombUI = GameObject.Find("Canvas/bombNum").GetComponent<Text>();



	}

	void Update() {
		gameTimes += Time.deltaTime;
		if (gameTimes > 80f) {
			gameTimes = 80f;
		}
		ScoreUI.text = "Score : " + score;
		BombUI.text = "X " + bombNum;

		if (Input.GetKeyDown (KeyCode.Space) && bombNum > 0) {
			userBomb ();
		}
	}

	public void addScore(int val){
		score += val;
	}

	public void addBomb(int val){
		bombNum += val;
	}

	public int getBomb(){
		return bombNum;
	}

	public int getScore(){
		return score;
	}
	public void transformGameState(){
		if (gameState == GameState.Running) {
			gameState = GameState.Pause;
			Time.timeScale = 0;
		} else {
			gameState = GameState.Running;
			Time.timeScale = 1;
		}
	}

	public void userBomb(){
		if (bombNum > 0) {
			bombNum--;
		} else {
			bombNum = 0;
		}
	}

}
