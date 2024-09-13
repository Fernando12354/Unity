using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUniversal : MonoBehaviour
{
    public GameObject c1;   
    public GameObject c2;
    public GameObject cuboSalida;
    public  GameObject CanvasDialogosSecundarios;

    void Start()
    {
        // Al iniciar la escena, cargar el estado de los cubos según el número de veces que se ha cargado la escena
        CargarEstadoCubos();
    }

    private void CargarEstadoCubos()
    {
        int numCargas = PlayerPrefs.GetInt("NumCargasEscena", 0);
        if(numCargas > 2){
            numCargas=0;
            PlayerPrefs.SetInt("NumCargasEscena", 0); // Resetear en PlayerPrefs
            PlayerPrefs.Save();
        }

        if (numCargas == 0)
        {
            c1.SetActive(true);  // Activar c1
            c2.SetActive(false); // Desactivar c2
            cuboSalida.SetActive(false); // Desactivar cuboSalida

             if(CanvasDialogosSecundarios!=null){
                CanvasDialogosSecundarios.SetActive(true);
            }
        }
        else if (numCargas == 1)
        {

            if(CanvasDialogosSecundarios!=null){
                CanvasDialogosSecundarios.SetActive(false);
            }
            c1.SetActive(false);
            c2.SetActive(true);
            cuboSalida.SetActive(false);
        }
        else if (numCargas == 2)
        {
             if(CanvasDialogosSecundarios!=null){
                CanvasDialogosSecundarios.SetActive(false);
            }
            c1.SetActive(false);
            c2.SetActive(false);
            cuboSalida.SetActive(true);
             // Resetear numCargas a 0 para la próxima ejecución
            PlayerPrefs.SetInt("NumCargasEscena", 0);
            PlayerPrefs.Save();
        }
    }

    public void GuardarEstadoCubos()
    {
        int numCargas = PlayerPrefs.GetInt("NumCargasEscena", 0);
        PlayerPrefs.SetInt("NumCargasEscena", numCargas + 1);
        PlayerPrefs.Save();
    }
}
