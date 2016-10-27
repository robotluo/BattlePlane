using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

	public GameObject bullet;

	public float speed = 0.2f;

	void createBullet(){
		GameObject.Instantiate (bullet, transform.position, Quaternion.identity);
	}

	//开枪
	public void openFire(){
		InvokeRepeating ("createBullet", 0, speed);
	}

	//停止开枪
	public void closeFire(){
		CancelInvoke ("createBullet");
	}
}


