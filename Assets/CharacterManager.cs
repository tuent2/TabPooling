using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer DefaultSR;
    public SpriteRenderer HairSR;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StepEdit(NewSlotData newSlotData)
    {
        Debug.Log(newSlotData.typeOfNewBody);
        switch (newSlotData.typeOfNewBody)
        {   
            
            case TypeOfNewBody.Default:
                
                gameObject.SetActive(true);
                DefaultSR.gameObject.SetActive(newSlotData.id != 0);
                DefaultSR.sprite = newSlotData.sprite;
               // DefaultSR.transform.localPosition = Vector2.zero;
                DefaultSR.transform.parent.localScale = new Vector2(DefaultSR.transform.localScale.x, 0);
                DefaultSR.transform.parent.DOScaleY(1, .2f);
                
                break;
            case TypeOfNewBody.Hair:
                HairSR.gameObject.SetActive(newSlotData.id != 0);
                HairSR.sprite = newSlotData.sprite;
                HairSR.transform.localPosition = Vector2.zero;

                //HairSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                HairSR.transform.parent.localScale = new Vector2(HairSR.transform.parent.localScale.x, 0);
                HairSR.transform.parent.DOScaleY(1, .2f);

                //objectDrag = eyeSR.transform;
               // GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();

                break;
            //case TypeOfNewBody.Mouth:
            //    mouthSR.gameObject.SetActive(slotData.id != 0);
            //    mouthSR.sprite = slotData.sprite;
            //    mouthSR.transform.localPosition = Vector2.zero;

            //    mouthSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
            //    numberDirection *= -1;

            //    mouthSR.transform.parent.localScale = new Vector2(mouthSR.transform.parent.localScale.x, 0);
            //    mouthSR.transform.parent.DOScaleY(1, .2f);

            //    objectDrag = mouthSR.transform;
            //    GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();
            //    break;
            //case TypeOfNewBody.Acc:
            //    accSR.gameObject.SetActive(slotData.id != 0);
            //    accSR.sprite = slotData.sprite;
            //    accSR.transform.localPosition = Vector2.zero;

            //    accSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
            //    numberDirection *= -1;

            //    accSR.transform.parent.localScale = new Vector2(accSR.transform.parent.localScale.x, 0);
            //    accSR.transform.parent.DOScaleY(1, .2f);

            //    objectDrag = accSR.transform;
            //    GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();
            //    break;
            //case TypeOfNewBody.Body:
            //    bodyMR.enabled = true;
            //    isSelectedBody = true;
            //    if (slotData.skinSkeletonDataAsset == null)
            //    {
            //        bodySA.skeleton.SetSkin(defaultSkin);

            //    }
            //    else
            //    {
            //        bodySA.skeleton.SetSkin(slotData.skinSkeletonDataAsset);
            //    }
            //    bodySA.Skeleton.SetSlotsToSetupPose();
            //    bodySA.transform.localPosition = Vector2.zero;

            //    objectDrag = null;
            //    break;
            default:
                break;
        }

        //GameManager.Instance.gameplayUI.SetActiveSizeObjectDragSlider(canDragging && objectDrag && slotData.id != 0);
        //if (objectDrag)
        //{
        //    //GameManager.Instance.gameplayUI.SetSizeObjectDragSlider(objectDrag.localScale.x);
        //    objectDrag.localScale = Vector3.one;
        //    GameManager.Instance.gameplayUI.ResetSizeObjectDragSlider();
        //}
    }
}
