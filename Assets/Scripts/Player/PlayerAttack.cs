using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAttack : MonoBehaviour
{

    [Header("Mana Bar Variables")]
    public Slider manaSlider;
    private float mana;
    private float manaMax = 200;

    public int manaRegenMultiplier;

    private int manaToCastFireball = 20;

    [Header("Castbar Variables")]
    public Slider castbarSlider;
    private float timeToCast = 0.4f;
    public GameObject castBarParent;
    [SerializeField] private TextMeshProUGUI manaBarText;

    bool isCasting;

    [Header("Damage Variables")]
    public float fireBallDamage;
    private PlayerMovement playerMovement;

    [Header("Tab Target")]
    public EnemyHealth enemyTargeted = null;
    public bool isEnemyTargeted;
    Vector3 initialRotation;
    public bool rotateToTarget;

    [Header("Floating Damage References")]
    [SerializeField] private GameObject floatingText;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        mana = manaMax;
        manaSlider.maxValue = manaMax;
        manaSlider.value = mana;
    }

    // Update is called once per frame
    void Update()
    {

        manaSlider.value = mana;
        manaBarText.text = Mathf.RoundToInt(mana).ToString();

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.CompareTag("Enemy") && isCasting == false) {
                enemyTargeted = hitInfo.transform.gameObject.GetComponent<EnemyHealth>();
                isEnemyTargeted = true;
                enemyTargeted.isTargeted = true;
                enemyTargeted.updateHealthBar();
                Debug.Log("You clicked on an enemy!");
            }
        }

        if (enemyTargeted != null) {
          isEnemyTargeted = true;
          if (enemyTargeted.isActiveAndEnabled == false) {
            isEnemyTargeted = false;
            rotateToTarget = false;
            enemyTargeted = null;
          }
        } 
       
        if (Input.GetKeyDown(KeyCode.Escape) && enemyTargeted != null) {
            if (isCasting == true) CancelEverything();
            else if (isCasting == false && enemyTargeted != null) {
                enemyTargeted.isTargeted = false;
                enemyTargeted = null;
                isEnemyTargeted = false;
            }
        }

        // Mana Regen
        if (mana < manaMax) mana += (Time.deltaTime * manaRegenMultiplier);

        // Main Fireball Attack
        if(Input.GetKeyDown(KeyCode.Alpha1) && mana > manaToCastFireball && isCasting == false && playerMovement.isMoving == false) {
            if (isEnemyTargeted == true) {
                isCasting = true;

                castBarParent.SetActive(true);
                castbarSlider.value = 0;
                castbarSlider.maxValue = timeToCast;

                mana -= manaToCastFireball;

                //initialRotation = new Vector3(enemyTargeted.transform.position.x, transform.position.y, enemyTargeted.transform.position.z);
                //rotateToTarget = true;

                Invoke("CastFireball", timeToCast);
            } else Debug.Log("You do not have an enemy targeted Dumbass");
            
        }

        if(isEnemyTargeted) gameManager.isTargetedEnemy = true;
        else gameManager.isTargetedEnemy = false;

        if(isCasting == true && playerMovement.isMoving == true) CancelEverything();

        //if(rotateToTarget == true) {
        //    transform.LookAt(initialRotation);
        //}

        if(isCasting == true && castbarSlider.value < timeToCast) castbarSlider.value += Time.deltaTime;
    }

    void CastFireball() {

        enemyTargeted.takeDamage(Mathf.RoundToInt(fireBallDamage));

        if(floatingText) {
            GameObject newFloatingText = Instantiate(floatingText, new Vector3 (transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(0, 1), transform.position.z), Quaternion.identity);
            newFloatingText.GetComponentInChildren<TextMeshProUGUI>().text = fireBallDamage.ToString();
            Destroy(newFloatingText, 0.4f);
        }

        isCasting = false;
        castBarParent.SetActive(false);
    }

    void CancelEverything() {
        CancelInvoke();
        isCasting = false;
        castBarParent.SetActive(false);
    }
}
