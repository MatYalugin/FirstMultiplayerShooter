using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

public class Health : MonoBehaviour
{
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

    public GameObject healthBarGO;

    public Material auraMat;
    public Material playerMat;
    public bool isImmortal;

    public GameObject rifleHand1;
    public GameObject rifleHand2;
    public GameObject pistolHand;
    public GameObject knifeHand;
    public GameObject playerMesh;

    public Camera playerCamera;
    public void Start()
    {
        if (networkID.IsMine == true)
        {
            abovePlayerText.enabled = false;
        }
        isImmortal = true;
        Invoke("MakeNotImmortal", 3f);
        if (networkID.IsMine)
        {
            uiText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
            float currentHealth = healthMax;
            if (uiText != null)
                uiText.text = currentHealth.ToString();
        }

        if (syncPropertyAgent.GetPropertyWithName(healthKey).version == 0)
            syncPropertyAgent.Modify(healthKey, healthMax);

        if(networkID.IsMine == false)
        {
            healthBarGO.GetComponent<Canvas>().worldCamera = playerCamera;
        }
    }

    private void Update()
    {
        if (isImmortal)
        {
            float currentHealth = healthMax;
            if (uiText != null)
                uiText.text = currentHealth.ToString();

            rifleHand1.GetComponent<MeshRenderer>().material = auraMat;
            rifleHand2.GetComponent<MeshRenderer>().material = auraMat;
            pistolHand.GetComponent<MeshRenderer>().material = auraMat;
            knifeHand.GetComponent<MeshRenderer>().material = auraMat;
            playerMesh.GetComponent<MeshRenderer>().material = auraMat;
        }
        else
        {
            rifleHand1.GetComponent<MeshRenderer>().material = playerMat;
            rifleHand2.GetComponent<MeshRenderer>().material = playerMat;
            pistolHand.GetComponent<MeshRenderer>().material = playerMat;
            knifeHand.GetComponent<MeshRenderer>().material = playerMat;
            playerMesh.GetComponent<MeshRenderer>().material = playerMat;
        }
    }

    public void Damage(float value)
    {
        if (!isImmortal)
        {
            OnHealthChanged(-value);
        }
    }

    public void Heal(float value)
    {
        OnHealthChanged(value);
    }

    public void Kill()
    {
        OnHealthChanged(-healthMax);
    }

    public void Restore()
    {
        OnHealthChanged(healthMax);
    }

    private void OnHealthChanged(float healthDelta)
    {
        float currentHealth = syncPropertyAgent.GetPropertyWithName(healthKey).GetFloatValue();
        currentHealth = Mathf.Clamp(currentHealth + healthDelta, 0, healthMax);

        syncPropertyAgent.Modify(healthKey, currentHealth);
    }

    public void OnHealthChanged()
    {
        float currentHealth = syncPropertyAgent.GetPropertyWithName(healthKey).GetFloatValue();

        if (currentHealth == 0 && OnDeath != null && networkID.IsMine)
            OnDeath.Invoke();

        if (currentHealth == 0)
        {
            Invoke("MakeImmortalWithDelay", 0.1f);
            Invoke("MakeNotImmortal", 2f);
        }

        if (abovePlayerText != null)
            abovePlayerText.text = currentHealth.ToString();

        if (uiText != null)
            uiText.text = currentHealth.ToString();
    }
    public void MakeImmortalWithDelay()
    {
        isImmortal = true;
    }
    public void MakeNotImmortal()
    {
        isImmortal = false;
    }
}
