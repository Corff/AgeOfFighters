using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public bool GOPanelFadeON = false;
    public Text winner;
    public Text backButtonGO;
    public Text stats;
    public Text winsSign;
    public Text gameOverTitle;
    public float durationFade = 0.5f;



    // Update is called once per frame
    void Update()
    {
        if (GOPanelFadeON == true)
        {
            AlphaFade();
        }
    }

    public void AlphaFade()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(AlphaFadeDo(canvGroup, canvGroup.alpha, 1));
        if(canvGroup.alpha == 1)
        {
            Time.timeScale = 0;
        }
    }

    public IEnumerator AlphaFadeDo(CanvasGroup canvasGroup, float start, float end)
    {
        float timer = 0f;

        while(timer < durationFade)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, timer / durationFade);

            yield return null;
        }
    }
}
