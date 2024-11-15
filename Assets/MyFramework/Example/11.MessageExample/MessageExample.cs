using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace MyFramework
{
    public class MessageExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/11.框架示例", false, 11)]
        private static void MenuClicked()
        {
            MsgCenter.RemoveAllEvent("Do");
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<MessageExample>();
        }
#endif
        void DoSomething(object data)
        {
            
            Debug.LogFormat("Received Do msg:{0}", data);
        }

        void DoSomething2(object data, bool data2)
        {
            if(data2)
                Debug.LogFormat("Received Do msg: {0}", data);
        }

        private void Awake() {

            AddEvent("Do", (Callback<string>)DoSomething);
            AddEvent("Do2", (Callback<string, bool>)DoSomething2);
        }

        private IEnumerator Start()
        {
            BroadcastEvent("Do", "hello");
            BroadcastEvent("Do2", "1", false);

            yield return new WaitForSeconds(1.0f);

            BroadcastEvent("Do", "hello1");
            BroadcastEvent("Do2", "3", true);
        }

        
        //protected override void OnBeforeDestroy()
        //{

        //}
    }

    public partial class EventTypes
    {

    }
}