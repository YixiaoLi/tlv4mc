$:.push File.dirname(__FILE__)
require 'set'
require 'pp'
require 'rubygems'
require 'json'
require 'enumerator'
require 'util'
require 'cpu'


visualize_rule do|resource,logs|
  loads = FmpCPU.parse(resource,logs)
  FmpCPU.print_shapes(loads)
end