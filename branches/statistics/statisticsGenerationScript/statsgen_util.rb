$:.push File.dirname(__FILE__) + "/../visualizeScript"
require 'rubygems'
require 'json'
require 'util'


# ���v���̐������J�n���郁�\�b�h
# f: ���v���̐����菇���L�q�����u���b�N
def generate_statisticsfile(&f)
  raw_res,logs = ARGF.readlines().break{|line|
    line.chop == '---'
  }
  resource = JSON.parse(raw_res.join)
  f.call(resource,logs)
end