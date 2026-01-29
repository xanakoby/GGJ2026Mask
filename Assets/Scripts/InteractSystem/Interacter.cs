using UnityEngine;

public class Interacter : MonoBehaviour
{
    public PlayerInput playerInput;
    public Interactable interactable;

    [Header("The gameobject i collide with")]
    public GameObject watchingObject;

    [Header("The gameobject trigger where i'm in")]
    public GameObject watchingTriggerObject;
    private void OnEnable()
    {
        playerInput.OnInteractionAction += CheckInteractable;

    }
    private void OnDisable()
    {
        playerInput.OnInteractionAction -= CheckInteractable;
    }

    public void CheckInteractable()
    {
        Debug.Log("Interacter: CheckInteractable called");
        if (watchingTriggerObject != null && watchingTriggerObject.GetComponent<Interactable>() != null)
        {
            interactable = watchingTriggerObject.GetComponent<Interactable>();
            interactable.interaction?.Invoke();

            interactable = null;
            return;
        }

        if (watchingObject != null && watchingObject.GetComponent<Interactable>() != null)
        {
            interactable = watchingObject.GetComponent<Interactable>();
            interactable.interaction?.Invoke();

            interactable = null;
        }
    }
}
