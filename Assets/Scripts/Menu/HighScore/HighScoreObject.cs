using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreObject : MonoBehaviour
{
    public TextMeshProUGUI nameLabel, pointLabel;

    public string namePlayer { get; private set; }
    public int points { get; private set; }

    public HighScoreObject(string n, int p)
    {
        namePlayer = n;
        points = p;
    }

    public void Initialize(HighScoreObject hs)
    {
        nameLabel.text = hs.namePlayer;
        pointLabel.text = hs.points.ToString();
    }
}
