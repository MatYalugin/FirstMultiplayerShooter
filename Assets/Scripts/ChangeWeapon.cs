using UnityEngine;
using SWNetwork; // подключаем библиотеку

public class ChangeWeapon : MonoBehaviour
{
    readonly string CURRENT_WEAPON = "CurrentWeapon"; // ключ синхронизации параметра

    NetworkID networkID; // ссылка на мультиплеер компоненту
    SyncPropertyAgent syncPropertyAgent; // ссылка на компоненту синхронизации свойств

    public GameObject activeWeapon;
    public GameObject knife;
    public GameObject pistol;
    public GameObject rifle;

    public bool changing;

    private void Start() {
        // получаем ссылки
        networkID = GetComponent<NetworkID>();
        syncPropertyAgent = GetComponent<SyncPropertyAgent>();
    }

    private void Update()
    {
        // ничего не делать, если данный игрок не локальный
        if (!networkID.IsMine) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && knife != null)
        {
            syncPropertyAgent.Modify(CURRENT_WEAPON, 0); // 0 это типа нож
            // SwitchTo(knife);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && pistol != null) {
            syncPropertyAgent.Modify(CURRENT_WEAPON, 1); // 1 пистолет
            // SwitchTo(pistol);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && rifle != null) {
            syncPropertyAgent.Modify(CURRENT_WEAPON, 2); // 2 винтовка
            // SwitchTo(rifle);
        }
    }

    public void OnCurrentWeaponIndexChanged() {
        int weaponIndex = syncPropertyAgent.GetPropertyWithName(CURRENT_WEAPON).GetIntValue();
        if (weaponIndex == 0) {
            SwitchTo(knife);
        } else if (weaponIndex == 1) {
            SwitchTo(pistol);
        } else if (weaponIndex == 2) {
            SwitchTo(rifle);
        }
    }

    // немного сократим код путём добавления общего метода
    private void SwitchTo(GameObject nextWeapon) {
        activeWeapon?.SetActive(false);
        activeWeapon = nextWeapon;
        activeWeapon.SetActive(true);
    }
}