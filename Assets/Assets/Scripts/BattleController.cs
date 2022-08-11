using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;


    public string text;
    public Text textGame;
    public int idFase;

    private Transform trainer;
    private Transform pokemonPlayer;
    private Transform posA;
    private Transform posB;


    void Awake()
    {
        instance = this;

        trainer = GameObject.Find("PokemonTrainer").transform;
        pokemonPlayer = PokemonPlayer.instance.transform;
        posA = GameObject.Find("posA").transform;
        posB = GameObject.Find("posB").transform;
    }

    void Start()
    {
        idFase = 0;
        text = "Wild " + PokemonEnemy.instance.namePokemonEnemy + " appeared!";
        StartCoroutine("Dialogue", text);
    }

    void Update()
    {
        if(idFase == 2)
        {
            trainer.GetComponent<Animator>().SetBool("Throwing", true);

            float step = 2 * Time.deltaTime;
            trainer.position = Vector3.MoveTowards(trainer.position, posB.position, step);
            pokemonPlayer.position = Vector3.MoveTowards(pokemonPlayer.position, posA.position, step);
        }
    }

    public IEnumerator Dialogue(string coroutineText)
    {
        int letter = 0;
        textGame.text = "";

        while (letter <= coroutineText.Length - 1)
        {
            textGame.text += coroutineText[letter];
            letter += 1;
            yield return new WaitForSeconds(0.05f);
        }

        idFase += 1;

        yield return new WaitForSeconds(1);

        switch (idFase)
        {
            case 1:
                text = "Go! " + PokemonPlayer.instance.namePokemonPlayer + "!";
                StartCoroutine("Dialogue", text);
                break;
        }
    }
}
