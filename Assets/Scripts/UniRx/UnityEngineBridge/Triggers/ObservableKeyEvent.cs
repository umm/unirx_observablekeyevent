using System;
using System.Collections.Generic;
using UnityEngine;
using UnityModule;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace UniRx.Triggers {

    public class ObservableKeyEvent : Singleton<ObservableKeyEvent> {

        private static readonly Dictionary<KeyEventType, Func<KeyCode, bool>> DELEGATE_MAP = new Dictionary<KeyEventType, Func<KeyCode, bool>>() {
            { KeyEventType.Key, Input.GetKey },
            { KeyEventType.KeyDown, Input.GetKeyDown },
            { KeyEventType.KeyUp, Input.GetKeyUp },
        };

        private enum KeyEventType {
            Key,
            KeyDown,
            KeyUp,
        }

        private Dictionary<KeyEventType, Dictionary<KeyCode, IObservable<Unit>>> streamMap;

        private Dictionary<KeyEventType, Dictionary<KeyCode, IObservable<Unit>>> StreamMap {
            get {
                if (this.streamMap == default(Dictionary<KeyEventType, Dictionary<KeyCode, IObservable<Unit>>>)) {
                    this.streamMap = new Dictionary<KeyEventType, Dictionary<KeyCode, IObservable<Unit>>>() {
                        { KeyEventType.Key, new Dictionary<KeyCode, IObservable<Unit>>() },
                        { KeyEventType.KeyDown, new Dictionary<KeyCode, IObservable<Unit>>() },
                        { KeyEventType.KeyUp, new Dictionary<KeyCode, IObservable<Unit>>() },
                    };
                }
                return this.streamMap;
            }
        }

        public IObservable<Unit> OnKeyAsObservable(KeyCode keyCode) {
            return this.GetOrCreateSubject(KeyEventType.Key, keyCode);
        }

        public IObservable<Unit> OnKeyDownAsObservable(KeyCode keyCode) {
            return this.GetOrCreateSubject(KeyEventType.KeyDown, keyCode);
        }

        public IObservable<Unit> OnKeyUpAsObservable(KeyCode keyCode) {
            return this.GetOrCreateSubject(KeyEventType.KeyUp, keyCode);
        }

        private IObservable<Unit> GetOrCreateSubject(KeyEventType keyEventType, KeyCode keyCode) {
            if (!this.StreamMap[keyEventType].ContainsKey(keyCode)) {
                this.StreamMap[keyEventType][keyCode] = Observable.EveryUpdate().Where(_ => DELEGATE_MAP[keyEventType](keyCode)).AsUnitObservable();
            }
            return this.StreamMap[keyEventType][keyCode];
        }

    }

    public static class KeyEventComponentExtension {

        public static IObservable<Unit> OnKeyAsObservable(this Component component, KeyCode keyCode) {
            return ObservableKeyEvent.Instance.OnKeyAsObservable(keyCode).TakeUntilDestroy(component);
        }

        public static IObservable<Unit> OnKeyDownAsObservable(this Component component, KeyCode keyCode) {
            return ObservableKeyEvent.Instance.OnKeyDownAsObservable(keyCode).TakeUntilDestroy(component);
        }

        public static IObservable<Unit> OnKeyUpAsObservable(this Component component, KeyCode keyCode) {
            return ObservableKeyEvent.Instance.OnKeyUpAsObservable(keyCode).TakeUntilDestroy(component);
        }

    }

}