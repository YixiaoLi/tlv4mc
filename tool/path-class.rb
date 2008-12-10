#! /usr/bin/env ruby
# -*- mode:ruby; coding:utf-8 -*-

ARGF.each do |line|
  path,klass = line.split ':',2
  if klass =~ /class\s+([^ <]+(?:<[^>]+>)?)\s+/ then
    puts %("#{File.dirname(path)}","#{$1.gsub(/\s+/,'').chomp}")
  else
    raise 'must not happen'
  end
end
