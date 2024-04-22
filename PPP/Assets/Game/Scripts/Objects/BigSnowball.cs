using UnityEngine;

public class BigSnowball : Snowball
{
    private void Awake() { }

    private void OnEnable() { }

    private void OnDisable() { }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Penguin_Controller.I.SetHasPopsicle(false);

        base.OnTriggerEnter2D(collision);
    }

    public void ObjectDeactivate()
    {
        PoolManager.I.ReturnPool(this.gameObject);
    }
}
