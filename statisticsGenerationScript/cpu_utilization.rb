$:.push File.dirname(__FILE__)
require 'rubygems'
require 'set'
require 'pp'

require 'statsgen_util'
require 'json'

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
