using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class ItemOfSlot : MonoBehaviour
{
    [SerializeField] Image LockImage;
    [SerializeField] Image FocusImage;
    [SerializeField] Image IconImage;
    [SerializeField] GameObject LockGold;
    [SerializeField] TextMeshProUGUI LockCoinText;
    [SerializeField] GameObject LockAds;
    [SerializeField] Button button;
    public SlotData slotData;
    StateOfSlot stateOfSlot;
    UnityAction<SlotData> callbackClick;
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
        button.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);
            //GameManager.Instance.gameplayUI.AbortItemsEarnCoin();
            string nameSave = slotData.typeOfBody.ToString().ToUpper() + "_" + slotData.id;
            bool isUnlock = PlayerPrefs.GetInt(nameSave, -1) == 0;
            if (isUnlock)
            {
                //if (tapItemAP)
                //{
                //    tapItemAP.Stop();
                //}
                //tapItemAP = AudioManager.Instance.PlayOneShotReturnRef(AudioManager.Instance.audioClipData.tapItemsAudioClip[UnityEngine.Random.Range(0, AudioManager.Instance.audioClipData.tapItemsAudioClip.Length)], callback: () =>
                //{
                //    tapItemAP = null;
                //});
                //GameManager.Instance.ShowInterTapEveryWhere();

                callbackClick?.Invoke(slotData);
            }
            else
            {
                SetStateUpdate();
            }
        });
    }

    public void SetStateUpdate()
    {
        //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);
        string nameSave = slotData.typeOfBody.ToString().ToUpper() + "_" + slotData.id;
        switch (stateOfSlot)
        {
            case StateOfSlot.Ads:
                //Ads
                // FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbADS_REWARD_CLICK_xxx, slotData.name));
                Debug.Log("Show Ads");
                PlayerPrefs.SetInt(nameSave, (int)StateOfSlot.Unlock);
                PlayerPrefs.Save();
                UnlockItem();
                //AdsIronSourceMediation.Instance.ShowRewardedAd((bool isWatched) =>
                //{
                //    if (isWatched)
                //    {
                //        FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbADS_REWARD_COMPLETED_xxx, slotData.name));
                //        PlayerPrefs.SetInt(nameSave, (int)StateOfSlot.Unlock);
                //        PlayerPrefs.Save();

                //        UnlockItem();
                //    }
                //});
                break;
            case StateOfSlot.Gold:
                //Gold
                if (GameManager.THIS.coinTotal >= slotData.priceGold)
                {
                    //FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbBUY_NAMEITEM, slotData.name.ToString()));
                    GameManager.THIS.AddValueCoin(-slotData.priceGold);
                    PlayerPrefs.SetInt(nameSave, (int)StateOfSlot.Unlock);
                    PlayerPrefs.Save();

                    UnlockItem();
                }
                else
                {
                    Debug.Log("Need More Coin");
                    //GameManager.Instance.notificationPopup.ShowWithText("Need more coins!");
                    //earnGoldObject.SetActive(true);
                    //GameManager.Instance.earnCoinsPopup.Show();
                }
                break;
            default:
                break;
        }
    }

    public void UnlockItem(bool isSelected = true)
    {
       // AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.itemUnlockAudioClip);
        //Vibration.Vibrate(DataGame.numberPowerVibration);

        GameManager.THIS.slotDatasAllItemNotOwer.Remove(slotData);
        switch (slotData.typeOfBody)
        {
            case TypeOfBody.Head:
                GameManager.THIS.slotDatasHeadItemOwer.Add(slotData);
                break;
            case TypeOfBody.Eye:
                GameManager.THIS.slotDatasEyeItemOwer.Add(slotData);
                break;
            case TypeOfBody.Mouth:
                GameManager.THIS.slotDatasMouthItemOwer.Add(slotData);
                break;
            case TypeOfBody.Acc:
                GameManager.THIS.slotDatasAccItemOwer.Add(slotData);
                break;
            case TypeOfBody.Body:
                GameManager.THIS.slotDatasBodyItemOwer.Add(slotData);
                break;
            default:
                break;
        }
        LockImage.gameObject.SetActive(false);
        FocusImage.gameObject.SetActive(false);
        LockGold.SetActive(false);
        LockAds.SetActive(false);
       // UnlockItem.ga.SetActive(true); them vao cho dep
        //earnGoldObject.SetActive(false);
        //DOTween.Sequence()
        //    .SetId(gameObject.GetInstanceID() + "UnlockItem")
        //    .AppendInterval(2)
        //    .AppendCallback(() =>
        //    {
        //        unlockObject.SetActive(false);
        //    });
        if (isSelected)
        {
            callbackClick?.Invoke(slotData);
        }
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

    public void SetData(SlotData slotData, UnityAction<SlotData> callbackClick)
    {
        this.slotData = slotData;
        this.callbackClick = callbackClick;
        //SetInteractive(false);
        IconImage.sprite = slotData.sprite;
        LockCoinText.text = slotData.priceGold.ToString();
        string nameSave = slotData.typeOfBody.ToString().ToUpper() + "_" + slotData.id;
        bool isUnlock = PlayerPrefs.GetInt(nameSave, -1) == 0;
        if (isUnlock)
        {
            stateOfSlot = StateOfSlot.Unlock;
            SetStateFirst();

            return;
        }
        stateOfSlot = slotData.stateOfSlot;

        if (stateOfSlot == StateOfSlot.Unlock && PlayerPrefs.HasKey(nameSave) == false)
        {
            PlayerPrefs.SetInt(nameSave, (int)StateOfSlot.Unlock);
            PlayerPrefs.Save();
        }
        SetStateFirst();
    }

    public void SetStateFirst()
    {
        switch (stateOfSlot)
        {
            case StateOfSlot.Unlock:
                LockImage.gameObject.SetActive(false);
                FocusImage.gameObject.SetActive(false);
                LockGold.SetActive(false);
                LockAds.SetActive(false);
                //unlockObject.SetActive(false);
                //earnGoldObject.SetActive(false);
                break;
            case StateOfSlot.Ads:
                LockImage.gameObject.SetActive(true);
                FocusImage.gameObject.SetActive(false);
                LockGold.SetActive(false);
                LockAds.SetActive(true);
                //unlockObject.SetActive(false);
                //earnGoldObject.SetActive(false);
                break;
            case StateOfSlot.Gold:
                LockImage.gameObject.SetActive(true);
                FocusImage.gameObject.SetActive(false);
                LockGold.SetActive(true);
                LockAds.SetActive(false);
                //unlockObject.SetActive(false);
                //earnGoldObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
