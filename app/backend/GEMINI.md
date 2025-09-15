## 回答規約
- 常に日本語で回答すること。

## コーディング規約
### 設計思想について
- SOLID 原則を意識したドメイン駆動設計であること
- Core/Domain 層, Core/Application層, Infrastructure層, Presentation層 に分割されていること

### Core/Domain 層について
- ValueObject や Entity、Aggregation、DomainServiceなどのドメインロジックを記述する
- どの層にも依存しない。

### Core/Application 層について
- ドメインオブジェクトが提供するメソッドを組み合わせて、一連のドメインオブジェクトに対する処理を実装する
- Core/Domain層に依存する。

### Infrastructure 層について
- DB など、外部に依存する処理を請け負う
- Core/Domain 層や Core/Application 層に定義されたインタフェースを実装する。

### Presentation 層について
- リクエストのバインディングやバリデーション、レスポンスの返却を請け負う
- Application 層に依存する。
