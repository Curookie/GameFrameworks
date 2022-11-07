using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Curookie.Util 
{
    public class GoogleSheetManager : MonoBehaviour 
    {
    #region VARIABLES & PROPERTIES
        public string URL = "";
        readonly string DATA_SHEET_URL = $"{URL}/export?format=tsv";
    #endregion

    #region UNITY_EVENTS
    #endregion

    #region MAIN_FUNCTIONS
        IEnumerator DownloadDatas() {
            UnityWebRequest www = UnityWebRequest.Get(DATA_SHEET_URL);
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) {
                Debug.Error($"ERROR: {www.error}");
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