using System.Collections;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    public void StartRevertCoroutine(MaskPickup maskPickup)
    {
        StartCoroutine(RevertToOriginalAfterDelay(maskPickup));
    }

    private IEnumerator RevertToOriginalAfterDelay(MaskPickup maskPickup)
    {
        Debug.Log("Esperando para restaurar el modelo original.");
        // Espera el tiempo especificado
        yield return new WaitForSeconds(maskPickup.maskDuration);

        // Cambia de nuevo al modelo original
        maskPickup.ChangePlayerModel(false);
        Debug.Log("Modelo restaurado al original después de " + maskPickup.maskDuration + " segundos.");
    }
}

