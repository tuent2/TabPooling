using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        UpdateDataItemOfSlot();
    }

    [SerializeField] ItemOfSlot itemOfSlotPrefab;
    [SerializeField] Sprite noneItemOfSlotSprite;
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] Button completeButton;
    [ConditionalHide] public TypeOfBody typeOfBody;
    List<ItemOfSlot> itemOfSlotsPooling = new List<ItemOfSlot>();
    List<ItemOfSlot> itemOfSlotsCurrent = new List<ItemOfSlot>();
    Dictionary<TypeOfBody, int> stepDictionary = new Dictionary<TypeOfBody, int>();
    ItemOfSlot itemOfSlotSave;
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
        ItemOfSlot itemOfSlot;
        for (int i = itemOfSlotsCurrent.Count - 1; i >= 0; i--)
        {
            itemOfSlot = itemOfSlotsCurrent[i];
            itemOfSlot.gameObject.SetActive(false);
            itemOfSlotsPooling.Add(itemOfSlot);
            itemOfSlotsCurrent.RemoveAt(i);
        }
        yield return new WaitForSeconds(.01f);
        SlotData slotData;
        itemOfSlotSave = null;
        if (typeOfBody != TypeOfBody.Head && typeOfBody != TypeOfBody.Body)
        {
            slotData = new SlotData();
            slotData.typeOfBody = typeOfBody;
            slotData.sprite = noneItemOfSlotSprite;
            ItemOfSlot itemOfSlotNone = GetItemOfSlot();
            itemOfSlotNone.SetData(slotData, delegate (SlotData slotDataCallback)
            {
                if (itemOfSlotSave == null || slotDataCallback.id != itemOfSlotSave.slotData.id)
                {
                    //GameManager.Instance.characterManager.ResetDirect();

                    if (itemOfSlotSave)
                        itemOfSlotSave.UnPickItem();
                    itemOfSlotSave = itemOfSlotNone;
                }
                itemOfSlotNone.PickItem();
               // GameManager.Instance.characterManager.StepEdit(slotDataCallback); chinh mat nhan vat
                if (itemOfSlotNone.slotData.typeOfBody != TypeOfBody.Body)
                {
                    if (nextButton.gameObject.activeSelf == false)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (completeButton.gameObject.activeSelf == false)
                    {
                        completeButton.gameObject.SetActive(true);
                    }
                }

                if (stepDictionary.ContainsKey(typeOfBody) == false)
                {
                    stepDictionary.Add(typeOfBody, itemOfSlotSave.slotData.id);
                }
                else
                {
                    stepDictionary[typeOfBody] = itemOfSlotSave.slotData.id;
                }
            });
            if (itemOfSlotSave == null && stepDictionary.TryGetValue(typeOfBody, out int indexItemSave)) 
           
            {
                itemOfSlotSave = itemOfSlotNone.slotData.id == indexItemSave ? itemOfSlotNone : null;
                if (itemOfSlotSave)
                {
                    //itemOfSlotSave.slotData = itemOfSlotNone.slotData;
                    itemOfSlotSave.PickItem();
                }
            }
            //itemOfSlotShowing.Add(itemOfSlotNone);
            yield return new WaitForSeconds(.02f);
        }

        List<SlotData> slotDataTypeCurrent = GameManager.THIS.allDataMonstersRemoteState.GetSlotDatas(typeOfBody);
        for (int i = 0; i < slotDataTypeCurrent.Count; i++)
        {
            slotData = slotDataTypeCurrent[i];
            ItemOfSlot itemOfSlotSelect = GetItemOfSlot();
            itemOfSlotSelect.SetData(slotData, delegate (SlotData slotDataCallback)
            {
                if (itemOfSlotSave == null || slotDataCallback.id != itemOfSlotSave.slotData.id)
                {
                    //GameManager.Instance.characterManager.ResetDirect();

                    if (itemOfSlotSave)
                        itemOfSlotSave.UnPickItem();
                    itemOfSlotSave = itemOfSlotSelect;
                }
                itemOfSlotSelect.PickItem();
               // GameManager.Instance.characterManager.StepEdit(slotDataCallback);

                if (itemOfSlotSelect.slotData.typeOfBody != TypeOfBody.Body)
                {
                    if (nextButton.gameObject.activeSelf == false)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (completeButton.gameObject.activeSelf == false)
                    {
                        completeButton.gameObject.SetActive(true);
                    }
                }
                
                if (stepDictionary.ContainsKey(typeOfBody) == false)
                {
                    stepDictionary.Add(typeOfBody, itemOfSlotSave.slotData.id);
                }
                else
                {
                    stepDictionary[typeOfBody] = itemOfSlotSave.slotData.id;
                }
            });
            if (itemOfSlotSave == null && stepDictionary.TryGetValue(typeOfBody, out int indexItemSave)) 
        
            {
                itemOfSlotSave = itemOfSlotSelect.slotData.id == indexItemSave ? itemOfSlotSelect : null;
                if (itemOfSlotSave)
                {
                    //itemOfSlotSave.slotData = itemOfSlotSelect.slotData;
                    itemOfSlotSave.PickItem();
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

    ItemOfSlot GetItemOfSlot()
    {
        ItemOfSlot itemOfSlot = null;
        for (int i = itemOfSlotsPooling.Count - 1; i >= 0; i--)
        {
            if (itemOfSlotsPooling[i].gameObject.activeSelf == false)
            {
                itemOfSlot = itemOfSlotsPooling[i];
                itemOfSlotsPooling.RemoveAt(i);
                break;
            }
        }
        if (!itemOfSlot)
        {
            if (itemOfSlotPrefab.gameObject.activeSelf)
            {
                itemOfSlotPrefab.gameObject.SetActive(false);
            }
            itemOfSlot = Instantiate(itemOfSlotPrefab, itemOfSlotPrefab.transform.parent);
        }
        itemOfSlotsCurrent.Add(itemOfSlot);

        itemOfSlot.gameObject.SetActive(true);
        return itemOfSlot;
    }
}
