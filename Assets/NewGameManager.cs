using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum PhaseGame
{
    Home,
    Edit,
    Show,
    Album
}
public class NewGameManager : MonoBehaviour
{
    public static NewGameManager THIS;

    public ChangeColorPanelController changeColorPanelController;
    public NewGameplayUI newGameplayUI;
    public int coinTotal;
    public CharacterManager characterManager;
    [ConditionalHide] public AlbumManager albumManager;
    public List<NewSlotData> slotDatasAllItemNotOwer = new List<NewSlotData>();

    public NewSlotInBodyData newSlotsInBodyData;
    public NewDataMonstersRemoteState newDataMonstersRemoteState;
    public List<NewSlotData> slotDatasDefaultItemOwer = new List<NewSlotData>();
    public List<NewSlotData> slotDatasHairItemOwer = new List<NewSlotData>();
    public List<NewSlotData> slotDatasEyeItemOwer = new List<NewSlotData>();
    public List<NewSlotData> slotDatasDressItemOwer = new List<NewSlotData>();
    public List<NewSlotData> slotDatasShoeItemOwer = new List<NewSlotData>();

    [ConditionalHide] public PhaseGame phaseGame;

    private void Awake()
    {
        THIS = this;
        newDataMonstersRemoteState.HandleDatasAllWithRemoteConfig();
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


    public void ClickOpenColorPanel()
    {
        if (changeColorPanelController.gameObject.activeSelf == false )
        {
            changeColorPanelController.gameObject.SetActive(true);
            changeColorPanelController.setOpenPanelDefaults(characterManager.objectDrag);
        }
        else
        {
            changeColorPanelController.gameObject.SetActive(false);

        }
    }

    public void ChangePhaseComplete()
    {
        phaseGame = PhaseGame.Show;
        HandlerPhaseChange();
    }

    public void ChangePhaseAlbum()
    {
        phaseGame = PhaseGame.Album;
        HandlerPhaseChange();
    }

    public void HandlerPhaseChange()
    {
        switch (phaseGame)
        {
            case PhaseGame.Home:
                break;
            case PhaseGame.Edit:
                break;
            case PhaseGame.Show:
                break;
            case PhaseGame.Album:
                //AudioManager.Instance.PlayMusicFade(completeUI.GetCompleteMusic());
                //completeUI.Hide();
                newGameplayUI.gameObject.SetActive(false);
                //homeUI.Hide();
                if (albumManager)
                    albumManager.Show();
                characterManager.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    #region NewSlotData
    public void GenerateListSlotDataNotOwer()
    {
        if (slotDatasAllItemNotOwer.Count == 0)
        {
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.defaultSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.hairSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.eyeSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.dressSlotsSorted));
            slotDatasAllItemNotOwer.AddRange(GetListSlotDataNotOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.shoeSlotsSorted));
        }
    }
    public void GenerateListSlotDataOwer()
    {
        if (slotDatasDefaultItemOwer.Count == 0)
        {
            slotDatasDefaultItemOwer.AddRange(GetListSlotDataOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.defaultSlotsSorted));
        }
        if (slotDatasHairItemOwer.Count == 0)
        {
            slotDatasHairItemOwer.AddRange(GetListSlotDataOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.hairSlotsSorted));
        }
        if (slotDatasEyeItemOwer.Count == 0)
        {
            slotDatasEyeItemOwer.AddRange(GetListSlotDataOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.eyeSlotsSorted));
        }
        if (slotDatasDressItemOwer.Count == 0)
        {
            slotDatasDressItemOwer.AddRange(GetListSlotDataOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.dressSlotsSorted));
        }
        if (slotDatasShoeItemOwer.Count == 0)
        {
            slotDatasShoeItemOwer.AddRange(GetListSlotDataOwerPartBody(NewGameManager.THIS.newDataMonstersRemoteState.shoeSlotsSorted));
        }
     }
    List<NewSlotData> GetListSlotDataNotOwerPartBody(List<NewSlotData> slotDatasOrigin)
    {
        List<NewSlotData> slotsDataResult = new List<NewSlotData>();
        for (int i = 0; i < slotDatasOrigin.Count; i++)
        {
            if (slotDatasOrigin[i].stateOfNewSlot == StateOfNewSlot.Unlock)
            {
                continue;
            }
            else
            {
                string nameSave = slotDatasOrigin[i].typeOfNewBody.ToString().ToUpper() + "_" + slotDatasOrigin[i].id;
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
    List<NewSlotData> GetListSlotDataOwerPartBody(List<NewSlotData> slotDatasOrigin)
    {
        List<NewSlotData> slotsDataResult = new List<NewSlotData>();
        for (int i = 0; i < slotDatasOrigin.Count; i++)
        {
            if (slotDatasOrigin[i].stateOfNewSlot == StateOfNewSlot.Unlock)
            {
                slotsDataResult.Add(slotDatasOrigin[i]);
            }
            else
            {
                string nameSave = slotDatasOrigin[i].typeOfNewBody.ToString().ToUpper() + "_" + slotDatasOrigin[i].id;
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
