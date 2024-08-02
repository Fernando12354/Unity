using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SopaDeLetrasManager : MonoBehaviour
{
    public int filas = 10;
    public int columnas = 7;
    public GameObject prefabLetra;
    public Text textoTemporizador;
    public Text textoPalabras;
    public Text textoMensaje;
    public float tiempoLimite = 60f; // Tiempo en segundos
    public int numeroDePalabrasPorJuego = 5; // Número de palabras por cada sopa
    public int maximoRondas = 5; // Número máximo de sopas de letras

    private char[,] sopaDeLetras;
    private List<string> todasLasPalabras = new List<string>
    {
        "UNITY", "JUEGO", "CODIGO", "LETRA", "PROGRAMA", "SCRIPT", "GRAFICOS", "SONIDO", "ESCENA", "OBJETO", 
        "COLLIDER", "ANIMACION", "PREFAB", "TEXTURA", "CAMARA", "FISICAS", "TRANSFORM", "POSICION", "ESCALA", "ROTACION"
    };

    private List<string> palabrasActuales = new List<string>();
    private HashSet<string> palabrasUsadas = new HashSet<string>();
    private float tiempoRestante;
    private int palabrasEncontradas;
    private int rondaActual = 0;

    private List<Letra> letrasSeleccionadas = new List<Letra>();

    void Start()
    {
        IniciarJuego();
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            textoTemporizador.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();

            if (tiempoRestante <= 0)
            {
                // Tiempo agotado, reorganizar las palabras
                ReorganizarSopaDeLetras();
            }
        }
    }

    void IniciarJuego()
    {
        if (rondaActual >= maximoRondas)
        {
            textoMensaje.text = "¡Has completado todas las sopas de letras!";
            return;
        }

        sopaDeLetras = new char[filas, columnas];
        tiempoRestante = tiempoLimite;
        palabrasEncontradas = 0;

        SeleccionarPalabrasAleatorias();
        GenerarSopaDeLetras();
        MostrarSopaDeLetras();
        ActualizarTextoPalabras();
        textoMensaje.text = "";
    }

    void SeleccionarPalabrasAleatorias()
    {
        palabrasActuales.Clear();
        List<string> palabrasDisponibles = new List<string>();

        // Filtrar palabras que no han sido usadas
        foreach (string palabra in todasLasPalabras)
        {
            if (!palabrasUsadas.Contains(palabra))
            {
                palabrasDisponibles.Add(palabra);
            }
        }

        for (int i = 0; i < numeroDePalabrasPorJuego; i++)
        {
            if (palabrasDisponibles.Count > 0)
            {
                int indice = Random.Range(0, palabrasDisponibles.Count);
                string palabraSeleccionada = palabrasDisponibles[indice];
                palabrasActuales.Add(palabraSeleccionada);
                palabrasUsadas.Add(palabraSeleccionada);
                palabrasDisponibles.RemoveAt(indice);
            }
        }

        // Verificar palabras seleccionadas
        Debug.Log("Palabras seleccionadas: " + string.Join(", ", palabrasActuales));
    }

    void GenerarSopaDeLetras()
    {
        // Limpiar la sopa de letras
        for (int x = 0; x < filas; x++)
        {
            for (int y = 0; y < columnas; y++)
            {
                sopaDeLetras[x, y] = '\0';
            }
        }

        // Lógica para colocar las palabras en la matriz
        foreach (var palabra in palabrasActuales)
        {
            if (!ColocarPalabraEnSopa(palabra))
            {
                Debug.LogWarning("No se pudo colocar la palabra: " + palabra);
            }
        }

        // Llenar el resto con letras aleatorias
        RellenarConLetrasAleatorias();
    }

    bool ColocarPalabraEnSopa(string palabra)
    {
        int maxIntentos = 100;
        int intentos = 0;

        while (intentos < maxIntentos)
        {
            int direccion = Random.Range(0, 3); // 0: horizontal, 1: vertical, 2: diagonal
            int x = Random.Range(0, filas);
            int y = Random.Range(0, columnas);

            int dx = 0, dy = 0;
            bool puedeColocar = false;

            // Determinar la dirección
            if (direccion == 0 && y + palabra.Length <= columnas)
            {
                dx = 0; dy = 1; // horizontal
                puedeColocar = true;
            }
            else if (direccion == 1 && x + palabra.Length <= filas)
            {
                dx = 1; dy = 0; // vertical
                puedeColocar = true;
            }
            else if (direccion == 2 && x + palabra.Length <= filas && y + palabra.Length <= columnas)
            {
                dx = 1; dy = 1; // diagonal
                puedeColocar = true;
            }

            if (puedeColocar && PuedeColocarPalabra(x, y, dx, dy, palabra))
            {
                for (int i = 0; i < palabra.Length; i++)
                {
                    sopaDeLetras[x + i * dx, y + i * dy] = palabra[i];
                }
                Debug.Log("Palabra colocada exitosamente: " + palabra + " en (" + x + ", " + y + ")");
                return true; // Palabra colocada exitosamente
            }

            intentos++;
        }

        Debug.LogWarning("No se pudo colocar la palabra: " + palabra + " después de " + maxIntentos + " intentos.");
        return false; // No se pudo colocar la palabra
    }

    bool PuedeColocarPalabra(int x, int y, int dx, int dy, string palabra)
    {
        for (int i = 0; i < palabra.Length; i++)
        {
            int nuevoX = x + i * dx;
            int nuevoY = y + i * dy;

            // Verifica si estamos dentro de los límites
            if (nuevoX < 0 || nuevoX >= filas || nuevoY < 0 || nuevoY >= columnas)
            {
                Debug.Log("Fuera de límites al intentar colocar: " + palabra + " en (" + nuevoX + ", " + nuevoY + ")");
                return false;
            }

            // Verifica si la posición está ocupada por una letra diferente
            if (sopaDeLetras[nuevoX, nuevoY] != '\0' && sopaDeLetras[nuevoX, nuevoY] != palabra[i])
            {
                Debug.Log("Conflicto al colocar la palabra: " + palabra + " en (" + nuevoX + ", " + nuevoY + ")");
                return false;
            }
        }
        return true;
    }

    void RellenarConLetrasAleatorias()
    {
        for (int x = 0; x < filas; x++)
        {
            for (int y = 0; y < columnas; y++)
            {
                if (sopaDeLetras[x, y] == '\0') // Si la posición está vacía
                {
                    sopaDeLetras[x, y] = (char)('A' + Random.Range(0, 26));
                }
            }
        }
    }

    void MostrarSopaDeLetras()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < filas; x++)
        {
            for (int y = 0; y < columnas; y++)
            {
                Vector3 posicion = ObtenerPosicionDeMatriz(x, y);
                GameObject letraObj = Instantiate(prefabLetra, posicion, Quaternion.identity, transform);
                Letra letraScript = letraObj.GetComponent<Letra>();
                letraScript.ConfigurarLetra(sopaDeLetras[x, y]);
            }
        }
    }

    Vector3 ObtenerPosicionDeMatriz(int x, int y)
    {
        // Convertir las coordenadas de la matriz a coordenadas en el mundo
        float espaciado = 1.0f; // Ajusta este valor según el tamaño del prefab y el espaciado deseado
        return new Vector3(y * espaciado, -x * espaciado, 0); // Asumiendo que el eje Z es el que se utiliza para el espaciado en 2D
    }

    void ActualizarTextoPalabras()
    {
        textoPalabras.text = "Encuentra las palabras:\n";
        foreach (var palabra in palabrasActuales)
        {
            textoPalabras.text += palabra + ",";
        }
    }

    void ReorganizarSopaDeLetras()
    {
        Debug.Log("Tiempo agotado. Reorganizando la sopa de letras con las mismas palabras.");
        GenerarSopaDeLetras(); // Reorganiza las mismas palabras en la sopa
        MostrarSopaDeLetras();
        tiempoRestante = tiempoLimite; // Reinicia el temporizador
        palabrasEncontradas = 0; // Reinicia el conteo de palabras encontradas
    }

    void GenerarNuevaSopaDeLetras()
    {
        Debug.Log("Todas las palabras encontradas. Generando nueva sopa de letras.");
        rondaActual++;
        if (rondaActual >= maximoRondas)
        {
            textoMensaje.text = "¡Has completado todas las sopas de letras!";
            return;
        }
        palabrasActuales.Clear(); // Limpia las palabras actuales para forzar nuevas
        IniciarJuego();
    }

    public void PalabraEncontrada()
    {
        palabrasEncontradas++;
        if (palabrasEncontradas == palabrasActuales.Count)
        {
            // Todas las palabras encontradas antes de que el tiempo se agote
            GenerarNuevaSopaDeLetras();
        }
    }

    public void LetraSeleccionada(Letra letra)
    {
        letrasSeleccionadas.Add(letra);

        string palabraFormada = "";
        foreach (var l in letrasSeleccionadas)
        {
            palabraFormada += l.textoLetra.text;
        }

        if (palabrasActuales.Contains(palabraFormada))
        {
            // Marca la palabra como encontrada
            textoMensaje.text = "¡Palabra encontrada: " + palabraFormada + "!";
            foreach (var l in letrasSeleccionadas)
            {
                // Opcional: cambiar el color de las letras encontradas
                l.GetComponent<Text>().color = Color.green;
            }
            letrasSeleccionadas.Clear();
            PalabraEncontrada();
        }
    }
}
