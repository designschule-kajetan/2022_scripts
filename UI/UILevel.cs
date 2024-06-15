using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    [Header("PanelStart")]
    [SerializeField] private CanvasGroup panelStart;
    [SerializeField] private Button buttonStartLevel;
    
    [Header("PanelWin")]
    [SerializeField] private CanvasGroup panelWin;
    [SerializeField] private Button buttonNextLevel;
    [SerializeField] private Button buttonAgain1; 
    [SerializeField] private Button buttonBackToMenu1;
    
    [Header("PanelLose")]
    [SerializeField] private CanvasGroup panelLose;
    [SerializeField] private Button buttonAgain2; 
    [SerializeField] private Button buttonBackToMenu2;
    
    void Start()
    {
        panelWin.HideCanvasGroup();
        panelLose.HideCanvasGroup();
        
        buttonStartLevel.onClick.AddListener(() =>
        {
            panelStart.HideCanvasGroup();
            GameController.Instance.StartLevel();
        });
        
        buttonAgain1.onClick.AddListener(GameController.Instance.ReloadLevel);
        buttonAgain2.onClick.AddListener(GameController.Instance.ReloadLevel);
        buttonBackToMenu1.onClick.AddListener(GameController.Instance.LoadMenu);
        buttonBackToMenu2.onClick.AddListener(GameController.Instance.LoadMenu);
        
        buttonNextLevel.onClick.AddListener(GameController.Instance.LoadNextLevel);
    }

    public void ShowWinScreen()
    {
        panelWin.ShowCanvasGroup();
    }

    public void ShowLoseScreen()
    {
        panelLose.ShowCanvasGroup();
    }
}
