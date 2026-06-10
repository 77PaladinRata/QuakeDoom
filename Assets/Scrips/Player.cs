using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform gunPosition;
    ///* Solo una
    [SerializeField]
    private InputManager inputManager;
    ///* MMMMMM
    [SerializeField]
    private Text ammoText;
    /// * MMMMMMM
    [SerializeField]
    private UnityEvent onGunGrabbed;
    [SerializeField]
    private UnityEvent onGunDropped;
    private Gun currentGun;
    private void Start()
    {
        onGunDropped ?. Invoke(); ///*para que no me mate tan rapido
        GetComponent<Health>().InitializeHealth();
    }
    ///*el anterior
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {  ///* este ya no
        ///*other.GetComponent<Gun>().GrabGun(gunPosition);
            currentGun = other.GetComponent<Gun>();
            currentGun.GrabGun(gunPosition, ammoText);
        ///*currentGun.GrabGun(gunPosition);
            onGunGrabbed ?. Invoke();
        ///*currentGun.OnGunEmpty+= DropGun; ///* QUE Cansado
        ///*currentGun.OnGunEmpty.AddListener(() => { DropGun(); });
            currentGun.OnGunEmpty.AddListener(DropGun);
            
        }
    } ///* De la Nueva que pusimos
    private  void Update()
    {
        if (currentGun != null)
        {
            currentGun.HandleFire(inputManager.LeftButtonPressed, inputManager.LeftButtonHeld);
            if (inputManager.RightButtonPressed) ///* otro if/si
            {
                currentGun. ChargeGun();
            }
        }
    }///* estoy cansado de agregarmecanicas
    public void DropGun()
    {
        if (currentGun == null) return;
        Destroy(currentGun.gameObject);
        currentGun = null;
        onGunDropped ?. Invoke();
    }///* el enemigo para el player
    public void PushBack(Transform enemy, float force)
    {
        Vector3 pushDirection = (transform.position - enemy.position).normalized;
        GetComponent<Rigidbody>().AddForce(pushDirection * force, ForceMode. Impulse);
    }
}
