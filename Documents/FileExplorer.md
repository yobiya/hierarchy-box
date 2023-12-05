# File explorer

## 概要

一般的なファイルエクスプローラーの機能です。

コンテクストメニューを設定ファイルで拡張することができます。

## 設定ファイル

一度File explorerの機能を実行すると、トップ画面の「Open setting file directory」ボタンで開くディレクトリに
設定情報の記述されているFileExplorerContextCommands.jsonファイルが生成されます。

JSONで記述されているComands配列にコンテキストメニューの要素を記述します。

JSONのFile配列に、ファイルのコンテキストメニューを記述し
Directory配列にディレクトリのコンテキストメニューを記述します。

キー|値
---|---
Name|コンテキストメニューの名前
Command|実行するコマンド
WorkingDirectory|コマンドを実行するディレクトリのパス

### 置き換えられる特殊文字列

Directory

文字列名|置き換えらえる要素
---|---
{DIRECTORY_PATH}|コンテキストメニューを開いたディレクトリのパス
