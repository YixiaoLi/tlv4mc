$:.push File.dirname(__FILE__) + "/../visualizeScript"
require 'rubygems'
require 'json'
require 'util'  #保存場所："/../visualizeScript"

class Object
	# 真偽値にする
	def to_b
		true & self
	end
end


# 統計情報ファイル内のSetting
class StatisticsFile_Setting
	attr_reader:json
	attr_reader:title
	attr_reader:axisXTitle
	attr_reader:axisYTitle
	attr_reader:defaultType
	attr_reader:majorTickMarkInterval
	attr_reader:minorTickMarkInterval
	attr_reader:majorGridVisible
	attr_reader:minorGridVisible

	def initialize
		@json = {}
	end

	# 内容をJSONで標準出力
	def output
		print JSON.pretty_generate(@json)
	end

	# 以下、インスタンス変数のセッター

	def set_Title(str)
		@title = str.to_s
		@json.store( "Title", @title )
	end

	def set_AxisXTitle(str)
		@axisXTitle = str.to_s
		@json.store( "AxisXTitle", @axisXTitle )
	end

	def set_AxixYTitle(str)
		@axisYTitle = str.to_s
		@json.store( "AxixYTitle", @axisYTitle )
	end

	def set_DefaultType(str)
		@defaultType = str.to_s
		@json.store( "DefaultType", @defaultType )
	end

	def set_MajorTickMarkInterval(num)
		@majorTickMarkInterval = Float(num)
		@json.store( "MajorTickMarkInterval", @majorTickMarkInterval )
	end 

	def set_MinorTickMarkInterval(num)
		@minorTickMarkInterval = Float(num)
		@json.store( "MinorTickMarkInterval", @minorTickMarkInterval )
	end

	def set_MajorGridVisible(bool)
		@majorGridVisible = bool.to_b
		@json.store( "MajorGridVisible", @majorGridVisible )
	end

	def set_MinorGridVisible(bool)
		@minorGridVisible = bool.to_b
		@json.store( "MinorGridVisible", @minorGridVisible )
	end
end


# 統計情報ファイル内のSeries
class StatisticsFile_Series
	attr_reader:json

	def initialize
		@points = []
		@json = { "Points" => @points }
		JSON.pretty_generate(@json)
	end

	# データポイントの追加
	# 引数の名前は、統計情報ファイルのフォーマットと対応します
	def add( xvalue, yvalue, xlabel=nil, ylabel=nil, color=nil )
		point = { "XValue" => Float(xvalue), "YValue" => Float(yvalue) }
		if xlabel != nil then point.store( "XLabel", xlabel.to_s ) end
		if ylabel != nil then point.store( "YLabel", ylabel.to_s ) end
		if color  != nil then point.store( "Color", color.to_s ) end
		@points.push( point )
	end

	# 内容をJSONで標準出力
	def output
		print JSON.pretty_generate(@json)
	end
end



# 統計情報ファイル
# このクラスを構成する要素を設定し、outputメソッドを呼ぶことで
# 統計情報ファイルを標準出力できます。
class StatisticsFile
	attr_accessor:name
	attr_reader:setting
	attr_reader:series

	# name: 統計情報名
	def initialize( name = "__tmp__",\
                      setting = StatisticsFile_Setting.new,\
					  series = StatisticsFile_Series.new)
		@name = name
		@setting = setting
		@series = series
	end

	def set_Setting(setting)
		if setting.is_a?(StatisticsFile_Setting) then 
			@setting = setting
			@json.store( "Setting", @setting.json )
		end
	end

	def set_Series(series)
		if setting.is_a?(StatisticsFile_Series) then 
			@series = series
			@json.store( "Series", @series.json )
		end
	end

	def get_json
		return @json = { @name => {"Setting" => @setting.json, "Series" => @series.json }}
	end

	# 内容をJSONで標準出力
	def output
		print JSON.pretty_generate(self.get_json)
	end
end


# 統計情報の生成を開始するメソッド
# f: 統計情報の生成手順を記述したブロック
def generate_statisticsfile(&f)
  raw_res,logs = ARGF.readlines().break{|line|
    line.chop == '---'
  }
  resource = JSON.parse(raw_res.join)
  f.call(resource,logs)
end