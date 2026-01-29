using DesignPatterns.Generics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public CanvasGroup maskSelectionObj;
    public Image boardMaskSelection;

    public override void Awake()
    {
        base.Awake();
    }
    public void ShowMaskSelectionMenu()
    {
        maskSelectionObj.gameObject.SetActive(true);
    }
    public void HideMaskSelectionMenu()
    {
        maskSelectionObj.gameObject.SetActive(false);
    }
}
