using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Collections;
using TMPro;

namespace NetworkConnectionClient
{

    public class Player : NetworkBehaviour
    {
        public TextMeshProUGUI textarea;

        public NetworkVariable<FixedString64Bytes> Message = new NetworkVariable<FixedString64Bytes>();

        // Start is called before the first frame update
        void Start()
        {
/*            status = GameObject.Find("Status").GetComponent<TextMeshProUGUI>();
            buttons = GameObject.Find("LayoutVerticalButtons");
            gameActiveButtons = GameObject.Find("LayoutVerticalGameActiveButtons");*/
        }

        public void SendGreeting()
        {
            if (IsHost)
            {
                SubmitMessageServerRpc("Host says hi");
            }
            else if (IsClient)
            {
                SubmitMessageServerRpc("Client says hi");
            }

        }
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        [ServerRpc(RequireOwnership = false)]
        void SubmitMessageServerRpc(string message, ServerRpcParams rpcParams = default)
        {
            Message.Value = message;
        }

/*        [ClientRpc]
        void SubmitMessageClientRpc(ClientRpcParams rpcParams = default)
        {
            Message.Value = "Host says hi";
        }*/

        // Update is called once per frame
        void Update()
        {
            if (textarea == null)
            {
                textarea = GameObject.Find("Greeting").GetComponent<TextMeshProUGUI>();
            }

            if (textarea != null && Message != null)
            {
                textarea.text = Message.Value.ToString();
            }
        }
    }

}