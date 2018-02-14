# unirx_observablekeyevent

* キーボード入力イベントの UniRx ラッパー

## Requirement

* [UniRx](https://github.com/neuecc/unirx) ([umm](https://github.com/umm-projects/unirx))

## Install

```shell
npm install github:umm-projects/unirx_observablekeyevent
```

## Usage

### シンプル版

```csharp
using UniRx;
using UnityEngine;

public class Sample {

    public void Hoge() {
        ObservableKeyEvent.OnKeyAsObservable(KeyCode.A).Subscribe(_ => Debug.Log("A"));
        ObservableKeyEvent.OnKeyDownAsObservable(KeyCode.B).Subscribe(_ => Debug.Log("B"));
        ObservableKeyEvent.OnKeyUpAsObservable(KeyCode.C).Subscribe(_ => Debug.Log("C"));
    }

}
```

* ストリームの Dispose などを自前で行う必要があります

### Component 版

```csharp
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Sample : MonoBehaviour {

    private void Start() {
        this.OnKeyAsObservable(KeyCode.A).Subscribe(_ => Debug.Log("A"));
        this.OnKeyDownAsObservable(KeyCode.B).Subscribe(_ => Debug.Log("B"));
        this.OnKeyUpAsObservable(KeyCode.C).Subscribe(_ => Debug.Log("C"));
    }

}
```

* 該当のインスタンスが Destroy されると自動的に購読が Dispose されます

## License

Copyright (c) 2018 Tetsuya Mori

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)

