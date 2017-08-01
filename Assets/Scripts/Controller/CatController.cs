using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CatController : NetworkCharacter {

    private Animator _anim;
    private TextMesh _displayName;
    private short _skinID;

  //  [SyncVar]  public string _playerName;

    void Start() {
        _anim = GetComponent<Animator>();
        _displayName = GetComponentInChildren<TextMesh>();
        var hud = FindObjectOfType<CustomHUD>();
        var manager = FindObjectOfType<CustomLobbyManager>();
        if(hud != null) {
            GetComponent<CatMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            hud.SetLocalCat(this);
        }
    }
    public override void OnStartLocalPlayer() {
        if(CameraController.Singleton != null)
        CameraController.Singleton.SetFocus(transform);
    }

    public void Dancing(bool value) {
        _anim.SetBool("Dance", value);
    }
    //[Command]
    //public void CmdSetPlayerName(string name) {
    //    _playerName = name;
    //}
    //public void SetSkin(short id) {
    //    _skinID = id;
    //}
    //private IEnumerator SyncPlayerName() {
    //    while (_displayName.text == "Hello World" || _displayName.text != _playerName) {
    //        Debug.Log("FUCK THIS");
    //        _displayName.text = _playerName;
    //        yield return null;
    //    }
    //}
}
