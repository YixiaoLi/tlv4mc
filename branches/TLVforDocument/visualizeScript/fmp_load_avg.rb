$LOAD_PATH.push File.dirname(__FILE__)
require 'cpu'
require 'pp'
require 'util'

def get(cpus,t)
  cpus.map{|loads|
    l = loads.find{|load|
      t < load.time.to_i
    }
    l ? l.load : 0
  }.reduce{|x,y| x+y }
end

visualize_rule do|resource,logs|
  PRECISION = 100_000

  cpus = FmpCPU.parse resource,logs

  min = cpus.map{|loads| loads.first.time.to_i}.min
  max = cpus.map{|loads| loads.last.time.to_i}.max

  avgs = []
  prev = nil
  min.step(max,PRECISION) do|i|
    current = get(cpus,i)
    if prev == nil then
      avgs << AspCPU::Load.new(i,current)
    else
      avgs << AspCPU::Load.new(i,(current+prev)/2)
    end
    prev = current
  end

  puts AspCPU.to_shapes(avgs.uniq_by{|x,y|
    x.load == y.load
  })
end
