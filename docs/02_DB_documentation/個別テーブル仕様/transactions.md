## テーブル名
- 論理名: `取引`
- 物理名: `transactions`

## カラム
- `id`
  - 主キー
  - 取引ID。
- `user_id`
  - 外部キー(users)
  - 取引を記録したユーザー
- `csv_import_id`
  - 外部キー(csv_imports)
  - csv取り込みした取引と紐づく
- `recurring_transaction_id`
  - 外部キー(recurring_transactions)
  - 定期取引と紐づく
- `amount`
  - 取引額
- `type`
  - 取引種別
- `category`
  - 取引カテゴリ
- `memo`
  - 取引に付属するメモ
- `place`
  - 取引の場所
- `is_deleted`
  - 削除フラグ