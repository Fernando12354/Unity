using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManagerEscena : MonoBehaviour
{
    public GameObject CuboTienda;
    public GameObject CuboEnfermeria;
    public GameObject CuboSalida;

    // Este método se llamará cuando inicie la escena
    void Start()
    {
        // Verificar el estado de cada cubo almacenado en PlayerPrefs
        CuboTienda.SetActive(PlayerPrefs.GetInt("CuboTienda", 1) == 1); // Por defecto activo
        CuboEnfermeria.SetActive(PlayerPrefs.GetInt("CuboEnfermeria", 0) == 1); // Por defecto desactivado
        CuboSalida.SetActive(PlayerPrefs.GetInt("CuboSalida", 0) == 1); // Por defecto desactivado
    }

    // Este método será llamado cuando cambias de escena
    public void CambiarEscena(string nuevaEscena)
    {
        // Antes de cambiar de escena, guardar el estado actual de los cubos
        GuardarEstadoCubo();

        // Cargar la nueva escena
        SceneManager.LoadScene(nuevaEscena);
    }

    // Este método guardará el estado actual de los cubos
    void GuardarEstadoCubo()
    {
        PlayerPrefs.SetInt("CuboTienda", CuboTienda.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("CuboEnfermeria", CuboEnfermeria.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("CuboSalida", CuboSalida.activeSelf ? 1 : 0);
    }

    // Este método se puede usar para actualizar el estado de los cubos al regresar a la escena
    public void ActualizarEstadoCubos(bool activarTienda, bool activarEnfermeria, bool activarSalida)
    {
        CuboTienda.SetActive(activarTienda);
        CuboEnfermeria.SetActive(activarEnfermeria);
        CuboSalida.SetActive(activarSalida);

        // Guardar el nuevo estado
        GuardarEstadoCubo();
    }
}
