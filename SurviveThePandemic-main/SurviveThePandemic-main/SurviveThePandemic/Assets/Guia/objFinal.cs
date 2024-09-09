using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objFinal : MonoBehaviour
{
    public GameObject panelMision;
    public GameObject panelAnterior;
    public GameObject elemento;
   public GameObject ZonaDeDialogo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            panelAnterior.SetActive(false);
            panelMision.SetActive(true);
            //Linea de codigo para activar el dialogo
            if(ZonaDeDialogo!=null){
                ZonaDeDialogo.SetActive(true);
            }
            Destroy(elemento);
        }
    }
}
