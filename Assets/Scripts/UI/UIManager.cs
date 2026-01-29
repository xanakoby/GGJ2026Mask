using DesignPatterns.Generics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PlayerInput playerInput;
    public CanvasGroup maskSelectionObj;
    [SerializeField] private RectTransform boardMaskSelection;
    [SerializeField] private RectTransform handle;
    bool isSelectingMask;
    public Vector2 analogDirection;
    [SerializeField] private CanvasGroup pause;

    public override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        playerInput.OnPlayerMoveAction += () =>
        {
            if (!isSelectingMask)
                return;

            analogDirection.x = playerInput.MovementX;
            analogDirection.y = playerInput.MovementY;
        };
        playerInput.OnPlayerStandAction += () =>
        {
            analogDirection = Vector2.zero;
        };
    }
    private void OnDisable()
    {
        playerInput.OnPlayerMoveAction -= () =>
        {
            if (!isSelectingMask)
                return;

            analogDirection.x = playerInput.MovementX;
            analogDirection.y = playerInput.MovementY;
        };
        playerInput.OnPlayerStandAction -= () =>
        {
            analogDirection = Vector2.zero;
        };
    }
    private void Update()
    {
        if(isSelectingMask)
        SetJoystickPosition(analogDirection);
        //variableJoystick.Direction = analogDirection;
    }
    public void ShowMaskSelectionMenu()
    {
        maskSelectionObj.gameObject.SetActive(true);
        isSelectingMask = true;
    }
    public void HideMaskSelectionMenu()
    {
        maskSelectionObj.gameObject.SetActive(false);
        isSelectingMask = false;
    }
    public void SetJoystickPosition(Vector2 normalizedInput)
    {
        if (normalizedInput.magnitude > 1f)
        {
            normalizedInput.Normalize();
        }

        float radius = boardMaskSelection.rect.width / 2f;

        handle.anchoredPosition = normalizedInput * radius;
    }
}
