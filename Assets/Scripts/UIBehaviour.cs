using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    [SerializeField] private GameObject _goodEndLabel;
    [SerializeField] private GameObject _badEndLabel;

    private void Update()
    {
        if (Game.Status == 1)
            EndGame(true);
        else if (Game.Status == -1)
            EndGame(false);
    }

    public void TogglePause()
    {
        if (GameIsPaused)
            ContinueGame();
        else
            PauseGame();
        
        PauseMenuUI.SetActive(GameIsPaused);
    }

    public void ContinueButton_OnClick()
    {
        TogglePause();
    }

    public void QuitButton_OnClick()
    {
        Application.Quit();
    }

    public void Skill1Button_OnClick()
    {
        if (Game.SkillPoints > 0)
        {
            Game.SkillPoints--;
            PlayerController.Degree++;
        }
    }

    public void Skill2Button_OnClick()
    {
        if (Game.SkillPoints > 0)
        {
            Game.SkillPoints--;
            Player.Shields += 2;
        }
    }

    public void Skill3Button_OnClick()
    {
        if (Game.SkillPoints > 0)
        {
            Game.SkillPoints--;
            Player.Protection++;
            Asteroid.Damage--;
        }
    }

    public void EndGame(bool endIsGood)
    {
        if (endIsGood)
            _goodEndLabel.SetActive(true);
        else
            _badEndLabel.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
