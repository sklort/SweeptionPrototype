using UnityEngine;
using UnityEngine.SceneManagement;

public class TutEnd : MonoBehaviour
{
   public void endTut()
   {
      SceneManager.LoadScene("TitleScreen");
   }
}
