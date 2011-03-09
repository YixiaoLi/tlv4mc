$:.push File.dirname(__FILE__)
require 'rubygems'
require 'set'
require 'pp'

require 'statsgen_util'
require 'json'


# aspにおけるタスクのCPU利用率を統計情報ファイルで得るメソッド
# tasks: タスク名の配列
# logs: 標準形式トレースログの配列
def asp_cpu_utilization(tasks, logs)
	statsFile = StatisticsFile.new
	
	max = 0.0         # 最大時刻
	min = Float::MAX  # 最小時刻
	
	logs.each do |l|
		t = TraceLog.time(l).to_f
		if t > max then max = t end
		if t < min then min = t end
	end
	
	range = max - min  # ログの時刻幅
	
	tasks.each do |task| 
		pre = 0.0  # RUNNING開始時刻
		num = 0.0  # あるタスクの総RUNNING時間
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
		dat = (num / range) * 100.0  # 使用率を%で算出
		
		statsFile.series.add(0, dat, task)
	end
	
	return statsFile
end



generate_statisticsfile do|resource,logs|
	tasks = []
	
	resource['Resources'].each do |name,res|
		if res['Type'] == 'Task' then tasks << name end		
	end
	statsFile = asp_cpu_utilization(tasks, logs)
	statsFile.output
end
