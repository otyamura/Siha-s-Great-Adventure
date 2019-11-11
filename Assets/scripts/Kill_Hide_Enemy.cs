using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_Hide_Enemy : MonoBehaviour
{
    public string targetObjectName;
    public float stepOnRate;
    public string sceneName;  // シーン名：Inspectorで指定
    public string moveScripts;
    public bool micFlag = true;

    private bool killFlag = true;

    public AudioClip killSound;
    public AudioClip killedSound;
    AudioSource audioSource;

    GameObject targetObject;
    Rigidbody2D targetRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetObject = GameObject.Find(targetObjectName);
        targetRigidbody2D = targetObject.GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == targetObjectName) {
            /* //踏みつけ判定になる高さ
             float stepOnHeight = (targetObject.GetComponent<BoxCollider2D>().size.y * (stepOnRate / 100f));
             //踏みつけ判定のワールド座標
             float judgePos = targetObject.GetComponent<Transform>().position.y - (targetObject.GetComponent<BoxCollider2D>().size.y / 2f) + stepOnHeight; 
             */
            float judgePos = GetComponent<Transform>().position.y;
            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y < judgePos)
                {//gameover
                    killFlag = false;
                }
            }

            if (killFlag)
            {//跳ねさせる
                audioSource.PlayOneShot(killedSound);
                //Thread.Sleep(80);
                targetRigidbody2D.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                for(int i = 0; i < 100000; i++) { }
                gameObject.SetActive(false);
                if (!(micFlag))
                {
                    targetObject.GetComponent<OnKeyPress_MoveGravity>().groundFlag = false;
                }
                else {
                    targetObject.GetComponent<OnMic_MoveGravity>().groundFlag = false;
                }
            }
            else
            {//gameover
                audioSource.PlayOneShot(killSound);
                SceneManager.LoadScene(sceneName);
            }

        }
    }
}
