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
    public bool started = false;

    [SerializeField]
    private Text hightScoreText;
    [SerializeField]
    private Score scores;
    [SerializeField]
    private Text actualScoreText;
    public int score = 0;

    [SerializeField]
    private GameObject panelAskName;
    [SerializeField]
    private InputField playerName;
    private bool askedName = false;

    [SerializeField]
    private Pacman pacman;
    [SerializeField]
    private Text lifeText;
    public int lifes = 3;

    [SerializeField]
    private Ghost[] ghosts;
    public int ghostMultiplier = 1;

    private int pelletsLength;

    [SerializeField]
    private VolumeControl volumeControl;

    void Start()
    {
        Time.timeScale = 1f;
        pelletsLength = FindObjectsOfType<Pellet>().Length;
        hightScoreText.text = scores.SearchHightScore().ToString();
        lifeText.text = lifes.ToString();
        volumeControl.DefineSliders();
    }

    void Update()
    {
        if (!started)
        {
            DecreaseStartTime();
        }
        else
        {
            lifeText.text = lifes.ToString();
            if (lifes <= 0 || pelletsLength <= 0)
            {
                if (!askedName)
                {
                    AskName();
                }
            }
        }
    }

    private void DecreaseStartTime()
    {
        countStart -= Time.deltaTime;
        float minutes = Mathf.FloorToInt(countStart % 60);
        countStartText.text = minutes.ToString();
        if(minutes == -1)
        {
            panelStart.SetActive(false);
            panelUI.SetActive(true);
            started = true;
            pacman.started = true;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void PauseNotGame()
    {
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);

        scores.AddEntrie(score, playerName.text);
        actualScoreText.text = score.ToString();
        panelEnd.SetActive(true);        
    }

    public void AskName()
    {
        panelUI.SetActive(false);
        panelAskName.SetActive(true);
    }

    public void AskNameConfirm()
    {
        askedName = true;
        EndGame();
    }

    public void LoseLife()
    {
        lifes--;
        pacman.DeathSequence();
        if (lifes > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
    }

    public void DefineScore(int winScore)
    {
        score += winScore;
        actualScoreText.text = score.ToString();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        DefineScore(points);
        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pelletsLength--;
        Destroy(pellet.gameObject);
        DefineScore(pellet.points);
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
