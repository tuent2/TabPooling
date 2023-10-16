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
   
    [ConditionalHide] public GameObject objectDrag;
    [ConditionalHide] public bool canDragging;
    [ConditionalHide] public bool isSelectedShoe;
    
    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(canDragging);
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
                objectDrag = DefaultSR.gameObject;
                break;
            case TypeOfNewBody.Hair:
                HairSR.gameObject.SetActive(newSlotData.id != 0);
                HairSR.sprite = newSlotData.sprite;
                //HairSR.transform.localPosition = Vector2.zero;

                //HairSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                HairSR.transform.localScale = new Vector2(HairSR.transform.localScale.x, 0);
                HairSR.transform.DOScaleY(1, .2f);
                objectDrag = HairSR.gameObject;
                
                //objectDrag = eyeSR.transform;
                // GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();

                break;
            case TypeOfNewBody.Eye:
                EyeSR.gameObject.SetActive(newSlotData.id != 0);
                EyeSR.sprite = newSlotData.sprite;

                //EyeSR.transform.localPosition = Vector2.zero;

                //mouthSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;
             
                EyeSR.transform.localScale = new Vector2(EyeSR.transform.localScale.x, 0);
                EyeSR.transform.DOScaleY(1, .2f);
                objectDrag = EyeSR.gameObject;
                //objectDrag = mouthSR.transform;
                // GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();
                break;
            case TypeOfNewBody.Dress:
                DressSR.gameObject.SetActive(newSlotData.id != 0);
                DressSR.sprite = newSlotData.sprite;
                // DressSR.transform.localPosition = Vector2.zero;
               
                //accSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                DressSR.transform.localScale = new Vector2(DressSR.transform.localScale.x, 0);
                DressSR.transform.DOScaleY(1, .2f);

                objectDrag = DressSR.gameObject;
                //GameManager.Instance.gameplayController.UpdatePosObjectFirstSet();
                break;
            case TypeOfNewBody.Shoe:
                ShoeSR.gameObject.SetActive(newSlotData.id != 0);
                ShoeSR.sprite = newSlotData.sprite;
                // DressSR.transform.localPosition = Vector2.zero;
         
                //accSR.transform.parent.localScale = new Vector3(numberDirection, 1, 1);
                //numberDirection *= -1;

                ShoeSR.transform.localScale = new Vector2(DressSR.transform.localScale.x, 0);
                ShoeSR.transform.DOScaleY(1, .2f);
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
        transform.DOScale(new Vector3(3f, 3f, 3f), .2f).SetEase(Ease.Linear);
        transform.DOMove(new Vector2(3.39f, -17.36f), .2f).SetEase(Ease.Linear);
        //bodyMR.enabled = false;
    }

    public void HandleShowBodyEdit()
    {
        transform.DOScale(new Vector3(1f, 1f, .1f), .2f).SetEase(Ease.Linear);
        transform.DOMove(new Vector2(1.132354f, -3.74366f), .2f).SetEase(Ease.Linear);
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
                    objectDrag = DefaultSR.gameObject;
                }
                canDragging = false;
                HandleShowBodyEdit();
                break;
            case TypeOfNewBody.Hair:
                if (HairSR.gameObject.activeSelf && HairSR.sprite != null)
                {
                    objectDrag = HairSR.gameObject;
                }
                canDragging = false;
                HandleShowSlotOtherEdit();
                break;
            case TypeOfNewBody.Eye:
                if (EyeSR.gameObject.activeSelf && EyeSR.sprite != null)
                {
                    objectDrag = EyeSR.gameObject;
                }
                canDragging = false;
                HandleShowSlotOtherEdit();
                break;
            case TypeOfNewBody.Dress:
                if (DressSR.gameObject.activeSelf && DressSR.sprite != null)
                {
                    objectDrag = DressSR.gameObject;
                }
                canDragging = false;
                HandleShowBodyEdit();
                break;
            case TypeOfNewBody.Shoe:
                objectDrag = null;
                canDragging = false;
                HandleShowBodyEdit();
                break;
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
