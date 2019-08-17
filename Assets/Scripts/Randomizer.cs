using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Randomizer : MonoBehaviour
{
    //----------------------------------------------------------------------------------------
    [SerializeField] private Transform cardCenter; //A carta que fica em cima
    [SerializeField] private Transform[] Pos = new Transform[9]; //Aqui eu posiciono todos meus transforms para referenciar a Cena

    [SerializeField] private GameObject[] Cards = new GameObject[9]; //Aqui eu posiciono as cartas

    private int[] holdNumber = new int[9];

    private GameObject teste;

    //----------------------------------------------------------------------------------------

    private int number; //Estoca o número aleátorio da carta

    private int Ignite = 0; //Faz o príncipio começar

    private int lastnumber; //Esse é o ultimo numero aleatorio

    private float lefTime = 5; //Tempo restante para executar

    private int repeat = 1; //Numero de vezes que vai se repetir

    private int actualCard; //Numero da carta instanciada;

    private int Indice; //Para a lista

    [SerializeField] private Text winorLose; //Aqui ele cria um texto de você perdeu ou ganhou

    static public Text globalwinorLose;

    //----------------------------------------------------------------------------------------Aqui ficam as variaveis para evitar repetição
    public int numeroSorteado;

    public List<int> numerosJaSorteados = new List<int>(); //Lista de numeros já sorteado

    public List<int> TabelaNumeros = new List<int>(); //Lista que instancia com o array

    //----------------------------------------------------------------------------------------Tampa as cartas

    [SerializeField] private GameObject white_cardOBJ; //A carta branca

    private bool whiteCard = false;

    private bool unlockNew = true;

    //----------------------------------------------------------------------------------------TEXTO CONTROLLER
    static public bool UnlockText = false;
    static public bool EndProcess = false;
    //----------------------------------------------------------------------------------------
    private int sizelist; //Controla o tamanho da lista
    static public bool unlockDestroy = false;

    public bool Destroct;
    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------

    static public int numberCardsAcc;
    [SerializeField] private Text displayAcc;

    static public bool liberaReset = false;

    private int controlaup = 0;
    void Start()
    {
        //print(Pos[0].transform.position);
        //print(numerosJaSorteados.Count);  
        Destroct = unlockDestroy;

        globalwinorLose = winorLose;
    }

    // Update is called once per frame
    void Update()
    {


        if (Ignite == 0) //Confere se as cartas nunca foram inicializadas
        {

            for (int i = 0; i < Pos.Length; i++)
            {
                number = Random.Range(0, 9);
                Instantiate(Cards[number], new Vector3(Pos[i].position.x, Pos[i].position.y, Pos[i].position.z), Quaternion.identity);
                holdNumber[i] = number; //Aqui guarda quais cartas foram criadas
                TabelaNumeros.Add(holdNumber[i]);
                if (i == 8)
                {

                    Ignite = 1; //Termina á entrega de cartas
                    lastnumber = Random.Range(0, 9);
                }
            }

        }

        //Tempo para aparecer as cartas que eu tenho q "clicar"
        lefTime -= Time.deltaTime;

        if (lefTime <= 0 && repeat == 1)
        {


            if (sizelist < 9)
            {
                lefTime = 0;
                number = Random.Range(0, TabelaNumeros.Count); // 0 ate o final da tabela
                Indice = TabelaNumeros[number]; //pega a posição da tabela e o respectivo numero e joga num Indice
                //Tava aqui
            }
            //print(number);
            if (sizelist != 9 && sizelist < 10)
            {
                Instantiate(Cards[Indice], new Vector2(cardCenter.position.x, cardCenter.position.y), Quaternion.identity);
                //Tentativa de mudar a tag do objeto



                actualCard = Indice + 1; //Aqui ele parelha com a variavel global das cartas
                repeat = 0;
                numerosJaSorteados.Add(Indice); //Ja marca o numero sorteado pra nao ser mais usado
                TabelaNumeros.Remove(TabelaNumeros[number]); //Remove da tabela instanciada com o Array
                sizelist = numerosJaSorteados.Count;
                //print(sizelist);
            }
            else if (sizelist == 9)
            {
                sizelist += 1;
            }
            //print(actualCard);
        }

        // -------------------Espera pra selecionar as cartas

        if (repeat == 0 && whiteCard == false)
        {
            StartCoroutine(Wait(2, 1));
            //print("Deu certo!");
            whiteCard = true;
        }


        //Aqui ele cria as cartas brancas para tampar as coloridas
        if (whiteCard == true && unlockNew == true)
        {
            for (int ninepos = 0; ninepos < Pos.Length; ninepos++)
            {
                Instantiate(white_cardOBJ, new Vector2(Pos[ninepos].position.x, Pos[ninepos].position.y), Quaternion.identity);
            }
            whiteCard = false;
            unlockNew = false;
            EndProcess = true;

        }

        if (actualCard == numberCard.numCard && UnlockText == true)
        {
            winorLose.text = "You Win";
        }
        else if (actualCard != numberCard.numCard && UnlockText == true)
        {
            winorLose.text = "You Lose";
            liberaReset = true;
            SceneManager.LoadScene(0);
            Ignite = 0;
        }

        if(Input.GetKeyUp(KeyCode.R)){
            SceneManager.LoadScene(0);
            Ignite = 0;
        }

        /*/if(Input.GetKeyUp(KeyCode.E) && TabelaNumeros.Count > 0){
            RandomNumber();
        }*/

        if (UnlockText == true)
        {
            switch (winorLose.text)
            {
                case "You Win":
                    lefTime = 1;
                    repeat = 1;
                    UnlockText = false;
                    print("Win");
                    numberCardsAcc += 1;

                    break;
                case "You Lose":
                    lefTime = 0;
                    repeat = 0;
                    UnlockText = false;
                    print("Lose");

                    break;
            }
        }

        displayAcc.text = numberCardsAcc.ToString();

        if(numberCardsAcc == 9){
            numberCardsAcc = 0;
            SceneManager.LoadScene(0);
            Ignite = 0;
        }

        if(Input.GetKeyUp(KeyCode.Escape)){
            Application.Quit();
        }
    }

    void RandomNumber()
    { //Aqui ele vai deletar periodicamente os números 
        int indice = Random.Range(0, TabelaNumeros.Count); //Seleciona um numero aleatorio da tabela
        numeroSorteado = TabelaNumeros[indice]; //pega o numero sorteado e joga numa variavel
        numerosJaSorteados.Add(numeroSorteado); //Essa mesma variavel adiciona a lista de numeros sorteados
        TabelaNumeros.Remove(TabelaNumeros[indice]); //E ja apaga da tabela original
    }

    IEnumerator Wait(int time, int executions)
    {
        for (int i = 0; i < executions; i++)
        {
            yield return new WaitForSeconds(time);

        }

    }
}
