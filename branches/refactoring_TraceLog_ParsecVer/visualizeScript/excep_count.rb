$:.push File.dirname(__FILE__)
require 'rubygems'
require 'pp'

require 'util'

visualize_rule do|resource,logs|
  handlers = resource['resources'].select{|name,attrs|
    attrs['type'] == 'interrupthandler'
  }.map{|x,_|
    x
  }

  handler_logs =logs.map{|name|
    if name =~ /\A\[(.+?)\](#{handlers.join('|')})\.enter/i then
      [$1.to_i]
    else
      []
    end
  }.flatten

  max = handler_logs.size
  sum = 0
  shapes = handler_logs.zip(handler_logs.tail)[0..-2].map {|a,b|
    sum += 1
    <<END
    {
    "From": "#{a}(10)",
    "To": "#{b}(10)",
    "Shape": {
      "Alpha": 100,
      "Area": [
      "0,0",
      "100%,#{sum.to_f / max * 100}%"
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
    "EventName": "EXC_COUNT",
  }
END
    }
    puts "[#{shapes.join(",")}]"

end