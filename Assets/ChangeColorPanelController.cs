using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeColorPanelController : MonoBehaviour
{
    Color currentColor;
    float rValue;
    float gValue;
    float bValue;
    [SerializeField] Slider rSlider;
    [SerializeField] Slider gSlider;
    [SerializeField] Slider bSlider;
    void Start()
    {
        rSlider.onValueChanged.AddListener(ClickUpteColor);
        gSlider.onValueChanged.AddListener(ClickUpteColor);
        bSlider.onValueChanged.AddListener(ClickUpteColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOpenPanelDefaults(GameObject dragObject)
    {
        currentColor = dragObject.GetComponent<SpriteRenderer>().color;
        rValue = currentColor.r;
        gValue = currentColor.g;
        bValue = currentColor.b;
        Debug.Log(rValue);
        Debug.Log(gValue);
        Debug.Log(bValue);

        rSlider.value = rValue;
        gSlider.value = gValue;
        bSlider.value = bValue;
        NewGameManager.THIS.characterManager.canDragging = true;
    }



    public void ClickUpteColor(float value)
    {
        if (NewGameManager.THIS.characterManager.objectDrag && NewGameManager.THIS.characterManager.canDragging == true)
        {
            Debug.Log("123");
            rValue = rSlider.value;
            gValue = gSlider.value;
            bValue = bSlider.value;
            NewGameManager.THIS.characterManager.objectDrag.GetComponent<SpriteRenderer>().color =  new Color(rValue, gValue, bValue);
            
        }
            
    }
    
}
