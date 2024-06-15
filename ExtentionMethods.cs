using UnityEngine;

public static class ExtentionMethods
{
    public static void ShowCanvasGroup(this CanvasGroup myCanvasGroup)
    {
        myCanvasGroup.alpha = 1f;
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
    }
    
    
    public static void HideCanvasGroup(this CanvasGroup myCanvasGroup)
    {
        myCanvasGroup.alpha = 0f;
        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;
    }
}
