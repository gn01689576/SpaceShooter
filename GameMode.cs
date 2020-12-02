using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMode : MonoBehaviour
{
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    public List<BulletCharacter> tempBullets;
    public float firerate1;
    float nextfire;
    void Start()
    {
        tempBullets = new List<BulletCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextfire)
        {
            StopAllCoroutines();
            //ClearBulletsList();
            StartCoroutine(FirShotgun());
            nextfire = Time.time + firerate1;
        }

        if (Input.GetKey(KeyCode.Q) && Time.time > nextfire)
        {
            StopAllCoroutines();
            //ClearBulletsList();
            StartCoroutine(FirRound(5, firPoint.transform.position));
        }

        if (Input.GetKey(KeyCode.W) && Time.time > nextfire)
        {
            StopAllCoroutines();
            //ClearBulletsList();
            StartCoroutine(FirRoundGroup());
        }

        if (Input.GetKey(KeyCode.E) && Time.time > nextfire)
        {
            StopAllCoroutines();
            //ClearBulletsList();
            StartCoroutine(FireTurbine());
        }

        if (Input.GetKey(KeyCode.R) && Time.time > nextfire)
        {
            StopAllCoroutines();
            //ClearBulletsList();
           
            StartCoroutine(FireBallBulle());
        }
    }

    //三發散射
    IEnumerator FirShotgun()
    {
        Vector3 bulletDir = firPoint.transform.up; //發射方向
        Quaternion leftRota = Quaternion.AngleAxis(-30, Vector3.forward);
        Quaternion RightRota = Quaternion.AngleAxis(30, Vector3.forward); //製造兩個旋轉 繞Z軸左右旋轉30度
        for (int i=0;i<10;i++)     //發射次數
        {
            for (int j=0;j<3;j++) //一次發射三發
            {
                switch (j)
                {
                    case 0:
                        CreatBullet(bulletDir, firPoint.transform.position);  //第一顆子彈 不用旋轉
                        break;
                    case 1:
                        bulletDir = RightRota * bulletDir;//旋轉到下一個方向發射
                        CreatBullet(bulletDir, firPoint.transform.position);
                        break;
                    case 2:
                        bulletDir = leftRota*(leftRota * bulletDir); //右邊發射完要往左旋轉兩次發射
                        CreatBullet(bulletDir, firPoint.transform.position);
                        bulletDir = RightRota * bulletDir; //一輪發射完向右轉回去繼續下一輪
                        break;
                }
            }
            yield return new WaitForSeconds(0.2f); //0.2秒後下一波發射
        }
    }

    //圓形彈幕
    IEnumerator FirRound(int number,Vector3 creatPoint)
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(10, Vector3.forward);
        for (int i=0;i< number; i++)
        {
            for (int j=0;j<36;j++)
            {
                CreatBullet(bulletDir, creatPoint);
                bulletDir = rotateQuate * bulletDir;//發射角度旋轉10度到下一個方向
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    //組合圓形彈幕
    IEnumerator FirRoundGroup()
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);
        List<BulletCharacter> bullets = new List<BulletCharacter>();//放入8個子彈
        for (int i=0;i<8;i++)
        {
            var tempBullet = CreatBullet(bulletDir, firPoint.transform.position);
            bulletDir = rotateQuate * bulletDir; //發射子彈後旋轉45度到下一個方向
            bullets.Add(tempBullet); 
        }
        yield return new WaitForSeconds(1.0f);   //1秒後產生多波彈幕
        for (int i = 0; i < bullets.Count; i++)
        {
            StartCoroutine(FirRound(6, bullets[i].transform.position));//在之前產生子彈位置生成圓形彈幕
        }
    }

    //漩渦型彈幕
    IEnumerator FireTurbine()
    {
        Vector3 bulletDir = firPoint.transform.up;     
        Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.forward);
        float radius = 0.6f; //生成半徑
        float distance = 0.2f;//每生成一次增加距離
        for (int i=0;i<18;i++)
        {
            Vector3 firePoint = firPoint.transform.position + bulletDir * radius;//計算生成位置
            StartCoroutine(FirRound(1, firePoint));//在計算位置生成圓形彈幕
            yield return new WaitForSeconds(0.05f);//延遲0.05秒生成下一波
            bulletDir = rotateQuate * bulletDir;//改變發射方向
            radius += distance;     //生成半徑增加
        }
    }

    //變大變小圓形彈幕
    IEnumerator FireBallBulle()
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(10, Vector3.forward);
        float distance = 1.0f;
        for (int j=0;j<8;j++)
        {
            for (int i = 0; i < 36; i++)
            {
                Vector3 creatPoint = firPoint.transform.position + bulletDir * distance;
                BulletCharacter tempBullet = CreatBullet(bulletDir, creatPoint);
                tempBullet.isMove = false;
                StartCoroutine(tempBullet.DirChangeMoveMode(10.0f, 0.4f, 15));
                bulletDir = rotateQuate * bulletDir;
            }
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }
    public BulletCharacter CreatBullet(Vector3 dir,Vector3 creatPoint)
    {
        BulletCharacter bulletCharacter = Instantiate(bulletTemplate, creatPoint, Quaternion.identity);
        bulletCharacter.gameObject.SetActive(true);
        bulletCharacter.dir = dir;
        tempBullets.Add(bulletCharacter);
        return bulletCharacter;
    }

    
    public void ClearBulletsList()
    {
        if (tempBullets.Count>0)
        {
            for (int i=(tempBullets.Count-1);i>=0;i--)
            {
                Destroy(tempBullets[i].gameObject);
            }
        }

        tempBullets.Clear();

    }
}
