using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade instance = null;
    public float fadeSpeed = 2f;
    CanvasGroup canvasGroup;

    float fadeDirection;
    bool fade = false;
    float alpha = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;


        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        alpha = 1;

        Fade(-1);
    }

    void Update()
    {
        if (fade)
        {
            if(fadeDirection > 0)
            {
                alpha += fadeSpeed * Time.deltaTime;
                if (alpha >= 1)
                {
                    fade = false;
                    alpha = 1;
                }
            }
            else
            {
                alpha -= fadeSpeed * Time.deltaTime;
                if (alpha <= 0)
                {
                    fade = false;
                    alpha = 0;
                }
            }
            canvasGroup.alpha = alpha;
        }
    }

    public void Fade(int direction)
    {
        fade = true;
        fadeDirection = direction;
    }
}
