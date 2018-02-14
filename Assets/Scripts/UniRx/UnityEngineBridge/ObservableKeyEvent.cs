using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace UniRx {

    public static class ObservableKeyEvent {

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

        public static IObservable<Unit> OnKeyAsObservable(KeyCode keyCode) {
            return GetOrCreateSubject(KeyEventType.Key, keyCode);
        }

        public static IObservable<Unit> OnKeyDownAsObservable(KeyCode keyCode) {
            return GetOrCreateSubject(KeyEventType.KeyDown, keyCode);
        }

        public static IObservable<Unit> OnKeyUpAsObservable(KeyCode keyCode) {
            return GetOrCreateSubject(KeyEventType.KeyUp, keyCode);
        }

        private static IObservable<Unit> GetOrCreateSubject(KeyEventType keyEventType, KeyCode keyCode) {
            return Observable.EveryUpdate().Where(_ => DELEGATE_MAP[keyEventType](keyCode)).AsUnitObservable();
        }

    }

}