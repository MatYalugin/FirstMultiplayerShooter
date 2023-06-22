using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float distance;
    public Animator animator;
    public ParticleSystem muzzleFlash;
    public AudioSource shotSound;
    public float damage = 30f;
    public Camera mainCamera;
    public bool autoFire;
    public float fireRate = 2f;
    private bool isReadyToFire = true;
    private int ammo;
    public int maxAmmo;
    public int mags;
    public Text ammoText;
    public GameObject playerCamera;
    public float enableChangeWeaponScriptTime;
    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && mags != 0)
        {
            Reload();
        }
        Shot();
        ammoText.text = "Ammo: " + ammo + "/" + mags;

        //if(isReadyToFire != true)
        //{
            //playerCamera.GetComponent<ChangeWeapon>().enabled = false;
        //}
        //if (isReadyToFire == true)
        //{
            //playerCamera.GetComponent<ChangeWeapon>().enabled = true;
        //}
    }
    public void Shot()
    {

        if(autoFire == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isReadyToFire == true && ammo != 0)
            {
                ammo -= 1;
                isReadyToFire = false;
                Invoke("makeReadyToFire", fireRate);
                animator.Play("Shot");
                muzzleFlash.Play();
                shotSound.Play();
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance))
                {
                    if (hit.transform.tag.Equals("Player"))
                    {
                        hit.transform.GetComponent<Player>().Hurt(damage);
                    }
                }
            }
        }
        if (autoFire == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && isReadyToFire == true && ammo != 0)
            {
                ammo -= 1;
                isReadyToFire = false;
                Invoke("makeReadyToFire", fireRate);
                animator.Play("Shot");
                muzzleFlash.Play();
                shotSound.Play();
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance))
                {
                    if (hit.transform.tag.Equals("Player"))
                    {
                        hit.transform.GetComponent<Player>().Hurt(damage);
                    }
                }
            }
        }
    }

    public void Reload()
    {
        if(ammo != maxAmmo)
        {
            playerCamera.GetComponent<ChangeWeapon>().enabled = false;
            isReadyToFire = false;
            Invoke("makeReadyToFire", enableChangeWeaponScriptTime);
            animator.Play("Reload");
            ammo = maxAmmo;
            mags -= 1;
        }
    }
    public void makeReadyToFire()
    {
        isReadyToFire = true;
    }
}
