# 家計簿アプリ作成プロジェクト

## 概要
- 一般的な機能を有する家計簿 Web アプリを作成する。

## ディレクトリ構成
```
.
├── .devcontainer # DevContainerの設定
├── .gemini # Gemini CLI の設定
├── .vscode # VSCode の設定
├── app # アプリケーションのソースコード
│   ├── backend # バックエンド (Dotnet)。詳細は backend 配下の README 参照
│   └── frontend # フロントエンド (Angular)。詳細は frontend 配下の README 参照
├── docs # ドキュメント
├── .env # ローカル実行用の環境変数
├── compose.devcontainer.yml # 開発用composeファイル
├── compose.local.backend.yml # バックエンドローカル確認用composeファイル
└── compose.local.frontend.yml # フロントエンドローカル確認用composeファイル
```

## 環境構築手順
### 0. 前提
- Docker が起動していること
- VSCode の拡張機能「Dev Containers」が利用できる状態であること

### 1. リポジトリのクローン
- 下記のリポジトリをクローンする。
  - [HouseholdBudget](https://github.com/RyotaMiyatsuka/HouseholdBudget)

### 2. DevContainerの起動
1. VSCode でこのプロジェクトを開き、`ctrl + shift + p` を押下し、`Dev Containers: Open Folder in Container...` を選択してこのフォルダを開く。
2. 開発対象に合わせて下記を選択する。
   1. バックエンドの場合: `household-budget-backend`
   2. フロントエンドの場合: `household-budget-frontend`
