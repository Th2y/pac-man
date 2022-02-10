using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelStart;
    [SerializeField]
    private GameObject panelUI;
    [SerializeField]
    private GameObject panelEnd;
    [SerializeField]
    private Text countStartText;
    private float countStart = 5;
    private bool started = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!started)
        {
            decreaseStartTime();
        }
    }

    private void decreaseStartTime()
    {
        countStart -= Time.deltaTime;
        float minutes = Mathf.FloorToInt(countStart % 60);
        countStartText.text = minutes.ToString();
        if(minutes == -1)
        {
            panelStart.SetActive(false);
            panelUI.SetActive(true);
            started = true;
        }
    }
}
