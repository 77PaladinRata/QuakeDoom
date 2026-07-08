using UnityEngine;
using System.Collections;

public class EnemyTank : Enemy
{
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private float fireRate = 3f;
    [SerializeField]
    private Transform shootPivot;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float speed = 10f; ///* estaba n 10f
    private bool isShooting = false; ///* EStaba en 0F
    private float nextFireTime = 10f; ///* PERO lo cambie a 3f
    public override void OnEnable()
    {
        base.OnEnable();
        nextFireTime = 0f; ///* Para los Sonidos
        animator.Play("Appear, 1 0f");
        SoundManager. instance.Play("wizard_ appear"); ///*sonido MAGO *********************************
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    private void Update()
    {   ///* No habra problema con dos IFs
        if (health.CurrentHealth <= 0) return;
        if (CheckWin()) return; ///* no era para tanto
        if (IsInRange())
        {
            isShooting = true;
            {
                StartCoroutine(ShootCoroutine());
                nextFireTime = Time. time + fireRate;
            }
        ///* MOVIENDO los que ya estaban Abajo
        }///* agregando if
        else if (!isShooting)
        {
            FollowPlayer();
        }
        transform.LookAt(player.transform.position);
    }
    private void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        animator.Play("Walk");
    }
    private IEnumerator ShootCoroutine()
    {            ///* Borrado por que suena Trabado
        SoundManager. instance.Play(""); ///* sonido MAGO ********Recarga-Escopeta****wizard_ PrepareShoot********
        animator.Play("PrepareShoot", 0, 0f); ///* estaban en 0 y  0f
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.Play("Shoot", 0, 0f); ///* estaban en 0 y 0f
        SoundManager.instance.Play("wizard_ Shoot");///* SONIDO MAGO *******************Dispara-Escopeta********
        ///* Agregando Nuevas
        Vector3 direction = (player.transform.position - shootPivot.position).normalized;
        shootPivot. forward = direction;
        GameObject bullet = Instantiate(bulletPrefab, shootPivot.position, shootPivot.rotation);
        bullet.transform. LookAt(player.transform.position);
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isShooting = false;
    }
    public override void Die()
    {
        base.Die();
        SoundManager.instance.Play("wizard_ die"); ///* Sonido MAGO ***********************
    }
}
