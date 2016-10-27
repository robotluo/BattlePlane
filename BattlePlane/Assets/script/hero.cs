using UnityEngine;
using System.Collections;

public class hero : MonoBehaviour {

	public Sprite[] sprites;

	public float speed = 10;

	private SpriteRenderer spriteRenderer;

	private float times = 0; 

	private Vector3 lastMousePosition = Vector3.zero; //上一帧鼠标的位置

	private bool isMouseDown = false; 	//鼠标是否按下

	public int 		Hp = 4;				//主角血量
	public Sprite[] deathSprites;		//死亡动画
	private float   deathTimes = 0;		//死亡动画播放时间
	public float 	deathSpeed = 3;	//死亡动画播放速度

	public int 	 	gunType = 0;				 //武器类型
	public float 	reSetSuperGunTimes = 10.0f;  //超级武器持续时间
	public float 	superGunTimes = 0.0f;  		 //超级武器剩余时间

	public gun Gun_top;
	public gun Gun_left;
	public gun Gun_right;


	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		setGunLv0();
	}
	
	// Update is called once per frame
	void Update () {

		times += Time.deltaTime;
		int index = (int)(times * speed);
		spriteRenderer.sprite = sprites[index % 2];

		if (Input.GetMouseButtonDown (0)) {
			isMouseDown = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			isMouseDown = false;
			lastMousePosition = Vector3.zero;
		}
		//鼠标按下 并且此时不是 pause 状态
		if (isMouseDown && gameManager.Instance.gameState == GameState.Running) {
			if (lastMousePosition != Vector3.zero) {
				Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
				transform.position += offset;
				checkPosition ();
			}
			lastMousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}

		//主角死亡
		if (Hp <= 0) {
				deathTimes += Time.deltaTime;
				int deathSpritesIndex = (int)(deathTimes * deathSpeed);
				if (deathSpritesIndex < deathSprites.Length) { 			 //播放死亡动画
					spriteRenderer.sprite = deathSprites [deathSpritesIndex];
				} else {
				gameOver.Instance.show (gameManager.Instance.getScore());
					Destroy (this.gameObject); //播放完毕，销毁对象
				}
		}
		//Debug.Log (superGunTimes);
		//Debug.Log ("gunType" + gunType);
		superGunTimes -= Time.deltaTime;
		//超级武器时间到
		if (superGunTimes < 1e-4) {  //float 与 零值比较
			if(gunType == 2 ) { 
				setGunLv1();
				superGunTimes = reSetSuperGunTimes;
			}
			else if(gunType == 1 ) { 
				setGunLv0();
			}
		}

	}
	//三个setGunLv代码重复过高	
	private void setGunLv0() {
		gunType = 0;
		closeAll ();
		Gun_top.openFire ();
		Gun_left.closeFire ();
		Gun_right.closeFire ();
	}
	private void setGunLv1() {
		gunType = 1;
		closeAll ();
		Gun_top.closeFire ();
		Gun_left.openFire ();
		Gun_right.openFire ();
	}
	private void setGunLv2() {
		gunType = 2;
		closeAll ();
		Gun_top.openFire ();
		Gun_left.openFire ();
		Gun_right.openFire ();
	}
		
	private void closeAll()
	{
		Gun_top.closeFire ();
		Gun_left.closeFire ();
		Gun_right.closeFire ();
	}

	//控制主角不飞出边界
	void checkPosition(){
		
		float x = transform.position.x;
		float y = transform.position.y;

		if (x <= -2.25f) {	x = -2.25f; }
		if (x >=  2.25f) {	x =  2.25f; }
		if (y <= -3.68f) {	y = -3.68f; }
		if (y >=  3.33f) {	y =  3.33f; }

		transform.position = new Vector3 (x,y,0);
	}

	//碰撞检测
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Prop") {
			this.GetComponent<AudioSource> ().Play (); //获得物品声音
			if (other.gameObject.GetComponent<enemy> ().type == EnemyType.Prop0) {
				superGunTimes = reSetSuperGunTimes; //重置超级武器时间

				if (gunType == 0) {
					setGunLv1 ();
				} else if (gunType == 1) {
					setGunLv2 ();
				}
			} else if(other.gameObject.GetComponent<enemy> ().type == EnemyType.Prop1){
				if (gameManager.Instance.getBomb () < 3) {
					gameManager.Instance.addBomb (1);
				}
			}
			Destroy (other.gameObject);
		}
		if (other.tag == "Enemy") {
			Hp--;
		}
	}
}
