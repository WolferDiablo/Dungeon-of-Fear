using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject interactKey;
    public ContainerStuffs containerStuffs;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            interactKey.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            interactKey.SetActive(false);
        }
    }

    public void Interacted() {
        containerStuffs.gameManager.currency += Random.Range(40,50);
        containerStuffs.gameManager.StartDungeonEnd();
        interactKey.SetActive(false);
        gameObject.SetActive(false);
    }
}
