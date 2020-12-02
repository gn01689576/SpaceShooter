using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreat : MonoBehaviour
{
    public GameObject Emeny;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f) 
        {
            Vector3 pos = new Vector3(Random.Range(-12.5f, 12.5f), 4.5f, 0); 
            Instantiate(Emeny, pos, transform.rotation);//產生敵人
            time = 0f; //時間歸零
        }
    }
}
