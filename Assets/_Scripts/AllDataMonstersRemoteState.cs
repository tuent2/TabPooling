using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllDataMonstersRemoteState : MonoBehaviour
{
    [ConditionalHide] public List<SlotData> headSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> eyeSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> mouthSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> accSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> bodySlotsSorted = new List<SlotData>();
    public void HandleDatasAllWithRemoteConfig()
    {
        HandleDataWithRemoteConfig(DataGame.idHeadsHide_FBRemote, GameManager.THIS.allSlotsInBodyData.headSlotsSorted, ref headSlotsSorted);
        HandleDataWithRemoteConfig(DataGame.idEyesHide_FBRemote, GameManager.THIS.allSlotsInBodyData.eyeSlotsSorted, ref eyeSlotsSorted);
        HandleDataWithRemoteConfig(DataGame.idMouthsHide_FBRemote, GameManager.THIS.allSlotsInBodyData.mouthSlotsSorted, ref mouthSlotsSorted);
        HandleDataWithRemoteConfig(DataGame.idAccsHide_FBRemote, GameManager.THIS.allSlotsInBodyData.accSlotsSorted, ref accSlotsSorted);
        HandleDataWithRemoteConfig(DataGame.idBodysHide_FBRemote, GameManager.THIS.allSlotsInBodyData.bodySlotsSorted, ref bodySlotsSorted);
    }
    void HandleDataWithRemoteConfig(string idPartsHide_RemoteConfig, List<SlotData> slotDatasListImport, ref List<SlotData> slotDatasListResult)
    {
        try
        {
            string idPartsHide_FBRemote = PlayerPrefs.GetString(idPartsHide_RemoteConfig, "");
            //Debug.Log(idPartsHide_FBRemote);
            string[] idPartsHide = idPartsHide_FBRemote.Split(',');
            //Debug.Log(idPartsHide);
            var idPartsHideList = idPartsHide.ToList();
            for (int i = 0; i < slotDatasListImport.Count; i++)
            {
                bool isSlotHided = false;
                for (int j = 0; j < idPartsHideList.Count; j++)
                {
                    if (!int.TryParse(idPartsHideList[j], out int id))
                    {
                        id = 0;
                    }
                    if (slotDatasListImport[i].id == id)
                    {
                        idPartsHideList.RemoveAt(j);
                        isSlotHided = true;
                        break;
                    }
                }
                if (!isSlotHided)
                {
                    slotDatasListResult.Add(slotDatasListImport[i]);
                }
            }
        }
        catch (System.Exception)
        {
            slotDatasListResult = new List<SlotData>(slotDatasListImport);
            Debug.Log("<color=orange>Remote Config sai" + slotDatasListImport[0].typeOfBody.ToString() + "</color>");
        }
    }

    public List<SlotData> GetSlotDatas(TypeOfBody typeOfBody)
    {
        List<SlotData> slotDatas = new List<SlotData>();
        switch (typeOfBody)
        {
            case TypeOfBody.Head:
                slotDatas = headSlotsSorted;
                break;
            case TypeOfBody.Eye:
                slotDatas = eyeSlotsSorted;
                break;
            case TypeOfBody.Mouth:
                slotDatas = mouthSlotsSorted;
                break;
            case TypeOfBody.Acc:
                slotDatas = accSlotsSorted;
                break;
            case TypeOfBody.Body:
                slotDatas = bodySlotsSorted;
                break;
            default:
                break;
        }
        return slotDatas;
    }
    public SlotData GetSlotDataById(int id, TypeOfBody typeOfBody)
    {
        var slotDatas = GetSlotDatas(typeOfBody);
        SlotData slotData = slotDatas.Find(x => x.id == id);
        return slotData;
    }
}
