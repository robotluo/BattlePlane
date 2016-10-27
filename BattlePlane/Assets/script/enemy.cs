using UnityEngine;
using System.Collections;


public enum EnemyType{
	Enemy0,
	Enemy1,
	Enemy2,
	Prop0,
	Prop1
}

public class enemy : MonoBehaviour {

	public float speed = 1f; 					//飞行速度

	public int hp = 1;							//血量

	public int score = 100; 					//分数

	public EnemyType type = EnemyType.Enemy0; 	//飞机类型

	private SpriteRenderer spriteRenderer; 		//自身 sprite

	public bool isDeath = false; 	 			//是否死亡
	public Sprite[] deathSprites; 	 			//死亡动画
	public float	deathSpeed = 10; 			//死亡动画播放速度 10 f/s
	private float 	deathTimes = 0;  			//死亡时间

	public bool isHit = false; 					//是否被击中
	public Sprite[] hitSprites;					//击中动画
	public float 	hitSpeed = 10;				//击中动画播放速度 10 f/s
	private float 	hitTimes = 0;  				//击中时间

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.down * speed * Time.deltaTime * gameManager.Instance.gameTimes * 0.05f);
		if (transform.position.y <= -5.5f) {
			Destroy (this.gameObject);
		}

		if (isDeath) { //判断是否而亡
			deathTimes += Time.deltaTime;
			int deathSpritesIndex = (int)(deathTimes * deathSpeed);
			if (deathSpritesIndex < deathSprites.Length) { 			 //播放死亡动画
				spriteRenderer.sprite = deathSprites [deathSpritesIndex];
			} else {
				gameManager.Instance.addScore (score);
				toDie ();
			}
		}
		if (isHit) { //判断是否被击中
			//Debug.Log ("hitTimes :" + hitTimes);
			hitTimes += Time.deltaTime;
			int hitSpritesIndex = (int)(hitTimes * deathSpeed);
			//Debug.Log ("hitSpritesIndex" + itSpritesIndex);
			//Debug.Log ("hitTimes" + hitTimes);
			if (hitSpritesIndex < hitSprites.Length) {  //播放击中动画
				spriteRenderer.sprite = hitSprites [hitSpritesIndex];
			} else {  //播放完毕，重置 sprite 、 hitTimes 和 isHit
				spriteRenderer.sprite = hitSprites [0];
				hitTimes = 0;
				isHit = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.Space) && gameManager.Instance.getBomb () > 0) {
			toDie ();
		}
			

	}

	public void Hit(){
		hp--;
		if (hp <= 0) {
			isDeath = true; 	//已经死亡
		} else {
			isHit = true;		//击中未死亡
		}
		//Destroy (this.gameObject);
	}
	public void toDie(){
		isDeath = true;
		Destroy (this.gameObject); //播放完毕，销毁对象
	}
}
