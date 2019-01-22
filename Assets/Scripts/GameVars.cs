using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameVars : MonoBehaviour {

    public enum GAMEMODE { DEATHMATCH, MASTERHILL };
    public GAMEMODE m_gameMode;
    public Hashtable players;
    public int teamWinner;

    void Awake () {
        DontDestroyOnLoad(transform.gameObject);
        ResetGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            ResetGame();
    }

    void ResetGame ()
    {
        players = new Hashtable();
        teamWinner = 0;
        PlayerHandler.iNumPlayers = 0;
        m_gameMode = GAMEMODE.DEATHMATCH;
    }

    public string ChangeMode()
    {
        if (m_gameMode == GAMEMODE.DEATHMATCH)
            m_gameMode = GAMEMODE.MASTERHILL;
        else
            m_gameMode = GAMEMODE.DEATHMATCH;
        return m_gameMode.ToString();
    }

    public void InitGame()
    {
        if (m_gameMode == GAMEMODE.DEATHMATCH)
            SceneManager.LoadScene("1_Game_Death");
        else
            SceneManager.LoadScene("2_Game_Rey");
    }
}
