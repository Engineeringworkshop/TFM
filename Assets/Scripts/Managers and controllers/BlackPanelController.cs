using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackPanelController : MonoBehaviour
{
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private Image blackPanelImage;

    [Header("Configuration")]
    [SerializeField] [Range(0.0f, 1.0f)] private float defaultFadeIn;
    [SerializeField] private float defaultFadeTime;

    private WaitForSecondsRealtime fadeTimer;

    private void OnValidate()
    {
        if (blackPanelImage == null)
        {
            blackPanelImage = blackPanel.GetComponent<Image>();
        }
    }

    private void Awake()
    {
        fadeTimer = new WaitForSecondsRealtime(defaultFadeTime);

        blackPanel.SetActive(false);
    }

    #region Fade Out

    /// <summary>
    /// Fade out the black panel starting at defaultFadeIn alpha value and finishing at 0 alpha value during defaultFadeTime seconds
    /// </summary>
    /// <returns></returns>
    public bool FadeOutBlackPanel()
    {
        return FadeOutBlackPanel(defaultFadeIn, 0); ;
    }

    /// <summary>
    /// Fade out the black panel starting at the startAlpha value and finishing at endAlpha during defaultFadeTime seconds
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <returns></returns>
    public bool FadeOutBlackPanel(float startAlpha, float endAlpha)
    {
        return FadeOutBlackPanel(startAlpha, endAlpha, defaultFadeTime);
    }

    /// <summary>
    /// Fade out the black panel starting at the startAlpha value and finishing at endAlpha during time seconds
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool FadeOutBlackPanel(float startAlpha, float endAlpha, float time)
    {
        blackPanel.SetActive(true);

        StartCoroutine(FadeOut(startAlpha, endAlpha, time));

        return true;
    }

     public IEnumerator FadeOut(float startAlpha, float endAlpha, float time)
    {
        float timeCounter = 0.0f;

        blackPanel.SetActive(true);

        // Set alpha to fadeIn value to start fade out
        SetAlphaValue(blackPanelImage, startAlpha);

        // Reduce alpha each frame
        while (blackPanelImage.color.a > endAlpha)
        {
            var currAlpha = Mathf.Lerp(startAlpha, endAlpha, timeCounter / time);

            SetAlphaValue(blackPanelImage, currAlpha);

            timeCounter += Time.deltaTime;

            yield return null;
        }

        blackPanel.SetActive(false);
    }

    #endregion

    #region Fade In

    /// <summary>
    /// Fade in the black panel starting at 0 alpha value and finishing at defaultFadeIn value during defaultFadeTime seconds
    /// </summary>
    /// <returns></returns>
    public bool FadeInBlackPanel()
    {
        return FadeInBlackPanel(0, defaultFadeIn, defaultFadeTime);
    }

    /// <summary>
    /// Fade in the black panel starting at the startAlpha value and finishing at endAlpha during defaultFadeTime seconds
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <returns></returns>
    public bool FadeInBlackPanel(float startAlpha, float endAlpha)
    {
        return FadeInBlackPanel(0, startAlpha, endAlpha);
    }

    /// <summary>
    /// Fade out the black panel starting at the startAlpha value and finishing at endAlpha during time seconds
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public bool FadeInBlackPanel(float startAlpha, float endAlpha, float time)
    {
        blackPanel.SetActive(true);

        StartCoroutine(FadeIn(startAlpha, endAlpha, time));

        return false;
    }

    public IEnumerator FadeIn(float startAlpha, float endAlpha, float time)
    {
        float timeCounter = 0.0f;

        blackPanel.SetActive(true);

        // Set alpha to 0 value to start fade in
        SetAlphaValue(blackPanelImage, startAlpha);

        // Increase alpha each frame
        while (blackPanelImage.color.a < endAlpha)
        {
            var currAlpha = Mathf.Lerp(startAlpha, endAlpha, timeCounter / time);

            SetAlphaValue(blackPanelImage, currAlpha);

            timeCounter += Time.deltaTime;

            yield return null;
        }
    }

    #endregion

    #region Auxiliar method

    private void SetAlphaValue(Image image, float alphaValue)
    {
        Color newColor = new Color(image.color.r, image.color.g, image.color.b, alphaValue);

        image.color = newColor;
    }

    #endregion
}
