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

    private Image image;
    private float alpha = 0;
    private Color panelColour;
    private Color textColour;
    private float newalpha = 1;


    void Start()
    {
        panelColour = new Color(0, 219, 235, alpha);
        textColour = new Color(255, 255, 255, alpha);
        winner.color = textColour;
        backButtonGO.color = textColour;
        stats.color = textColour;
        winsSign.color = textColour;
        gameOverTitle.color = textColour;
        image = gameObject.GetComponent<Image>();
        image.color = panelColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (GOPanelFadeON == true && newalpha < 255)
        {
            newalpha += Time.deltaTime;
            if (newalpha > 255) newalpha = 255;
            Debug.LogWarning(newalpha);
            panelColour.a = newalpha;
            image.color = panelColour;
            textColour.a = newalpha;
            winner.color = textColour;
            backButtonGO.color = textColour;
            stats.color = textColour;
            winsSign.color = textColour;
            gameOverTitle.color = textColour;

        }
    }
}
