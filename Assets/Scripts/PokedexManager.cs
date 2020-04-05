using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PokedexManager : MonoBehaviour
{
    public static PokedexManager Instance;

    public List<Pokedex.TypesColor> TypesColor;
    public List<Texture2D> ImgIcons;
    public Transform ScvPokedex;
    public PokedexOptions PokedexOptions;
    [Space]
    public GameObject PnlPokedexPage;
    public PokedexPage PokedexPage;

    private string pokedexJson = "pokedex.json";
    private string caminhoJson = string.Empty;

    private Pokedex pokedex;
    private int currentPokemon = 0;

    private void Awake()
    {
        Instance = this;

        caminhoJson = Path.Combine(Application.dataPath, $"StreamingAssets/{pokedexJson}");
    }

    IEnumerator Start()
    {
        yield return CarregarPokedex((pokedex) =>
        {
            this.pokedex = pokedex;
        });

        carregarPokemons();
    }

    void carregarPokemons()
    {
        for (int i = 0; i < pokedex.pokemons.Count; i++)
        {
            PokedexOptions pokedexOptions = Instantiate(PokedexOptions, ScvPokedex);
            pokedexOptions.SetInfo(pokedex.pokemons[i], ImgIcons[i]);
        }
    }

    public IEnumerator CarregarPokedex(Action<Pokedex> callback)
    {
        string url = caminhoJson;

        if (!Application.isEditor)
        {
#if UNITY_ANDROID
            Debug.Log("Pegando caminho android");
            url = "jar:file://" + Application.dataPath + "!/assets/" + pokedexJson;
#elif UNITY_IOS
            url = "file://"+Application.dataPath + "/Raw/" + pokedexJson;
#endif
        }

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (string.IsNullOrEmpty(www.error))
            {
                callback(JsonConvert.DeserializeObject<Pokedex>(www.downloadHandler.text));
            }
        }
    }

    public void AbrirInfoPokemon(Pokedex.Pokemon pokemon)
    {
        PnlPokedexPage.SetActive(true);
        PokedexPage.SetInfo(pokemon, ImgIcons[pokemon.id - 1]);
    }

    public void FecharInfoPokemon()
    {
        PnlPokedexPage.SetActive(false);
    }

    public (Pokedex.Pokemon, Texture2D) BuscarPokemon(string num)
    {
        int index = Convert.ToInt32(num) - 1;
        return (pokedex.pokemons[index], ImgIcons[index]);
    }

    public (Pokedex.Pokemon, Texture2D) BuscarPokemon(int index)
    {
        return (pokedex.pokemons[index], ImgIcons[index]);
    }

    public void MovimentarPokedex(bool ehProximo, Action<Pokedex.Pokemon, Texture2D> callback)
    {
        currentPokemon = (ehProximo) ? currentPokemon + 1 : currentPokemon - 1;

        (Pokedex.Pokemon pokemonEv, Texture2D icon) = BuscarPokemon(currentPokemon);

        callback(pokemonEv, icon);
    }

    public int GetTotalPokemonsPokedex()
    {
        return pokedex.pokemons.Count;
    }

    public int GetPokemonAtual()
    {
        return currentPokemon;
    }

    public void SetarPokemonAtual(int id)
    {
        currentPokemon = pokedex.pokemons.IndexOf(pokedex.pokemons.Find(x => x.id == id));
    }
}
