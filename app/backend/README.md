# バックエンドプロジェクト

## ディレクトリ構成
```

```

## マイグレーション手順
```sh
cd /workspace/app/backend/src/Infrastructure/
dotnet ef migrations add {マイグレーション名} --startup-project ../Presentation
dotnet ef database update --startup-project ../Presentation
```

## モックサーバーの起動
```sh
prism mock /workspace/docs/01_API_documentation/backend.yml --port 4010
```
