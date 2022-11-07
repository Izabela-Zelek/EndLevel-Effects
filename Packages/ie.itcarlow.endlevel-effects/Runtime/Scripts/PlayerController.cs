using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _timeUntilDeath = 2.0f;

    void Update()
    {
        _timeUntilDeath -= Time.deltaTime;

        if (_timeUntilDeath <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
