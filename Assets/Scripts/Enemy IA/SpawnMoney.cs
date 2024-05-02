using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoney : MonoBehaviour
{
    public GameObject[] prefabs; // Arreglo de prefabs que quieres instanciar

    // Llamamos a esta función cuando queremos instanciar los prefabs
    public void InstanciarMonedas()
    {
        // Genera un número aleatorio entre 1 y 3
        int cantidadAInstanciar = Random.Range(1, 4);

        for (int i = 0; i < cantidadAInstanciar; i++)
        {
            // Selecciona un prefab aleatorio del arreglo
            GameObject prefabSeleccionado = prefabs[Random.Range(0, prefabs.Length)];

            // Instancia el prefab en la posición del objeto asociado
            Instantiate(prefabSeleccionado, transform.position, Quaternion.identity);
        }
    }
}
