using System;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
   //
   //game logic implemented by following Zigurous' tutorial, then tweaked
   //

   [SerializeField] private GameObject playerHealthObject;
   [SerializeField] PlayerHealth playerHealthBar;
   [SerializeField] GameBoss gameBoss;
   [SerializeField] private GameObject gameBossObject;
   [SerializeField] private Timer timer;
   [SerializeField] private GameObject tickOnObject;
   [SerializeField] private EnemyHealth enemyHealth;

   private int playerHealth;
   public int width;
   public int height;
   public int mineCount;
   public int currentMineCount;
   private bool gameOver;
   private bool firstClick;

   private Board board;
   private Cell[,] state;

   private AudioSource _source;
   [SerializeField] private AudioClip _start;
   [SerializeField] private AudioClip _flag;

   private void OnValidate()
   {
      mineCount = Mathf.Clamp(mineCount, 0, width * height);
   }

   private void Awake()
   {
      
      gameBossObject = GameObject.Find("GameBoss");
      gameBoss = gameBossObject.GetComponent<GameBoss>();
      board = GetComponentInChildren<Board>();
      _source = GetComponent<AudioSource>();
   }

   private void Start()
   {
      _source.clip = _start;
      _source.Play();
      currentMineCount = gameBoss.mineCount;
      playerHealth = 1;
      NewGame();
   }

   private void NewGame()
   {
      width = gameBoss.width;
      height = gameBoss.height;
      mineCount = gameBoss.mineCount;
      state = new Cell[width, height];
      gameOver = false;

      GenerateCells();
      GenerateMines();
      GenerateNumbers();
      
      Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
      float cameraSize = ((height/2.3f) + (1 * gameBoss.globalDifficulty));
      Camera.main.orthographicSize = cameraSize;
      board.Draw(state);
   }
   
   private void GenerateCells()
   {
      for (int x = 0; x < width; x++)
      {
         for (int y = 0; y < height; y++)
         {
            Cell cell = new Cell();
            cell.position = new Vector3Int(x, y, 0);
            cell.type = Cell.Type.Empty;
            state[x, y] = cell;
         }
      }
   }

   private void GenerateMines()
   {
      for (int i = 0; i < mineCount; i++)
      {
         int x = Random.Range(0, width);
         int y = Random.Range(0, height);

         while (state[x, y].type == Cell.Type.Mine)
         {
            x++;

            if (x >= width)
            {
               x = 0;
               y++;

               if (y >= height)
               {
                  y = 0;
               }
            }
         }

         state[x, y].type = Cell.Type.Mine;
      }
   }

   private void GenerateNumbers()
   {
      for (int x = 0; x < width; x++)
      {
         for (int y = 0; y < height; y++)
         {
            Cell cell = state[x, y];

            if (cell.type == Cell.Type.Mine)
            {
               continue;
            }

            cell.number = CountMines(x, y);

            if (cell.number > 0)
            {
               cell.type = Cell.Type.Number;
            }

            state[x, y] = cell;
         }
      }
   }

   private int CountMines(int cellX, int cellY)
   {
      int count = 0;

      for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
      {
         for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
         {
            if (adjacentX == 0 && adjacentY == 0)
            {
               continue;
            }

            int x = cellX + adjacentX;
            int y = cellY + adjacentY;

            if (GetCell(x, y).type == Cell.Type.Mine)
            {
               count++;
            }
         }
      }

      return count;
   }

   private void flagNumber()
   {
      int flagCount = 0;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
      Cell cell = GetCell(cellPosition.x, cellPosition.y);
      
      for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
      {
         for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
         {
            if (adjacentX == 0 && adjacentY == 0)
            {
               continue;
            }

            int x = cellPosition.x + adjacentX;
            int y = cellPosition.y + adjacentY;

            if (GetCell(x, y).type != Cell.Type.Invalid && cell.flagged == true)  
            {
               flagCount++;
            }
         }
      }
   }
   
   
   private void Update()
   {
      Debug.Log(currentMineCount);
      playerHealth = gameBoss.playerHealthMain;
      playerHealthBar.healthbarHealthValue = playerHealth;
      ifLost();
      if (!gameOver)
      {
         if (firstClick == false)
         {
            if (Input.GetMouseButtonDown(0))
            {
               Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
               Cell cell = GetCell(cellPosition.x, cellPosition.y);
               cell.type = Cell.Type.Empty;
               state[cellPosition.x, cellPosition.y] = cell;
               board.Draw(state);
               Reveal();
               firstClick = true;
            }

         }
         else if  (firstClick == true)
         {
            if (Input.GetKeyDown(KeyCode.F))
            {
               Flag();
            }
            else if (Input.GetMouseButtonDown(0))
            {
               Reveal();
            }

         }

      }
      checkWin();
   }

   private void CheckReset()
   {
      for (int x = 0; x < width; x++)
      {
         for (int y = 0; y < height; y++)
         {
            Cell cell = state[x, y];

            if (cell.type != Cell.Type.Mine && !cell.revealed)
            {
               return;
            }

         }
      }
      NewGame();
      enemyHealth.healthbarHealthValue = currentMineCount;
   }

   private void Flag()
   {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
      Cell cell = GetCell(cellPosition.x, cellPosition.y);

      if (cell.type == Cell.Type.Invalid || cell.revealed)
      {
         return;
      }

      if (cell.type == Cell.Type.Mine)
      {
         currentMineCount -= 1;
         
      }
      else if (cell.flagged)
      {
         currentMineCount -= 1;
      }
      else
      {
         currentMineCount++;
      }


      cell.flagged = !cell.flagged;
      state[cellPosition.x, cellPosition.y] = cell;
      board.Draw(state);
      _source.clip = _flag;
      _source.Play();
   }

   private void Reveal()
   {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
      Cell cell = GetCell(cellPosition.x, cellPosition.y);
      if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
      {
         return;
      }

      switch (cell.type)
      {
         case Cell.Type.Mine:
            Explode(cell);
            break;

         case Cell.Type.Empty:
            Flood(cell);
            CheckReset();
            break;

         default:
            cell.revealed = true;
            state[cellPosition.x, cellPosition.y] = cell;
            CheckReset();
            break;
      }
      
      board.Draw(state);
   }

   private void RevealAdjacent(Cell cell)
   {
      
   }
   

   private void Explode(Cell cell)
   {
      cell.revealed = true;
      cell.exploded = true;
      state[cell.position.x, cell.position.y] = cell;
      
      // IMPLEMENT HP LOSS
      currentMineCount -= 1;
      playerHealth -= 1;
      gameBoss.playerHealthMain -= 1;
      playerHealthBar.SetHealth(playerHealth);


   }



   private void ifLost()
   {
      if (playerHealth <= 0)
      {
         tickOnObject.GetComponent<NewMonoBehaviourScript>().enabled = false;
         SceneManager.LoadScene("LoseScreen");
      }
   }

   private void Flood(Cell cell)
   {
      if (cell.revealed) return;
      if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;

      cell.revealed = true;
      state[cell.position.x, cell.position.y] = cell;

      if (cell.type == Cell.Type.Empty)
      {
         Flood(GetCell(cell.position.x - 1, cell.position.y));
         Flood(GetCell(cell.position.x + 1,cell.position.y));
         Flood(GetCell(cell.position.x, cell.position.y - 1));
         Flood(GetCell(cell.position.x, cell.position.y + 1));
      }
   }

   private Cell GetCell(int x, int y)
   {
      if (IsValid(x, y))
      {
         return state[x, y];
      }
      else
      {
         return new Cell();
      }
   }

   private bool IsValid(int x, int y)
   {
      return x >= 0 && x < width && y >= 0 && y < height;
   }

   private void checkWin()
   {
      if (currentMineCount <= 0)
      {
         SceneManager.LoadScene("WinScreen");
      }
   }
}
