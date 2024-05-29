using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarLuzBoss : MonoBehaviour
{
    public GameObject _luzBoss;

    public void DesactivarLuz()
    {
        _luzBoss.SetActive(false);
    }
}
