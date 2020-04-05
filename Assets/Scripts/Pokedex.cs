using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokedex
{
    public List<Pokemon> pokemons;

    public partial class Pokemon
    {
        public int id;
        public string num;
        public string name;
        public string img;
        public List<string> type;
        public string height;
        public string weight;
        public List<string> weaknesses;
        public List<Evolution> next_evolution;
        public List<Evolution> prev_evolution;
    }

    public partial class Evolution
    {
        public string num;
        public string name;
    }

    [Serializable]
    public partial class TypesColor
    {
        public string type;
        public Color color;
    }
}
