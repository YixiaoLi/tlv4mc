$:.push File.dirname(__FILE__)
require 'rubygems'
require 'set'
require 'pp'

require 'statsgen_util'
require 'json'

def cpu_utilization(tasks, logs)
	statsFile = StatisticsFile.new
	
	max = 0.0
	min = Float.MAX
	
	logs.each do |l|
		t = TraceLog.time(l).to_f
		if t > max then max = t end
		if t < min then min = t end
	end
	
	range = max - min
	
	tasks.each do |task| 
		pre = 0.0
		num = 0.0
		logs.each do|l|
			if /#{task}\.state=(\w+)/ =~ l then
				if $1 == "RUNNING" then 
					pre = TraceLog.time(l).to_f
				else
					if pre > 0.0 then
						num += TraceLog.time(l).to_f - pre 
						pre = 0.0
					end
				end
			end
		end
		dat = (num / range) * 100.0
		
		statsFile.series.add(0, dat, task)
	end
	
	return statsFile
end



generate_statisticsfile do|resource,logs|
	tasks = []
	
	resource['Resources'].each do |name,res|
		if res['Type'] == 'Task' then tasks << name end		
	end
	cpu_utilization(tasks, logs)
	statsFile.print
end
