using UnityEngine;
using SWNetwork; // ���������� ����������

public class ChangeWeapon : MonoBehaviour
{
    readonly string CURRENT_WEAPON = "CurrentWeapon"; // ���� ������������� ���������

    NetworkID networkID; // ������ �� ����������� ����������
    SyncPropertyAgent syncPropertyAgent; // ������ �� ���������� ������������� �������

    public GameObject activeWeapon;
    public GameObject knife;
    public GameObject pistol;
    public GameObject rifle;

    public bool changing;

    private void Start() {
        // �������� ������
        networkID = GetComponent<NetworkID>();
        syncPropertyAgent = GetComponent<SyncPropertyAgent>();
    }

    private void Update()
    {
        // ������ �� ������, ���� ������ ����� �� ���������
        if (!networkID.IsMine) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && knife != null)
        {
            syncPropertyAgent.Modify(CURRENT_WEAPON, 0); // 0 ��� ���� ���
            // SwitchTo(knife);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && pistol != null) {
            syncPropertyAgent.Modify(CURRENT_WEAPON, 1); // 1 ��������
            // SwitchTo(pistol);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1) && rifle != null) {
            syncPropertyAgent.Modify(CURRENT_WEAPON, 2); // 2 ��������
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

    // ������� �������� ��� ���� ���������� ������ ������
    private void SwitchTo(GameObject nextWeapon) {
        activeWeapon?.SetActive(false);
        activeWeapon = nextWeapon;
        activeWeapon.SetActive(true);
    }
}