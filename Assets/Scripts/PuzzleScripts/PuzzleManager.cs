using UnityEngine;
using TMPro; // Include this if you want to change the text on the sign

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Components")]
    public PressurePlate[] plates; // Drag all 3 black planes here
     
    [Header("Win Effects")]
    public GameObject[] Objects;
    public Animator doorAnimator; // Animator for the door
    public SceneLoadTrigger sceneLoadTrigger; 


    public void CheckForWin()
    {
        foreach (PressurePlate plate in plates)
        {
            if (plate.isActivated == false)
            {
                return; 
            }
        }
 WinGame();
    }

    void WinGame()  
    {
        Debug.Log("YOU WIN!");
        if (Objects != null)
        {
            foreach (GameObject obj in Objects)
            {
                obj.SetActive(true);
            }
        }

        
        sceneLoadTrigger.LoadScenes();
        sceneLoadTrigger.UnLoadScenes();
    }
}