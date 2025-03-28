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
   private int gridMineCount = 0;
   public int flagCount;

   private Board board;
   private Cell[,] state;

   private AudioSource _source;
   [SerializeField] private AudioClip _start;
   [SerializeField] private AudioClip _flag;

   private void OnValidate()
   {
      width = gameBoss.width;
      height = gameBoss.height;
      float maxMineFloat = (width * height) * 0.3f;
      int maxMine = Mathf.FloorToInt(maxMineFloat);
      Debug.Log(maxMine);
      mineCount = Mathf.Clamp(mineCount, 0, maxMine);
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
      flagCount = gameBoss.mineCount;
      playerHealth = 1;
      NewGame();
   }

   private void NewGame()
   {
      gridMineCount = 0;
      width = gameBoss.width;
      height = gameBoss.height;
      firstClick = false;
      mineCount = gameBoss.mineCount;
      state = new Cell[width, height];
      gameOver = false;

      GenerateCells();
      // GenerateMines();
      // GenerateNumbers();
      
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
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
      Cell mouseCell = GetCell(cellPosition.x, cellPosition.y);
      int firstX = cellPosition.x;
      int firstY = cellPosition.y;
      for (int i = 0; i < mineCount; i++)
      {
         int x = Random.Range(0, width);
         int y = Random.Range(0, height);
         // make sure to not spawn on/near mine
          if (x == firstX && y == firstY)
          {
             i -= 1;
             continue;
          }
          if (x == (firstX - 1) && y == firstY)
          {
             i -= 1;
             continue;
          }
         
          if (x == (firstX + 1) && y == firstY)
          {
             i -= 1;
             continue;
          }
         
          if (x == firstX && y == (firstY - 1))
          {
             i -= 1;
             continue;
          }
          if (x == firstX && y == (firstY + 1))
          {
             i -= 1;
             continue;
          }
          if (x == (firstX - 1) && y == (firstY - 1))
          {
             i -= 1;
             continue;
          }
          if (x == (firstX + 1) && y == (firstY - 1))
          {
             i -= 1;
             continue;
          }
          if (x == (firstX - 1) && y == (firstY + 1))
          {
             i -= 1;
             continue;
          }
          if (x == (firstX + 1) && y == (firstY + 1))
          {
             i -= 1;
             continue;
          }
         

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
         gridMineCount++;
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
   
   private int CountFlags(int cellX, int cellY)
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

            if (GetCell(x, y).flagged || GetCell(x, y).exploded)
            {
               count++;
            }
         }
      }

      return count;
   }
   
   private void Update()
   {
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
               GenerateMines();
               GenerateNumbers();
               state[cellPosition.x, cellPosition.y] = cell;
               board.Draw(state);
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
               RevealAdjacent();

            }

         }
         
      }
      
      checkWin();
   }

   private void CheckReset()
   {
      int flags = 0;
      for (int x = 0; x < width; x++)
      {
         for (int y = 0; y < height; y++)
         {
            Cell cell = state[x, y];
      
            if (cell.flagged || cell.exploded)
            {
               flags++;
            }

            if (flags >= gridMineCount)
            {
               NewGame();
            }
         }
      }
      
      
      
   }
   
   private void CheckClear()
   {
      for (int x = 0; x < width; x++)
      {
         for (int y = 0; y < height; y++)
         {
            Cell cell = state[x, y];

            if (cell.type == Cell.Type.Mine )
            {
               if (cell.flagged || cell.revealed)
               {
                  CheckReset();
               }
            }
            
         }
      }
      
      // enemyHealth.healthbarHealthValue = currentMineCount;
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

      if (cell.type == Cell.Type.Mine && !cell.flagged)
      {
         currentMineCount -= 1;
         
      }
      else if (cell.type == Cell.Type.Mine && cell.flagged)
      {
         currentMineCount++;
      }

      if (cell.type == Cell.Type.Number && !cell.flagged)
      {
         currentMineCount++;
      }

      if (cell.type == Cell.Type.Number && cell.flagged)
      {
         currentMineCount -= 1;
      }


      cell.flagged = !cell.flagged;
      state[cellPosition.x, cellPosition.y] = cell;
      board.Draw(state); 
      CheckClear();
      _source.clip = _flag;
      _source.Play();

      if (cell.flagged)
      {
         flagCount -= 1;
      }

      if (!cell.flagged)
      {
         flagCount++;
      }
      
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
            CheckClear();
            break;

         case Cell.Type.Empty:
            Flood(cell);
            break;
         
         default:
            cell.revealed = true;
            state[cellPosition.x, cellPosition.y] = cell;
            CheckClear();
            break;
      }
      
      
      board.Draw(state);
   }
   
   private void RevealAdjacent()
   {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
      Cell cell = GetCell(cellPosition.x, cellPosition.y);
      int nearFlagCount = CountFlags(cellPosition.x, cellPosition.y);
      int adjMineCount = CountMines(cellPosition.x, cellPosition.y);


      if (nearFlagCount == 0)
      {
         return;
      }

      if (nearFlagCount < adjMineCount)
      {
         return;
      }

      if (nearFlagCount > adjMineCount)
      {
         return;
      }

      for (int xFalseFlag = -1; xFalseFlag <= 1; xFalseFlag++)
      {
         for (int yFalseFlag = -1; yFalseFlag <= 1; yFalseFlag++)
         {
            int adjX = cellPosition.x + xFalseFlag;
            int adjY = cellPosition.y + yFalseFlag;
            cell = GetCell(adjX, adjY);
            
            if (cell.type == Cell.Type.Invalid)
            {
                  continue;
            }
            
            if (cell.flagged && cell.type != Cell.Type.Mine)
            {
               cell.flagged = false;
               state[adjX, adjY] = cell;
               board.Draw(state);

               cell.revealed = true;
               state[adjX, adjY] = cell;
               board.Draw(state);
               
               flagCount++;
            }

            if (cell.exploded == false)
            {
               if (cell.type == Cell.Type.Mine && cell.flagged == false)
               {
                  cell.exploded = true;
                  cell.revealed = true;
                  state[adjX, adjY] = cell;
                  board.Draw(state);

                  currentMineCount -= 1;
                  playerHealth -= 1;
                  gameBoss.playerHealthMain -= 1;
                  playerHealthBar.SetHealth(playerHealth);

                  return;
               }
            }
         }
      }
      
      for (int revCellX = -1; revCellX <= 1; revCellX++)
      {
            for (int revCellY = -1; revCellY <= 1; revCellY++)
            {
               int revX = revCellX + cellPosition.x;
               int revY = revCellY + cellPosition.y;
               cell = GetCell(revX, revY);
               
               if (cell.type == Cell.Type.Invalid)
               {
                     continue;
               }
               
               if (cell.type == Cell.Type.Number)
               {
                  cell.revealed = true;
                  state[revX, revY] = cell;
                  board.Draw(state);
               }

               if (cell.type == Cell.Type.Empty)
               {
                  Flood(cell);
               }
            }
      }
   
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

      flagCount -= 1;
      
      CheckClear();
      
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
         Flood(GetCell(cell.position.x - 1, cell.position.y - 1));
         Flood(GetCell(cell.position.x - 1, cell.position.y + 1));
         Flood(GetCell(cell.position.x + 1, cell.position.y - 1));
         Flood(GetCell(cell.position.x + 1, cell.position.y + 1));
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
