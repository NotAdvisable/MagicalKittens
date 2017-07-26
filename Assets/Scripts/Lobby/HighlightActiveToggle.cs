using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightActiveToggle : MonoBehaviour {

    public void Highlight(bool value) {
        var colors = GetComponent<Toggle>().colors;
        colors.colorMultiplier = (value) ? 5 : 1;
        GetComponent<Toggle>().colors = colors;
    }
}
