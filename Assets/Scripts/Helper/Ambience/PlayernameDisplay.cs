using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(TextMesh))]
public class PlayernameDisplay : MonoBehaviour
{
    public CatController Controller { get; set; }

    private MeshRenderer _renderer;
    private TextMesh _textMesh;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _textMesh = GetComponent<TextMesh>();
        _renderer.enabled = false;
    }

    public void SetText(string text)
    {
        _renderer.enabled = true;
        _textMesh.text = text;
    }

    private void Update()
    {
        SetText(Controller.PlayerName);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
