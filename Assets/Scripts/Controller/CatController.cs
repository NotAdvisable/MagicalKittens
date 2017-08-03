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
    [SerializeField]
    private GameObject _currentProjectile;
    [SerializeField]
    private PlayernameDisplay _playerNameDisplay;
    [SerializeField]
    private float _shootCooldown = 1f;

    public bool IsLobbyCat
    {
        get { return m_bIsLobbyCat; }
        set { m_bIsLobbyCat = value; }
    }
    public bool IsInitialCat
    {
        get { return m_bIsInitialCat; }
        set { m_bIsInitialCat = value; }
    }

    public string PlayerName
    {
        get { return m_PlayerName; }
        set { m_PlayerName = value; }
    }

    [SyncVar]
    private bool m_bIsLobbyCat;
    [SyncVar(hook = "OnInitialCatStatusChanged")]
    private bool m_bIsInitialCat;
    [SyncVar(hook = "OnPlayerNameChanged")]
    public string m_PlayerName;

    private bool bSendPlayerDataOnStart = false;
    private Animator _anim;
    private short _skinID;
    private bool _cooldownComplete = true;


    void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerNameDisplay.Controller = this;
    }

    void Start()
    {
        var hud = FindObjectOfType<CustomHUD>();
        if (hud != null)
        {
            GetComponent<CatMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }

        SceneManager.activeSceneChanged += OnSceneChanged;
        NetworkState.Singleton.AddPlayer(this);
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

            if (bSendPlayerDataOnStart)
            {
                CmdSendPlayerData(hud.ReturnCharacterID(), hud.ReturnPlayerName());
            }
        }
    }

    private void OnPlayerNameChanged(string newName)
    {
        m_PlayerName = newName;
        _playerNameDisplay.SetText(newName);
    }

    private void OnInitialCatStatusChanged(bool bIsInitialCat)
    {
        m_bIsInitialCat = bIsInitialCat;
        var hud = FindObjectOfType<CustomHUD>();
        if (hud && m_bIsInitialCat)
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
                bSendPlayerDataOnStart = true;
            }
        }
    }

    public void Dancing(bool value)
    {
        _anim.SetBool("Dance", value);
    }

    public void SpawnProjectile() {
        if (_cooldownComplete) {
            StartCoroutine(ShootCoolDown());
            CmdSpawnProjectile();
        }
    }
    [Command]
    private void CmdSpawnProjectile() {
        var projectile = Instantiate(_currentProjectile, _particleSpawnPosition.position, transform.rotation);
        NetworkServer.Spawn(projectile);
    }
    private IEnumerator ShootCoolDown() {
        _cooldownComplete = false;
        yield return new WaitForSeconds(_shootCooldown);
        _cooldownComplete = true;
    }

    [Command]
    public void CmdSendPlayerData(int selectedCatIndex, string name)
    {
        m_PlayerName = name;
        OnPlayerNameChanged(name);

        if (IsLobbyCat)
        {
            (NetworkManager.singleton as CustomLobbyManager).ChangeSelectedPrefab(connectionToClient, 0, selectedCatIndex);
        }
    }
}
