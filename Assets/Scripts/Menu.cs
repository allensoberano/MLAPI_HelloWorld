using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;
using MLAPI.Transports.UNET;
using System;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public InputField inputField;

    private void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = false;
        //if connection is correct then approve
        string password = System.Text.Encoding.ASCII.GetString(connectionData);
        if(password == "mygame")
        {
            approve = true;
        }

        //If approve is true, the connection gets added. If it's false. The client gets disconnected
        callback(true, null, approve, new Vector3(0,10,0), Quaternion.identity);
    }

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        HideMenuPanel();
    }

    public void Join()
    {
        if(inputField.text.Length <= 0)
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
        } else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = inputField.text;
        }

        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("mygame");
        NetworkManager.Singleton.StartClient();
        HideMenuPanel();
    }

    private void HideMenuPanel()
    {
        menuPanel.SetActive(false);
    }


}
