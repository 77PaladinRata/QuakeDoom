using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private GameObject enemyHitParticles;
    [SerializeField]
    private GameObject wallHitParticles;
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
    {///* Agregando muchas cosas para la bala
        string tag = collision.gameObject.tag;
        if (tag == "Enemy")
        {                                   ///*Nombre del sonido 
            SoundManager.instance.Play("Impacto Carne"); ///*bullet_hit_enemy
            PoolManager.Instance.GetObject(enemyHitParticles, transform.position);
        }
        else
        {                               ///*Nombre del sonido 
            SoundManager.instance.Play("Impacto Metal"); ///*bullet_hit_wall
            PoolManager. Instance.GetObject(wallHitParticles, transform.position);
        }
        gameObject.SetActive(false);
    }
    
}
