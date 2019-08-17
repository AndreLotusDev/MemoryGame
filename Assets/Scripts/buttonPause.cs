using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPause : MonoBehaviour
{
    private int Control;

    [SerializeField] private GameObject panel; //Nao utilizada

    [SerializeField] private CanvasGroup Deact; //Serve para ativar e desativar o painel
    void Start()
    {
        Control = 1;
        //print(Time.timeScale);
    }

    void Update()
    {
        
    }

    public void btnClick() //Serve como uma valvula inversosa - Poderia ter sido feita com ! Atenção á boas práticas 
    {
        if (Control == 1)
        {
            Time.timeScale = 1;
            Deact.alpha = 0;
            Deact.blocksRaycasts = false; //bloqueia qualquer input do painel
        }
        else if (Control == -1)
        {
            Time.timeScale = 0;
            Deact.alpha = 1;
            Deact.blocksRaycasts = true; //restaura as configurações do painel
        }
        Control *= -1;

        //print(Time.timeScale);
    }
}
