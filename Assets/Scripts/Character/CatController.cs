using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CatController : NetworkCharacter
{
    [SerializeField]
    private Transform _particleSpawnPosition;
    [SerializeField] [SyncVar]
    private int _currentProjectileID = 0;
    [SerializeField]
    private PlayernameDisplay _playerNameDisplay;
    [SerializeField]
    private float _shootCooldown = 1f;

    public bool IsLobbyCat
    {
        get { return _isLobbyCat; }
        set { _isLobbyCat = value; }
    }
    public bool IsInitialCat
    {
        get { return _isInitialCat; }
        set { _isInitialCat = value; }
    }

    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    [SyncVar]
    private bool _isLobbyCat;
    [SyncVar(hook = "OnInitialCatStatusChanged")]
    private bool _isInitialCat;
    [SyncVar(hook = "OnPlayerNameChanged")]
    public string _playerName;

    private bool _sendPlayerDataOnStart = false;

    internal void Respawn()
    {
        throw new NotImplementedException();
    }

    private short _skinID;
    private bool _cooldownComplete = true;


    void Awake()
    {
        _playerNameDisplay.Controller = this;
    }

    protected override void Start()
    {
        base.Start();
        var hud = FindObjectOfType<CustomHUD>();
        if (hud != null)
        {
            GetComponent<CatMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }

        SceneManager.activeSceneChanged += OnSceneChanged;
        NetworkState.Singleton.AddPlayer(this);
        if(EventController.Singleton != null)  EventController.Singleton.OnBossDiedEvent += DisableWhenBossKilled;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (IsLobbyCat)
        {
            //Hide and wait until the new pawn has been spawned
            SkinnedMeshRenderer catRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            catRenderer.enabled = false;
            StartCoroutine(WaitForNewLocalPlayer());

            SceneManager.activeSceneChanged -= OnSceneChanged;
        }
    }

    private IEnumerator WaitForNewLocalPlayer()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() =>
        {
            CatController localCat = GetLocalCat();
            return localCat != this && localCat;
        });
        Destroy(gameObject);
    }

    private CatController GetLocalCat()
    {
        return NetworkState.Singleton.GetCurrentPlayers().Where(element => element.isLocalPlayer).FirstOrDefault();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
        NetworkState.Singleton.RemovePlayer(this);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        var hud = FindObjectOfType<CustomHUD>();

        if (hud)
        {
            hud.SetLocalCat(this);

            if (_sendPlayerDataOnStart)
            {
                CmdSendPlayerData(hud.ReturnCharacterID(), hud.ReturnPlayerName());
            }
        }
    }

    private void OnPlayerNameChanged(string newName)
    {
        _playerName = newName;
        _playerNameDisplay.SetText(newName);
    }

    private void OnInitialCatStatusChanged(bool bIsInitialCat)
    {
        _isInitialCat = bIsInitialCat;
        var hud = FindObjectOfType<CustomHUD>();
        if (hud && _isInitialCat)
        {
            if (hasAuthority)
            {
                //We are the local player object, we are the initial cat
                CmdSendPlayerData(hud.ReturnCharacterID(), hud.ReturnPlayerName());
            }
            else
            {
                //If we have no authority at this point we either are not set up yet
                //or we are no local player at all. Setting this will ensure that
                //local player is sent when the local player callback is invoked
                _sendPlayerDataOnStart = true;
            }
        }
    }
    private void DisableWhenBossKilled()
    {
        GetComponent<CatMovement>().enabled = false;
    }
    public void SetSpellID(int id)
    {
        _currentProjectileID = id;
    }
    public void Dancing(bool value)
    {
        _anim.SetBool("Dance", value);
    }
    public override void Hit(float dmg, GameObject aggressor)
    {
        base.Hit(dmg,null);
    }
    public override void Die()
    {
        var positions = FindObjectOfType<CustomLobbyManager>().startPositions;
        transform.position = positions[UnityEngine.Random.Range(0, positions.Count)].position;
        _health.Respawn();
        if (isLocalPlayer)
        {
            CameraController.Singleton.TurnOffBossCam();
            FindObjectOfType<Bossfight>().ResetDirection();
        }
    }
    public void SpawnProjectile() {
        if (_cooldownComplete) {
            StartCoroutine(ShootCoolDown());
            CmdSpawnProjectile();
        }
    }
    [Command]
    private void CmdSpawnProjectile() {
        NetworkState.Singleton.RpcSpawnProjectile(_currentProjectileID, _particleSpawnPosition.position, transform.rotation,gameObject);
    }
    private IEnumerator ShootCoolDown() {
        _cooldownComplete = false;
        yield return new WaitForSeconds(_shootCooldown);
        _cooldownComplete = true;
    }

    [Command]
    public void CmdSendPlayerData(int selectedCatIndex, string name)
    {
        _playerName = name;
        OnPlayerNameChanged(name);

        if (IsLobbyCat)
        {
            (NetworkManager.singleton as CustomLobbyManager).ChangeSelectedPrefab(connectionToClient, 0, selectedCatIndex);
        }
    }
}
