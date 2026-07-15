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
    private Health health; ///* menu de perder
    private Rigidbody rb;
    public float CurrentHealth => health.CurrentHealth; ///* no tanto
    private FirstPersonMovement firstPersonMovement;    ///* para que no se tambalee
    private Gun currentGun;
    private void Awake() ///* el menu de perder agregados
    {
        firstPersonMovement = GetComponent<FirstPersonMovement>(); ///* sin torbulencia
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }
    private void Start()
    {
        onGunDropped ?. Invoke(); ///*para que no me mate tan rapido
        health.InitializeHealth();  
    }
    ///*el anterior
    private void OnTriggerEnter(Collider other)
    {   ///* Agregandole para  Cambiar el arma Automatica
        if (other.CompareTag("Gun") && currentGun == null) ///* la otra arma
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
        ///* GetComponent<Rigidbody>().AddForce(pushDirection * force, ForceMode. Impulse);
        ///*rb.AddForce(pushDirection * force, ForceMode.Impulse);
        firstPersonMovement.AddKnockback(pushDirection, force);
    }
    public void Die()
    {
        DropGun();
        GetComponent<FirstPersonMovement>().enabled = false;
        GetComponentInChildren<FirstPersonLook>().enabled = false;
        rb. isKinematic = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
