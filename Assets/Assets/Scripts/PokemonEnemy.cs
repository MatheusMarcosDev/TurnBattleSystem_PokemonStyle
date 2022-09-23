using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonEnemy : MonoBehaviour
{
    public static PokemonEnemy instance;

    public string namePokemonEnemy;
    public int levelPokemonEnemy;
    public int xpPokemonEnemy;
    public float hpMaxPokemonEnemy;
    public float hpPokemonEnemy;
    public float hpPercentagePokemonEnemy;

    public string[] skillsPokemonEnemy;
    public int[] damageSkillsPokemonEnemy;
    public GameObject[] animations;

    private string actionPokemonEnemy;

    private Transform hpBarSize;
    private Vector3 vector3;

    private int idCommand;
    private int hit;
    public int idPhase;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpPokemonEnemy = hpMaxPokemonEnemy;
        hpBarSize = GameObject.Find("PokemonEnemyHP").transform;
        hpPercentagePokemonEnemy = hpPokemonEnemy / hpMaxPokemonEnemy;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonEnemy;
        hpBarSize.localScale = vector3;
    }

    public void TakeDamage(int hit)
    {
        hpPokemonEnemy -= hit;

        if (hpPokemonEnemy <= 0)
        {
            hpPokemonEnemy = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        hpPercentagePokemonEnemy = hpPokemonEnemy / hpMaxPokemonEnemy;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonEnemy;
        hpBarSize.localScale = vector3;
    }

    public IEnumerator InitAction()
    {
        idCommand = Random.Range(0, skillsPokemonEnemy.Length);

        yield return new WaitForSeconds(1);

        StartCoroutine("Command", idCommand);
    }

    public IEnumerator Command(int idAction)
    {
        switch(idAction)
        {
            case 0:
                StartCoroutine("DealDamage");
                break;

            case 1:
                StartCoroutine("DealDamage");
                break;

            case 2:
                StartCoroutine("DealDamage");
                break;

            case 3:
                StartCoroutine("DealDamage");
                break;

            case 4:
                StartCoroutine("DealDamage");
                break;
        }
        yield return new WaitForSeconds(1);
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
                PokemonPlayer.instance.InitPokemonPlayerCommand();
                break;

            case 3:

                break;
        }
    }
    public IEnumerator DealDamage()
    {
        GameObject tempPrefab = Instantiate(animations[idCommand]) as GameObject;
        tempPrefab.transform.position = PokemonPlayer.instance.transform.position;

        hit = Random.Range(1, damageSkillsPokemonEnemy[idCommand]);
        actionPokemonEnemy = "Foe " + namePokemonEnemy + " used " + skillsPokemonEnemy[idCommand] + "!";
        StartCoroutine("Dialogue", actionPokemonEnemy);
        Debug.Log(hit);
        yield return new WaitForSeconds(1);
        Destroy(tempPrefab);
        PokemonPlayer.instance.TakeDamage(hit);
        idPhase = 2;
    }
}
