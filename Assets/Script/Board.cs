using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoes;
    // public Tetromino tetromino{ get; private set; }
    public Piece activePiece { get; private set; }
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);
    private Piece piece1;
    private int numberofrowsclear=0;
    public int scoreoneline=40;
    public int scoretwoline=80;
    public int scorethreeline=120;
    public int scorefourline=160;
    public int lineoneclear=1;
    public int linetwoclear=2;
    public int linethreeclear=3;
    public int linefourclear=4;
    public Text score;
    public Text Highscore;
    public Text line;
    private int currentScore=0;
    private int currentline=0;
    private int startingHighscore;
    public RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
            
        }
    }
     private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        this.piece1=GetComponentInChildren<Piece>();
        for (int i = 0; i < this.tetrominoes.Length; i++) {
            this.tetrominoes[i].Initialize();
        }
    }
    private void Start()
    {
        // currentScore=0;
        // Highscore.text="0";
        SpawnPiece();
        // Debug.Log(boardSize.x);
        
        // startingHighscore=PlayerPrefs.GetInt("highScore");
        Highscore.text=PlayerPrefs.GetInt("highScore").ToString();
        // PlayerPrefs.SetInt("highScore",0);
        

    }
    public void SpawnPiece()
    {
        // if(!)
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(activePiece, spawnPosition)) {
            Set(this.activePiece);
            // updateHighscore();
        } else {
            GameOver();
        }
    }
    public void updateUI(){
        score.text=currentScore.ToString();
        line.text=currentline.ToString();
    }
    public void upadatescore(){
        if(numberofrowsclear>0){
            if(numberofrowsclear==1){
                clearoneline();
            }
            else if(numberofrowsclear==2){
                cleartwoline();
            }
            else if(numberofrowsclear==3){
                clearthreeline();
            }
            else if(numberofrowsclear==4){
                clearfourline();
            }
            numberofrowsclear=0;
            updateHighscore();
        }
    }
    public void updateHighscore(){
        if(currentScore>PlayerPrefs.GetInt("highScore")){
            PlayerPrefs.SetInt("highScore",currentScore);
        }

    }
    public void clearoneline(){
        currentScore+=scoreoneline;
        currentline+=lineoneclear;
    }
    public void cleartwoline(){
        currentScore+=scoretwoline;
        currentline+=linetwoclear;
    }
    public void clearthreeline(){
        currentScore+=scorethreeline;
        currentline+=linethreeclear;
    }
    public void clearfourline(){
        currentScore+=scorefourline;
        currentline+=linefourclear;
    }
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;
            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (this.tilemap.HasTile(tilePosition)) {
                return false;
            }
            // if(FindObjectOfType<Piece>().CheckIsaboveGrid()){

            // }
        }

        return true;
    }
    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        // Clear from bottom to top
        while (row < bounds.yMax)
        {
            // Only advance to the next row if the current is not cleared
            // because the tiles above will fall down when a row is cleared
            if (IsLineFull(row)) {
                LineClear(row);
            } else {
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

            // The line is not full if a tile is missing
            if (!tilemap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        // Clear all tiles in the row
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);
        }
        numberofrowsclear++;
        // Shift every row above down one
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
        piece1.playlandsound();
    }
    public void GameOver(){
        SceneManager.LoadScene(2);
    }
}

