using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    public TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

    public int targetScore = 100; // Puntaje objetivo para ganar, ajustable desde el inspector
    public string victoryScene; // Nombre de la escena a cargar al ganar, ajustable en el inspector
    public Canvas pauseCanvas, victoryCanvas, gameOverCanvas; // Canvas para pausa, victoria y derrota

    public Text scoreText; // Texto para mostrar la puntuación
    private int score = 0; // Puntuación actual del jugador
    private bool isPaused = true; // El juego está en pausa al inicio

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < tetrominoes.Length; i++)
        {
            tetrominoes[i].Initialize();
        }

        pauseCanvas.gameObject.SetActive(true);
        victoryCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void StartGame() // Llamado al presionar el botón de continuar
    {
        isPaused = false;
        pauseCanvas.gameObject.SetActive(false);
        SpawnPiece();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Puntuación: " + score;
    }

    public void SpawnPiece()
    {
        if (isPaused) return;

        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];

        activePiece.Initialize(this, spawnPosition, data);

        if (IsValidPosition(activePiece, spawnPosition))
        {
            Set(activePiece);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isPaused = true;
        tilemap.ClearAllTiles();
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition) || tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                AddScore(10); // Añade 10 puntos por cada línea eliminada
            }
            else
            {
                row++;
            }
        }
    }

    public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }

    private void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        if (score >= targetScore)
        {
            Victory();
        }
    }

    private void Victory()
    {
        isPaused = true;
        victoryCanvas.gameObject.SetActive(true);
    }

    public void LoadVictoryScene() // Llamado por el botón de continuar en el canvas de victoria
    {
        if (!string.IsNullOrEmpty(victoryScene))
        {
            SceneManager.LoadScene(victoryScene);
        }
    }

    public void RestartGame() // Llamado por el botón de reiniciar en canvas de victoria y derrota
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
