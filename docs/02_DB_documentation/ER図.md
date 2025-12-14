# ER図

```mermaid
erDiagram
    users ||--o{ sessions : "has"
    users ||--o{ categories : "has"
    users ||--o{ transactions : "has"
    categories ||--o{ transactions : "belongs_to"

    users {
        uuid id PK "ユーザーID"
        varchar(256) email UK "メールアドレス"
        varchar(100) user_name "ユーザー名"
        datetime created_at "作成日時"
        datetime updated_at "更新日時"
    }

    sessions {
        varchar(64) id PK "セッションID"
        uuid user_id FK "ユーザーID"
        datetime expires_at "有効期限"
        boolean is_active "有効フラグ"
        datetime created_at "作成日時"
        datetime updated_at "更新日時"
    }

    categories {
        uuid id PK "カテゴリID"
        varchar(50) name "カテゴリ名"
        uuid user_id FK "ユーザーID"
        datetime created_at "作成日時"
        datetime updated_at "更新日時"
    }

    transactions {
        uuid id PK "取引ID"
        decimal(18_2) amount "金額"
        varchar(3) currency "通貨コード"
        date date "取引日"
        int transaction_type "取引種別(0:Income/1:Expense)"
        uuid category_id FK "カテゴリID"
        varchar(500) memo "メモ(NULL許容)"
        varchar(100) place "場所(NULL許容)"
        uuid user_id FK "ユーザーID"
        datetime created_at "作成日時"
        datetime updated_at "更新日時"
    }
```

## テーブル一覧

| テーブル名 | 説明 |
|-----------|------|
| users | ユーザー情報 |
| sessions | 認証セッション情報 |
| categories | カテゴリ情報 |
| transactions | 取引情報 |

## リレーション

| 親テーブル | 子テーブル | 関係 |
|-----------|-----------|------|
| users | sessions | 1:N |
| users | categories | 1:N |
| users | transactions | 1:N |
| categories | transactions | 1:N |

## インデックス

| テーブル | インデックス | カラム |
|---------|-------------|--------|
| users | ix_users_email | email |
| sessions | ix_sessions_user_id | user_id |
| sessions | ix_sessions_user_id_is_active | user_id, is_active |
| categories | ix_categories_user_id | user_id |
| categories | ix_categories_user_id_name | user_id, name |
| transactions | ix_transactions_user_id | user_id |
| transactions | ix_transactions_user_id_date | user_id, date |
