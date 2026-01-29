using UnityEngine;
using UnityEngine.Events;
using static BulletTest;

public class Interactable : MonoBehaviour
{
    public UnityEvent interaction;
    public Interacter interacter;
    public Player player;
    public enum TriggerOn
    {
        Null,
        Enter,
        Exit
    }
    public TriggerOn triggerOn;
    public bool triggerOnAnything;

    private void OnCollisionEnter2D(Collision2D col)
    {
        interacter = col.gameObject.GetComponent<Interacter>();
        player = col.gameObject.GetComponent<Player>();

        if (triggerOnAnything == true)
        {
            interaction?.Invoke();
            return;
        }
        else if(player != null)
        {
            //collide col player
            interaction?.Invoke();
            return;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        interacter = null;
        player = null;
    }
    private void OnTriggerEnter(Collider col)
    {
        interacter = col.GetComponent<Interacter>();
        player = col.GetComponent<Player>();

        if (triggerOn == TriggerOn.Enter && triggerOnAnything == true)
        {
            interaction?.Invoke();
            return;
        }
        if (triggerOn == TriggerOn.Enter && col.GetComponent<Player>() && col.GetComponent<Interacter>())
        {
            interaction?.Invoke();
            return;
        }

        if (triggerOn == TriggerOn.Null && interacter != null && player != null)
        {
            interacter.watchingTriggerObject = gameObject;
            return;
        }

        if (interacter != null && player != null)
        {
            if (triggerOn == TriggerOn.Exit)
            {
                return;
            }

            interacter.watchingObject = gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        interacter = col.GetComponent<Interacter>();
        player = col.GetComponent<Player>();

        if (triggerOn == TriggerOn.Exit && triggerOnAnything == true)
        {
            interaction?.Invoke();
            return;
        }

        if (triggerOn == TriggerOn.Null && interacter != null && interacter.watchingTriggerObject == gameObject)
        {
            interacter.watchingTriggerObject = null;

            player = null;
            interacter = null;

            return;
        }

        if (interacter != null && player != null)
        {
            if (triggerOn == TriggerOn.Exit)
            {
                interaction?.Invoke();
                return;
            }

            player = null;
            interacter = null;
        }
    }

    private void OnDestroy()
    {
        if (interacter != null && interacter.watchingTriggerObject != null)
        {
            interacter.watchingTriggerObject = null;
        }
    }

    public void DestroyItSelfGameObject()
    {

        Destroy(gameObject);
    }

    public void DestroyItSelfInteractable()
    {
        Destroy(gameObject.GetComponent<Interactable>());
    }
    public void SetFalse()
    {
        if (interacter != null)
            interacter.watchingTriggerObject = null;
        gameObject.SetActive(false);
    }
    public void DisableInteractable()
    {
        if (interacter != null)
            interacter.watchingTriggerObject = null;
        gameObject.gameObject.GetComponent<Interactable>().enabled = false;
    }

    public void Message(string _msg)
    {
        Debug.Log(_msg);
    }
    public void TriggerInteractable()
    {
        interaction?.Invoke();
    }
}
