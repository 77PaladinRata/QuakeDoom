using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    ///* Agregando faltantes para que se mueva
    ///* private float damage = 10f;
    protected float damage = 10f;
    public float Damage { set { damage = value; } }
    ///* void Awake()
    void OnEnable()
    {
        GetComponent<Rigidbody>().linearVelocity = transform. forward * speed;
    }
    ///* Supuestamente con esto se tiene que mover
    ///* cambiando titulos
///*private void ///* cambiando cosas
    public virtual void OnCollisionEnter(Collision collision)
    {
        ///*if (collision.gameObject.CompareTag("Enemy"))
        ///*{
            ///*collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        ///*}   ///*collision
        
        Destroy(gameObject);
    }
}
