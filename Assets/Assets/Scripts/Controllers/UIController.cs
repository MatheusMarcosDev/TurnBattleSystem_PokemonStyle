using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header ("Pokemon Player")]
    public TextMeshPro namePP;
    public TextMeshPro levelPP;
    public TextMeshPro currentHPPP;
    public TextMeshPro maxHPPP;

    [Header("Pokemon Enemy")]
    public TextMeshPro namePE;
    public TextMeshPro levelPE;

    [Header("UI CoreGame")]
    public Text textGame;
    public GameObject menuA;
    public GameObject menuB;

    void Start()
    {
        instance = this;

        menuA.SetActive(false);
        menuB.SetActive(false);

        namePP.text = ScriptableObjectPP.instance.currentPokemonPlayerObject.namePP;
        levelPP.text = ScriptableObjectPP.instance.currentPokemonPlayerObject.currentLevelPP.ToString();
        maxHPPP.text = currentHPPP.text = ScriptableObjectPP.instance.currentPokemonPlayerObject.maxLifePP.ToString();

        namePE.text = ScriptableObjectPE.instance.currentPokemonEnemyObject.namePE;
        levelPE.text = ScriptableObjectPE.instance.currentPokemonEnemyObject.currentLevelPE.ToString();
    }

    private void LateUpdate()
    {
        currentHPPP.text = ScriptableObjectPP.instance.currentPokemonPlayerObject.currentLifePP.ToString();
    }
}
