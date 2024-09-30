using UnityEngine;
using UnityEngine.EventSystems;

public class GuionReceptor : MonoBehaviour, IDropHandler
{
    public char letraCorrecta;    // Letra que corresponde a este guion
    private bool letraAsignada = false; // Para evitar que se ponga más de una letra

    public void OnDrop(PointerEventData eventData)
    {
        if (!letraAsignada)
        {
            GameObject objetoArrastrado = eventData.pointerDrag;
            LetraArrastrable letraArrastrable = objetoArrastrado.GetComponent<LetraArrastrable>();

            if (letraArrastrable != null)
            {
                // Si la letra es correcta, la "fija" en el guion
                if (letraArrastrable.letra == letraCorrecta)
                {
                    objetoArrastrado.transform.SetParent(this.transform); // Mueve la letra al guion
                    letraArrastrable.BloquearArrastre(); // Evita que se pueda mover otra vez
                    letraAsignada = true;
                    FindObjectOfType<AhorcadoGame>().IntentarLetra(true); // Notifica que se adivinó la letra correctamente
                }
                else
                {
                    FindObjectOfType<AhorcadoGame>().IntentarLetra(false); // Notifica que fue incorrecto
                }
            }
        }
    }
}

