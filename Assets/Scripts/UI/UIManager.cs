using DesignPatterns.Generics;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private HorizontalLayoutGroup maskSelectionLayout;
    [SerializeField] private Image[] maskImages;
    [SerializeField] float switchTime = 0.2f;
    //public CanvasGroup maskSelectionObj;
    //[SerializeField] private RectTransform boardMaskSelection;
    //[SerializeField] private RectTransform handle;
    //bool isSelectingMask;
    //public Vector2 analogDirection;
    [SerializeField] private CanvasGroup pause;
    bool isSwitchingMask;
    bool switchedLeftRight;

    public override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        //playerInput.OnPlayerMoveAction += () =>
        //{
        //    if (!isSelectingMask)
        //        return;

        //    analogDirection.x = playerInput.MovementX;
        //    analogDirection.y = playerInput.MovementY;
        //};
        //playerInput.OnPlayerStandAction += () =>
        //{
        //    analogDirection = Vector2.zero;
        //};
        playerInput.OnSwitchLMask += SwitchToLeftMask;
        playerInput.OnSwitchRMask += SwitchToRightMask;
    }
    private void OnDisable()
    {
        //playerInput.OnPlayerMoveAction -= () =>
        //{
        //    if (!isSelectingMask)
        //        return;

        //    analogDirection.x = playerInput.MovementX;
        //    analogDirection.y = playerInput.MovementY;
        //};
        //playerInput.OnPlayerStandAction -= () =>
        //{
        //    analogDirection = Vector2.zero;
        //};
        playerInput.OnSwitchLMask -= SwitchToLeftMask;
        playerInput.OnSwitchRMask -= SwitchToRightMask;
    }
    private void Update()
    {
        //if(isSelectingMask)
        //SetJoystickPosition(analogDirection);
        //variableJoystick.Direction = analogDirection;
    }
    //public void ShowMaskSelectionMenu()
    //{
    //    maskSelectionObj.gameObject.SetActive(true);
    //    isSelectingMask = true;
    //}
    //public void HideMaskSelectionMenu()
    //{
    //    maskSelectionObj.gameObject.SetActive(false);
    //    isSelectingMask = false;
    //}
    //public void SetJoystickPosition(Vector2 normalizedInput)
    //{
    //    if (normalizedInput.magnitude > 1f)
    //    {
    //        normalizedInput.Normalize();
    //    }

    //    float radius = boardMaskSelection.rect.width / 2f;

    //    handle.anchoredPosition = normalizedInput * radius;
    //}
    public void SwitchToLeftMask()
    {
        if (isSwitchingMask)
            return;

        maskSelectionLayout.padding.left = -120;
        if (!switchedLeftRight)
        {
            //l'ultimo switch èra destra, quindi era 2.341 e diventa 1.234
            maskImages[3].sprite = maskImages[2].sprite;
            maskImages[2].sprite = maskImages[1].sprite;
            maskImages[1].sprite = maskImages[0].sprite;
            maskImages[0].sprite = maskImages[3].sprite;

            
        }
        //la mask che sta a sinistra è quella che compare a destra
        StartCoroutine(LerpToInTime(0.2f, -120, 10));

        switchedLeftRight = false;
    }
    public void SwitchToRightMask()
    {
        if(isSwitchingMask)
            return;

        maskSelectionLayout.padding.left = 10;
        if (switchedLeftRight)
        {
            //l'ultimo switch èra sinistra, quindi era 1.234 e diventa 2.341
            maskImages[0].sprite = maskImages[1].sprite;
            maskImages[1].sprite = maskImages[2].sprite;
            maskImages[2].sprite = maskImages[3].sprite;
            maskImages[3].sprite = maskImages[0].sprite;
        }
        //la mask che sta a destra è quella che compare a sinistra
        StartCoroutine(LerpToInTime(switchTime, 10, -120));

        switchedLeftRight = true;
    }
    private IEnumerator LerpToInTime(float duration, float startPadding, float endingPadding)
    {
        isSwitchingMask = true;

        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            t = Mathf.SmoothStep(0, 1, t);

            int currentPadding = (int)Mathf.Lerp(startPadding, endingPadding, t);
            maskSelectionLayout.padding.left = currentPadding;

            // Forza il Layout Group a ricalcolare i figli immediatamente
            LayoutRebuilder.MarkLayoutForRebuild(maskSelectionLayout.transform as RectTransform);

            yield return null;
        }

        // Assicuriamoci di arrivare al valore esatto alla fine
        maskSelectionLayout.padding.left = (int)endingPadding;
        LayoutRebuilder.MarkLayoutForRebuild(maskSelectionLayout.transform as RectTransform);

        isSwitchingMask = false;
    }
}

