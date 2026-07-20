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
    public override void OnEnable()
    {
        base.OnEnable();
        nextFireTime = 0f;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        animator.Play("Idle", 0, 0f);
        SoundManager.instance.Play("bipbupmiau");////*** SONIDO FRANCOTIRADOR ///********* ////********
    }
    private void Update()
    {
        if (IsInRange && Time. time >= nextFireTime)
        {
            StartCoroutine(AimAndShoot());
            nextFireTime = Time.time + fireRate;
        }
    }
    private IEnumerator AimAndShoot()   ///* temporizador
    {
        SoundManager.instance.Play("Sniper_timer"); ////*** SONIDO FRANCOTIRADOR ///********* ////********
        animator.Play("Aim", 0, 0f);
        yield return animator.WaitForCurrentAnimation();
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        laserBeam.SetActive(true);
        float duration = aimTime;
        while (duration > 0f)
        {
            duration --;
            timerText.text = duration.ToString();
            yield return new WaitForSeconds(1f);
        }
        SoundManager. instance.Play("Sniper_shot"); ////*** SONIDO FRANCOTIRADOR ///********* ////********
        laserBeam.SetActive(false);
        player.GetComponent<Health>(). TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        SoundManager. instance.Play("Sniper_die");////*** SONIDO FRANCOTIRADOR ///********* ////******** 
    }
}
