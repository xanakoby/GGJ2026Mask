using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] int currentHealth = 1;
    [SerializeField] int maxHealth = 1;
    public UnityEvent onDamageTaken;
    public UnityEvent onHealthTaken;
    public UnityEvent onDeath;

    [Space]
    public bool canBeImmortalAfterDamage;
    public UnityEvent onStartImmortal;
    public UnityEvent onStopImmortal;
    [SerializeField] bool canTakeDamage = true;
    public float immortalTime = 1;

    public bool takeDamageOnStay = true;
    public List<Damager> damagers;

    [SerializeField] bool isPlayer;
    [SerializeField] bool debugPlayer;

    private void Start()
    {
        currentHealth = maxHealth;
        if (isPlayer)
        {
            UIManager.Instance.UpdateHealthDisplay(currentHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Damager dam = col.gameObject.GetComponent<Damager>();
        if (dam == null)
            return;

        if (canBeImmortalAfterDamage && dam != null)
        {
            if (!damagers.Contains(dam))
            {
                damagers.Add(dam);
            }
        }

        if (!canTakeDamage)
            return;
        //mando il messaggio del danno
        //CalculateDamagerValue(dam);
        //Publisher.Publish(new UpdatePlayerHealthMessage(dam));
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        Damager dam = col.gameObject.GetComponent<Damager>();

        if (canBeImmortalAfterDamage && dam != null)
        {
            if (damagers.Contains(dam))
            {
                damagers.Remove(dam);
            }
        }
    }
    public void CalculateDamagerValue(Damager _damager)
    {
        if (_damager != null)
        {
            switch (_damager.damageType)
            {
                case EDamageType.subtract:

                    if (!canTakeDamage)
                    {
                        Debug.Log("non posso prendere dannooooo");
                        return;
                    }

                    currentHealth -= _damager.value;

                    if (currentHealth <= 0)
                    {
                        currentHealth = 0;
                        //HealthUIManager.Instance.UpdateHealth(currentHealth);
                        onDeath?.Invoke();
                        return;
                    }
                    else
                    {
                        onDamageTaken?.Invoke();
                    }

                    if (canBeImmortalAfterDamage)
                    {
                        //damagers.Add(_damager);
                        if (canTakeDamage)
                            GameManager.Instance.StartCoroutine(startImmortalTime());
                    }

                    break;

                case EDamageType.add:

                    currentHealth += _damager.value;

                    if (currentHealth > maxHealth)
                    {
                        currentHealth = maxHealth;
                        onHealthTaken?.Invoke();
                    }
                    break;

                case EDamageType.instantDeath:

                    currentHealth = 0;
                    onDeath?.Invoke();

                    break;
            }
            //HealthUIManager.Instance.UpdateHealth(currentHealth);
        }
    }

    IEnumerator startImmortalTime()
    {
        if (debugPlayer)
            Debug.Log(transform.gameObject.name + " Start immortal time");

        canTakeDamage = false;
        onStartImmortal?.Invoke();
        yield return new WaitForSeconds(immortalTime);

        if (debugPlayer)
            Debug.Log(transform.gameObject.name + " Stop immortal time");

        onStopImmortal?.Invoke();
        canTakeDamage = true;

        if (takeDamageOnStay)
        {
            if (damagers.Count > 0)
            {
                //mando il messaggio del danno
                //CalculateDamagerValue(damagers[0]);
                //Publisher.Publish(new UpdatePlayerHealthMessage(damagers[0]));
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        if
(currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke();
            return;
        }
        else
        {
            onDamageTaken?.Invoke();
        }
    }
    public void DebugMessage(string _msg)
    {
        Debug.Log(_msg);
    }
}