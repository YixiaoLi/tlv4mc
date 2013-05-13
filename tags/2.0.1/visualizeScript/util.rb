require 'rubygems'
require 'json'

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

  
  def uniq_by(&f)
    ys = [self.first]
    self.tail.each do|x|
      unless f.call(ys[-1],x) then
        ys << x
      end
    end

    ys
end

end

class Array
  def tail
    if self[1..-1] then
      self[1..-1] 
    else
      []
    end
  end

  def break(&f)
    i = self.index(&f)
    [self[0..i-1],self[i+1..-1]]
  end
end

class TraceLog
  def self.time(s)
    if s =~ /^\[(.*?)\]/ then
      $1
    end
  end
end

def visualize_rule(&f)
  raw_res,logs = ARGF.readlines().break{|line|
    line.chop == '---'
  }
  resource = JSON.parse(raw_res.join.downcase)
  f.call(resource,logs)
end
