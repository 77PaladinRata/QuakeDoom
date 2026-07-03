using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    ///* Agregando faltantes para que se mueva
    ///* private float damage = 10f;
    protected float damage = 10f;
    public float Damage { set { damage = value; } }
    ///* Para que no se trabe ===============================
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.linearVelocity = transform.forward * speed;
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
    ///* Para que no se trabe ===============================

      ///*void OnEnable()
      ///*{
          ///*GetComponent<Rigidbody>().linearVelocity = transform. forward * speed;
      ///*}
    ///* Supuestamente con esto se tiene que mover
    ///* cambiando titulos

///*private void ///* cambiando cosas
      ///*public virtual void OnCollisionEnter(Collision collision)
      ///*{
        ///*if (collision.gameObject.CompareTag("Enemy"))
        ///*{
            ///*collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        ///*}   ///*collision
        
        ///* Destroy(gameObject);
          ///*gameObject.SetActive(false);
      ///*}
}
