class AspCPU
  Load = Struct.new 'Load',:time,:load
  Task = Struct.new 'Task',:time,:name,:state

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

class FmpCPU
  FmpLoad = Struct.new 'FmpLoad',:time,:load
  FmpTask = Struct.new 'FmpTask',:prcID,:state
  StateEvent    = Struct.new 'StateEvent',:time,:name,:state
  MigrateEvent  = Struct.new 'MigrateEvent',:time,:name,:prcID
  
  def self.parse(resource,logs)
    states  = {}

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
        states[name] = FmpTask.new prcID,attr['state'].to_sym
      end 
    }
    
    # initial load
    cpu_count = states.map{|_,x| x.prcID}.max + 1
    loads = Array.new(cpu_count) do|prcID|
      active_tasks = states.select{|_,task|
        task.prcID == prcID and (task.state== :running or task.state== :runnable)
      }
      [FmpLoad.new(TraceLog.time(logs.first),active_tasks.length)]
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
        loads[log.prcID] << FmpLoad.new(log.time,
          state_charts[prev][:mig_in] + loads[log.prcID][-1].load)
        # out
        loads[task.prcID] << FmpLoad.new(log.time,
          state_charts[prev][:mig_out] + loads[task.prcID][-1].load)
      else
        loads[task.prcID] << FmpLoad.new(log.time,
          state_charts[prev][current] + loads[task.prcID][-1].load)
      end

      if current == :mig
        task.prcID = log.prcID
      else
        task.state = log.state
      end
    end
    loads
  end

  def self.print_shapes(loads)
    max_load = loads.flatten.map{|x| x.load }.max
    first = true
  
    puts "["
    loads.each_with_index do|load,i|
      load.zip(load.tail.map{|x| x.time})[0..-2].map {|a,b|
      if first then
        first = false
      else
        puts ","
      end
      puts <<END
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
       puts "]"
     end
  end
end

