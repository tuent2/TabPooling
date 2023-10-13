using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer DefaultSR;
    public SpriteRenderer HairSR;
    public SpriteRenderer EyeSR;
    public SpriteRenderer DressSR;
    public SpriteRenderer ShoeSR;

    [ConditionalHide] public Transform objectDrag;
    [ConditionalHide] public bool canDragging;
    [ConditionalHide] public bool isSelectedShoe;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StepEdit(NewSlotData newSlotData)
    {
        //Debug.Log(newSlotData.typeOfNewBody);
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
                //HairSR.transform.localPosition = Vector2.zero;

                //HairSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                HairSR.transform.parent.localScale = new Vector2(HairSR.transform.parent.localScale.x, 0);
                HairSR.transform.parent.DOScaleY(1, .2f);

                //objectDrag = eyeSR.transform;
               // GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();

                break;
            case TypeOfNewBody.Eye:
                EyeSR.gameObject.SetActive(newSlotData.id != 0);
                EyeSR.sprite = newSlotData.sprite;
                //EyeSR.transform.localPosition = Vector2.zero;

                //mouthSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                EyeSR.transform.parent.localScale = new Vector2(EyeSR.transform.parent.localScale.x, 0);
                EyeSR.transform.parent.DOScaleY(1, .2f);

                //objectDrag = mouthSR.transform;
               // GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();
                break;
            case TypeOfNewBody.Dress:
                DressSR.gameObject.SetActive(newSlotData.id != 0);
                DressSR.sprite = newSlotData.sprite;
               // DressSR.transform.localPosition = Vector2.zero;

                //accSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                DressSR.transform.parent.localScale = new Vector2(DressSR.transform.parent.localScale.x, 0);
                DressSR.transform.parent.DOScaleY(1, .2f);

                //objectDrag = accSR.transform;
                //GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();
                break;
            case TypeOfNewBody.Shoe:
                ShoeSR.gameObject.SetActive(newSlotData.id != 0);
                ShoeSR.sprite = newSlotData.sprite;
                // DressSR.transform.localPosition = Vector2.zero;

                //accSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                ShoeSR.transform.parent.localScale = new Vector2(DressSR.transform.parent.localScale.x, 0);
                ShoeSR.transform.parent.DOScaleY(1, .2f);
                //bodyMR.enabled = true;
                //isSelectedBody = true;
                //if (slotData.skinSkeletonDataAsset == null)
                //{
                //    bodySA.skeleton.SetSkin(defaultSkin);

                //}
                //else
                //{
                //    bodySA.skeleton.SetSkin(slotData.skinSkeletonDataAsset);
                //}
                //bodySA.Skeleton.SetSlotsToSetupPose();
                //bodySA.transform.localPosition = Vector2.zero;

                objectDrag = null;
                break;
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

    public void HandleShowSlotOtherEdit()
    {
        transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), .2f).SetEase(Ease.Linear);
        transform.DOMoveY(-8.11f, .2f).SetEase(Ease.Linear);
        //bodyMR.enabled = false;
    }

    public void HandleShowBodyEdit()
    {
        transform.DOScale(new Vector3(.8f, .8f, .8f), .2f).SetEase(Ease.Linear);
        transform.DOMoveY(-1.48f, .2f).SetEase(Ease.Linear);
        //bodyMR.enabled = true;
    }

    public void ChangeStepEdit(TypeOfNewBody typeOfNewBody)
    {
        Debug.Log(typeOfNewBody);
        objectDrag = null;
        switch (typeOfNewBody)
        {
            case TypeOfNewBody.Default:
                if (DefaultSR.gameObject.activeSelf && DefaultSR.sprite != null)
                {
                    objectDrag = DefaultSR.transform;
                }
                canDragging = false;
               // HandleShowSlotOtherEdit();
                break;
            case TypeOfNewBody.Hair:
                if (HairSR.gameObject.activeSelf && HairSR.sprite != null)
                {
                    objectDrag = HairSR.transform;
                }
                canDragging = true;
               // HandleShowSlotOtherEdit();
                break;
            //case TypeOfNewBody.Mouth:
            //    if (mouthSR.gameObject.activeSelf && mouthSR.sprite != null)
            //    {
            //        objectDrag = mouthSR.transform;
            //    }
            //    canDragging = true;
            //    HandleShowSlotOtherEdit();
            //    break;
            //case TypeOfNewBody.Acc:
            //    if (accSR.gameObject.activeSelf && accSR.sprite != null)
            //    {
            //        objectDrag = accSR.transform;
            //    }
            //    canDragging = true;
            //    HandleShowSlotOtherEdit();
            //    break;
            //case TypeOfNewBody.Body:
            //    objectDrag = null;
            //    canDragging = false;
            //    HandleShowBodyEdit();
            //    break;
            default:
                break;
        }
        //GameManager.Instance.gameplayUI.SetActiveSizeObjectDragSlider(canDragging && objectDrag);
        //if (objectDrag)
        //{
        //    GameManager.Instance.gameplayUI.SetSizeObjectDragSlider(objectDrag.localScale.x);
        //}

    }
}
