using UnityEngine;
using System.Collections;

public class factory : MonoBehaviour {


	public GameObject enemy0;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject prop0;
	public GameObject prop1;

	public float speedEnemy0 = 1f;
	public float speedEnemy1 = 5f;
	public float speedEnemy2 = 10f;
	public float speedProp0  = 7f;
	public float speedProp1  = 10f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("createEnemy0",1,speedEnemy0);
		InvokeRepeating ("createEnemy1",3,speedEnemy1);
		InvokeRepeating ("createEnemy2",5,speedEnemy2);

		InvokeRepeating ("createProp0",7,speedProp0);
		InvokeRepeating ("createProp1",10,speedProp1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void createEnemy0() {
		float tmpX = Random.Range (-2.1f, 2.1f);
		GameObject.Instantiate(enemy0,new Vector2(tmpX,transform.position.y),Quaternion.identity);
	}

	void createEnemy1() {
		float tmpX = Random.Range (-2.0f, 2.0f);
		GameObject.Instantiate(enemy1,new Vector2(tmpX,transform.position.y),Quaternion.identity);
	}

	void createEnemy2() {
		float tmpX = Random.Range (-1.7f, 1.7f);
		GameObject.Instantiate(enemy2,new Vector2(tmpX,transform.position.y),Quaternion.identity);
	}

	void createProp0() {
		float tmpX = Random.Range (-2.1f, 2.1f);
		GameObject.Instantiate(prop0,new Vector2(tmpX,transform.position.y),Quaternion.identity);
	}

	void createProp1() {
		float tmpX = Random.Range (-2.1f, 2.1f);
		GameObject.Instantiate(prop1,new Vector2(tmpX,transform.position.y),Quaternion.identity);
	}
}
