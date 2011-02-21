$:.push File.dirname(__FILE__) + "/../visualizeScript"
require 'rubygems'
require 'json'
require 'util'


# 統計情報の生成を開始するメソッド
# f: 統計情報の生成手順を記述したブロック
def generate_statisticsfile(&f)
  raw_res,logs = ARGF.readlines().break{|line|
    line.chop == '---'
  }
  resource = JSON.parse(raw_res.join)
  f.call(resource,logs)
end