using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public GameObject Emeny;
    public float time;

    public GameObject GameTitle; 
    public GameObject GameOverTitle; 
    public GameObject PlayButton;
    public GameObject RestartButton;
    public bool IsPlaying = false; 
    // Start is called before the first frame update
    void Start()
    {
        GameTitle.SetActive(true);       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f && IsPlaying == true)
        {
            Vector3 pos = new Vector3(Random.Range(-12.5f, 12.5f), 4.5f, 0);
            Instantiate(Emeny, pos, transform.rotation);//產生敵人
            time = 0f; //時間歸零
        }
    }

    public void GameStart()
    {
        {
            IsPlaying = true; //設定IsPlaying為true，代表遊戲正在進行中
            GameTitle.SetActive(false); 
            PlayButton.SetActive(false);
        }
    }

    public void GameOver()
    {
        IsPlaying = false;
        GameOverTitle.SetActive(true);
        RestartButton.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
    }
}
