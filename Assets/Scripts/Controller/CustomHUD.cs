﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;

/// <summary>
/// Handles the UI in the lobby scene
/// </summary>
public class CustomHUD : MonoBehaviour {

    [SerializeField] private InputField _serverAdressInputField;

    [Header("DefaultViews")]
    [SerializeField] private GameObject[] _views = new GameObject[2];

    [Header("Characters")]
    [SerializeField] private SkinnedMeshRenderer _protoRenderer;
    [SerializeField] private Material[] _materials = new Material[4];
    [SerializeField] private string[] _defaultNames = new string[4];

    [Header("Canvases")]
    [SerializeField] private Canvas[] _canvases = new Canvas[2];

    private Canvas _currentCanvas;
    private GameObject _currentView;
    private PlayerPrefs prefs;
    private short _currentCharacterID;
    private CatController _localCat;
    private string _playerName;


    void Start () {
      if (_serverAdressInputField != null) _serverAdressInputField.text = NetworkManager.singleton.networkAddress;
        _currentCanvas = _canvases[0];
        _currentView = _views[0];
        SwitchCharacterTo(0);

        _playerName = "";
	}


    public void SwitchCharacterTo(int i) {
        _currentCharacterID = (short) i;
        _protoRenderer.material = _materials[i];
    }
    public void SwitchViewTo(int i) {
        _currentView.SetActive(false);
        _currentView = _views[i];
        _currentView.SetActive(true);
    }

    public void SwitchCanvasTo(int i) {
        if (i > 0) _protoRenderer.materials = new Material[0];
        else _protoRenderer.material = _materials[_currentCharacterID];
        _currentCanvas.gameObject.SetActive(false);
        _currentCanvas = _canvases[i];
        _currentCanvas.gameObject.SetActive(true);
    }
    public void EditServerAdress(string s) {
        NetworkManager.singleton.networkAddress = s;
    }
    public void EditNetworkName(string name) {
        _playerName = name;
    }
    public void SetLocalCat(CatController cat) {
        _localCat = cat;
    }
    private void FindLocalCat() {

        var player = NetworkState.Singleton.GetCurrentPlayers().FirstOrDefault(cat => cat.isLocalPlayer);
        if (player != null) _localCat = player;
    }

    public void SetReady(bool value) {
        _localCat.Dancing(value);
        _localCat.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
    }
    public void UIStartHost() {
        if (!NetworkServer.active) {
            NetworkManager.singleton.StartHost();
        }
    }
    public void UIStartClient() {
        if (!NetworkClient.active) {
            NetworkManager.singleton.StartClient();
        }
    }
    public void UIStop() {
        if (NetworkServer.active || NetworkClient.active) {
            NetworkManager.singleton.StopHost();
        }
    }
    public void Exit() {
        Application.Quit();
    }
    public short ReturnCharacterID() {
        return _currentCharacterID;
    }
    public string ReturnPlayerName() {
        if (_playerName == "") {
            _playerName = _defaultNames[_currentCharacterID];
        }
        return _playerName;
    }
}
