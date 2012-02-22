#!/usr/bin/ruby -Ks
# -*- coding: japanese-cp932 -*-

#���́F�W�����́i���\�[�X�t�@�C���CASP�̃��O�j
#�o�́F�W���o�́i�W���`���̃��O�j
# $Id: converter.rb miwa $

# $KCODE = "UTF-8"
require 'strscan'
require "optparse"
require 'rubygems'
require 'json'
#require 'JsonParser'


#�W�����͂���̓ǂݍ���
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

#�W�����͂��烊�\�[�X�t�@�C���ƃ��O��ǂݍ��݁C���ꂼ��raw_res�Clogs�Ɋi�[
raw_res,logs = ARGF.readlines().break{|line|
  line.chop == '---'
}

##�ϊ��Əo��


#JSON�`���̃��\�[�X�t�@�C�����n�b�V���Ɋi�[
$ruby_obj = JSON.parse(raw_res.join)
# $Context = Hash::new{|i| Array.new}  #���݂̃R���e�L�X�g�̖��O���i�[����ϐ�
# $Context = Hash.new([])
# $Context = Hash.new([])
# @TaskAtr = Hash::new{|hash, key| hash[key] = {} }
# array = Array.new(5){ |i| Array.new(5,0) }
$PrcArray = Array.new #�v���Z�b�T�Ǘ��p�̔z��
$PrcNum = 0 #�v���Z�b�T�����Ǘ�����ϐ�

#���\�[�X�̏����Ǘ�����X�[�p�[�N���X#
class ResourceClass 
  #���\�[�X���Ǘ�����N���X�̏������i���\�[�X�N���X�ɕύX�H�j
  def initialize
  end
#���\�[�X���Ǘ����邽�߂̃n�b�V�����쐬
  def CreHash
  end
  #�^�X�Nid����^�X�N�����擾���郁�\�b�h
  def ResName
  end
  #�^�X�N�̏�Ԃ�ύX���郁�\�b�h
  #�^�X�Nid�ł͂Ȃ��C�^�X�N��������悤�ɕύX
  def ChangeAtr
  end
  #�^�X�N�̏�Ԃ��Q�Ƃ��郁�\�b�h
  def ResAtr
  end
  #�^�X�N��Ԃ̈ꗗ��\�����郁�\�b�h�i�f�o�b�O�p�j
  def PrintAtr()
  end
  #���s���̃^�X�N�����݂��邩�֐��@Resrunning�Ɠ��l�̏����ōs���邩�H
  def ExistRunning?()
  end
  #���s���̃^�X�N����Ԃ��֐�
  def ResRunning()
  end  
end

#�^�X�N��Ԃ̈ꗗ��\�����郁�\�b�h�i�f�o�b�O�p�j
class  FmpResourceClass < ResourceClass
  #���\�[�X���Ǘ�����N���X�̏������i���\�[�X�N���X�ɕύX�H�j
  def initialize
    pnum = #�v���Z�b�T���𐔂���
      # @TaskAtr = Hash::new #�^�X�N�̑�����ێ�����n�b�V��
      @TaskAtr = Hash::new{|hash, key| hash[key] = {} }
      @CyclicHandlerAtr = Hash::new{|hash, key| hash[key] = {} }
      @AlarmHandlerAtr = Hash::new{|hash, key| hash[key] = {} }
    self.CreHash #���ꂼ��̃^�X�N�̏�Ԃ�ێ�����n�b�V���̐���
  end
  def CreHash
    #���ꂼ��̃^�X�N�̏�Ԃ�ێ�����n�b�V�����v���Z�b�T���ɐ���
    #�v���Z�b�T���𐔂���
    $ruby_obj["Resources"].each{|key,value|#���ׂẴ��\�[�X�ɂ����āC
      if @TaskAtr.key?(value["Attributes"]["prcId"]) #�n�b�V�����Ƀv���Z�b�TID�̃n�b�V�������݂��邩�m�F���A
      else #���݂��Ȃ��ꍇ��
        $PrcArray.push value["Attributes"]["prcId"] #�v���Z�b�T�Ǘ��p�̔z��Ƀv���Z�b�TID�̂��̂�ǉ�
        $PrcNum = $PrcNum + 1 #�v���Z�b�T����ύX
      end
      case value["Type"]
      when "Task"
        @TaskAtr[value["Attributes"]["prcId"]][key] = value["Attributes"]["state"] #TaskAtr�Ɍ��݂̏�Ԃ�l�Ƃ��ăn�b�V����ǉ�
      when  "CyclicHandler"
        @CyclicHandlerAtr[value["Attributes"]["prcIdC"]][key] = value["Attributes"]["state"] #TaskAtr�Ɍ��݂̏�Ԃ�l�Ƃ��ăn�b�V����ǉ�
      when "AlarmHandler"
        @AlarmHandlerAtr[value["Attributes"]["prcIdA"]][key] = value["Attributes"]["state"] #TaskAtr�Ɍ��݂̏�Ԃ�l�Ƃ��ăn�b�V����ǉ�
        #"prcIdA"
      else
      end
    }
    $Context = Array.new($PrcNum+1){Array.new}
  end
  #�^�X�Nid���烊�\�[�X�����擾���郁�\�b�h
  def ResName(prcid,tskid,type)
    prcid_type = case type
                 when "Task"
                   "prcId"
                 when  "CyclicHandler"
                   "prcIdC"
                 when "AlarmHandler"
                   "prcIdA"
                 when "InterruptHandler"
                   "prcIdI"
                 when "InterruptServiceRoutine"
                   "prcIdR"
                 when "CPUExceptionHandler"
                   "prcIdE"
                 when "TaskExceptionRoutine"
                   "prcIdX"
                 when "ApplogString"
                   "id"
                 when "ApplogState"
                   "id"
                 else 
                   p "okasii"
                   exit()
                 end
    $ruby_obj["Resources"].each{|key,value|
      #Attribute��ID�Ŕ��f
      #    TaskExceptionRoutin�̏ꍇ(�^�X�NID�������Ȃ�)
      if nil == tskid && value["Type"] == type && prcid == value["Attributes"][prcid_type]
        return key
      end
      #    TaskExceptionRoutin�ȊO
      #�}�C�O���[�V�������N�������(ID�͌ŗL)
      if type == "Task" || type == "CyclicHandler" || type == "AlarmHandler" || type == "ApplogString" || type == "ApplogState"
        if value["Attributes"]["id"].to_i == tskid.to_i && value["Type"] == type
          return key
        else
        end
        #�}�C�O���[�V�������N����Ȃ�����
      else
        if value["Attributes"]["id"].to_i == tskid.to_i && value["Type"] == type && prcid == value["Attributes"][prcid_type]
          return key
        else
        end
      end
    }
    return nil #��O������������ƍs��
  end
  #�^�X�N�̏�Ԃ�ύX���郁�\�b�h
  #�^�X�Nid�ł͂Ȃ��C�^�X�N��������悤�ɕύX
  def ChangeAtr(prcid,name,type,toatr)
    case type
    when "Task"
      @TaskAtr[prcid][name] = toatr
    when  "CyclicHandler"
      @CyclicHandlerAtr[prcid][name] = toatr
    when "AlarmHandler"
      @AlarmHandlerAtr[prcid][name] = toatr
    else
    end
  end
  
  #���\�[�X�̏�Ԃ��Q�Ƃ��郁�\�b�h ����(�v���Z�b�TID�C���\�[�XID�C���\�[�X����)
  def ResAtr(prcid,id,type)
    case type
    when "Task"
      return @TaskAtr[prcid][ResName(prcid,id,"Task")]
    when "CyclicHandler"
      return @CyclicHandlerAtr[prcid][ResName(prcid,id,"CyclicHandler")]
    when "AlarmHandler"
      return @AlarmHandlerAtr[prcid][ResName(prcid,id,"AlarmHandler")]
    else
    end
  end
  
  #�^�X�N��Ԃ̈ꗗ��\�����郁�\�b�h�i�f�o�b�O�p�j
  def PrintAtr()
    p "ptintAtr"
    $PrcArray.each{|key1|
      p "prcID = ",key1
      @TaskAtr[key1].each{|key2,value|
        print key2 ," = ",value ,"\n"
      }   
    }
  end

  #�^�X�N��Ԃ̈ꗗ��\�����郁�\�b�h�i�f�o�b�O�p�j
  def PrintAtr2()
    p @TaskAtr
    p @CyclicHandlerAtr
    p @AlarmHandlerAtr
  end
  
  #���s���̃^�X�N�����݂��邩�֐��@Resrunning�Ɠ��l�̏����ōs���邩�H
  def ExistRunning?(prcid,type)
    case type
    when "Task"
      @TaskAtr[prcid].each{|key,value|
        if "RUNNING"==value
          return 1
        end
      }
    when  "CyclicHandler"
      @CyclicHandlerAtr[prcid].each{|key,value|
        if "RUNNING"==value
          return 1
        end
      }
    when "AlarmHandler"
      @AlarmHandlerAtr[prcid].each{|key,value|
        if "RUNNING"==value
          return 1
        end
      }
    else
    end
    return nil
  end
  
  #���s���̃^�X�N����Ԃ��֐�
  def ResRunning(prcid)
    @TaskAtr[prcid].each{|key,value|
      if "RUNNING"==value
        return key
      end
    }
    return nil
  end  
  def Migration(fromprcid,toprcid,id,type)
    #���\�[�Xid�ł����̂�?�܂��ȒP�ɖ��O�Q�b�g�ł��邵�ЂƂ܂��������ŁD
    #���̃v���Z�b�T�ɂ��郊�\�[�X�̏�����菜��
    case type
    when "Task"
      @TaskAtr[fromprcid].delete(FmpResource.ResName(fromprcid,id,"Task"))
      @TaskAtr[toprcid][FmpResource.ResName(fromprcid,id,"Task")] = "RUNNABLE" 
    when  "CyclicHandler"
      @CyclicHandlerAtr[fromprcid].delete(FmpResource.ResName(fromprcid,id,"CyclicHandler"))
      @CyclicHandlerAtr[toprcid][FmpResource.ResName(fromprcid,id,"CyclicHandler")] = "RUNNABLE" 
    when "AlarmHandler"
      @AlarmHandlerAtr[fromprcid].delete(FmpResource.ResName(fromprcid,id,"AlarmHandler"))
      @AlarmHandlerAtr[toprcid][FmpResource.ResName(fromprcid,id,"AlarmHandler")] = "RUNNABLE" 
    else
    end
  end
end

=begin #FMP�X�N���v�g�m�F�p�֐�

FmpResource = FmpResourceClass.new
FmpResource.PrintAtr
p FmpResource.ResAtr(1,6)
p FmpResource.ResRunning(1)
p FmpResource.ExistRunning?(1)
FmpResource.ChangeAtr(1,"MAIN_TASK1","RUNNING")
p FmpResource.ResAtr(1,6)
p FmpResource.ResRunning(1)
p FmpResource.ExistRunning?(1)
p FmpResource.ResName(1,6,"Task")
=end

FmpResource = FmpResourceClass.new

#Debug
#FmpResource.PrintAtr2
#p FmpResource.Migration(1,2,1,"Task")
#p FmpResource.Migration(1,2,1,"CyclicHandler")
#p FmpResource.Migration(1,2,1,"AlarmHandler")
#FmpResource.PrintAtr2
#exit(0)
#p FmpResource.ResName(1,1,"AlarmHandler")
#FmpResource.ChangeAtr(1,FmpResource.ResName(1,1,"AlarmHandler"),"AlarmHandler","RUNNING")
#exit(0)

#1�s�����O��ǂݎ��
logs.each do|line|
  line.chomp!
  # �eOS���ʕ����̎擾
  if /\[(\d+)\]:\[(\d+)\]: (.*)/ =~ line
    time = $1
    prcid = ($2).to_i
    pattern = $3
  end
  #�e�p�^�[�����̏����̋L�q
  if /dispatch to task (\d+)\./ =~ pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      # FmpResource.PrintAtr
      print "[",time,"]",$Context[prcid][-1],".preempt()\r\n"
      print "[",time,"]",$Context[prcid][-1],".state=RUNNABLE\r\n"
      # $Context[prcid].each{|key| puts "key = ",key} 
      # puts "$Context[1] = "
      # $Context[1].each{|key| puts "key = ",key} 
      # puts "$Context[1] = "
      # $Context[2].each{|key| puts "key = ",key} 
      FmpResource.ChangeAtr(prcid,$Context[prcid][-1],"Task","RUNNABLE")
    end
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".dispatch()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"Task"),"\r\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"Task")

    FmpResource.ChangeAtr(prcid,FmpResource.ResName(prcid,$1,"Task"),"Task","RUNNING")
  elsif /task (\d+) becomes ([^\.]+)\./ =~pattern
    if FmpResource.ResAtr(prcid,$1,"Task") == "DORMANT" && $2 == "RUNNABLE"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".activate()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNING" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".exit()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNING" && $2 == "WAITING"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".wait()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNABLE" && $2 == "SUSPENDED"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".suspended()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING" && $2 == "WAITING-SUSPENDED"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".suspended()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "SUSPENDED" && $2 == "RUNNABLE"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".resume()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING-SUSPENDED" && $2 == "WAITING"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".resume()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING" && $2 == "RUNNABLE"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".releaseFromWaiting()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING-SUSPENDED" && $2 == "SUSPENDED"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".releaseFromWaiting()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "SUSPENDED" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING-SUSPENDED" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\r\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNABLE" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\r\n"
    end
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".state=",$2,"\r\n"
    FmpResource.ChangeAtr(prcid,FmpResource.ResName(prcid,$1,"Task"),"Task",$2)
  elsif /enter to ((?!sns)(?!get_utm)(?!ext_ker)[^ix]\w+[_]\w+)( (.+))?\.?/ =~pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      if nil == $2
        print "[",time,"]",FmpResource.ResRunning(prcid),".enterSVC(",$1,")\r\n"
      else
        print "[",time,"]",FmpResource.ResRunning(prcid),".enterSVC(",$1,",",$3.delete(" "),")\r\n"
      end
    end
  elsif /leave from ((?!sns)(?!get_utm)(?!ext_ker)[^ix]\w+[_]\w+)( (.+))?\.?/ =~pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      if nil == $2
        print "[",time,"]",FmpResource.ResRunning(prcid),".leaveSVC(",$1,")\r\n"
      else
        print "[",time,"]",FmpResource.ResRunning(prcid),".leaveSVC(",$1,",",$3.delete(" "),")\r\n"
      end
    end
  elsif /task (\d+) migrates from processor (\d+) to processor (\d+)\./ =~pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      if FmpResource.ResAtr($2.to_i,$1,"Task") == "RUNNING"
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"Task"),".state=RUNNABLE\r\n"
      end
    end
    if nil != FmpResource.ResName($2.to_i,$1,"Task")
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"Task"),".prcId=",$3,"\r\n"      
      FmpResource.Migration($2.to_i,$3.to_i,$1,"Task")
    end
  elsif /cyclic handler (\d+) migrates from processor (\d+) to processor (\d+)\./ =~pattern
    if FmpResource.ExistRunning?(prcid,"CyclicHandler")
      if FmpResource.ResAtr($2.to_i,FmpResource.ResName(prcid,$1,"CyclicHandler"),"CyclicHandler") == "RUNNING"
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"CyclicHandler"),".state=RUNNABLE\r\n"
      end
    end
    if nil != FmpResource.ResName($2.to_i,$1,"CyclicHandler")
      print "[",time,"]",FmpResource.ResName($2.to_i,$1,"CyclicHandler"),".prcIdC=",$3,"\r\n"      
      FmpResource.Migration($2.to_i,$3.to_i,$1,"CyclicHandler")
    end
  elsif /alarm handler (\d+) migrates from processor (\d+) to processor (\d+)\./ =~pattern
    if FmpResource.ExistRunning?(prcid,"AlarmHandler")
      if FmpResource.ResAtr($2.to_i,FmpResource.ResName(prcid,$1,"AlarmHandler"),"AlarmHandler") == "RUNNING"
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"AlarmHandler"),".state=RUNNABLE\r\n"
      end
    end
    if nil != FmpResource.ResName($2.to_i,$1,"AlarmHandler")
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"AlarmHandler"),".prcIdA=",$3,"\r\n"      
      FmpResource.Migration($2.to_i,$3.to_i,$1,"AlarmHandler")
    end
  elsif /enter to ((i\w+[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,")\r\n"
    else
        print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,",",$4.delete(" "),")\r\n"  
    end
  elsif /leave from ((i\w+[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,")\r\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,",",$4.delete(" "),")\r\n"
    end
    #�ȉ��Q�e�X�g�����{ $4�̕��������H
  elsif /enter to ((x?sns[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,")\r\n"      
    else
      print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,",",$4.delete(" "),")\r\n"      
    end
  elsif /leave from ((x?sns[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,")\r\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,",",$4.delete(" "),")\r\n"
    end
    #�ȉ��S�e�X�g������
  elsif /enter to get_utm( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".enterSVC(get_utm)\r\n"
    else
      print "[",time,"]",$Context[prcid][-1],".enterSVC(get_utm,",$2.delete(" "),")\r\n"
    end
  elsif /leave from get_utm( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(get_utm)\r\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(get_utm,",$2.delete(" "),")\r\n"
    end
  elsif	/enter to ext_ker( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".enterSVC(ext_ker)\r\n"
    else
      print "[",time,"]",$Context[prcid][-1],".enterSVC(ext_ker,",$2.delete(" "),")\r\n"
    end
  elsif /leave from ext_ker( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(ext_ker)\r\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(ext_ker,",$2.delete(" "),")\r\n"
    end
  elsif /enter to int handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".enter()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"InterruptHandler"),"\r\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"InterruptHandler")
  elsif	/leave from int handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".leave()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".state=DORMANT\r\n"
    $Context[prcid].pop
  elsif /enter to isr ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".enter()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),"\r\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"InterruptServiceRoutine")
  elsif	/leave from isr ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".leave()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".state=DORMANT\r\n"
    $Context[prcid].pop
  elsif /enter to cyclic handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".enter()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"CyclicHandler"),"\r\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"CyclicHandler")
  elsif	/leave from cyclic handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".leave()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".state=DORMANT\r\n"
    $Context[prcid].pop
  elsif /enter to alarm handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".enter()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"AlarmHandler"),"\r\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"AlarmHandler")
  elsif	/leave from alarm handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".leave()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".state=DORMANT\r\n"
    $Context[prcid].pop
  elsif /enter to exc handler ([^\.]+)\.?/ =~pattern 
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".enter()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),"\r\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"CPUExceptionHandler")
  elsif /leave from exc handler ([^\.]+)\.?/ =~pattern 
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".leave()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".state=DORMANT\r\n"
    $Context[prcid].pop
  elsif /enter to tex ([^\.]+)\.?/ =~ pattern
    if FmpResource.ResName(prcid,$1,"TaskExceptionRoutine") != nil
    print "[",time,"]",FmpResource.ResName(prcid,$1,"TaskExceptionRoutine"),".enter()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"TaskExceptionRoutine"),".state=RUNNING\r\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"TaskExceptionRoutine"),"\r\n"
    end
  elsif /leave from tex ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,nil,"TaskExceptionRoutine"),".leave()\r\n"
    print "[",time,"]",FmpResource.ResName(prcid,nil,"TaskExceptionRoutine"),".state=DORMANT\r\n"
  elsif /applog str : ID ([^: ]+) : ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"ApplogString"),".str=",$2,"\r\n"
  elsif /applog strtask : TASK ([^: ]+) : ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".applog_str=",$2,"\r\n"
  elsif	/applog state : ID ([^: ]+) : (\d+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"ApplogState"),".state=",$2,"\r\n"
  elsif	/applog statetask : TASK ([^: ]+) : (\d+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".applog_state=",$2,"\r\n"
  else
    #�G���[�������K�v
  end
end


