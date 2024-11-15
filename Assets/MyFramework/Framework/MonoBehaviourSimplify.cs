using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

namespace MyFramework
{    
    public abstract partial class MonoBehaviourSimplify : MonoBehaviour
    {
        public void Show()
        {
            GameObjectSimplify.Show(gameObject);
        }

        public void Hide()
        {
            GameObjectSimplify.Hide(gameObject);
        }

        public void Identity()
        {
            TransformSimplify.Identity(transform);
        }

        public void Delay(float seconds, Callback onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        private IEnumerator DelayCoroutine(float seconds, Callback onFinished)
        {
            yield return new WaitForSeconds(seconds);

            onFinished();
        }

        #region msgCenter

        private Dictionary<string, Delegate> mEvents = new Dictionary<string, Delegate>();

        #region Add event

        private void AddEventCheck(string eventType, Delegate callback)
        {
            if (!mEvents.ContainsKey(eventType))
            {
                mEvents.Add(eventType, null);
            }
            Delegate d = mEvents[eventType];

            if (d != null && d.GetType() != callback.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件对应的类型为{1}，要添加的委托类型为{2}", eventType, d.GetType(), callback.GetType()));
            }
        }

        public void AddEvent(string eventType, Callback callback)
        {
            AddEventCheck(eventType, callback);
            MsgCenter.AddEvent(eventType, callback);
            mEvents[eventType] = (Callback)mEvents[eventType] + callback;
        }

        public void AddEvent<T>(string eventType, Callback<T> callback)
        {
            AddEventCheck(eventType, callback);
            MsgCenter.AddEvent(eventType, callback);
            mEvents[eventType] = (Callback<T>)mEvents[eventType] + callback;
        }

        public void AddEvent<A, B>(string eventType, Callback<A, B> callback)
        {
            AddEventCheck(eventType, callback);
            MsgCenter.AddEvent(eventType, callback);
            mEvents[eventType] = (Callback<A, B>)mEvents[eventType] + callback;
        }

        public void AddEvent<A, B, C>(string eventType, Callback<A, B, C> callback)
        {
            AddEventCheck(eventType, callback);
            MsgCenter.AddEvent(eventType, callback);
            mEvents[eventType] = (Callback<A, B, C>)mEvents[eventType] + callback;
        }

        public void AddEvent<A, B, C, D>(string eventType, Callback<A, B, C, D> callback)
        {
            AddEventCheck(eventType, callback);
            MsgCenter.AddEvent(eventType, callback);
            mEvents[eventType] = (Callback<A, B, C, D>)mEvents[eventType] + callback;
        }

        public void AddEvent<A, B, C, D, E>(string eventType, Callback<A, B, C, D, E> callback)
        {
            AddEventCheck(eventType, callback);
            MsgCenter.AddEvent(eventType, callback);
            mEvents[eventType] = (Callback<A, B, C, D, E>)mEvents[eventType] + callback;
        }

        #endregion

        #region remove event

        private void RemoveEventCheck(string eventType, Delegate callback)
        {
            if (mEvents.ContainsKey(eventType))
            {
                Delegate d = mEvents[eventType];

                if (d == null)
                {
                    throw new Exception(string.Format("该事件{0}没有委托，无法移除", eventType));
                }
                else if (d.GetType() != callback.GetType())
                {
                    throw new Exception(string.Format("该事件{0}的委托{1}，与要移除的委托{2}类型不一致，无法移除", eventType, d.GetType(), callback.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("事件{0}不存在", eventType));
            }
        }

        public void RemoveEvent(string eventType, Callback callback)
        {
            RemoveEventCheck(eventType, callback);
            mEvents[eventType] = (Callback)mEvents[eventType] - callback;
            MsgCenter.RemoveEvent(eventType, callback);
        }


        public void RemoveEvent<A>(string eventType, Callback<A> callback)
        {
            RemoveEventCheck(eventType, callback);
            mEvents[eventType] = (Callback<A>)mEvents[eventType] - callback;
            MsgCenter.RemoveEvent(eventType, callback);
        }


        public void RemoveEvent<A, B>(string eventType, Callback<A, B> callback)
        {
            RemoveEventCheck(eventType, callback);
            mEvents[eventType] = (Callback<A, B>)mEvents[eventType] - callback;
            MsgCenter.RemoveEvent(eventType, callback);
        }


        public void RemoveEvent<A, B, C>(string eventType, Callback<A, B, C> callback)
        {
            RemoveEventCheck(eventType, callback);
            mEvents[eventType] = (Callback<A, B, C>)mEvents[eventType] - callback;
            MsgCenter.RemoveEvent(eventType, callback);
        }


        public void RemoveEvent<A, B, C, D>(string eventType, Callback<A, B, C, D> callback)
        {
            RemoveEventCheck(eventType, callback);
            mEvents[eventType] = (Callback<A, B, C, D>)mEvents[eventType] - callback;
            MsgCenter.RemoveEvent(eventType, callback);
        }


        public void RemoveEvent<A, B, C, D, E>(string eventType, Callback<A, B, C, D, E> callback)
        {
            RemoveEventCheck(eventType, callback);
            mEvents[eventType] = (Callback<A, B, C, D, E>)mEvents[eventType] - callback;
            MsgCenter.RemoveEvent(eventType, callback);
        }

        #endregion

        #region Broadcast event

        public void BroadcastEvent(string eventType)
        {
            MsgCenter.BroadcastEvent(eventType);
        }

        public void BroadcastEvent<A>(string eventType, A arg)
        {
            MsgCenter.BroadcastEvent(eventType, arg);
        }

        public void BroadcastEvent<A, B>(string eventType, A arg1, B arg2)
        {
            MsgCenter.BroadcastEvent(eventType, arg1, arg2);
        }

        public void BroadcastEvent<A, B, C>(string eventType, A arg1, B arg2, C arg3)
        {
            MsgCenter.BroadcastEvent(eventType, arg1, arg2, arg3);
        }

        public void BroadcastEvent<A, B, C, D>(string eventType, A arg1, B arg2, C arg3, D arg4)
        {
            MsgCenter.BroadcastEvent(eventType, arg1, arg2, arg3, arg4);
        }

        public void BroadcastEvent<A, B, C, D, E>(string eventType, A arg1, B arg2, C arg3, D arg4, E arg5)
        {
            MsgCenter.BroadcastEvent(eventType, arg1, arg2, arg3, arg4, arg5);
        }

        #endregion

        #endregion

        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (var msgkey in mEvents.Keys)
            {
                MsgCenter.RemoveEvent(msgkey, (Callback)mEvents[msgkey]);
                Debug.Log(mEvents[msgkey].GetType());
            }
            mEvents.Clear();
        }

        protected void OnBeforeDestroy()
        {

        }
    }
}