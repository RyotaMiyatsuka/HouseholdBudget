## テーブル名
- 論理名: `定期取引`
- 物理名: `recurring_transactions`

## カラム
- `id`
  - 主キー
  - 定期取引ID
- `user_id`
  - 外部キー(users)
  - 登録したユーザー
- `interval_unit`
  - 定期取引の間隔
- `price`
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
