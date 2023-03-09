using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject treeVisual;
    [SerializeField] private Animator treeVisualAnimator;

    private RespawnPoint respawnPoint;
    private Transform treeSpawnPoint;
    private bokidController playerController;
    private bool onSoil;
    private bool grown = false;

    // Start is called before the first frame update
    void Start()
    {
        treeVisualAnimator = treeVisual.GetComponent<Animator>();
        treeSpawnPoint = transform.Find("SpawnPoint");
        respawnPoint = RespawnPoint.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (onSoil)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
//Debug.Log("Grow to the heavens!");
                if (!grown) {
                    Vector2 position = playerController.transform.position;
                    position.x = treeSpawnPoint.position.x;
                    playerController.transform.position = position;
                    treeVisualAnimator.SetTrigger("Grow");
                    respawnPoint.transform.position = treeSpawnPoint.position;
                    grown = true;
                }
                else {
                    playerController.transform.position = treeSpawnPoint.position;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player"))
        {
//Debug.Log("On Soil");
            onSoil = true;

            if (!playerController) {
                playerController = collider.GetComponent<bokidController>();
            }

            if (MessageUI.instance) {
                if (!grown) {
                    MessageUI.SetPersistentMessage("Press 'E' to grow tree");
                }
                else {
                    MessageUI.SetPersistentMessage("Press 'E' to ascend tree");
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player")))
        {
//Debug.Log("Off Soil");
            onSoil = false;

            if (MessageUI.instance) {
                MessageUI.ClearPersistentMessage();
            }
        }
    }

	private void OnDrawGizmos() {
        if (!treeSpawnPoint) {
            treeSpawnPoint = transform.Find("SpawnPoint");
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(treeSpawnPoint.position, 0.1f);
	}
}
