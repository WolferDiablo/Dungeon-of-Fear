using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    private EnemyHealth enemyHealthScript;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float sphereRadius;

    private GameObject player;
    NavMeshAgent enemyNavMeshAgent;

    private float attackAgain;
    private PlayerHealth playerHealth;

    [SerializeField] bool readyToAttack;
    bool attackingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        readyToAttack = true;

        enemyHealthScript = GetComponent<EnemyHealth>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealthScript.isAttacked) {
            if(Physics.CheckSphere(transform.position, sphereRadius, playerLayerMask) && attackingPlayer == false) MoveToPlayer();
            if (Physics.CheckSphere(transform.position, 2f, playerLayerMask)) {
                attackingPlayer = true;
                enemyNavMeshAgent.isStopped = true;
                enemyNavMeshAgent.velocity = Vector3.zero;
               AttackPlayer(); 
            }
            else if(Physics.CheckSphere(transform.position, 2f, playerLayerMask) == false) attackingPlayer = false; 
        }
        if(attackAgain <= 2.48 && readyToAttack == false) {
            attackAgain += Time.deltaTime;
        } else if (attackAgain >= 2.48) {
            attackAgain = 0;
            readyToAttack = true;
        }
        if(playerHealth.isDead == true) {
           enemyHealthScript.isAttacked = false; 
           enemyHealthScript.ResetHealth();
        } 
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
    
    void AttackPlayer() {
        if(readyToAttack && attackingPlayer) {
          playerHealth.updateHealth(Random.Range(20,30));
          readyToAttack = false;
        }
    }

    void MoveToPlayer() {
        enemyNavMeshAgent.isStopped = false;
        enemyNavMeshAgent.SetDestination(player.transform.position);
    }
}
