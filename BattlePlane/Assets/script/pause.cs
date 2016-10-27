using UnityEngine;
using System.Collections;

public class pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUpAsButton (){
		gameManager.Instance.transformGameState ();  //按下时修改游戏状态
		this.GetComponent<AudioSource> ().Play (); //按下声音
	}
}
