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
    public float PercentagePokemonPlayer;

    public string[] skillsPokemonPlayer;
    public int[] damageSkillsPokemonPlayer;
    public GameObject[] animations;

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

        PercentagePokemonPlayer = hpPokemonPlayer / hpMaxPokemonPlayer;
        vector3 = hpBarSize.localScale;
        vector3.x = PercentagePokemonPlayer;
        hpBarSize.localScale = vector3;

        PercentagePokemonPlayer = xpPokemonPlayer / 100f;
        vector3 = xpBarSize.localScale;
        vector3.x = PercentagePokemonPlayer;
        xpBarSize.localScale = vector3;
    }

    public void TakeDamage(int hit)
    {
        hpPokemonPlayer -= hit;

        if (hpPokemonPlayer < 0)
        {
            hpPokemonPlayer = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        PercentagePokemonPlayer = hpPokemonPlayer / hpMaxPokemonPlayer;
        vector3 = hpBarSize.localScale;
        vector3.x = PercentagePokemonPlayer;
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
                actionPokemonPlayer = "Foe " + PokemonEnemy.instance.namePokemonEnemy + " fainted!";
                StartCoroutine("Dialogue", actionPokemonPlayer);
                idPhase = 5;
                break;

            case 5:
                StartCoroutine("GainXP", PokemonEnemy.instance.xpPokemonEnemy);
                idPhase = 6;
                break;
        }
    }

    public IEnumerator DealDamage()
    {
        GameObject tempPrefab = Instantiate(animations[idCommand]) as GameObject;
        tempPrefab.transform.position = PokemonEnemy.instance.transform.position;

        hit = Random.Range(1, damageSkillsPokemonPlayer[idCommand]);
        actionPokemonPlayer = namePokemonPlayer + " used " + skillsPokemonPlayer[idCommand] + "!";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        yield return new WaitForSeconds(1);
        PokemonEnemy.instance.TakeDamage(hit);
        Destroy(tempPrefab);

        if (PokemonEnemy.instance.hpPokemonEnemy <= 0)
        {
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
        actionPokemonPlayer = namePokemonPlayer + " gained " + amountXP + " EXP. Points!";
        StartCoroutine("Dialogue", actionPokemonPlayer);
        xpPokemonPlayer += amountXP;

        PercentagePokemonPlayer = xpPokemonPlayer / 100f;
        vector3 = xpBarSize.localScale;
        vector3.x = PercentagePokemonPlayer;
        xpBarSize.localScale = vector3;

        yield return new WaitForSeconds(1);
    }
}
