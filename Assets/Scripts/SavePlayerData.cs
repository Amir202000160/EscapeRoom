using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePlayerData : MonoBehaviour
{
   [SerializeField] public TMP_InputField playerName;
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveDataAndLoadSceneB()
    {
        string playerNamee = playerName.text;
        PlayerPrefs.SetString("PLayerName",playerNamee);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
