using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using System.IO;
[CreateAssetMenu(fileName = "AllSlotsInBodyData", menuName = "ScriptableObjects/All Slots In Body Data", order = 3)]
public class AllSlotsInBodyData : ScriptableObject
{
    [Header("Head Datas")]
    public List<SlotData> headSlots = new List<SlotData>();
    [Header("Eye Datas")]
    public List<SlotData> eyeSlots = new List<SlotData>();
    [Header("Mouth Datas")]
    public List<SlotData> mouthSlots = new List<SlotData>();
    [Header("Acc Datas")]
    public List<SlotData> accSlots = new List<SlotData>();
    [Header("Body Datas")]
    public List<SlotData> bodySlots = new List<SlotData>();

    [Header("Slots Sored In Body Datas")]
    [Space(30)]
    [ConditionalHide] public List<SlotData> headSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> eyeSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> mouthSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> accSlotsSorted = new List<SlotData>();
    [ConditionalHide] public List<SlotData> bodySlotsSorted = new List<SlotData>();

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

    public void SortAndUpdateStateAllSlots()
    {
        SortSlot(headSlots, out headSlotsSorted);
        SortSlot(eyeSlots, out eyeSlotsSorted);
        SortSlot(mouthSlots, out mouthSlotsSorted);
        SortSlot(accSlots, out accSlotsSorted);
        SortSlot(bodySlots, out bodySlotsSorted);
    }




    public void SortSlot(List<SlotData> listSlotIn, out List<SlotData> listSlotOut)
    {
        var listGet = listSlotIn.OrderBy((x) => x.indexSort);
        listSlotOut = new List<SlotData>(listGet.ToArray());
    }

#if UNITY_EDITOR

    [MenuItem("Custom Scriptable Asset/AllSlotsInBodyData", false, 3)]
    public static void OpenData()
    {
        var GO = AssetDatabase.LoadAssetAtPath<AllSlotsInBodyData>("Assets/_Datas/Datas/AllSlotsInBodyData.asset");
        Selection.activeObject = GO;
    }


    public int countScoreToExp = 10;
   
    private static string path = "Assets/_ConfigAllSlotsData/CSV/AllSlotDatas.csv";
    private static string pathSpriteHeads = "Assets/_Textures/Character/Head/Head.png";
    private static string pathSpriteHeads2 = "Assets/_Textures/Character/Head2/Head2.png";
    private static string pathSpriteHeads3 = "Assets/_Textures/Character/Head3/Head3.png";
    private static string pathSpriteEyes = "Assets/_Textures/Character/Eye/Eye.png";
    private static string pathSpriteMouths = "Assets/_Textures/Character/Mouth/Mouth.png";
    private static string pathSpriteAccs = "Assets/_Textures/Character/Acc/Acc.png";
    private static string pathSpriteBody = "Assets/_Textures/Character/BodyInMonster/BodyInMonster.png";

    //public void WriteCSV()
    //{
    //    using (var writer = new StreamWriter(path)) ;
    //}


    public void ImportCSV()
    {
        headSlots.Clear();
        eyeSlots.Clear();
        mouthSlots.Clear();
        accSlots.Clear();
        bodySlots.Clear();


        List<Sprite> spritesHeads = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHeads)
            .OfType<Sprite>().ToList();


        List<Sprite> spritesHeads2 = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHeads2)
            .OfType<Sprite>().ToList();
        List<Sprite> spritesHeads3 = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHeads3)
            .OfType<Sprite>().ToList();
        spritesHeads.AddRange(spritesHeads2);
        spritesHeads.AddRange(spritesHeads3);

        List<Sprite> spritesEyes = AssetDatabase.LoadAllAssetsAtPath(pathSpriteEyes)
            .OfType<Sprite>().ToList();
        List<Sprite> spritesMouths = AssetDatabase.LoadAllAssetsAtPath(pathSpriteMouths)
            .OfType<Sprite>().ToList();
        List<Sprite> spritesAccs = AssetDatabase.LoadAllAssetsAtPath(pathSpriteAccs)
            .OfType<Sprite>().ToList();


        List<Sprite> spritesBody = AssetDatabase.LoadAllAssetsAtPath(pathSpriteBody)
            .OfType<Sprite>().ToList();

        string[] allLines = File.ReadAllLines(path);
        bool isTitle = true;
        TypeOfBody typeOfBody = TypeOfBody.Head;
        foreach (string s in allLines)
        {
            if (isTitle)
            {
                isTitle = false;
                continue;
            }
            string[] splitData = s.Split(',');
            if (splitData.Length != 7)
            {
                Debug.Log(s + " does not have 7 values");
                return;
            }
            if (splitData[0] == "")
            {
                typeOfBody++;
                continue;
            }

            SlotData slotData = new SlotData();
            slotData.typeOfBody = typeOfBody;
            slotData.id = int.Parse(splitData[0]);
            slotData.name = splitData[2];
            slotData.skinSkeletonDataAsset = splitData[2];
            slotData.indexSort = int.Parse(splitData[3]);
            string stateOfSlotStr = splitData[5].Trim().ToLower();
            switch (stateOfSlotStr)
            {
                case "free":
                    slotData.stateOfSlot = StateOfSlot.Unlock;
                    break;
                case "ads":
                    slotData.stateOfSlot = StateOfSlot.Ads;
                    break;
                case "gold":
                    slotData.stateOfSlot = StateOfSlot.Gold;
                    slotData.priceGold = int.Parse(splitData[6]);
                    break;
                default:
                    break;
            }
            Sprite spriteFind = null;
            if (typeOfBody == TypeOfBody.Body)
            {
                var nameSlot = splitData[2];
                nameSlot = nameSlot.ToUpper();
                spriteFind = spritesBody.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfBody == TypeOfBody.Head)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesHeads.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfBody == TypeOfBody.Eye)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesEyes.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfBody == TypeOfBody.Mouth)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesMouths.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfBody == TypeOfBody.Acc)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesAccs.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            slotData.sprite = spriteFind;
            switch (typeOfBody)
            {
                case TypeOfBody.Head:
                    headSlots.Add(slotData);
                    break;
                case TypeOfBody.Eye:
                    eyeSlots.Add(slotData);
                    break;
                case TypeOfBody.Mouth:
                    mouthSlots.Add(slotData);
                    break;
                case TypeOfBody.Acc:
                    accSlots.Add(slotData);
                    break;
                case TypeOfBody.Body:
                    bodySlots.Add(slotData);
                    break;
                default:
                    break;
            }
        }
        SaveData();
    }
    void SaveData()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
#endif
}
    


public enum TypeOfBody
{
    Head,
    Eye,
    Mouth,
    Acc,
    Body
}
public enum StateOfSlot
{
    Unlock,
    Ads,
    Gold
}

[Serializable]
public struct SlotData
{
    public string name;
    public int id;
    public TypeOfBody typeOfBody;
    public int indexSort;
    public Sprite sprite;
    //[ConditionalHide] public SkeletonDataAsset skeletonDataAsset;
    //[ConditionalHide] public SkeletonDataAsset skeletonDataAsset;
    [ConditionalHide("typeOfBody", (int)TypeOfBody.Body)]
    //[SpineSkin(dataField: "skeletonDataAsset", defaultAsEmptyString: true)]
    public string skinSkeletonDataAsset;
    public StateOfSlot stateOfSlot;
    [ConditionalHide("stateOfSlot", (int)StateOfSlot.Gold)] public int priceGold;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SlotData))]
//[CanEditMultipleObjects]
public class SlotDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        var rectPro = rect;
        EditorGUI.PropertyField(rectPro, property, label, true);
        //if (GUI.changed)
        //{
        //    var skeletonDataAssetProp = property.FindPropertyRelative("skeletonDataAsset");
        //    if (skeletonDataAssetProp != null)
        //    {
        //        string path = "Assets/_Datas/Datas/AllSlotsInBodyData.asset";
        //        var myPrefab = (AllSlotsInBodyData)AssetDatabase.LoadMainAssetAtPath(path);
        //        skeletonDataAssetProp.objectReferenceValue = myPrefab.skeletonDataAsset;
        //    }
        //}

        if (property.isExpanded)//Có được mở rộng hay không
        {
            var spriteProp = property.FindPropertyRelative("sprite");

            Texture2D texture = AssetPreview.GetAssetPreview(spriteProp.objectReferenceValue);

            if (texture)
            {
                var rectGetted = rect;
                rectGetted.width = 50;
                rectGetted.height = 50;
                rectGetted.x = rect.xMax - rectGetted.width;
                EditorGUI.DrawTextureTransparent(rectGetted, texture, ScaleMode.ScaleToFit);
            }
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
}

#endif

#if UNITY_EDITOR
[CustomEditor(typeof(AllSlotsInBodyData), false)]
[CanEditMultipleObjects]
public class AllSlotsInBodyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AllSlotsInBodyData datas = (AllSlotsInBodyData)target;
        //if (GUILayout.Button("Write File CSV", GUILayout.Height(40)))
        //{
        //    datas.WriteCSV();
        //    EditorUtility.SetDirty(datas);
        //}
        //GUILayout.Space(20);
        if (GUILayout.Button("Import Data From CSV", GUILayout.Height(40)))
        {
            datas.ImportCSV();
            EditorUtility.SetDirty(datas);
        }
        GUILayout.Space(20);

        if (GUILayout.Button("Update Data Sort List", GUILayout.Height(40)))
        {
            datas.SortAndUpdateStateAllSlots();
            EditorUtility.SetDirty(datas);
        }
        GUILayout.Space(20);

        
        base.OnInspectorGUI();
    }
}
#endif