using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokedexType : MonoBehaviour
{
    //enum types {
    //    Psychic = 0,
    //    Bug = 1,
    //    Ghost = 2,
    //    Dark = 3,
    //    Ice = 4,
    //    Rock = 5,
    //    Dragon = 6,
    //    Fairy = 7,
    //    Water = 8,
    //    Eletric = 9,
    //    Flying = 10,
    //    Steel = 11,
    //    Fighting = 12,
    //    Normal = 13,
    //    Ground = 14,
    //    Grass = 15,
    //    Fire = 16,
    //    Poison = 17
    //}

    public Image Img;
    public TextMeshProUGUI TxtType;

    public void SetInfo(string type)
    {
        Pokedex.TypesColor typeColor = PokedexManager.Instance.TypesColor.Find(x => x.type == type);
        Img.color = typeColor.color;
        TxtType.text = typeColor.type;
    }
}
