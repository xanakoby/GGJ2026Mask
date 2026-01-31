using UnityEngine;

public enum EDamageType
{
    subtract,
    add,
    instantDeath
}
public class Damager : MonoBehaviour
{
    public int value = 1;
    public EDamageType damageType;

    Damageable dam;

    private void OnCollisionEnter2D(Collision2D col)
    {
        dam = col.gameObject.GetComponent<Damageable>();

        //Damageable damageable = col.gameObject.GetComponent<Damageable>();
        //if (damageable == null || damageable.gameObject.tag != "Damageable")
        //    return;

        //damageable.CalculateDamagerValue(this);
    }

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    Damageable damageable = col.gameObject.GetComponent<Damageable>();

    //    if (damageable != null)
    //    {
    //        if (damageable.damagers.Contains(this))
    //        {
    //            damageable.damagers.Remove(this);
    //        }
    //    }
    //}

    private void OnDisable()
    {
        if (dam != null)
        {
            if (dam.damagers.Contains(this))
            {
                dam.damagers.Remove(this);
            }
        }
    }
    private void OnDestroy()
    {
        if (dam != null)
        {
            if (dam.damagers.Contains(this))
            {
                dam.damagers.Remove(this);
            }
        }
    }
}
