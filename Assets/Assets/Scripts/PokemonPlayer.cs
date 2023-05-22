using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonPlayer : MonoBehaviour
{
    public static PokemonPlayer instance;

    private Transform   xpBarSize;
    private Transform   hpBarSize;
    private Vector3     vector3;
    private string      actionPokemonPlayer;
    private int         idCommand;
    private int         hit;

    public PokemonPlayerObject  pokemonPlayerObject;
    public PokemonEnemyObject pokemonEnemyObject;
    public float                PercentagePokemonPlayer;
    public int                  idPhase;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetupPokemonPlayer();
    }

    void SetupPokemonPlayer()
    {
        pokemonPlayerObject.currentLifePP = pokemonPlayerObject.maxLifePP;

        BarHPPokemonPlayer();
        BarXPPokemonPlayer();
    }

    void BarHPPokemonPlayer()
    {
        hpBarSize = GameObject.Find("PokemonPlayerHP").transform;

        PercentagePokemonPlayer = pokemonPlayerObject.currentLifePP / pokemonPlayerObject.maxLifePP;
        vector3 = hpBarSize.localScale;
        vector3.x = PercentagePokemonPlayer;
        hpBarSize.localScale = vector3;
    }

    void BarXPPokemonPlayer()
    {
        xpBarSize = GameObject.Find("PokemonPlayerXP").transform;

        PercentagePokemonPlayer = pokemonPlayerObject.currentExpPP / 100f;
        vector3 = xpBarSize.localScale;
        vector3.x = PercentagePokemonPlayer;
        xpBarSize.localScale = vector3;
    }

    public void TakeDamage(int hit)
    {
        pokemonPlayerObject.currentLifePP -= hit;

        if (pokemonPlayerObject.currentLifePP < 0)
        {
            pokemonPlayerObject.currentLifePP = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        BarHPPokemonPlayer();
    }

    public void SkillsPokemon()
    {
        string[] buttonNames = { "TextSkillA", "TextSkillB", "TextSkillC", "TextSkillD" };

        for (int i = 0; i < buttonNames.Length; i++)
        {
            GameObject buttonObject = GameObject.Find(buttonNames[i]);
            buttonObject.GetComponent<Text>().text = pokemonPlayerObject.skillsNamePP[i];
        }
    }

    public void InitPokemonPlayerCommand()
    {
        actionPokemonPlayer = "What will " + pokemonPlayerObject.namePP + " do?";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        idPhase = 3;
    }

    public IEnumerator GainXP(int amountXP)
    {
        actionPokemonPlayer = pokemonPlayerObject.namePP + " gained " + amountXP + " EXP. Points!";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        pokemonPlayerObject.currentExpPP += amountXP;

        BarXPPokemonPlayer();

        yield return new WaitForSeconds(1);
    }

    public IEnumerator Command(int idAction)
    {
        switch (idAction)
        {
            case 0:
                idCommand = 0;
                actionPokemonPlayer = "";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;

            case 1:
                idCommand = 1;
                actionPokemonPlayer = "";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;

            case 2:
                idCommand = 2;
                actionPokemonPlayer = "";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;

            case 3:
                idCommand = 3;
                actionPokemonPlayer = "";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;
        }

        idPhase = 1;

        return null;
    }

    public IEnumerator Dialogue(string coroutineText)
    {
        if (!string.IsNullOrEmpty(coroutineText))
        {
            int letter = 0;
            UIController.instance.textGame.text = "";

            while (letter <= coroutineText.Length - 1)
            {
                UIController.instance.textGame.text += coroutineText[letter];
                letter += 1;
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(1);

        switch (idPhase)
        {
            case 1:
                StartCoroutine("DealDamage");
                break;

            case 2:
                PokemonEnemy.instance.StartCoroutine("InitAction");
                break;

            case 3:
                UIController.instance.menuA.SetActive(true);
                break;

            case 4:
                actionPokemonPlayer = "Foe " + pokemonEnemyObject.name + " fainted!";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                idPhase = 5;
                break;

            case 5:
                StartCoroutine("GainXP", pokemonEnemyObject.defeatExpPE);
                idPhase = 6;
                break;
        }
    }

    public IEnumerator DealDamage()
    {
        GameObject tempPrefab = Instantiate(pokemonPlayerObject.animationsSkillsPP[idCommand]) as GameObject;
        tempPrefab.transform.position = PokemonEnemy.instance.transform.position;

        hit = Random.Range(1, pokemonPlayerObject.damagesSkillsPP[idCommand] + 1);
        actionPokemonPlayer = pokemonPlayerObject.namePP + " used " + pokemonPlayerObject.skillsNamePP[idCommand] + "!";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        yield return new WaitForSeconds(1);
        PokemonEnemy.instance.TakeDamage(hit);
        Destroy(tempPrefab);

        if (pokemonEnemyObject.currentLifePE <= 0)
        {
            idPhase = 4;
        }
        else
        {
            idPhase = 2;
        }
    }
}
