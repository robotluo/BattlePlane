using UnityEngine;
using System.Collections;

public class buttle : MonoBehaviour {

	public float speed = 2.0f;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.up * speed * Time.deltaTime);
		if (transform.position.y >= 5.0f) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Enemy") {
			other.gameObject.SendMessage ("Hit");
			Destroy (this.gameObject);
		}
	}
}
