using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth;
    [SerializeField]
    private int enemyHealthMax = 200;

    public Gradient gradient;

    public bool isTargeted;
    public bool koolAid;
    public bool isAttacked;

    public GameObject containerObject;
    private ContainerStuffs containerScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyHealthMax;
        containerObject = GameObject.FindGameObjectWithTag("Container");
        containerScript = containerObject.GetComponent<ContainerStuffs>();
        containerScript.gameManager.numberOfEnemies ++;

    }

    void Update() {
        if (isTargeted == true && koolAid == false) {
            koolAid = true;
            containerScript.gameManager.isTargetedEnemy = true;
        } 
        else if (isTargeted == false && koolAid == true)  {
            containerScript.enemyHealthSlider.value = 0;
            koolAid = false;
            containerScript.gameManager.isTargetedEnemy = false;
        }

        if (enemyHealth <= 0) {
            enemyHealth = enemyHealthMax;
            deathEvent();
        } 
        if(enemyHealth < enemyHealthMax) isAttacked = true;
    }

    public void takeDamage(int damage) {
        if (enemyHealth != 0) {
            enemyHealth -= damage;
            containerScript.enemyHealthSlider.value = enemyHealth;
            containerScript.enemyFill.color = gradient.Evaluate(containerScript.enemyHealthSlider.normalizedValue);
        }
    }

    public void updateHealthBar() {
        containerScript.enemyHealthSlider.maxValue = enemyHealthMax;
        containerScript.enemyHealthSlider.value = enemyHealth;
        containerScript.enemyFill.color = gradient.Evaluate(containerScript.enemyHealthSlider.normalizedValue);
    }

    void deathEvent() {
        containerScript.gameManager.currency += Random.Range(2,7);
        containerScript.playerStats.updateXP(Random.Range(150,180));
        containerScript.gameManager.numberOfEnemies --;
        isAttacked = false;
        GetComponent<Outline>().enabled = false;
        gameObject.SetActive(false);
    }
    public void ResetHealth() { 
        enemyHealth = enemyHealthMax; 
        containerScript.gameManager.enemySlider.SetActive(false);
    }
}