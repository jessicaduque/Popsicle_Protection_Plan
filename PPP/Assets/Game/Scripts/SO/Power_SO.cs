using UnityEngine;

[CreateAssetMenu(fileName = "Power", menuName = "ScriptableObjects/Power", order = 1)]
public class Power_SO : ScriptableObject
{
    public string power_name;
    public string power_brief_description;
    public string power_long_description;

    public Sprite power_sprite;

    public float power_rechargeTime;
    public float power_useTime;

    public bool power_isPenguinPower;

    public GameObject power_controllerPrefab;
}
