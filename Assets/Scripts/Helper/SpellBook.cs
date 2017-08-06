using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : Collectible {

    [SerializeField] private int _spellID;
    [SerializeField] private int _respawnTimeInSecs = 4;

    private SpellBook(int id)
    {
        _spellID = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        var cat = other.GetComponent<CatController>();
        if (cat != null)
        {
            cat.SetSpellID(_spellID);
            NetworkState.Singleton.RespawnProp(_spellID-1, transform.position, transform.rotation, _respawnTimeInSecs);
            Destroy(gameObject);
        }
    }
}
