using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;

    public string text;

    public int idPhase;

    private Transform pokemonPlayer;
    private Transform trainer; 
    private Transform posA; 
    private Transform posB; 

    void Awake()
    {
        instance = this;

        pokemonPlayer = PokemonPlayer.instance.transform;

        trainer = GameObject.Find("PokemonTrainer").transform;
        posA = GameObject.Find("posA").transform;
        posB = GameObject.Find("posB").transform;
    }

    void Start()
    {
        idPhase = 0; 
        text = "Wild " + ScriptableObjectPE.instance.currentPokemonEnemyObject.namePE + " appeared!"; 
        StartCoroutine("Dialogue", text); 
    }

    void FixedUpdate()
    {
        if (idPhase == 1)
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
        UIController.instance.textGame.text = "";

        while (letter <= coroutineText.Length - 1)
        {
            UIController.instance.textGame.text += coroutineText[letter];
            letter += 1;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        idPhase += 1;

        switch (idPhase)
        {
            case 1:
                text = "Go! " + ScriptableObjectPP.instance.currentPokemonPlayerObject.namePP + "!";
                StartCoroutine("Dialogue", text);
                break;

            case 2:
                PokemonPlayer.instance.InitPokemonPlayerCommand();
                trainer.GetComponent<Animator>().SetBool("Throwing", false);
                break;
        }
    }

    public void Fight()
    {
        UIController.instance.menuA.SetActive(false);
        UIController.instance.menuB.SetActive(true);
        PokemonPlayer.instance.SkillsPokemon();
    }

    public void Command(int idCommand)
    {
        UIController.instance.menuB.SetActive(false);
        PokemonPlayer.instance.StartCoroutine("Command", idCommand);
    }
}
