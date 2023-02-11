using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("Currency")]
    public int currency;
    public TextMeshProUGUI currencyText;

    [Header("Enemy Variables")]
    public bool isTargetedEnemy;
    public GameObject enemySlider;
    public Vector3[] enemySpawns;
    public GameObject[] enemies;

    [Header("Customize Character References")]
    public GameObject inGameUI;
    public GameObject customizingUI;
    public GameObject cam;
    public GameObject mainCamera;
    public GameObject customizingCam;
    bool isCustomizing;

    public int numberOfEnemies;

    public Rigidbody playerRB;
    public GameObject player;
    public Transform spawnPoint;

    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    
    private enum BottomCustomize {Cynlinder, Cube, Sphere};
    private BottomCustomize bottomCustomize;

    [Header("Bottom Customizing")]
    public GameObject bottomCylinder;
    public GameObject bottomCube;
    public GameObject bottomSphere;

    [Header("Other References")]
    public GameObject chest;
    public Transform safeHavenSpawn;
    bool chestActivated;
    public GameObject deathScreen;

    void Start() {
        isCustomizing = true;
        enemySpawns = new Vector3[numberOfEnemies];
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemie in enemies) {
            for (int i = 0; i < enemySpawns.Length; i++) {
                if (enemySpawns[i] == Vector3.zero) {
                    enemySpawns[i] = enemie.transform.position;
                    break;
                }
            }
        }
    }

    private void Update () {
        currencyText.text = currency.ToString();
        if(isTargetedEnemy) enemySlider.SetActive(true);
        else enemySlider.SetActive(false);

        if(isCustomizing) {
            inGameUI.SetActive(false);
            customizingUI.SetActive(true);
            cam.SetActive(false);
            mainCamera.SetActive(false);
            playerAttack.enabled = false;
            playerMovement.enabled = false;
            customizingCam.SetActive(true);

            playerRB.useGravity = false;

        } else {
            mainCamera.SetActive(true);
            customizingCam.SetActive(false);
            inGameUI.SetActive(true);
            customizingUI.SetActive(false);
            cam.SetActive(true);
            playerAttack.enabled = true;
            playerMovement.enabled = true;

            playerRB.useGravity = true;

        }

        if(numberOfEnemies == 0 && chestActivated == false)  {
            chest.SetActive(true);
            chestActivated = true;
        }
    }

    public void updateCurrency(int addCurrency) {
        currency += addCurrency;
    }

    public void LeftArrow() {
        if(bottomCustomize != BottomCustomize.Sphere) {
            bottomCustomize ++;
            SwitchBottoms();
        }
        
    }

    public void RightArrow() {
        if(bottomCustomize != BottomCustomize.Cynlinder) {
            bottomCustomize --;
            SwitchBottoms();
        }
    }

    void SwitchBottoms() {
        switch(bottomCustomize) {
            case BottomCustomize.Cynlinder:
                bottomCylinder.SetActive(true);
                bottomCube.SetActive(false);
                bottomSphere.SetActive(false);
                break;
            case BottomCustomize.Cube:
                bottomCylinder.SetActive(false);
                bottomCube.SetActive(true);
                bottomSphere.SetActive(false);
                break;
            case BottomCustomize.Sphere:
                bottomCylinder.SetActive(false);
                bottomCube.SetActive(false);
                bottomSphere.SetActive(true);
                break;
        }
    }

    public void FinishedCustomizing() {
        player.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 2, spawnPoint.transform.position.z);
        isCustomizing = false;
    }

    public void StartDungeonEnd() {
        Invoke("DungeonEnd", 5f);
    }

    void DungeonEnd() {
        player.transform.position = new Vector3(safeHavenSpawn.transform.position.x, safeHavenSpawn.transform.position.y + 2, safeHavenSpawn.transform.position.z);
        foreach (GameObject enemie in enemies) {
            enemie.SetActive(true);
            for(int i = 0; i < enemySpawns.Length; i++) {
                if(enemySpawns[i] != Vector3.zero) {
                    enemie.transform.position = enemySpawns[i];
                    enemySpawns[i] = Vector3.zero;
                    break;
                }
            }
        }

        foreach(GameObject enemie in enemies) {
            for (int i = 0; i < enemySpawns.Length; i++) {
                if (enemySpawns[i] == Vector3.zero) {
                    enemySpawns[i] = enemie.transform.position;
                    break;
                }
            }
            
        }
        numberOfEnemies = enemies.Length;
        chestActivated = false;
    }

    public void RespawnButton() {
        DungeonEnd();
        deathScreen.SetActive(false);
        playerAttack.enabled = true;
        playerMovement.enabled = true;
        playerHealth.health = playerHealth.healthMax * 0.5f;
        playerHealth.isDead = false;
        if (currency >= 20) currency -= 20;
        else currency -= currency;
    }
}
