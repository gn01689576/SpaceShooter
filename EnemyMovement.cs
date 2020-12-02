using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, -0.01f, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ship" || col.tag == "Bullet") //如果碰撞的標籤是Ship或Bullet
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(col.gameObject); //消滅碰撞的物件
            Destroy(gameObject); //消滅物件本身
            if (col.tag == "Ship")
            {
                Instantiate(explosion, transform.position, transform.rotation);
                GameObject.Find("BackGround").GetComponent<GameCtrl>().GameOver();
            }
        }
    }
}
