using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonPlayer : MonoBehaviour
{
    public static PokemonPlayer instance;

    public string namePokemonPlayer;
    public int levelPokemonPlayer;
    public int xpPokemonPlayer;
    public float hpMaxPokemonPlayer;
    public float hpPokemonPlayer;
    public float hpPercentagePokemonPlayer;

    public string[] skillsPokemonPlayer;
    public int[] damageSkillsPokemonPlayer;

    private string actionPokemonPlayer;

    private Transform hpBarSize;
    private Vector3 vector3;

    private Transform xpBarSize;

    private GameObject buttonSkillA;
    private GameObject buttonSkillB;
    private GameObject buttonSkillC;
    private GameObject buttonSkillD;

    private int idCommand;
    private int hit;
    public int idPhase;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpPokemonPlayer = hpMaxPokemonPlayer;

        hpBarSize = GameObject.Find("PokemonPlayerHP").transform;
        xpBarSize = GameObject.Find("PokemonPlayerXP").transform;

        hpPercentagePokemonPlayer = hpPokemonPlayer / hpMaxPokemonPlayer;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonPlayer;
        hpBarSize.localScale = vector3;
    }

    public void TakeDamage(int hit)
    {
        hpPokemonPlayer -= hit;

        if (hpPokemonPlayer < 0)
        {
            hpPokemonPlayer = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        hpPercentagePokemonPlayer = hpPokemonPlayer / hpMaxPokemonPlayer;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonPlayer;
        hpBarSize.localScale = vector3;
    }

    public void SkillsPokemon()
    {
        buttonSkillA = GameObject.Find("TextSkillA");
        buttonSkillB = GameObject.Find("TextSkillB");
        buttonSkillC = GameObject.Find("TextSkillC");
        buttonSkillD = GameObject.Find("TextSkillD");

        buttonSkillA.GetComponent<Text>().text = skillsPokemonPlayer[0];
        buttonSkillB.GetComponent<Text>().text = skillsPokemonPlayer[1];
        buttonSkillC.GetComponent<Text>().text = skillsPokemonPlayer[2];
        buttonSkillD.GetComponent<Text>().text = skillsPokemonPlayer[3];
    }

    public IEnumerator Command(int idAction)
    {
        switch (idAction)
        {
            case 0:
                idCommand = 0;
                actionPokemonPlayer = " ";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;

            case 1:
                idCommand = 1;
                actionPokemonPlayer = " ";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;

            case 2:
                idCommand = 2;
                actionPokemonPlayer = " ";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;

            case 3:
                idCommand = 3;
                actionPokemonPlayer = " ";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                break;
        }
        idPhase = 1;

        return null;
    }

    public IEnumerator Dialogue(string coroutineText)
    {
        int letter = 0;
        BattleController.instance.textGame.text = "";

        while (letter <= coroutineText.Length - 1)
        {
            BattleController.instance.textGame.text += coroutineText[letter];
            letter += 1;
            yield return new WaitForSeconds(0.05f);
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
                BattleController.instance.menuA.SetActive(true);
                break;

            case 4:
                yield return new WaitForSeconds(1);
                StartCoroutine("GainedXP", PokemonEnemy.instance.xpPokemonEnemy);
                break;
        }
    }

    public IEnumerator DealDamage()
    {
        hit = Random.Range(1, damageSkillsPokemonPlayer[idCommand]);
        actionPokemonPlayer = namePokemonPlayer + " used " + skillsPokemonPlayer[idCommand] + "!";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        yield return new WaitForSeconds(1);
        PokemonEnemy.instance.TakeDamage(hit);

        if (PokemonEnemy.instance.hpPokemonEnemy <= 0)
        {
            actionPokemonPlayer = "Foe " + PokemonEnemy.instance.namePokemonEnemy + " fainted!";
            StartCoroutine("Dialogue", actionPokemonPlayer);
            idPhase = 4;
        }
        else
        {
            idPhase = 2;
        }
    }

    public void InitPokemonPlayerCommand()
    {
        actionPokemonPlayer = "What will " + PokemonPlayer.instance.namePokemonPlayer + " do?";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        idPhase = 3;
    }

    public IEnumerator GainXP(int amountXP)
    {
        actionPokemonPlayer = namePokemonPlayer + " gained" + amountXP + " EXP. Points!";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        idPhase = 5;

        yield return new WaitForSeconds(1);
    }
}
