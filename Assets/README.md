# unirx_observablekeyevent

* キーボード入力イベントの UniRx ラッパー

## Requirement

* [UniRx](https://github.com/neuecc/unirx) ([umm](https://github.com/umm/unirx))

## Install

### With Unity Package Manager

```bash
upm add package dev.upm-packages.unirx-observablekeyevent
```

Note: `upm` command is provided by [this repository](https://github.com/upm-packages/upm-cli).

You can also edit `Packages/manifest.json` directly.

```jsonc
{
  "dependencies": {
    // (snip)
    "dev.upm-packages.unirx-observablekeyevent": "[latest version]",
    // (snip)
  },
  "scopedRegistries": [
    {
      "name": "Unofficial Unity Package Manager Registry",
      "url": "https://upm-packages.dev",
      "scopes": [
        "dev.upm-packages"
      ]
    }
  ]
}
```

### Any other else (classical umm style)

```shell
npm install github:umm/unirx_observablekeyevent
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
