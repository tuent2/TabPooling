﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using System.IO;
[CreateAssetMenu(fileName = "NewSlotInBodyData", menuName = "ScriptableObjects/New Slots In Body Data", order = 3)]
public class NewSlotInBodyData : ScriptableObject
{
    [Header("Default Datas")]
    public List<NewSlotData> defaultSlots = new List<NewSlotData>();
    [Header("Hair Datas")]
    public List<NewSlotData> hairSlots = new List<NewSlotData>();
    [Header("Eye Datas")]
    public List<NewSlotData> eyeSlots = new List<NewSlotData>();
    [Header("Dress Datas")]
    public List<NewSlotData> dressSlots = new List<NewSlotData>();
    [Header("Shoe Datas")]
    public List<NewSlotData> shoeSlots = new List<NewSlotData>();


    [Header("Slots Sored In Body Datas")]
    [Space(30)]
    [ConditionalHide] public List<NewSlotData> defaultSlotsSorted = new List<NewSlotData>();
    [ConditionalHide] public List<NewSlotData> hairSlotsSorted = new List<NewSlotData>();
    [ConditionalHide] public List<NewSlotData> eyeSlotsSorted = new List<NewSlotData>();
    [ConditionalHide] public List<NewSlotData> dressSlotsSorted = new List<NewSlotData>();
    [ConditionalHide] public List<NewSlotData> shoeSlotsSorted = new List<NewSlotData>();

    

    public List<NewSlotData> GetSlotDatas(TypeOfNewBody typeOfNewBody)
    {
        List<NewSlotData> newSlotDatas = new List<NewSlotData>();
        switch (typeOfNewBody)
        {
            case TypeOfNewBody.Default:
                newSlotDatas = defaultSlotsSorted;
                break;
            case TypeOfNewBody.Hair:
                newSlotDatas = hairSlotsSorted;
                break;
            case TypeOfNewBody.Eye:
                newSlotDatas = eyeSlotsSorted;
                break;
            case TypeOfNewBody.Dress:
                newSlotDatas = dressSlotsSorted;
                break;
            case TypeOfNewBody.Shoe:
                newSlotDatas = shoeSlotsSorted;
                break;
            
            default:
                break;
        }
        return newSlotDatas;
    }
    public NewSlotData GetSlotDataById(int id, TypeOfNewBody typeOfNewBody)
    {
        var slotDatas = GetSlotDatas(typeOfNewBody);
        NewSlotData slotData = slotDatas.Find(x => x.id == id);
        return slotData;
    }

    public void SortAndUpdateStateAllSlots()
    {
        NewSortSlot(defaultSlots, out defaultSlotsSorted);
        NewSortSlot(hairSlots, out hairSlotsSorted);
        NewSortSlot(eyeSlots, out eyeSlotsSorted);
        NewSortSlot(dressSlots, out dressSlotsSorted);
        NewSortSlot(shoeSlots, out shoeSlotsSorted);
       
    }




    public void NewSortSlot(List<NewSlotData> listSlotIn, out List<NewSlotData> listSlotOut)
    {
        var listGet = listSlotIn.OrderBy((x) => x.indexSort);
        listSlotOut = new List<NewSlotData>(listGet.ToArray());
    }

#if UNITY_EDITOR

    [MenuItem("Custom Scriptable Asset/NewSlotsInBodyData", false, 3)]
    public static void OpenData()
    {
        var GO = AssetDatabase.LoadAssetAtPath<AllSlotsInBodyData>("Assets/_Datas/Datas/NewSlotsInBodyData.asset");
        Selection.activeObject = GO;
    }


    public int countScoreToExp = 10;

    private static string path = "Assets/_ConfigAllSlotsData/CSV/AllSlotDatas1.csv";
    private static string pathSpriteHairs = "Assets/_Textures/Character1/hair/Hair.png";
    //private static string pathSpriteHeads2 = "Assets/_Textures/Character/Head2/Head2.png";
    //private static string pathSpriteHeads3 = "Assets/_Textures/Character/Head3/Head3.png";
    private static string pathSpriteEyes = "Assets/_Textures/Character1/eyes/Eye.png";
    private static string pathSpriteDress = "Assets/_Textures/Character1/dress/Dress.png";
    private static string pathSpriteShoes = "Assets/_Textures/Character1/shoe/Shoe.png";
    private static string pathSpriteDefault = "Assets/_Textures/Character1/default/Default.png";

    //public void WriteCSV()
    //{
    //    using (var writer = new StreamWriter(path)) ;
    //}


    public void ImportCSV()
    {
        defaultSlots.Clear();
        hairSlots.Clear();
        eyeSlots.Clear();
        dressSlots.Clear();
        shoeSlots.Clear();

        Debug.Log(hairSlots.Count);
        List<Sprite> spritesDefault = AssetDatabase.LoadAllAssetsAtPath(pathSpriteDefault)
            .OfType<Sprite>().ToList();
        List<Sprite> spritesHairs = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHairs)
            .OfType<Sprite>().ToList();

        Debug.Log(spritesHairs.Count);

        //List<Sprite> spritesHeads2 = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHeads2)
        //    .OfType<Sprite>().ToList();
        //List<Sprite> spritesHeads3 = AssetDatabase.LoadAllAssetsAtPath(pathSpriteHeads3)
        //    .OfType<Sprite>().ToList();
        //spritesHeads.AddRange(spritesHeads2);
        //spritesHeads.AddRange(spritesHeads3);

        List<Sprite> spritesEyes = AssetDatabase.LoadAllAssetsAtPath(pathSpriteEyes)
            .OfType<Sprite>().ToList();
        List<Sprite> spritesDress = AssetDatabase.LoadAllAssetsAtPath(pathSpriteDress)
            .OfType<Sprite>().ToList();
        List<Sprite> spritesShoes = AssetDatabase.LoadAllAssetsAtPath(pathSpriteShoes)
            .OfType<Sprite>().ToList();




        string[] allLines = File.ReadAllLines(path);
        bool isTitle = true;
        TypeOfNewBody typeOfNewBody = TypeOfNewBody.Default;
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
                typeOfNewBody++;
                continue;
            }

            NewSlotData newSlotData = new NewSlotData();
            newSlotData.typeOfNewBody = typeOfNewBody;
            newSlotData.id = int.Parse(splitData[0]);
            newSlotData.name = splitData[2];
            //NewSlotData.skinSkeletonDataAsset = splitData[2];
            newSlotData.indexSort = int.Parse(splitData[3]);
            string stateOfSlotStr = splitData[5].Trim().ToLower();
            switch (stateOfSlotStr)
            {
                case "free":
                    newSlotData.stateOfNewSlot = StateOfNewSlot.Unlock;
                    break;
                case "ads":
                    newSlotData.stateOfNewSlot = StateOfNewSlot.Ads;
                    break;
                case "gold":
                    newSlotData.stateOfNewSlot = StateOfNewSlot.Gold;
                    newSlotData.priceGold = int.Parse(splitData[6]);
                    break;
                default:
                    break;
            }
            Sprite spriteFind = null;
            if (typeOfNewBody == TypeOfNewBody.Default)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesDefault.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfNewBody == TypeOfNewBody.Hair)
            {
                var nameSlot = splitData[2];
                nameSlot = nameSlot.ToUpper();
                spriteFind = spritesHairs.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfNewBody == TypeOfNewBody.Eye)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesEyes.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfNewBody == TypeOfNewBody.Dress)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesDress.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }
            else if (typeOfNewBody == TypeOfNewBody.Shoe)
            {
                var nameSlot = splitData[2].ToUpper();
                spriteFind = spritesShoes.Find(x => x.name.ToUpper() == nameSlot);
                if (spriteFind == null)
                {
                    Debug.LogError("Miss: " + nameSlot);
                }
            }


            newSlotData.sprite = spriteFind;
            switch (typeOfNewBody)
            {
                case TypeOfNewBody.Default:
                    defaultSlots.Add(newSlotData);
                    break;
                case TypeOfNewBody.Hair:
                    hairSlots.Add(newSlotData);
                    break;
                case TypeOfNewBody.Eye:
                    eyeSlots.Add(newSlotData);
                    break;
                case TypeOfNewBody.Dress:
                    dressSlots.Add(newSlotData);
                    break;
                case TypeOfNewBody.Shoe:
                    shoeSlots.Add(newSlotData);
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



public enum TypeOfNewBody
{
    Default,
    Hair,
    Eye,
    Dress,
    Shoe,
    
}
public enum StateOfNewSlot
{
    Unlock,
    Ads,
    Gold
}

[Serializable]
public struct NewSlotData
{
    public string name;
    public int id;
    public TypeOfNewBody typeOfNewBody;
    public int indexSort;
    public Sprite sprite;
    //[ConditionalHide] public SkeletonDataAsset skeletonDataAsset;
    //[ConditionalHide] public SkeletonDataAsset skeletonDataAsset;
    //[ConditionalHide("typeOfBody", (int)TypeOfNewBody.Body)]
    ////[SpineSkin(dataField: "skeletonDataAsset", defaultAsEmptyString: true)]
    //public string skinSkeletonDataAsset;
    public StateOfNewSlot stateOfNewSlot;
    [ConditionalHide("stateOfNewSlot", (int)StateOfNewSlot.Gold)] public int priceGold;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NewSlotData))]
//[CanEditMultipleObjects]
public class NewSlotDataDrawer : PropertyDrawer
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
[CustomEditor(typeof(NewSlotInBodyData), false)]
[CanEditMultipleObjects]
public class NewAllSlotsInBodyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NewSlotInBodyData datas = (NewSlotInBodyData)target;
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