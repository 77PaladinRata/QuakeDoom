using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rotate rotateScript;
    ///* Poniendo cosas Nuevas
    [SerializeField]
    private GunData gunData;
    [SerializeField]
    private Transform bulletPivot;
    [SerializeField]
    private GameObject bulletPrefab;
    ///* Balas sin sonido
    private float nextFireTime;
    private int totalBullets;
    public void GrabGun(Transform gunPosition)
    {
        totalBullets = gunData.totalBullets; ///*NuevaBalas
        transform. SetParent (gunPosition);
        transform. localPosition = Vector3. zero;
        transform. localRotation = Quaternion.identity;
                  ///*"Idle"
        animator.Play("Grab", 0, 0f);
        rotateScript.canRotate = false;
        gameObject.GetComponent<Collider>().enabled = false;
    } ///* Nuevo entero
    public void Shoot()
    {
        float rayDistance = 1000f;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(rayDistance);
        }
        Vector3 direction = (targetPoint - transform.position).normalized;
        bulletPivot.forward = direction;
        GameObject bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        bullet.transform.LookAt(targetPoint);
        animator.Play("Shoot", 0, 0f);
    }
    ///* agregando innecesariamente sonido
    public void HandleFire(bool pressed, bool held)
    {
        if (gunData.gunType == GunType.Automatic)
        {
            if (held)
            {
                TryShoot();
            }
        }
        else if (gunData.gunType == GunType.SemiAutomatic)
        {
            if (pressed)
            {
                TryShoot();
            }
        }
    }
    private void TryShoot()
    {
        if (totalBullets > 0 && Time. time >= nextFireTime)
        {
            Shoot();
            totalBullets--;
            nextFireTime = Time.time + 1f / gunData. fireRate;
        }
    }
}
