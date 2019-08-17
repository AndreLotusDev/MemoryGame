using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointsDelet : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D obj){
        if(obj.CompareTag("card") || Randomizer.liberaReset == true){
            print("foi");
            Destroy(obj);
        }
    }

}
