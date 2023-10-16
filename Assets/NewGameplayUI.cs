using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewGameplayUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        prevButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapNextAudioClip);
            //GameManager.Instance.ShowInterTapEveryWhere();
           // tapPrevOrCompleteFX.Play();
           // Vibration.Vibrate(DataGame.numberPowerVibration);
            PrevSlotOfStep();
        });
        nextButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapNextAudioClip);
            //GameManager.Instance.ShowInterTapEveryWhere();
            //tapNextOrCompleteFX.Play();
            //Vibration.Vibrate(DataGame.numberPowerVibration);
            NextSlotOfStep();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        UpdateDataItemOfSlot();
    }

    
    [SerializeField] NewItemOfSlot newItemOfSlotPrefab;
    [SerializeField] Sprite noneItemOfSlotSprite;
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] Button completeButton;
    [SerializeField] Button ChangeCollorButton;
    [ConditionalHide] public TypeOfNewBody typeOfNewBody;
    List<NewItemOfSlot> newItemOfSlotsPooling = new List<NewItemOfSlot>();
    List<NewItemOfSlot> newItemOfSlotsCurrent = new List<NewItemOfSlot>();
    Dictionary<TypeOfNewBody, int> stepDictionary = new Dictionary<TypeOfNewBody, int>();
    NewItemOfSlot newItemOfSlotSave;

    //[SerializeField] List<SlotOfStep> slotsOfStep;
    public void UpdateDataItemOfSlot()
    {
        if (handleUpdateDataItemOfSlotCoroutine != null)
        {
            StopCoroutine(handleUpdateDataItemOfSlotCoroutine);
        }
        handleUpdateDataItemOfSlotCoroutine = StartCoroutine(IEnumHandleUpdateDataItemOfSlot());
    }
    Coroutine handleUpdateDataItemOfSlotCoroutine;
    IEnumerator IEnumHandleUpdateDataItemOfSlot()
    {
        //itemOfSlotShowing.Clear();
        NewItemOfSlot newItemOfSlot;
        for (int i = newItemOfSlotsCurrent.Count - 1; i >= 0; i--)
        {
            newItemOfSlot = newItemOfSlotsCurrent[i];
            newItemOfSlot.gameObject.SetActive(false);
            newItemOfSlotsPooling.Add(newItemOfSlot);
            newItemOfSlotsCurrent.RemoveAt(i);
        }
        yield return new WaitForSeconds(.01f);
        NewSlotData newSlotData;
        newItemOfSlotSave = null;
        if (typeOfNewBody != TypeOfNewBody.Default && typeOfNewBody != TypeOfNewBody.Shoe)
        {
            newSlotData = new NewSlotData();
            newSlotData.typeOfNewBody = typeOfNewBody;
            newSlotData.sprite = noneItemOfSlotSprite;
            NewItemOfSlot newItemOfSlotNone = GetItemOfSlot();
            newItemOfSlotNone.SetData(newSlotData, delegate (NewSlotData newSlotDataCallback)
            {
                if (newItemOfSlotSave == null || newSlotDataCallback.id != newItemOfSlotSave.newSlotData.id)
                {
                    //GameManager.Instance.characterManager.ResetDirect();

                    if (newItemOfSlotSave)
                        newItemOfSlotSave.UnPickItem();
                    newItemOfSlotSave = newItemOfSlotNone;
                }
                newItemOfSlotNone.PickItem();
                NewGameManager.THIS.characterManager.StepEdit(newSlotDataCallback);
                if (newItemOfSlotNone.newSlotData.typeOfNewBody != TypeOfNewBody.Shoe)
                {
                    if (nextButton.gameObject.activeSelf == false)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                    if (ChangeCollorButton.gameObject.activeSelf == false)
                    {
                        ChangeCollorButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (completeButton.gameObject.activeSelf == false)
                    {
                        completeButton.gameObject.SetActive(true);
                    }
                }

                if (stepDictionary.ContainsKey(typeOfNewBody) == false)
                {
                    stepDictionary.Add(typeOfNewBody, newItemOfSlotSave.newSlotData.id);
                }
                else
                {
                    stepDictionary[typeOfNewBody] = newItemOfSlotSave.newSlotData.id;
                }
            });
            if (newItemOfSlotSave == null && stepDictionary.TryGetValue(typeOfNewBody, out int indexItemSave))

            {
                newItemOfSlotSave = newItemOfSlotNone.newSlotData.id == indexItemSave ? newItemOfSlotNone : null;
                if (newItemOfSlotSave)
                {
                    //itemOfSlotSave.slotData = itemOfSlotNone.slotData;
                    newItemOfSlotSave.PickItem();
                }
            }
            //itemOfSlotShowing.Add(itemOfSlotNone);
            yield return new WaitForSeconds(.02f);
        }

        List<NewSlotData> slotDataTypeCurrent = NewGameManager.THIS.newDataMonstersRemoteState.GetSlotDatas(typeOfNewBody);
        for (int i = 0; i < slotDataTypeCurrent.Count; i++)
        {
            newSlotData = slotDataTypeCurrent[i];
            NewItemOfSlot itemOfSlotSelect = GetItemOfSlot();
            itemOfSlotSelect.SetData(newSlotData, delegate (NewSlotData newSlotDataCallback)
            {
                if (newItemOfSlotSave == null || newSlotDataCallback.id != newItemOfSlotSave.newSlotData.id)
                {
                   // NewGameManager.THIS.characterManager.ResetDirect();

                    if (newItemOfSlotSave)
                        newItemOfSlotSave.UnPickItem();
                    newItemOfSlotSave = itemOfSlotSelect;
                }
                itemOfSlotSelect.PickItem();
                NewGameManager.THIS.characterManager.StepEdit(newSlotDataCallback);

                if (itemOfSlotSelect.newSlotData.typeOfNewBody != TypeOfNewBody.Shoe)
                {
                    if (nextButton.gameObject.activeSelf == false)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                    if (ChangeCollorButton.gameObject.activeSelf == false)
                    {
                        ChangeCollorButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (completeButton.gameObject.activeSelf == false)
                    {
                        completeButton.gameObject.SetActive(true);
                    }
                }

                if (stepDictionary.ContainsKey(typeOfNewBody) == false)
                {
                    stepDictionary.Add(typeOfNewBody, newItemOfSlotSave.newSlotData.id);
                }
                else
                {
                    stepDictionary[typeOfNewBody] = newItemOfSlotSave.newSlotData.id;
                }
            });
            if (newItemOfSlotSave == null && stepDictionary.TryGetValue(typeOfNewBody, out int indexItemSave))

            {
                newItemOfSlotSave = itemOfSlotSelect.newSlotData.id == indexItemSave ? itemOfSlotSelect : null;
                if (newItemOfSlotSave)
                {
                    //itemOfSlotSave.slotData = itemOfSlotSelect.slotData;
                    newItemOfSlotSave.PickItem();
                }
            }
            //itemOfSlotShowing.Add(itemOfSlotSelect);

            yield return new WaitForSeconds(.02f);
        }
        //for (int i = 0; i < itemOfSlotShowing.Count; i++)
        //{
        //    itemOfSlotShowing[i].SetInteractive(true);
        //}

    }

    NewItemOfSlot GetItemOfSlot()
    {
        NewItemOfSlot newItemOfSlot = null;
        for (int i = newItemOfSlotsPooling.Count - 1; i >= 0; i--)
        {
            if (newItemOfSlotsPooling[i].gameObject.activeSelf == false)
            {
                newItemOfSlot = newItemOfSlotsPooling[i];
                newItemOfSlotsPooling.RemoveAt(i);
                break;
            }
        }
        if (!newItemOfSlot)
        {
            if (newItemOfSlotPrefab.gameObject.activeSelf)
            {
                newItemOfSlotPrefab.gameObject.SetActive(false);
            }
            newItemOfSlot = Instantiate(newItemOfSlotPrefab, newItemOfSlotPrefab.transform.parent);
        }
        newItemOfSlotsCurrent.Add(newItemOfSlot);

        newItemOfSlot.gameObject.SetActive(true);
        return newItemOfSlot;
    }

    public void NextSlotOfStep()
    {
        //Debug.Log(typeOfNewBody);
        typeOfNewBody++;
        //Debug.Log(typeOfNewBody);
        if ((int)typeOfNewBody > (int)TypeOfNewBody.Shoe)
        {
            typeOfNewBody = TypeOfNewBody.Shoe;
        }
        UpdateSlotOfStep();
        //GameManager.Instance.characterManager.ResetDirect();

        NewGameManager.THIS.characterManager.ChangeStepEdit(typeOfNewBody);

        completeButton.gameObject.SetActive(typeOfNewBody == TypeOfNewBody.Shoe && NewGameManager.THIS.characterManager.isSelectedShoe);
        nextButton.gameObject.SetActive(stepDictionary.ContainsKey(typeOfNewBody) && completeButton.gameObject.activeSelf == false);
        if (typeOfNewBody == TypeOfNewBody.Default || typeOfNewBody == TypeOfNewBody.Shoe)
        {
           // textDragGuideObject.SetActive(false);
        }
        else
        {
            //textDragGuideObject.SetActive(true);
        }
        if (typeOfNewBody == TypeOfNewBody.Default)
        {
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
        }
        ChangeCollorButton.gameObject.SetActive(false);
    }
    public void PrevSlotOfStep()
    {
        typeOfNewBody--;
        if ((int)typeOfNewBody <= (int)TypeOfNewBody.Default)
        {
            typeOfNewBody = TypeOfNewBody.Default;
        }
        UpdateSlotOfStep();
        //GameManager.Instance.characterManager.ResetDirect();

        NewGameManager.THIS.characterManager.ChangeStepEdit(typeOfNewBody);

        completeButton.gameObject.SetActive(typeOfNewBody == TypeOfNewBody.Shoe && NewGameManager.THIS.characterManager.isSelectedShoe);
        nextButton.gameObject.SetActive(stepDictionary.ContainsKey(typeOfNewBody) && completeButton.gameObject.activeSelf == false);
        if (typeOfNewBody == TypeOfNewBody.Default || typeOfNewBody == TypeOfNewBody.Shoe)
        {
            //textDragGuideObject.SetActive(false);
        }
        else
        {
            //textDragGuideObject.SetActive(true);
        }
        if (typeOfNewBody == TypeOfNewBody.Default)
        {
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
        }
        //ChangeCollorButton.gameObject.SetActive(false);
    }

    public void UpdateSlotOfStep()
    {
        //for (int i = 0; i < slotsOfStep.Count; i++)
        //{
        //    if (i < (int)typeOfNewBody)
        //    {
        //        slotsOfStep[i].SetState(StateSlotOfStep.Done);
        //    }
        //    else if (i == (int)typeOfBody)
        //    {
        //        slotsOfStep[i].SetState(StateSlotOfStep.Showing);
        //    }
        //    else
        //    {
        //        slotsOfStep[i].SetState(StateSlotOfStep.Pending);
        //    }
        //}
        UpdateDataItemOfSlot();
    }

   
}
