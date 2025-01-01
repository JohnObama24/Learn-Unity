using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class Dialogue : MonoBehaviour
{
    public GameObject DialogueCanvas; 
    public GameObject interactionUI; 
    public TMP_Text dialogText; 
    public TMP_Text Nama; 
    public TMP_Text Objektif; 
    public string[] Names; 
    public bool sudahBicara = false;

    // public bool NpcSebelumnya = false;

    // public int idMessage;
    public lihat nengok;
    public GameObject npcModel; 
    public float jarakRaycast = 3f;
    public GameObject Player;


    public float typeSpeed = 0.05f; 

    [System.Serializable]
    public class DataDialog
    {
        public string[] lineDialog; 
    }

    public DataDialog datadialog; 
    public int dialogCount = 0; 

    private int Index = 0;
    private bool pemainDekat = false; 
    private Coroutine typingCoroutine; 
    private bool sedangBerbicara = false; 

    void Start()
    {
        DialogueCanvas.SetActive(false); 
        interactionUI.SetActive(false); 
        nengok = GetComponentInChildren<lihat>();
        // Gerakan = gameObject.GetComponent<movement>();
        // Movement.enabled = true;
        // nengok.kameraNengok();
    }

  void Update()
    {
        Ray ray = new Ray(Player.transform.position, Player.transform.forward);
        RaycastHit hit;

        if (sudahBicara) {
            interactionUI.SetActive(false);
        }
        if (!DialogueCanvas.activeSelf && Physics.Raycast(ray, out hit, jarakRaycast))
        {
            if (!sedangBerbicara && hit.collider.gameObject == npcModel && !sudahBicara)
            {
                interactionUI.SetActive(true);
                pemainDekat = true;

                if (!sedangBerbicara && Input.GetKeyUp(KeyCode.E))
                {
                    if (!DialogueCanvas.activeSelf)
                    {
                        StartDialog();
                        nengok.playerNengok = true;
                    }
                    else
                    {
                        Next();
                    }
                }
            }
        }
        else
        {
            interactionUI.SetActive(false);
            pemainDekat = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Next();
        }

        if (dialogCount >= 3)
        {
            Objektif.text = "PERGI BOBOK";
        }
    }

    void StartDialog()
    {
  
            sedangBerbicara = true; 
            DialogueCanvas.SetActive(true); 
            interactionUI.SetActive(false); 
            Index = 0; 
            Nama.text = Names[Index];
            Player.GetComponent<movement>().enabled = false; 
            nengok.playerNengok = true;
            ShowLineWithTyping(); 
 

    }

    void Next()

    {
        Index++;
        if (Index < datadialog.lineDialog.Length)
        {
             Nama.text = Names[Index];
            ShowLineWithTyping(); 
        }
        else
        {
            EndDialog(); 
        }

    }

    void ShowLineWithTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Hentikan coroutine sebelumnya jika ada
        }
        typingCoroutine = StartCoroutine(TextType(datadialog.lineDialog[Index])); // Tampilkan dialog dengan efek mengetik
    }

    IEnumerator TextType(string line)
    {
        dialogText.text = ""; 
        foreach (char c in line.ToCharArray())
        {
            dialogText.text += c; 
            yield return new WaitForSeconds(typeSpeed);
        }
        typingCoroutine = null;
    }

    void EndDialog()
    {
        sedangBerbicara = false; 
        DialogueCanvas.SetActive(false); 
        dialogCount++;
        sudahBicara = true;
        Player.GetComponent<movement>().enabled = true;
        // NpcSebelumnya = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            pemainDekat = true; 
            interactionUI.SetActive(true); 

            if (nengok != null) {
                nengok.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            pemainDekat = false; 
            interactionUI.SetActive(false); 

                if (nengok != null) {
                nengok.enabled = false;
                // nengok.ResetRotate();
                nengok.ResetRotation();
            }
        }
    }
}
