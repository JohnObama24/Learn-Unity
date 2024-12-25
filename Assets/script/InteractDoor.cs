using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour
{   

    public Transform Tujuan;
    public GameObject InteractionUI;
    private bool isPlayerNearby = false;
    public bool sudahMasuk = true;

    // Start is called before the first frame update
    void Start()
    {
         if (InteractionUI != null)
        {
            InteractionUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerNearby && Input.GetKey(KeyCode.E)) {
            Teleport();
        }

    }


    private void Teleport() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) {
            if(Tujuan != null) {
                player.transform.position = Tujuan.position;
            }
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (InteractionUI != null)
            {
                InteractionUI.SetActive(true); // Tampilkan UI// Set pesan
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (InteractionUI != null)
            {
                InteractionUI.SetActive(false); // Sembunyikan UI
            }
        }
    }
}
