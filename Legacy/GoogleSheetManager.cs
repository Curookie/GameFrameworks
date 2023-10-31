using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Curookie.Util 
{
    public class GoogleSheetManager : MonoBehaviour 
    {
    #region VARIABLES & PROPERTIES
        public string googleSheetURL = "";
    #endregion

    #region UNITY_EVENTS
    #endregion

    #region MAIN_FUNCTIONS
        IEnumerator DownloadDatas() {
            UnityWebRequest www = UnityWebRequest.Get($"{googleSheetURL}/export?format=tsv");
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError($"ERROR: {www.error}");
            } 

            var data = www.downloadHandler.text;
            print(data);
        }
    #endregion

    #region SUB_FUNCTIONS
        void SetItemSO(string tsv) {
            
        }
    #endregion

    #region UI_FUNCTIONS
    #endregion

    #region COROUTINE_FUNCTIONS
    #endregion
        
    }
}