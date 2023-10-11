using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public Image BG;
    public List<GameObject> items = new List<GameObject>();
    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0f, -3000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f),fadeTime,false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime).OnComplete(()=> {
            BG.gameObject.SetActive(true);
            
        });
        StartCoroutine(ItemAnimation());

    }

    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f,-3000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup.DOFade(0, fadeTime);
        canvasGroup.DOFade(1, fadeTime).OnComplete(() => {
            BG.gameObject.SetActive(false);
        });

    }

    IEnumerator ItemAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }
        foreach (var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
