using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Urachacha
{
    public class LobbyTopPanel : MonoBehaviour
    {
        private readonly string connectionStatusMessage = "    연결 상태: ";

        [Header("UI References")]
        public Text ConnectionStatusText;

        #region UNITY

        public void Update()
        {
            ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        }

        #endregion
    }
}