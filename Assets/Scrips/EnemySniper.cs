using UnityEngine;
using UnityEngine.UI;
using System. Collections;

public class EnemySniper : Enemy
{
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private float fireRate = 3f;
    [SerializeField]
    private float aimTime = 4f;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private LaserBeam laserBeam;
    private float nextFireTime;
    private bool IsInRange => Vector3.Distance(transform.position, player.position) <= range;
    private bool isShooting = false; ///* nueva
    public override void OnEnable()
    {
        timerText.text = "";
        isShooting = false; ///* nueva
        base.OnEnable();
        laserBeam.ActivateLaser(false); ///* nueva
        nextFireTime = 0f;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        animator.Play("Idle", 0, 0f);
        SoundManager.instance.Play("bipbupmiau");////*** SONIDO APARECER ///********* ////********
    }
    private void Update()
    {   ///* MUCHOS IFs ///*
        if (health.IsDead) return;
        if (CheckWin()) return;
        transform.LookAt(player);
        if (!isShooting && IsInRange && Time. time >= nextFireTime)
        {
            isShooting = true; //* mas errores
            StartCoroutine(AimAndShoot());
            ///* nextFireTime = Time.time + fireRate;
        }
    }
    private IEnumerator AimAndShoot()   
    {
        laserBeam. Target = player;
        laserBeam.ActivateLaser(true);
        SoundManager.instance.Play("Sniper_damage"); ////*** SONIDO TE DECTECTA ///********* ////********
        animator.Play("Aim", 0, 0f);
        yield return animator.WaitForCurrentAnimation();
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()     ///* temporizador
    {
        ///* laserBeam.SetActive(true);
        float duration = aimTime;
        while (duration > 0f)
        {
            SoundManager.instance.Play("Sniper_timer"); ////* SONIDO TEMPORIZADOR ///***************
            duration --;
            timerText.text = duration.ToString();
            yield return new WaitForSeconds(1f);
        }
        timerText. text = "";
        animator.Play("Fire", 0, 0f);
        SoundManager. instance.Play("Sniper_shot"); ////*** SONIDO FRANCOTIRADOR ///********* ////********
        laserBeam.ActivateLaser(false);
        player.GetComponent<Health>(). TakeDamage(damage);
        isShooting = false;
        nextFireTime = Time.time + fireRate;
    }
    public override void Die()
    {
        laserBeam.ActivateLaser(false);
        base.Die();
        SoundManager. instance.Play("Sniper_die");////*** SONIDO FRANCOTIRADOR ///********* ////******** 
    }
}
