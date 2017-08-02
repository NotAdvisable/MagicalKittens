using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CatController : NetworkCharacter {


    [SerializeField] private Transform _particleSpawnPosition;
    [SerializeField] private GameObject _currentProjectile;
    [SerializeField] private float _ShootCoolDown = 1f;

    private bool _cooldownComplete = true;
    private Animator _anim;
    private TextMesh _displayName;
    private short _skinID;


    public bool IsLobbyCat {
        get { return bIsLobbyCat; }
        set { bIsLobbyCat = value; }
    }

    [SyncVar]
    private bool bIsLobbyCat = false;

    //  [SyncVar]  public string _playerName;

    void Start() {
        _anim = GetComponent<Animator>();
        _displayName = GetComponentInChildren<TextMesh>();
        var hud = FindObjectOfType<CustomHUD>();
        if (hud != null) {
            GetComponent<CatMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            hud.SetLocalCat(this);
        }

        //if (NetworkState.Singleton.GetCurrentPlayers().Any(element => element.isLocalPlayer)) {
        //    NetworkState.Singleton.KillAllLobbyPlayers();
        //}

        SceneManager.activeSceneChanged += OnSceneChanged;
        NetworkState.Singleton.AddPlayer(this);
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene) {
        if (newScene.name != "Lobby") {
            if (IsLobbyCat) {
                //Destroy(gameObject);
                Debug.Log("SceneChanged: " + oldScene.name + ", " + newScene.name);
                SceneManager.activeSceneChanged -= OnSceneChanged;
            }
        }
    }

    private void OnDestroy() {
        NetworkState.Singleton.RemovePlayer(this);
    }

    public override void OnStartLocalPlayer() {
        // if (CameraController.Singleton != null)
        //CameraController.Singleton.SetFocus(transform);
    }

    public void Dancing(bool value) {
        _anim.SetBool("Dance", value);
    }

    public void SpawnProjectile() {
        if (_cooldownComplete) {
            var projectile = Instantiate(_currentProjectile, _particleSpawnPosition.position, transform.rotation);
         //   projectile.
            StartCoroutine(ShootCoolDown());
        }
    }
    private IEnumerator ShootCoolDown() {
        _cooldownComplete = false;
        yield return new WaitForSeconds(_ShootCoolDown);
        _cooldownComplete = true;
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
