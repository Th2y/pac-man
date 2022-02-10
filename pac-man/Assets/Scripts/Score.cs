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

        if (!PlayerPrefs.HasKey("scoreTable"))
        {
            PopulateEmptyList();
        }

        string jsonString = PlayerPrefs.GetString("scoreTable");
        BestScores bestScores = JsonUtility.FromJson<BestScores>(jsonString);

        modelScoreTransformList = new List<Transform>();
        foreach(ModelScore modelScore in bestScores.modelScoreList)
        {
            CreateEntries(modelScore, entryContainer, modelScoreTransformList);
        }
    }

    private void PopulateEmptyList()
    {
        modelScoreList = new List<ModelScore>()
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
        };
        BestScores bestScores = new BestScores { modelScoreList = modelScoreList };
        bestScores = OrdenateList(bestScores);
        string json = JsonUtility.ToJson(bestScores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    private BestScores OrdenateList(BestScores bestScores)
    {
        for (int i = 0; i < bestScores.modelScoreList.Count; i++)
        {
            for (int j = i + 1; j < bestScores.modelScoreList.Count; j++)
            {
                if (bestScores.modelScoreList[j].score > bestScores.modelScoreList[i].score)
                {
                    ModelScore model = bestScores.modelScoreList[i];
                    bestScores.modelScoreList[i] = bestScores.modelScoreList[j];
                    bestScores.modelScoreList[j] = model;
                }
            }
        }

        if (bestScores.modelScoreList.Count > 10)
        {
            bestScores.modelScoreList.RemoveAt(10);
        }
        return bestScores;
    }

    private void AddEntrie(int score, string name)
    {
        ModelScore modelScore = new ModelScore { score = score, name = name };
        
        string jsonString = PlayerPrefs.GetString("scoreTable");
        BestScores bestScores = JsonUtility.FromJson<BestScores>(jsonString);
        bestScores.modelScoreList.Add(modelScore);

        bestScores = OrdenateList(bestScores);
        
        string json = JsonUtility.ToJson(bestScores);
        PlayerPrefs.SetString("scoreTable", json);
        PlayerPrefs.Save();
    }

    private void CreateEntries(ModelScore modelScore, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -60 * transformList.Count + 320);
        entryTransform.gameObject.SetActive(true);

        int posRank = transformList.Count + 1;
        entryTransform.Find("Position").GetComponent<Text>().text = posRank + "�";
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
