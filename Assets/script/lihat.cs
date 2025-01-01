using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lihat : MonoBehaviour
{

    public float speed = .01f;
    Quaternion rotgoal;
    Vector3 dir;
    public bool liat1;
    public bool liat2;

    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;

    private Quaternion initialRotate;
    private Coroutine resetCoroutine;

    public bool playerNengok;

    public Transform kameraPlayer;




    // Start is called before the first frame update
    void Start()
    {
        initialRotate = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(liat1 == true) {
            dir= (target.position - transform.position).normalized;
            rotgoal = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotgoal, speed);
            
        }
        if(liat2 == false) {
            dir= (target2.position - transform.position).normalized;
            rotgoal = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotgoal, speed);
        }
         if(playerNengok) {
            dir= (target2.position - kameraPlayer.transform.position).normalized;
            rotgoal = Quaternion.LookRotation(dir);
            kameraPlayer.transform.rotation = Quaternion.Slerp(kameraPlayer.transform.rotation, rotgoal, speed);

            // kameraPlayer.gameObject.GetComponent<movement>().enabled = false;
        }
        

    }
   public void ResetRotation()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine); // Hentikan coroutine yang sedang berjalan
        }

        resetCoroutine = StartCoroutine(ResetRotationCoroutine());
    }


    // public void kameraNengok() {
    //     if(playerNengok) {
    //         dir= (target2.position - kameraPlayer.transform.position).normalized;
    //         rotgoal = Quaternion.LookRotation(dir);
    //         kameraPlayer.transform.rotation = Quaternion.Slerp(kameraPlayer.transform.rotation, rotgoal, speed);

    //         kameraPlayer.gameObject.GetComponent<movement>().enabled = false;
    //     }
    // }

    private IEnumerator ResetRotationCoroutine()
    {
        while (Quaternion.Angle(transform.rotation, initialRotate) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotate, speed);
            yield return null;
        }

        transform.rotation = initialRotate; 
        resetCoroutine = null;
    }

}
