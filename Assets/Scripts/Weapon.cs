using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

public class Weapon : MonoBehaviour {

    ChangeWeapon changeWeapon;

    public float distance;
    public Animator animator;
    public ParticleSystem muzzleFlash;
    public SWAudioSource shotSound;
    public float damage = 30f;
    public Camera mainCamera;
    public bool autoFire;
    public float invokeAnimDuration = 3.5f;
    public float fireRate = 2f;
    private bool isReadyToFire = true;
    private int ammo;
    public int maxAmmo;
    public int mags;
    public Text ammoText;
    public GameObject playerGO;
    public float enableChangeWeaponScriptTime;

    public NetworkID networkID;

    void Start() {
        ammo = maxAmmo;

        if (networkID.IsMine) {
            ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
            changeWeapon = playerGO.GetComponent<ChangeWeapon>();
        }
    }

    void Update() {
        if (networkID.IsMine) {
            ammoText.text = "Ammo: " + ammo + "/" + mags;

            changeWeapon.enabled = isReadyToFire;

            TryShot();

            if (Input.GetKey(KeyCode.R) && mags != 0) {
                Reload();
            }
            if (Input.GetKey(KeyCode.F))
            {
                Inspection();
            }
        }
    }

    public void TryShot() {
        if (autoFire == false) {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isReadyToFire == true && ammo != 0) {
                FireBullet();
            }

        } else {
            if (Input.GetKey(KeyCode.Mouse0) && isReadyToFire == true && ammo != 0) {
                FireBullet();
            }
        }
    }

    private void FireBullet() {
        ammo -= 1;
        isReadyToFire = false;
        Invoke("makeReadyToFire", fireRate);
        animator.Play("Shot");
        muzzleFlash.Play();
        shotSound.Play();
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, distance)) {
            if (hit.transform.TryGetComponent(out Player player)) {
                player.health.Damage(damage);
            }
        }
    }

    public void Reload() {
        if (ammo != maxAmmo) {
            changeWeapon.enabled = false;
            isReadyToFire = false;
            Invoke("makeReadyToFire", enableChangeWeaponScriptTime);
            animator.Play("Reload");
            ammo = maxAmmo;
            mags -= 1;
        }
    }
    public void Inspection()
    {
        animator.Play("Inspection");
        isReadyToFire = false;
        Invoke("makeReadyToFire", invokeAnimDuration);
    }

    public void makeReadyToFire() {
        isReadyToFire = true;
    }
}