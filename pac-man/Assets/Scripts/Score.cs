using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;

    private List<ModelScore> modelScoreList;
    private List<Transform> modelScoreTransformList;

    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
        /*modelScoreList = new List<ModelScore>()
        {
            new ModelScore{ score = 1000, name = "Thay"},
            new ModelScore{ score = 2000, name = "Thay"},
            new ModelScore{ score = 3000, name = "Thay"},
            new ModelScore{ score = 10000, name = "Thay"},
            new ModelScore{ score = 8000, name = "Thay"},
            new ModelScore{ score = 6000, name = "Thay"},
            new ModelScore{ score = 4000, name = "Thay"},
            new ModelScore{ score = 5000, name = "Thay"},
            new ModelScore{ score = 7000, name = "Thay"},
            new ModelScore{ score = 9000, name = "Thay"},
        };*/

        string jsonString = PlayerPrefs.GetString("scoreTable");
        BestScores bestScores = JsonUtility.FromJson<BestScores>(jsonString);

        /*for (int i = 0; i < modelScoreList.Count; i++)
        {
            for (int j = i + 1; j < modelScoreList.Count; j++)
            {
                if(modelScoreList[j].score > modelScoreList[i].score)
                {
                    ModelScore model = modelScoreList[i];
                    modelScoreList[i] = modelScoreList[j];
                    modelScoreList[j] = model;
                }
            }
        }*/

        modelScoreTransformList = new List<Transform>();
        foreach(ModelScore modelScore in bestScores.modelScoreList)
        {
            CreateEntries(modelScore, entryContainer, modelScoreTransformList);
        }

        /*BestScores bestScores = new BestScores { modelScoreList = modelScoreList };
        string json = JsonUtility.ToJson(bestScores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();*/
    }

    private void CreateEntries(ModelScore modelScore, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -60 * transformList.Count + 320);
        entryTransform.gameObject.SetActive(true);

        int posRank = transformList.Count + 1;
        entryTransform.Find("Position").GetComponent<Text>().text = posRank + "º";
        entryTransform.Find("Score").GetComponent<Text>().text = modelScore.score.ToString();
        entryTransform.Find("Name").GetComponent<Text>().text = modelScore.name;
        transformList.Add(entryTransform);
    }

    private class BestScores
    {
        public List<ModelScore> modelScoreList;
    }

    [System.Serializable]
    private class ModelScore
    {
        public int score;
        public string name;
    }
}
