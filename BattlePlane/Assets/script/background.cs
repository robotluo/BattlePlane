using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {


	public float speed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.down * speed * Time.deltaTime);
		if (transform.position.y <= -8.52f ) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + 8.52f * 2);;
		}
	}
}
