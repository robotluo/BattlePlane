using UnityEngine;
using System.Collections;

public class quit : MonoBehaviour {

	void OnMouseUpAsButton(){
		//Application.LoadLevel (0);
		Debug.Log("hehe");
		Application.Quit();
	}
}
