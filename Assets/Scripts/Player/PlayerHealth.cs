using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    [Header("Health Variables")]
    public float health;
    public float healthMax = 200;
    public float healthRegenerationMultiplier;

    [Header("Slider Variables")]
    public Slider sliderVar;
    public Image fillColor;
    public Gradient gradient;
    [SerializeField] TextMeshProUGUI healthBarText;

    public GameObject deathScreen;
    public GameObject inGameUI;

    public bool isDead;

    PlayerMovement playerMovement;
    PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
        sliderVar.maxValue = healthMax;
        sliderVar.value = health;

        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerAttack = gameObject.GetComponent<PlayerAttack>();

    }

    void Update() {
        healthBarText.text = Mathf.RoundToInt(health).ToString() + "/" + Mathf.RoundToInt(healthMax).ToString();
        sliderVar.value = health;
        fillColor.color = gradient.Evaluate(sliderVar.normalizedValue);
        if(health < healthMax && health != 0) {
            health += (Time.deltaTime * healthRegenerationMultiplier);
        }
        if(isDead) health = 0;
    }

    public void updateHealth(int damage) {
        health -= damage;
        if(health <= 0) death();
    }

    void death() {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        playerAttack.enemyTargeted = null;
        playerAttack.isEnemyTargeted = false;
        deathScreen.SetActive(true);
        isDead = true;
    }
}
