using UnityEngine;

[CreateAssetMenu (fileName = "New Pokemon", menuName = "Pokemon/Pokemon Player")]

public class PokemonPlayerObject : ScriptableObject
{
    [Header ("Pokemon Info/Stats")]
    public string namePP;
    public float maxLifePP;
    public float currentLifePP;
    public int currentLevelPP;
    public int currentExpPP;

    [Header ("Skills Settings")]
    public GameObject[] animationsSkillsPP;
    public string[] skillsNamePP;
    public int[] damagesSkillsPP;
}
