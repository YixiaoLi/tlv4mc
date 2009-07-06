require 'set'
require 'pp'
require 'rubygems'
require 'json'
require 'enumerator'
# ------------------------------------------------------------
# util function
# ------------------------------------------------------------
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
end

class Array
  def tail
    self[1..-1]
  end

  def break(&f)
    i = self.index(&f)
    [self[0..i-1],self[i+1..-1]]
  end
end

def time(s)
  if s =~ /^\[(.*?)\]/ then
    $1
  end
end


# ------------------------------------------------------------
# parse
# ------------------------------------------------------------
raw_res,logs = ARGF.readlines().break{|line|
  line.chop == '---'
}

resource = JSON.parse(raw_res.join.downcase)


# ------------------------------------------------------------
# data type
# ------------------------------------------------------------
CpuLoad = Struct.new 'CpuLoad',:time,:load
Task = Struct.new 'Task',:prcID,:state
StateEvent    = Struct.new 'StateEvent',:time,:name,:state
MigrateEvent  = Struct.new 'MigrateEvent',:time,:name,:prcID
states  = {}

# ------------------------------------------------------------
# calculate
# ------------------------------------------------------------
task_logs = logs.flattenmap do|line|
  if line =~ /^\[(.*?)\]([^.]+)\.(\w+)=(\w+)/ then
    time  = $1
    name  = $2.downcase
    event = $3.downcase
    val = $4.downcase


    if resource['resources'][name]['type'] != 'task'
      []
    else
      case event
      when 'state'
        StateEvent.new time,name,val.to_sym
      when 'prcid'
        MigrateEvent.new time,name,val.to_i-1
      else
        []
      end
    end
  else
    []
  end
end

# taskó‘Ô‚Ì‰Šú‰»
resource['resources'].each{|name,value|
  if value['type'].downcase == 'task' and
    attr = value['attributes']
    prcID = attr['prcid'].to_i-1
    states[name] = Task.new prcID,attr['state'].to_sym
  end 
}

# initial load
cpu_count = states.map{|_,x| x.prcID}.max + 1
loads = Array.new(cpu_count) do|prcID|
  active_tasks = states.select{|_,task|
    task.prcID == prcID and (task.state== :running or task.state== :runnable)
  }
  [CpuLoad.new(time(logs.first),active_tasks.length)]
end

state_charts = {
  :run => {
    :run     =>  0,
    :wait    => -1,
    :mig_in  =>  1,
    :mig_out => -1
  },
  :wait => {
    :run     =>  1,
    :wait    =>  0,
    :mig_in  =>  0,
    :mig_out =>  0
  }
}

task_logs.map do|log|
  task = states[log.name]
  prev = case task.state
         when :running,:runnable
           :run
         when :dormant,:waiting,:suspend
           :wait
         end
   current = case log
             when StateEvent
               case log.state
               when :running,:runnable
                 :run
               when :dormant,:waiting,:suspend
                 :wait
               end
             when MigrateEvent
               :mig
             end

  if current == :mig
    # in
    loads[log.prcID] << CpuLoad.new(log.time,
      state_charts[prev][:mig_in] + loads[log.prcID][-1].load)
      
    # out
    loads[task.prcID] << CpuLoad.new(log.time,
      state_charts[prev][:mig_out] + loads[task.prcID][-1].load)
  else
    loads[task.prcID] << CpuLoad.new(log.time,
      state_charts[prev][current] + loads[task.prcID][-1].load)
  end

  if current == :mig
    task.prcID = log.prcID
  else
    task.state = log.state
  end
end

# ------------------------------------------------------------
# print
# ------------------------------------------------------------
max_load = loads.flatten.map{|x| x.load }.max

shapes = []
loads.each_with_index do|load,i|
  load.zip(load.tail.map{|x| x.time})[0..-2].map {|a,b|
    shapes << <<END
    {
      "From": "#{a.time}(10)",
      "To": "#{b}(10)",
      "Shape": {
        "Alpha": 100,
        "Area": [
          "0,0",
          "100%,#{a.load.to_f / max_load * 100}%"
        ],
        "Fill": "fffcbc0c",
        "Location": "0,0",
        "Offset": "0,0",
        "Pen": {
          "Color": "fffcbc0c",
          "Alpha": 255,
          "Width": 1
        },
        "Points": [
          "0,0",
          "100%,0",
          "100%,100%",
          "0,100%"
        ],
        "Size": "100%,100%",
        "Type": "Rectangle"
      },
      "EventName": "CPU#{i+1}",
    }
END
}
end

puts "[#{shapes.flatten.join(",")}]"
