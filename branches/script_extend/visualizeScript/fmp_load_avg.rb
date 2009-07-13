require File.dirname(__FILE__)+'/fmp'

def get(loads,t)
  loads.find{|load|
    t < load.time.to_i
  }
end

def average(xs)
  xs.reduce{|x,y|
    x+y
  }.to_f / xs.length
end

def pack(xs)
  if xs.size <= 1 then
    xs
  else
    ys = pack xs[1..-1]
    if ys.first.load == xs.first.load then
      ys.first.time = xs.first.time
      ys
    else
      [xs.first]+ys
    end

  end
end

PRECISION = 10_000
N = 3

load_avgs = @cpus.map{|loads|
  min = loads.first.time.to_i
  max = loads.last.time.to_i

  avgs = []
  min.step(max,PRECISION){|i|
    xs = (0..N).map{|x|
      l = get(loads,i+x*PRECISION)
      if l then l.load else 0 end
    }
    avgs << CpuLoad.new(i,average(xs))
  }
  avgs
}
#load_avgs.map{|x| pp pack(x) }
print_shapes load_avgs.map{|x| pack(x) }

