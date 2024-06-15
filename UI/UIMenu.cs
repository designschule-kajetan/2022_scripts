using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Transform parentButtons;
    [SerializeField] private GameObject prefabButtonLevel;
    
    void Start()
    {
        int i = 0;
        foreach (SceneReference levels in GameController.Instance.scenesLevel)
        {
            Button button = Instantiate(prefabButtonLevel, parentButtons).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = levels.Name;
            int capturedIndex = i;
            button.onClick.AddListener(()=>GameController.Instance.LoadLevel(capturedIndex));
            i++;

        }
    }
}
