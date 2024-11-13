using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missionWayPoint : MonoBehaviour
{
    // Indicator icon
    public Image img;
    // The target (location, enemy, etc..)
    public Transform target;
    public GameObject player;
    public GameObject playerWithMask; // Player opcional con cubrebocas
    // UI Text to display the distance
    public Text meter;
    // To adjust the position of the icon
    public Vector3 offset;

    public Camera mainCamera;

    public float hideDistance = 2f; // Distancia para ocultar los indicadores

    private void Update()
    {
        GameObject activePlayer = player; // Usar el player original como predeterminado

        // Si el player con cubrebocas está activo en la escena, usarlo en su lugar
        if (playerWithMask != null && playerWithMask.activeInHierarchy)
        {
            activePlayer = playerWithMask;
        }

        // Calcula la distancia entre el jugador activo y el objetivo
        float distanceToTarget = Vector3.Distance(target.position, activePlayer.transform.position);

        // Si la distancia es menor o igual a 'hideDistance', ocultar los indicadores
        if (distanceToTarget <= hideDistance)
        {
            // Desactivar el ícono y el texto de la distancia
            img.gameObject.SetActive(false);
            meter.gameObject.SetActive(false);
            return; // Salir del método para evitar ejecutar el resto del código
        }
        else
        {
            // Asegurarse de que los indicadores estén activados si la distancia es mayor a 'hideDistance'
            img.gameObject.SetActive(true);
            meter.gameObject.SetActive(true);
        }

        // Dar límites al ícono para que se quede dentro de la pantalla
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        // Convertir la posición del objetivo de 3D a 2D en la pantalla
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Verifica si el objetivo está detrás del jugador activo
        if (Vector3.Dot((target.position - activePlayer.transform.position).normalized, activePlayer.transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        // Limitar las posiciones X e Y
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Actualizar la posición del ícono
        img.transform.position = pos;

        // Actualizar el texto con la distancia en metros
        meter.text = ((int)distanceToTarget).ToString() + "m";
    }
}
