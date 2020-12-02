using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCharacter : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public bool isMove;
    bool isBallModePlay;
    void Start()
    {
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            BulletMove();
        }

    }

    public void BulletMove()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    
    public IEnumerator DirChangeMoveMode(float endTime, float dirChangeTime, float angle)//endTime 移動結束時間 dirChangeTime 方向改變時間 angle 方向改變角度
    {
        float time = 0;
        bool isRotate = true;
        isBallModePlay = true;
        while (isBallModePlay)
        {
            time += Time.deltaTime;
            transform.position += speed * dir * Time.deltaTime;
            if (time >= dirChangeTime && isRotate)
            {
                isRotate = false;
                StartCoroutine(BulletRotate(angle));
            }

            yield return null;
        }
    }

    //彈幕動態改變移動方向
    IEnumerator BulletRotate(float angle)
    {
        while (isBallModePlay)
        {
            Quaternion tempQuat = Quaternion.AngleAxis(angle, Vector3.forward);
            dir = tempQuat * dir;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
