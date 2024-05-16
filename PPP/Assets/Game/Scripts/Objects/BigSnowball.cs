using UnityEngine;

public class BigSnowball : Snowball
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Penguin_Controller.I.SetHasPopsicle(false);

        if (collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage(2);
        }

        _audioManager.PlaySfx("bigsnowballhit");
    }

    public void ObjectDeactivate()
    {
        PoolManager.I.ReturnPool(this.gameObject);
    }
}
