using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePlayerData : MonoBehaviour
{
    public TMP_InputField playerName;
    public GameObject Errortxt;
    void Start()
    {
        Errortxt.SetActive(false);
        playerName.text = null;

    }

    public void SaveDataAndLoadSceneB()
    {
        if (string.IsNullOrEmpty(playerName.text))

        {
            Errortxt.SetActive(true);
            Debug.Log("Player name is empty. Please enter a name.");
        }
        else
        {
            string playerNamee = playerName.text;
            PlayerPrefs.SetString("PLayerName", playerNamee);
            PlayerPrefs.Save();
            MainMENUManager.Instance.StartGame();

        }

    }

}
