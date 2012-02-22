$:.push File.dirname(__FILE__) + "/../visualizeScript"
require 'rubygems'
require 'json'
require 'util'  #�ۑ��ꏊ�F"/../visualizeScript"

class Object
	# �^�U�l�ɂ���
	def to_b
		true & self
	end
end


# ���v���t�@�C������Setting
class StatisticsFile_Setting
	attr_reader :json
	attr_reader :title
	attr_reader :axisXTitle
	attr_reader :axisYTitle
	attr_reader :defaultType
	attr_reader :majorTickMarkInterval
	attr_reader :minorTickMarkInterval
	attr_reader :majorGridVisible
	attr_reader :minorGridVisible

	def initialize
		@json = {}
	end

	# ���e��JSON�ŕW���o��
	def output
		print JSON.pretty_generate(@json)
	end

	# �ȉ��A�C���X�^���X�ϐ��̃Z�b�^�[

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


# ���v���t�@�C������Series
class StatisticsFile_Series
	attr_reader :json

	def initialize
		@points = []
		@json = { "Points" => @points }
		JSON.pretty_generate(@json)
	end

	# �f�[�^�|�C���g�̒ǉ�
	# �����̖��O�́A���v���t�@�C���̃t�H�[�}�b�g�ƑΉ����܂�
	def add( xvalue, yvalue, xlabel=nil, ylabel=nil, color=nil )
		point = { "XValue" => Float(xvalue), "YValue" => Float(yvalue) }
		if xlabel != nil then point.store( "XLabel", xlabel.to_s ) end
		if ylabel != nil then point.store( "YLabel", ylabel.to_s ) end
		if color  != nil then point.store( "Color", color.to_s ) end
		@points.push( point )
	end

	# ���e��JSON�ŕW���o��
	def output
		print JSON.pretty_generate(@json)
	end
end



# ���v���t�@�C��
# ���̃N���X���\������v�f��ݒ肵�Aoutput���\�b�h���ĂԂ��Ƃ�
# ���v���t�@�C����W���o�͂ł��܂��B
class StatisticsFile
	attr_accessor :name
	attr_reader :setting  # StatisticsFile_Setting�̃I�u�W�F�N�g
	attr_reader :series   # StatisticsFile_Series�̃I�u�W�F�N�g
	attr_reader :json

	# name: ���v���
	def initialize( name = "__tmp__",\
                      setting = StatisticsFile_Setting.new,\
					  series = StatisticsFile_Series.new)
		@name = name
		@json = { @name => {}}
		self.set_Setting( setting )
		self.set_Series( series )
	end

	# StatisticsFile_Setting�I�u�W�F�N�g�p�Z�b�^�[
	def set_Setting(setting)
		if setting.is_a?(StatisticsFile_Setting) then 
			@setting = setting
			@json[@name].store( "Setting", @setting.json )
		end
	end

	# StatisticsFile_Series�I�u�W�F�N�g�p�Z�b�^�[
	def set_Series(series)
		if series.is_a?(StatisticsFile_Series) then 
			@series = series
			@json[@name].store( "Series", @series.json )
		end
	end

	# ���e��JSON�ŕW���o��
	def output
		print JSON.pretty_generate(@json)
	end
end



# ���v���̐������J�n���郁�\�b�h
# f: ���v���̐����菇���L�q�����u���b�N
def generate_statisticsfile(&f)
  raw_res,logs = ARGF.readlines().break{|line|
    line.chop == '---'
  }
  resource = JSON.parse(raw_res.join)
  f.call(resource,logs)
end