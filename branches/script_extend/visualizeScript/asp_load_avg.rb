require File.dirname(__FILE__)+'/asp'

def get(loads,t)
  loads.find{|load|
    t < load.time.to_i
  }
end

def pack(xs)
  from = xs.first.time
  load = xs.first.load

  ys = []
  xs.each do|x|
    if x.load != load then
      ys << CpuLoad.new(from,load)
      
      from = x.time
      load = x.load
    end
  end
  ys << CpuLoad.new(from,load)
  ys
end

PRECISION = 100_000

min = @loads.first.time.to_i
max = @loads.last.time.to_i

avgs = []
prev = nil
min.step(max,PRECISION){|i|
  current = get(@loads,i).load
  if prev == nil then
    avgs << CpuLoad.new(i,current)
  else
    avgs << CpuLoad.new(i,(current+prev)/2)
  end
  prev = current
}

print_shapes pack(avgs)
#print_shapes load_avgs.map{|x| pack(x) }
