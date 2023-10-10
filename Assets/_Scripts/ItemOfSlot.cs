using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ItemOfSlot : MonoBehaviour
{
    [SerializeField] Image LockImage;
    [SerializeField] Image FocusImage;
    [SerializeField] Image IconImage;
    [SerializeField] GameObject LockGold;
    [SerializeField] TextMeshProUGUI LockCoinText;
    [SerializeField] GameObject LockAds;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .2f);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        DOTween.Kill(transform);
        DOTween.Complete(gameObject.GetInstanceID() + "UnlockItem");
        Debug.Log(gameObject.GetInstanceID() + "UnlockItem");
        UnPickItem();
        //if (tapItemAP)
        //{
        //    tapItemAP.Stop();
        //}
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnPickItem()
    {
        FocusImage.gameObject.SetActive(false);
        transform.GetChild(0).transform.localScale = Vector3.one;
        DOTween.Kill(gameObject.GetInstanceID() + "PickItem");
    }

    public void PickItem()
    {
        LockImage.gameObject.SetActive(false);
        FocusImage.gameObject.SetActive(true);
        LockGold.SetActive(false);
        LockAds.SetActive(false);
        DOTween.Sequence()
            .SetId(gameObject.GetInstanceID() + "PickItem")
            .Append(transform.GetChild(0).transform.DOScale(1.05f, .5f).SetEase(Ease.Linear))
            .Append(transform.GetChild(0).transform.DOScale(1f, .5f).SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Yoyo);
        //unlockObject.SetActive(true);
    }
}
