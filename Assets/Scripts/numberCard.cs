using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberCard : MonoBehaviour
{
    static public int numCard; //Variavel Global
    private SpriteRenderer Cards;
    [SerializeField] private int SelectNum; //Seleciona o numero para o global
    [SerializeField]private int controlDelet;

    static public int numberForDelet;

    static public bool liberaDel = false;
    void Start()
    {

        Cards = GetComponent<SpriteRenderer>();

    }

    void OnMouseUp()
    {

        //SO LIBERA SE JA TIVER TERMINADO A CONTAGEM

        if (Randomizer.EndProcess == true)
        {
            numCard = this.SelectNum;
            Randomizer.UnlockText = true;
            numberForDelet = this.controlDelet;

            //print(this.SelectNum);

            Cards.sortingOrder = 2;

            liberaDel = true;
            /*/if (Randomizer.unlockDestroy == true)
            {
                Randomizer.unlockDestroy = false;
                Destroy(this.gameObject);
            }*/
        }

        if(Randomizer.unlockDestroy == true){
            StartCoroutine("WaitSomeTime");
        }

    }
}
