using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public Transform content;
    public GameObject prefab;

    private List<HighScoreObject> _scores;
    private List<GameObject> _PrefabsInstatiateds;

    public void Calcule()
    {
        MakeList();
        SortList();
        _PrefabsInstatiateds = new List<GameObject>();
        for (int i = 0; i < _scores.Count; i++)
        {
            GameObject aux = Instantiate(prefab, content);
            aux.GetComponent<HighScoreObject>().Initialize(_scores[i]);
            _PrefabsInstatiateds.Add(aux);
        }
    }

    private void MakeList()
    {
        _scores = new List<HighScoreObject>();

        int qty = PlayerPrefs.GetInt("qty");

        for (int i = 0; i < qty; i++)
        {
            HighScoreObject aux = new HighScoreObject(PlayerPrefs.GetString("n" + i), PlayerPrefs.GetInt("p" + i));
            _scores.Add(aux);
        }
    }
    private void SortList()
    {
        _scores.Sort((p1, p2) => p2.points.CompareTo(p1.points));
    }

    public void DestroyPrefabList()
    {
        for (int i = 0; i < _PrefabsInstatiateds.Count; i++)
        {
            Destroy(_PrefabsInstatiateds[i]);
        }
        _PrefabsInstatiateds.Clear();
        _scores.Clear();
    }
}
