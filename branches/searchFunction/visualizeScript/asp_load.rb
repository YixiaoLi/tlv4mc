$:.push File.dirname(__FILE__)
require 'rubygems'
require 'set'
require 'pp'

require 'util'
require 'cpu'
require 'json'

visualize_rule do|resource,logs|
  loads = AspCPU.parse(resource,logs)
  puts AspCPU.to_shapes(loads)
end