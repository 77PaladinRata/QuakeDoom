using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public float damage;
    public float fireRate;
    public int totalBullets;
    public float reoladTime;
    public int cartridgeSize; ///* Falto una "r" y una "d"
    public GunType gunType;
    ///* No SE que sonidos ponerles
    public string shootSoundName;
    public string reloadSoundName;
    public string dropSoundName;
    public Sprite sprite; ///***ICON
}

public enum GunType
{
    Automatic,
    SemiAutomatic,
}
