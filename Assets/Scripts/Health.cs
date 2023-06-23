using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

public class Health : MonoBehaviour {

    public delegate void DeathDelegate();
    public event DeathDelegate OnDeath;

    [SerializeField] NetworkID networkID;
    [SerializeField] SyncPropertyAgent syncPropertyAgent;

    [Space]
    [SerializeField] float healthMax = 100;
    [SerializeField] string healthKey = "Health";

    [Header("UI")]
    [SerializeField] Text abovePlayerText;
    [SerializeField] Text uiText;

    private void Start() {
        if (networkID.IsMine)
        {
            uiText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
            float currentHealth = healthMax;
            if (uiText != null)
                uiText.text = currentHealth.ToString();
        }


        if (syncPropertyAgent.GetPropertyWithName(healthKey).version == 0)
            syncPropertyAgent.Modify(healthKey, healthMax);
    }

    public void Damage(float value) {
        OnHealthChanged(-value);
    }

    public void Heal(float value) {
        OnHealthChanged(value);
    }

    public void Kill() {
        OnHealthChanged(-healthMax);
    }

    public void Restore() {
        OnHealthChanged(healthMax);
    }

    private void OnHealthChanged(float healthDelta) {
        float currentHealth = syncPropertyAgent.GetPropertyWithName(healthKey).GetFloatValue();
        currentHealth = Mathf.Clamp(currentHealth + healthDelta, 0, healthMax);

        syncPropertyAgent.Modify(healthKey, currentHealth);
    }

    public void OnHealthChanged() {
        float currentHealth = syncPropertyAgent.GetPropertyWithName(healthKey).GetFloatValue();

        if (currentHealth == 0 && OnDeath != null && networkID.IsMine)
            OnDeath.Invoke();

        if (abovePlayerText != null)
            abovePlayerText.text = currentHealth.ToString();

        if (uiText != null)
            uiText.text = currentHealth.ToString();
    }
}