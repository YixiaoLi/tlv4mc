$LOAD_PATH.push File.dirname(__FILE__)
require 'cpu'
require 'pp'
require 'util'

def get(loads,t)
  loads.find{|load|
    t < load.time.to_i
  }
end

visualize_rule do|resource,logs|
  PRECISION = 100_000

  loads = AspCPU.parse resource,logs
  min = loads.first.time.to_i
  max = loads.last.time.to_i

  avgs = []
  prev = nil
  min.step(max,PRECISION) do|i|
    current = get(loads,i).load
    if prev == nil then
      avgs << AspCPU::Load.new(i,current)
    else
      avgs << AspCPU::Load.new(i,(current+prev).to_f/2)
    end
    prev = current
  end

  puts AspCPU.to_shapes(avgs.uniq_by{|x,y|
    x.load == y.load
  })
end

