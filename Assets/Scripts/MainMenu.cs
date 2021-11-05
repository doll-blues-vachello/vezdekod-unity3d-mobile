using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _levelBar;
    [SerializeField] private Text _levelLabel;

    private void Start()
    {
        UpdateLevelLabel();
    }
    
    public void PlayButton_OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelBar_OnValueChanged()
    {
        Game.Lvl = _levelBar.value switch
        {
            1 => Game.Level.Easy,
            2 => Game.Level.Medium,
            3 => Game.Level.Hard,
            _ => Game.Level.Medium
        };

        UpdateLevelLabel();
    }

    private void UpdateLevelLabel()
    {
        _levelLabel.text = _levelBar.value switch
        {
            1 => "Easy",
            2 => "Medium",
            3 => "Hard",
            _ => ""
        };
    }
}
