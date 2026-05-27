using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform gunPosition;
    ///* Nuevas
    [SerializeField]
    private UnityEvent onGunGrabbed;
    [SerializeField]
    private UnityEvent onGunDropped;
    private Gun currentGun;
    private void Start()
    {
        onGunDropped ?. Invoke();
    }
    ///*el anterior
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {  ///* este ya no
        ///*other.GetComponent<Gun>().GrabGun(gunPosition);
            currentGun = other.GetComponent<Gun>();
            currentGun. GrabGun (gunPosition);
            onGunGrabbed ?. Invoke();
        }
    }
}
