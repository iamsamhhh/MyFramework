using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace MyFramework
{
    public class MessageExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/11.Message example", false, 11)]
        private static void MenuClicked()
        {
            MsgCenter.RemoveAllEvent("Do");
            MsgCenter.RemoveAllEvent("Do2");
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<MessageExample>();
        }
#endif
        void DoSomething(object data)
        {
            
            Debug.LogFormat("Received Do msg:{0}", data);
        }

        void DoSomething2(object data)
        {
            Debug.LogFormat("Received Do2 msg: {0}", (bool)data);
        }

        private void Awake() {

            AddEvent("Do", DoSomething);
            AddEvent("Do2", DoSomething2);
        }

        private IEnumerator Start()
        {
            BroadcastEvent("Do", "hello");
            BroadcastEvent("Do2", false);

            yield return new WaitForSeconds(1.0f);

            BroadcastEvent("Do", "hello1");
            BroadcastEvent("Do2", true);
            Destroy(this);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        void OnDestroy()
        {
            BroadcastEvent("Do", "event removing...");
            // RemoveEvent("Do", DoSomething);
            // RemoveEvent("Do2", DoSomething2);
            RemoveAllLocalEvents();
            Debug.Log("events removed");
            BroadcastEvent("Do", "haha");
        }
    }
}