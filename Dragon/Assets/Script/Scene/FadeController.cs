using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    private bool isFadeOut = false; //フェードアウトフラグ
    private bool isFadeIn = true;   //フェードインフラグ

    private float fadeSpeed = 0.75f;    //フェイドアウトスピード
    [SerializeField] private Image fadeImage = default;
    [SerializeField, HeaderAttribute("色")]
    private float red, green, blue, alpha;  // 赤, 緑, 青, 透明度
    private string afterScene;
    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        SetColor(alpha);
    }

    // Update is called once per frame
    void Update()
    {
        fadein();
        fadeout();
        
    }

    // フェイドインの処理
    private void fadein()
    {
         // フェードインフラグがtrueのとき1回再生
        if (isFadeIn)  
        {
            // 透明度を上げる
            alpha -= fadeSpeed * Time.deltaTime;    
            SetColor(alpha);
            // 透明度が０になるとフェイドアウトを止める
            if (alpha <= 0) 
                isFadeIn = false;
        }
    }

    // フェイドアウトの処理
    private void fadeout()
    {
        // フェードアウトフラグがtrueのとき
        if (isFadeOut)  
        {
            // 透明度最大値
            int m_Maxalpha = 1;
            // 透明度を上げる
            alpha += fadeSpeed * Time.deltaTime;  
            SetColor(alpha);  
            // アルファ値が最大値になるとScene転移する
            if (alpha >= m_Maxalpha) 
            {
                isFadeOut = false;
                isFadeIn = true;
                sceneController.SceneMove = true;
                SceneManager.LoadScene(afterScene);
            }
        }
    }
    // フェードアウト
    public void fadeOutStart(float al, string nextScene)
    {
        SetColor(al);
        isFadeOut = true;
        afterScene = nextScene;
    }

    // カラー設定
    private void SetColor(float al)
    {
        fadeImage.color = new Color(red, green, blue, al);
    }
}
