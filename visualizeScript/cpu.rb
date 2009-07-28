class AspCPU
  Load = Struct.new 'CpuLoad',:time,:load
  Task    = Struct.new 'Task',:time,:name,:state

  def self.parse(resource,logs)
    states = {}
    resource['resources'].each{|name,value|
      if value['type'].downcase == 'task' and
        states[name] = value['attributes']['state']
      end 
    }
    
    task_logs = logs.flattenmap do|line|
      if line =~ /^\[(.*?)\]([^.]+)\.(\w+)=(\w+)/ then
        time  = $1
        name  = $2.downcase
        event = $3.downcase
        state = $4.downcase
        if resource['resources'][name]['type'] == 'task' and event == 'state' then
          Task.new time,name,state
        else
          []
        end
      else
        []
      end
    end
    
    loads = [Load.new(TraceLog.time(logs.first),
                      states.select{|_,state| state== 'running' or state=='runnable'}.length)]
    
    task_logs.each do|log|
      load = loads[-1].load
      current = log.state
      prev    = states[log.name]
      case current
      when 'dormant','waiting','suspend'
        case prev
        when 'running','runnable'
          loads << Load.new(log.time,load - 1)
        when 'dormant','waiting','suspend'
          # ignore
        end
      when 'running','runnable'
        case prev
        when 'dormant','waiting','suspend'
          loads << Load.new(log.time,load + 1)
        when 'running','runnable'
          # ignore
        end
      end
      
      states[log.name] = log.state
    end
    loads
  end

    def self.to_shapes(loads)
      max_load = loads.map{|x| x.load }.max

      shapes = loads.zip(loads.tail.map{|x| x.time})[0..-2].map {|a,b|
    <<END
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
    "EventName": "CPU_LOAD",
  }
END
    }
    "[#{shapes.join(",")}]"
  end
end