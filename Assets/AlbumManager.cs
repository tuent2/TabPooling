using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using DG.Tweening;
using Lean.Common;
using Lean.Touch;
public class AlbumManager : MonoBehaviour
{
    public GameObject containerObject;
    public Transform bgAlbumObject;

    List<MonsterData> monsterDataListInAblum = new List<MonsterData>();
    List<MonsterData> monsterDataListDontInAlbum = new List<MonsterData>();
    [ConditionalHide] public List<CharacterManager> characterManagerInAlbum = new List<CharacterManager>();
    [SerializeField] CharactersAlbumPooling characterManagerPooling;
    Camera cameraMain;
    private void Start()
    {
        for (int i = 0; i < monsterDataListInAblum.Count; i++)
        {
            var character = AddMonster(monsterDataListInAblum[i]);
            character.transform.localPosition = monsterDataListInAblum[i].posDropedInAlbum;
        }

        int countMonsterOnAlbum = characterManagerInAlbum.Count;
        //GameManager.Instance.albumUI.ShowValueMonsterInAlbum(countMonsterOnAlbum.ToString("00") + "/" + countMonsterOnAlbumMax.ToString("00"));

        UpdateStateAlbum();


        if (cameraMain == null)
        {
            cameraMain = Camera.main;
        }
    }

    public void UpdateStateAlbum()
    {
        //HandleMonsterDontInAlbum();
        //UpdateScoresAlbum();
        //UpdateCrownCharactersAlbum();
    }
    



    public void Show()
    {
        containerObject.SetActive(true);
        
        if (PlayerPrefs.GetInt(DataGame.isReviewedAlbum, 0) == 0)
        {
            ReviewAlbum();
        }
        else
        {
            NewGameManager.THIS.albumUI.Show();
            SetStateControllInAlbum(true);
        }
    }

    public void SetStateControllInAlbum(bool isState)
    {
        //isStateControllAlbum = isState;
        //leanTouchObject.SetActive(isState);
        //pressToSelectObject.SetActive(isState);
        //leanDragTranslateBG.enabled = isState;
        //leanPinchScaleBG.enabled = isState;
    }

    void ReviewAlbum()
    {
        //GameManager.Instance.ignoreRaycastUI.SetActive(true);
        NewGameManager.THIS.albumManager.SetStateControllInAlbum(false);

        //var leanConstrainScale = bgAlbumObject.GetComponent<LeanConstrainScale>();
        //leanConstrainScale.enabled = false;

        //bgAlbumObject.localScale = Vector3.one * .4f;
        //DOTween.Sequence()
        //    .AppendInterval(.5f)
        //    .Append(bgAlbumObject.DOScale(.97f, 1).SetEase(Ease.Linear))
        //    .Join(bgAlbumObject.DOLocalMove(new Vector2(0.072f, -5.1f), 1).SetEase(Ease.Linear))
        //    .AppendCallback(() =>
        //    {
        //        //GameManager.Instance.ignoreRaycastUI.SetActive(false);
        //       // GameManager.Instance.albumManager.SetStateControllInAlbum(true);

        //       // leanConstrainScale.enabled = true;

        //        PlayerPrefs.SetInt(DataGame.isReviewedAlbum, 1);
        //        PlayerPrefs.Save();

        //        NewGameManager.THIS.albumUI.Show();
        //    });


        PlayerPrefs.SetInt(DataGame.isReviewedAlbum, 1);
        PlayerPrefs.Save();

        NewGameManager.THIS.albumUI.Show();
    }

    CharacterManager AddMonster(MonsterData monsterData)
    {
        var characterManager = characterManagerPooling.GetObjectInPooling();
        characterManager.HandleShowAlbum(monsterData);
        //var textScore = characterManager.GetComponent<TextScoreMonster>();
        //textScore.SetData(characterManager.monsterData.scoreMonster);
        //textScoreMonsters.Add(textScore);
        //textScore.SetState(AlbumUI.isStateScores);
        characterManagerInAlbum.Add(characterManager);
        return characterManager;
    }


}

[Serializable]
public class BodyPart
{
    public TypeOfNewBody typeOfNewBody;
    public int idTypeOfBody;
    public Vector2 posSet;
    public float scale;
    public bool isDirectRight;
    public BodyPart()
    {
        this.typeOfNewBody = TypeOfNewBody.Default;
        this.idTypeOfBody = 0;
        this.posSet = Vector2.zero;
        this.scale = 1;
        this.isDirectRight = true;
    }
    public BodyPart(TypeOfNewBody typeOfNewBody, int idTypeOfBody, Vector2 posSet, float scale = 1, bool isFlip = false)
    {
        this.typeOfNewBody = typeOfNewBody;
        this.idTypeOfBody = idTypeOfBody;
        this.posSet = posSet;
        this.scale = scale;
        this.isDirectRight = isFlip;
    }
    public void SetData(TypeOfNewBody typeOfNewBody, int idTypeOfBody, Vector2 posSet, float scale = 1, bool isFlip = false)
    {
        this.typeOfNewBody = typeOfNewBody;
        this.idTypeOfBody = idTypeOfBody;
        this.posSet = posSet;
        this.scale = scale;
        this.isDirectRight = isFlip;
    }
}

[Serializable]
public class MonsterData
{
    [ConditionalHide] public string id;
    [ConditionalHide] public BodyPart defaultBP;
    [ConditionalHide] public BodyPart hairBP;
    [ConditionalHide] public BodyPart eyeBP;
    [ConditionalHide] public BodyPart dressBP;
    [ConditionalHide] public BodyPart shoeBP;
    [ConditionalHide] public int scoreMonster;
    [ConditionalHide] public bool isDropedInAlbum;
    [ConditionalHide] public Vector2 posDropedInAlbum;
    public MonsterData()
    {
        defaultBP = new BodyPart();
        hairBP = new BodyPart();
        eyeBP = new BodyPart();
        dressBP = new BodyPart();
        shoeBP = new BodyPart();
    }
    public void UpdatePosDropedInAlbum(Vector2 pos)
    {
        isDropedInAlbum = true;
        posDropedInAlbum = pos;
    }
}
[Serializable]
public class MonstersData
{
    [ConditionalHide] public List<MonsterData> monstersDataMaked;
    public static MonstersData ReadFile(string path)
    {
        // Does the file exist?
        if (File.Exists(path))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(path);
#if !UNITY_EDITOR
            string jsonString = DecodeString(fileContents);
#else
            string jsonString = fileContents;
#endif
            MonstersData monstersData = JsonUtility.FromJson<MonstersData>(jsonString);
            // Work with JSON
            return monstersData;
        }
        else
        {
            return null;
        }
    }

    public static void WriteFile(MonstersData monstersData)
    {
        string saveFile = DataGame.pathDataSave;
        string jsonString = JsonUtility.ToJson(monstersData);
#if !UNITY_EDITOR
        string json = EncodeString(jsonString);
#else
        string json = jsonString;
#endif
        // Work with JSON

        // Write JSON to file.
        File.WriteAllText(saveFile, json);
    }
    public static string EncodeString(string value)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        return encodedText;
    }
    public static string DecodeString(string encodedText)
    {
        byte[] decodedBytes = Convert.FromBase64String(encodedText);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        return decodedText;
    }
}
