# 家計簿アプリ作成プロジェクト

## 質問回答時の制約
- 英語で思考し、日本語で回答すること
- C# (dotnet) に関する質問である場合、python に類似した記述があれば参考情報として提示すること

## プロジェクト全体
### ディレクトリ構成
```txt
workspace/
├── .claude/                 # CLAUDE の設定
│   └── settings.local.json
├── .devcontainer/           # devcontainer の設定
│   ├── backend/
│   └── frontend/
├── .docker/
│   └── db/                  # ローカル開発環境の DB
├── .gemini/                 # Gemini CLI の設定
│   └── settings.json
├── .vscode/                 # vscode の設定
│   ├── launch.json
│   ├── settings.json
│   └── tasks.json
├── app/                     # アプリケーション本体
│   ├── backend/
│   └── frontend/
├── docs/
├── .env
├── .env.sample
├── .gitignore
├── CLAUDE.md
├── README.md
├── compose.devcontainer.yml
└── package-lock.json
```

## FrontEnd 開発関連
[フロントエンド](app/frontend) の開発を行う際の規約

## BackEnd 開発関連
[バックエンド](app/backend) の開発を行う際の規約

### 技術スタック
- 言語: C#
- フレームワーク: .NET 9
- DB: MySQL

### ディレクトリ構成
```txt
backend/
├── src/                    # ソースコード
│   ├── Core/
│   │   ├── Application/    # Application 層
│   │   └── Domain/         # Domain 層
│   ├── Defines/            # 共通の定義
│   ├── Infrastructure/     # Infrastructure 層
│   └── Presentation/       # Presentation 層
├── .editorconfig
├── Dockerfile
├── GEMINI.md
└── README.md
```

### コーディング規約
#### 全体
- SOLID 原則を意識したドメイン駆動設計を意識すること

#### Domain 層について
- ValueObject や Entity、Aggregation、DomainServiceなどのドメインロジックを記述する
- どの層にも依存しない。

#### Application 層について
- ドメインオブジェクトが提供するメソッドを組み合わせて、一連のドメインオブジェクトに対する処理を実装する
- Domain層に依存する。

#### Infrastructure 層について
- DB など、外部に依存する処理を請け負う
- Domain 層や Application 層に定義されたインタフェースを実装する。

#### Presentation 層について
- リクエストのバインディングやバリデーション、レスポンスの返却を請け負う
- Application 層に依存する。
