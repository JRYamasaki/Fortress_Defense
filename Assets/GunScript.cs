using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {

    public bool isReloading = false;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public int maxMagazineCapacity = 20;
    private int currentMagazineCapacity = 20;
    public float reloadTime = 1f;

    public Animator animator;

    public AudioSource gunShot;
    public AudioSource reload;

    public Text ammoText;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        currentMagazineCapacity = maxMagazineCapacity;
        ammoText.color = Color.black;
        ammoText.text = "Ammo: "+ currentMagazineCapacity + "/20";
    }

    private void OnEnable()
    {
        isReloading = false;
        //animator.SetBool("Reloading", false);
    }

    private void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + currentMagazineCapacity.ToString() + "/20";
    }

    private bool userWantsToReload()
    {
        return currentMagazineCapacity != maxMagazineCapacity && Input.GetKeyDown("r");
    }

    // Update is called once per frame
    void Update() {
        if (isReloading)
        {
            return;
        }

        if (currentMagazineCapacity <= 0 || userWantsToReload())
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            UpdateAmmoText();
        }
    }

    void Shoot()
    {
        currentMagazineCapacity--;
        muzzleFlash.Play();
        gunShot.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.1f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reload.Play();
        currentMagazineCapacity = maxMagazineCapacity;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.25f);
        UpdateAmmoText();

        isReloading = false;
    }
}
