using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager THIS;

    public int coinTotal;
    public List<SlotData> slotDatasAllItemNotOwer = new List<SlotData>();

    public AllSlotsInBodyData allSlotsInBodyData;
    public AllDataMonstersRemoteState allDataMonstersRemoteState;
    public List<SlotData> slotDatasHeadItemOwer = new List<SlotData>();
     public List<SlotData> slotDatasEyeItemOwer = new List<SlotData>();
     public List<SlotData> slotDatasMouthItemOwer = new List<SlotData>();
     public List<SlotData> slotDatasAccItemOwer = new List<SlotData>();
     public List<SlotData> slotDatasBodyItemOwer = new List<SlotData>();
    // Start is called before the first frame update
    private void Awake()
    {
        THIS = this;
        allDataMonstersRemoteState.HandleDatasAllWithRemoteConfig();
        GenerateListSlotDataNotOwer();
        GenerateListSlotDataOwer();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddValueCoin(int coinAdd, float duration = .2f)
    {
        if (coinAdd > 0)
        {
            Debug.Log("Coin > 0");
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.getCoinAudioClip);
            //Vibration.Vibrate(DataGame.numberPowerVibration);
        }
        float coinTotal = this.coinTotal;
        DOTween.Complete("Add Coin");
        DOTween.To(() => coinTotal, x => coinTotal = x, coinTotal + coinAdd, duration)
            .SetId("Add Coin")
            .SetUpdate(true)
            .OnUpdate(() =>
            {
                //callbackTextCoin?.Invoke((int)coinTotal);
                //SetValueCoin((int)coinTotal);
            })
            .OnComplete(() =>
            {
                //SetValueCoin((int)coinTotal);
            });
    }

    #region SlotData
    public void GenerateListSlotDataNotOwer()
    {
        if (slotDatasAllItemNotOwer.Count == 0)
        {
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.headSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.eyeSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.accSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.mouthSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.bodySlotsSorted));
        }
    }
    public void GenerateListSlotDataOwer()
    {
        if (slotDatasHeadItemOwer.Count == 0)
        {
            slotDatasHeadItemOwer.AddRange(GetListSlotDataOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.headSlotsSorted));
        }
        if (slotDatasEyeItemOwer.Count == 0)
        {
            slotDatasEyeItemOwer.AddRange(GetListSlotDataOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.eyeSlotsSorted));
        }
        if (slotDatasMouthItemOwer.Count == 0)
        {
            slotDatasMouthItemOwer.AddRange(GetListSlotDataOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.mouthSlotsSorted));
        }
        if (slotDatasAccItemOwer.Count == 0)
        {
            slotDatasAccItemOwer.AddRange(GetListSlotDataOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.accSlotsSorted));
        }
        if (slotDatasBodyItemOwer.Count == 0)
        {
            slotDatasBodyItemOwer.AddRange(GetListSlotDataOwerPartBody(GameManager.THIS.allDataMonstersRemoteState.bodySlotsSorted));
        }
    }
    List<SlotData> GetListSlotDataNotOwerPartBody(List<SlotData> slotDatasOrigin)
    {
        List<SlotData> slotsDataResult = new List<SlotData>();
        for (int i = 0; i < slotDatasOrigin.Count; i++)
        {
            if (slotDatasOrigin[i].stateOfSlot == StateOfSlot.Unlock)
            {
                continue;
            }
            else
            {
                string nameSave = slotDatasOrigin[i].typeOfBody.ToString().ToUpper() + "_" + slotDatasOrigin[i].id;
                bool isUnlock = PlayerPrefs.GetInt(nameSave, -1) == 0;
                if (isUnlock)
                {
                    continue;
                }
                else
                {
                    slotsDataResult.Add(slotDatasOrigin[i]);
                }
            }
        }
        return slotsDataResult;
    }
    List<SlotData> GetListSlotDataOwerPartBody(List<SlotData> slotDatasOrigin)
    {
        List<SlotData> slotsDataResult = new List<SlotData>();
        for (int i = 0; i < slotDatasOrigin.Count; i++)
        {
            if (slotDatasOrigin[i].stateOfSlot == StateOfSlot.Unlock)
            {
                slotsDataResult.Add(slotDatasOrigin[i]);
            }
            else
            {
                string nameSave = slotDatasOrigin[i].typeOfBody.ToString().ToUpper() + "_" + slotDatasOrigin[i].id;
                bool isUnlock = PlayerPrefs.GetInt(nameSave, -1) == 0;
                if (isUnlock)
                {
                    slotsDataResult.Add(slotDatasOrigin[i]);
                }
            }
        }
        return slotsDataResult;
    }
    #endregion
}
