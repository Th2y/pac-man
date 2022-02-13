using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Text progressText;
    private int progress = 0;

    public void GoToMenu()
    {
        StartCoroutine(LoadingScene("Menu"));
    }

    public void GoToLevel(int level)
    {
        StartCoroutine(LoadingScene("Level_" + level));
    }

    IEnumerator LoadingScene(string cena)
    {
        progressText.gameObject.SetActive(true);

        AsyncOperation load = SceneManager.LoadSceneAsync(cena);
        load.allowSceneActivation = false;
        while (progress < 89)
        {
            progress = (int)(load.progress * 100.0f);
            progressText.text = "Carregando... " + progress + "%";
            yield return null;
        }
        progress = 100;
        progressText.text = "Carregando... " + progress + "%";
        yield return new WaitForSeconds(2);
        progressText.gameObject.SetActive(false);
        load.allowSceneActivation = true;
    }
}
