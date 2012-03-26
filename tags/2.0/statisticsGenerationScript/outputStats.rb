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
	"Title":"Ruby�o��puts�ׂ�����",
	"AxisXTitle":"xTitle",
	"AxisYTitle":"yTilte",
	"SeriesTitle":"testSeries",
	"DefaultType":"Bar",
	},
	"Series":{
	"Label":"�Œ�f�[�^",
	"Points":[
	{"XLabel":"Task1", "YValue":"20"},
	{"XLabel":"Task2", "YValue":"30"}
	]
	}
END
end