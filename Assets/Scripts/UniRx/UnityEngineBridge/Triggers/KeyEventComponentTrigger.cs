using System;
using UnityEngine;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

namespace UniRx.Triggers {

    public static partial class KeyEventComponentTriggerExtension {

        public static IObservable<Unit> OnKeyAsObservable(this Component component, KeyCode keyCode) {
            return ObservableKeyEvent.OnKeyAsObservable(keyCode).TakeUntilDestroy(component);
        }

        public static IObservable<Unit> OnKeyDownAsObservable(this Component component, KeyCode keyCode) {
            return ObservableKeyEvent.OnKeyDownAsObservable(keyCode).TakeUntilDestroy(component);
        }

        public static IObservable<Unit> OnKeyUpAsObservable(this Component component, KeyCode keyCode) {
            return ObservableKeyEvent.OnKeyUpAsObservable(keyCode).TakeUntilDestroy(component);
        }

    }

}