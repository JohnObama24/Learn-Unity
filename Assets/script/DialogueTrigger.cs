using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas; // Canvas untuk dialog
    public Dialogue dialog; // Komponen Dialogue NPC ini
    public GameObject NpcSebelumnya; // NPC sebelumnya (opsional untuk NPC pertama)

    void Start()
    {
        dialog.GetComponent<Dialogue>().enabled = false; // Nonaktifkan dialog NPC saat awal
        dialogueCanvas.SetActive(false); // Sembunyikan canvas dialog
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (NpcSebelumnya == null || NpcSebelumnya.GetComponent<Dialogue>().sudahBicara)
            {
                dialog.GetComponent<Dialogue>().enabled = true; 
            }
            else
            {
                Debug.Log("Selesaikan dialog dengan NPC sebelumnya terlebih dahulu!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialog.GetComponent<Dialogue>().enabled = false; // Nonaktifkan dialog

        }
    }
}
