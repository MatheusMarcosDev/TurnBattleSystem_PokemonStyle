using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;


    public string text;
    public Text textGame;
    public int idPhase;

    private Transform trainer;
    private Transform pokemonPlayer;
    private Transform posA;
    private Transform posB;
    public GameObject menuA;
    public GameObject menuB;


    void Awake()
    {
        instance = this;

        trainer = GameObject.Find("PokemonTrainer").transform;
        pokemonPlayer = PokemonPlayer.instance.transform;
        posA = GameObject.Find("posA").transform;
        posB = GameObject.Find("posB").transform;

        menuA.SetActive(false);
        menuB.SetActive(false);
    }

    void Start()
    {
        idPhase = 0;
        text = "Wild " + PokemonEnemy.instance.namePokemonEnemy + " appeared!";
        StartCoroutine("Dialogue", text);
    }

    void Update()
    {
        if(idPhase == 1)
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

        yield return new WaitForSeconds(1);

        idPhase += 1;

        switch (idPhase)
        {
            case 1:
                text = "Go! " + PokemonPlayer.instance.namePokemonPlayer + "!";
                StartCoroutine("Dialogue", text);
                break;

            case 2:
                PokemonPlayer.instance.InitPokemonPlayerCommand();
                break;
        }
    }

    public void Fight()
    {
        menuA.SetActive(false);
        menuB.SetActive(true);
        PokemonPlayer.instance.SkillsPokemon();
    }

    public void Command(int idCommand)
    {
        menuB.SetActive(false);
        PokemonPlayer.instance.StartCoroutine("Command", idCommand);
    }
}
