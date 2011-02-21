$:.push File.dirname(__FILE__)
require 'rubygems'
require 'set'
require 'pp'

# require 'statsgen_util'
require 'json'


module  Enumerable
  def scanl(init=nil,&f)
    accum = init
    xs = []

    self.map do|x|
      accum = f.call accum,x
      xs.push(accum)
    end

    xs
  end

  def flattenmap(&f)
    self.map(&f).flatten
  end

  
  def uniq_by(&f)
    ys = [self.first]
    self.tail.each do|x|
      unless f.call(ys[-1],x) then
        ys << x
      end
    end

    ys
end

end

class Array
  def tail
    if self[1..-1] then
      self[1..-1] 
    else
      []
    end
  end

  def break(&f)
    i = self.index(&f)
    [self[0..i-1],self[i+1..-1]]
  end
end

class TraceLog
  def self.time(s)
    if s =~ /^\[(.*?)\]/ then
      $1
    end
  end
end

def visualize_rule(&f)
  raw_res,logs = ARGF.readlines().break{|line|
    line.chop == '---'
  }
  resource = JSON.parse(raw_res.join.downcase)
  f.call(resource,logs)
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

class StatsDataPoint
	def initialize(xv=0.0, xl=nil, yv=0.0, yl=nil, c=nil)
		@x_value = xv
		@x_label = xl
		@y_value = yv
		@y_label = yl
		@color   = c
	end

	def print
		printf '{ "XValue":%f, "YValue":%f',@x_value, @y_value
        if @x_label != nil then printf ', "XLabel":"%s"', @x_label end
        if @y_label != nil then printf ', "YLabel":"%s"', @y_label end
        if @color   != nil then printf ', "Color":"%s"', @color   end
        printf '}'
	end
end

class StatsFile
	def initialize(dp=[])
		@points = dp
	end

	def addPoint(dp)
		@points.push(dp)
	end

	def print
		printf '{ "Series":{ "Points": ['
		printf "\n"
		@points.each do|point|
			point.print
			printf ",\n"
		end
		printf '] }}'
	end
end


generate_statisticsfile do|resource,logs|
	statsFile = StatsFile.new
	lange = TraceLog.time(logs[-1]).to_f - TraceLog.time(logs[0]).to_f
	resource['Resources'].each do |name,res|
		if res['Type'] != 'Task' then next end
		pre = 0.0
		num = 0.0
		logs.each do|l|
			if /#{name}\.state=RUNNING/ =~ l then 
				pre = TraceLog.time(l).to_f
		 	elsif /#{name}\.state=/ =~ l then
				if (pre > 0.0) then
					num += TraceLog.time(l).to_f - pre 
					pre = 0.0
				end

			end
		end
		dat = (num / lange) * 100.0
		point = StatsDataPoint.new(0,name,dat,nil,nil)
		statsFile.addPoint(point)
	end
	statsFile.print
end
