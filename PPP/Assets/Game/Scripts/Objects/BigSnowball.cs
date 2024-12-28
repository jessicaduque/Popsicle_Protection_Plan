public class BigSnowball : Snowball
{
    public void ObjectDeactivate()
    {
        PoolManager.I.ReturnPool(this.gameObject);
    }
}
