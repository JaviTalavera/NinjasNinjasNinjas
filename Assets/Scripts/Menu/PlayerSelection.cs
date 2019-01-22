using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {

    public imageCharacter m_character;
    private PlayerHandler m_playerHandler;
    private Image m_selectedImage;
    private bool m_CanMove = true;
    private void OnEnable()
    {
        m_selectedImage = GameObject.Find("imagePlayer" + m_playerHandler.m_playerId).GetComponent<Image>();
        ActualizaPuntero();
    }

    public void SetPlayerHandler(PlayerHandler playerHandler)
    {
        this.m_playerHandler = playerHandler;
    }

    public void ActualizaPuntero()
    {
        gameObject.transform.SetParent(m_character.transform);
        gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        m_selectedImage.sprite = m_character.GetComponent<Image>().sprite;
    }

	// Update is called once per frame
	void Update ()
    {
        if (m_playerHandler.m_selected)
        {
            if (m_CanMove && Input.GetAxis("Horizontal" + m_playerHandler.m_internalId) != 0)
            {
                string mode = GameObject.Find("GameVars").GetComponent<GameVars>().ChangeMode();
                GameObject.Find("tbGameMode").GetComponent<Text>().text = "<" + mode + ">";
                m_CanMove = false;
            }
            else if (!m_CanMove && Input.GetAxis("Horizontal" + m_playerHandler.m_internalId) == 0)
                m_CanMove = true;
            return;
        }
        if (m_CanMove && Input.GetAxis("Horizontal" + m_playerHandler.m_internalId) > 0 && m_character.m_derecha != null)
        {
            m_character = m_character.m_derecha;
            m_CanMove = false;
        }
        else if (m_CanMove && Input.GetAxis("Horizontal" + m_playerHandler.m_internalId) < 0 && m_character.m_izquierda != null)
        {
            m_character = m_character.m_izquierda;
            m_CanMove = false;
        }
        else if (m_CanMove && Input.GetAxis("Vertical" + m_playerHandler.m_internalId) > 0 && m_character.m_arriba != null)
        {
            m_character = m_character.m_arriba;
            m_CanMove = false;
        }
        else if (m_CanMove && Input.GetAxis("Vertical" + m_playerHandler.m_internalId) < 0 && m_character.m_abajo != null)
        {
            m_character = m_character.m_abajo;
            m_CanMove = false;
        }
        else if (Input.GetAxis("Vertical" + m_playerHandler.m_internalId) == 0 && Input.GetAxis("Horizontal" + m_playerHandler.m_internalId) == 0)
            m_CanMove = true;
        ActualizaPuntero();
    }
}
