using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キーを押すと、移動する（重力対応版）
public class OnMic_MoveGravity : MonoBehaviour {

	public float speed = 3; // スピード：Inspectorで指定
	public float jumppower = 8;  // ジャンプ力：Inspectorで指定
    public float walkThreshold = 0.1f;
    public float jumpThreshold = 0.1f;
	public string nowMode = "";
    public string jumpAnime = "";
    public string walkAnime = "";
    public string standAnime = "";
    public bool groundFlag = false; // 足が何かに触れているかどうか

    float vx = 0;
	bool leftFlag = false; // 左向きかどうか
	bool pushFlag = false; // スペースキーを押しっぱなしかどうか
	bool jumpFlag = false; // ジャンプ状態かどうか
		Rigidbody2D rbody;

	void Start () { // 最初に行う
		// 衝突時に回転させない
		rbody = GetComponent<Rigidbody2D>();
		rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void Update () { // ずっと行う
		vx = 0;
        float mic = MicVolumeSample.volumeRate;
		nowMode = standAnime;
        if (mic > walkThreshold) {
			vx = speed; // 右に進む移動量を入れる
			leftFlag = false;
			nowMode = walkAnime;
		} else if (mic < walkThreshold && !(groundFlag) ){
			nowMode = jumpAnime;
		}
		// もし、スペースキーが押されたとき、足が何かに触れていたら
		if (mic > jumpThreshold && groundFlag) { 
			if (pushFlag == false) { // 押しっぱなしでなければ
				jumpFlag = true; // ジャンプの準備
				pushFlag = true; // 押しっぱなし状態
			}
		} else {
			pushFlag = false; // 押しっぱなし解除
		}
	}
	void FixedUpdate() { // ずっと行う（一定時間ごとに）
		// 移動する（重力をかけたまま）
		rbody.velocity = new Vector2(vx, rbody.velocity.y);

        Animator animator = this.GetComponent<Animator>();
        animator.Play(nowMode);

        // 左右に向きを変える
        //this.GetComponent<SpriteRenderer>().flipX = leftFlag;
		//this.GetComponent<Animator>().Play(nowMode);
        float mic = MicVolumeSample.volumeRate;
        
		// もし、ジャンプするとき
		if (jumpFlag) {
			jumpFlag = false;
			rbody.AddForce(new Vector2(0, jumppower * mic), ForceMode2D.Impulse);
		}
	}
	void OnTriggerStay2D(Collider2D collision) { // 足が何かに触れたら
		groundFlag = true;
	}
	void OnTriggerExit2D(Collider2D collision) { // 足に何も触れなかったら
		groundFlag = false;
	}
}
