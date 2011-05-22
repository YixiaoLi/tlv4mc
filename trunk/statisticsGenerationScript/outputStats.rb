$:.push File.dirname(__FILE__)
require 'rubygems'
require 'set'
require 'pp'

require 'statsgen_util'
require 'json'

generate_statisticsfile do|resource,logs|
	puts <<END
	{
	"Setting":{
	"Title":"Ruby出力putsべた書き",
	"AxisXTitle":"xTitle",
	"AxisYTitle":"yTilte",
	"SeriesTitle":"testSeries",
	"DefaultType":"Bar",
	},
	"Series":{
	"Label":"固定データ",
	"Points":[
	{"XLabel":"Task1", "YValue":"20"},
	{"XLabel":"Task2", "YValue":"30"}
	]
	}
END
end