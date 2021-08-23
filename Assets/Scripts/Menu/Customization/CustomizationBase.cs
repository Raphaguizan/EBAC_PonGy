using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomizationBase : MonoBehaviour
{
    #region Color
    [Header("Color")]
    public Image box;

    private Color _color = new Color(1, 1, 1, 1);
    public Color myColor => _color;

    public void ChangeColorR(Slider slider)
    {
        _color.r = slider.value;
        box.color = _color;
    }

    public void ChangeColorG(Slider slider)
    {
        _color.g = slider.value;
        box.color = _color;
    }

    public void ChangeColorB(Slider slider)
    {
        _color.b = slider.value;
        box.color = _color;
    }

    public Color GetSelectedColor()
    {
        return _color;
    }
    #endregion

    #region Name
    [Header("Name")]
    public TextMeshProUGUI title;
    public TMP_InputField inputFiled;

    private string _name = "";
    public string myName => _name;

    public void UpdateTitle()
    {
        _name = inputFiled.text;
        title.text = _name;
    }
    #endregion
}
