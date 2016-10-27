# BattlePlane

　　在看完教程之后自己实现，以为很简单的地方结果思考了挺久，所以将自己的重做过程记录下来以便二次学习。  
　　采用帧动画形式实现微信打飞机游戏。  
　　简书地址：http://www.jianshu.com/p/cfa7e61c59d4
###1.创建背景轮播以及主角动画  
**背景轮转：**  
　　使用两张图片交错移动的方式实现背景轮播，当一张图片下移出视图，更新其位置置于顶端。为了防止出现图片衔接不流畅，需要取得图片真实像素。素材背景图片为 480\*852，所以当每次出界时，y轴需移动 852 \* 2。   
　　这里实践中出现了一个失误，当创建一个新脚本后，又把脚本文件名改变，同时没有改类名。造成脚本不能添加到 Component 上，将类名做相应改变即可。

**主角动画：**  
　　使用两张图片变化来实现主角动画。利用脚本控制 Sprite 展示不同的图片，实现动态效果。  

**设置Sorting Layer：**  
　　当有多个Sprite在同一层时，渲染出来的结果是不定的，所以需要设置 Sorting Layer ，层数越大，显示越上层。  
　　还可以设置 Order in Layer，用于设置对同一层的显示优先级。同样的，数值越大，显示越上层。  
  
###2.子弹、敌机、奖励物品的运动和随机生成  
子弹与奖励自动生成与运行：  
　　将物品设置成 Prefabs，按一定时间间隔实例化物品。为物品添加脚本，让其自己移动以及销毁。  



使用 GameObject.Instantiate() 方法创建实例:  
```
GameObject.Instantiate (bullet, transform.position, Quaternion.identity);  
```
InvokeRepeating 可以循环调用函数:  
```
InvokeRepeating ("fire", 1, speed);/(函数名，延迟，循环间隔)  
```
删除自身:  
```
Destroy (this.gameObject);  
```

　　敌机的控制与子弹类似，超出视图也需要销毁。只是敌机需要的视图上方随机生成，并且有 Hp 值。  

**随机生成思路：**  
　　在视图上方创建一个空 GameObject 用于产生物品和敌机，按照一定时间间隔调用所写生成函数，同时随机产生一个 X 坐标。  



###3.主角移动、敌人碰撞时爆炸效果  
主角控制：  
　　检测触摸位置，防止主角飞出屏幕。注意改变 position 时需要将屏幕坐标转换为世界坐标，不然会出现尺度不对。  
检测鼠标点击：  
```
Input.GetMouseButtonDown (0);//鼠标左键按下
Input.GetMouseButtonUp (0);//鼠标左键抬起
```
获取鼠标点击位置：  
```
Input.mousePosition
```

子弹与敌人碰撞:  
　　添加碰撞器，当子弹与物体碰撞时，判断 Tag 是否为 Enemy ，如果是，则调用物体的相关方法。  
```
other.gameObject.SendMessage ("Hit");
//让物体自己调用身上脚本的 Hit 方法，而不是 other 去调用
```
在 Hit 中利用 Hp 判断此时敌机是被击中还是摧毁，播放相应的动画。  


###4.武器切换、分数显示、游戏暂停  
**武器切换:**  
　　武器切换就是控制 top、left、right 三把枪是否发出子弹，Cancellnvoke 用于停止循环调用方法。在不同情况下对三把枪的开关进行不同控制。  
```
CancelInvoke ("createBullet");
```
**分数显示：**  
　　分数显示需要一个全局变量，那在 Unity 中，就使用一个单例来充当全局的作用。创建一个 GameManager 类来记录一些全局的信息，让其拥有一个 static 实例化，统计分数时都调用该实例的成员函数 addSorce 。  
　　添加一个 GUI text 控件，用于显示分数。  

**游戏暂停：**  
　　游戏暂停其实很简单，在Unity中我们是可以控制“时间流速”的，利用 Time.timeScale 可以控制时间流逝速度比例，当我们将其设置为 0 时，就相当于时间静止。  
```
		Time.timeScale = 0; //时间静止
		Time.timeScale = 1; //正常速度
        //在 0~1之间就可以实现慢动作的特效
        //大于1可以实现加速效果
```

**炸弹效果：**  
　　炸弹效果为按下 Space 键时敌机全部死亡，在 enemy 与 gameManager 中同时检测是否按下 Space 键以及是否有炸弹。在 enemy 中销毁敌机，在gameManager 中减少炸弹数量，但是要设置脚本的先后顺序，否则数量先减少了，敌机并不会被摧毁。  
设置脚本顺序：  
　　选择一个脚本 -> Exrcution Order -> 设置Default Time  

###5.游戏结束  
　　游戏结束时主要有几个点，显示当前分数、显示历史最高分，实现重新开始、实现结束游戏。  

显示历史分数：  
```
float highScore = PlayerPrefs.GetFloat("highScore",0);  
		if (score > highScore) {
			highScore = score;
		}
		PlayerPrefs.SetFloat ("highScore",highScore);
//需要存储在本地 
//PlayerPrefs.GetFloat() 和 PlayerPrefs.SetFloat() 方法用于存储
```
重新开始：  
```
SceneManager.LoadScene (0);//当前只有一个scene，所以直接载入0
//Application.LoadLevel (0); 在unity 5.3 中无法使用此方法。
```
结束游戏：  
```
	Application.Quit();
```





