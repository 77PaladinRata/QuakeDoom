using UnityEngine;
using System.Collections;

public class EnemyFollow2 : Enemy ///*MonoBehaviour ///*Agregando Enemy
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float yPosition = 2f;       ///*BORRANDO COSAS
    ///* animaciones enemigo
    ///* [SerializeField]
    ///*private float damage = 20f;
    [SerializeField]
    private float pushForce = 5f;
    ///* animaciones enemigo
    ///*private Transform player;
    private bool isFollowing = true;
    ///*private Animator animator;
    ///*private void Start()
    ///*{
        ///*animator = GetComponent<Animator>();
        ///*GetComponent<Health>().InitializeHealth(); ///*sFalto este
    ///*}
    ///* CREANDO UNA NUEVA
    public override void OnEnable()
    {
        base.OnEnable();
        animator.Play("Appear", 0, 0f); ///* Aparecer con sonido
        isFollowing = true; ///* Agregando el nombre de sonido
        SoundManager. instance.Play("wenk (!)"); ///*Aparecer ///* SONIDOS
    }
    public override void TakeDamage()
    {                           ///* Agregando el nombre de sonido
        SoundManager. instance.Play("gunter_quack"); ///* Daño
        if (!isFollowing) return;
        isFollowing = false;
        base. TakeDamage();
        StartCoroutine(StopAndFollow());
    }
    ///* CONSERVANDO ESTA
    ///*private void OnEnable()
    ///*{
        ///*player = GameObject.FindGameObjectWithTag("Player").transform;
    ///*}
    ///* CONSERVANDO ESTA
    private IEnumerator StopAndFollow()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isFollowing = true;
    }
    ///* CAMBIANDO CASI ESTA
    public void Die()
    {
        ///*StopAllCoroutines(); ///* Agregando el nombre de sonido
        SoundManager.instance.Play("[wenk]"); ///* muere el enemigo
        ///*GetComponent<Collider>().enabled = false; ///* para que haga la animacion
        isFollowing = false;
        animator.Play("Die", 0, 0f);
        ///*StartCoroutine(DieCoroutine());
        base.Die(); ///* Agregando esta
    }
    ///*private IEnumerator DieCoroutine()
    ///*{
        ///*yield return null;
        ///*yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        ///*Destroy(gameObject);
    ///*}
    private void Update()   ///*DEJAR ESTA COMO ESTA
    {
        if (!isFollowing) return;
        if (CheckWin()) return;
        Vector3 targetPosition = new Vector3(player.position.x, yPosition, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);
        transform. LookAt(targetPosition);
    }
    ///* Animacines enemigo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {                                          ///* muere el enemigo
            SoundManager.instance.Play("gunter_quack"); ///* Ataque
            collision.gameObject.GetComponent<Player>().PushBack(transform, pushForce);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
