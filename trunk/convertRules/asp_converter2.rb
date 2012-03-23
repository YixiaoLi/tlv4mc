#!/usr/bin/ruby -Ks
# -*- coding: japanese-cp932 -*-

#入力：標準入力（リソースファイル，ASPのログ）
#出力：標準出力（標準形式のログ）
# $Id: converter.rb miwa $

# $KCODE = "UTF-8"
require 'strscan'
require "optparse"
require 'rubygems'
require 'json'
#require 'JsonParser'


#標準入力からの読み込み
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

#標準入力からリソースファイルとログを読み込み，それぞれraw_res，logsに格納
raw_res,logs = ARGF.readlines().break{|line|
  line.chop == '---'
}



##変換と出力


#JSON形式のリソースファイルをハッシュに格納
$ruby_obj = JSON.parse(raw_res.join)
$Context = Array.new  #現在のコンテキストの名前を格納する変数


#リソースの情報を管理するスーパークラス#
class ResourceClass 
#リソースを管理するクラスの初期化（リソースクラスに変更？）
  def initialize
  end
  def CreHash
  end
  #タスクidからタスク名を取得するメソッド
  def ResName
  end
  #タスクの状態を変更するメソッド
  #タスクidではなく，タスク名を入れるように変更
  def ChangeAtr
  end
  #タスクの状態を参照するメソッド
  def ResAtr
  end
  #タスク状態の一覧を表示するメソッド（デバッグ用）
  def PrintAtr()
  end
  #実行中のタスクが存在するか関数　Resrunningと同様の処理で行けるか？
  def ExistRunning?()
  end
  #実行中のタスク名を返す関数
  def ResRunning()
  end  
end

#タスク状態の一覧を表示するメソッド（デバッグ用）
class  AspResourceClass < ResourceClass
#リソースを管理するクラスの初期化（リソースクラスに変更？）
  def initialize
    @TaskAtr = Hash::new #タスクの属性を保持するハッシュ
    self.CreHash #それぞれのタスクの状態を保持するハッシュの生成
  end
  def CreHash
    #それぞれのタスクの状態を保持するハッシュの生成
    $ruby_obj["Resources"].each{|key,value|#すべてのリソースにおいて，
      if value["Type"] == "Task" #属性がタスクの場合のみ
        @TaskAtr[key] = value["Attributes"]["state"]#TaskAtrに現在の状態を値としてハッシュを追加
      else
      end
    }
  end
  #タスクidからタスク名を取得するメソッド
  def ResName(tskid,type)
    $ruby_obj["Resources"].each{|key,value|
      #AttributeのIDで判断
      #    TaskExceptionRoutinの場合(タスクIDを持たない)
      if nil == tskid && value["Type"] == type
        return key
      end
      #    TaskExceptionRoutin以外
      if value["Attributes"]["id"].to_i == tskid.to_i && value["Type"] == type
        return key
      else
      end
    }
    return nil #例外処理をきちんと行う
  end
  #タスクの状態を変更するメソッド
  #タスクidではなく，タスク名を入れるように変更
  def ChangeAtr(tskname,toatr)
    @TaskAtr[tskname] = toatr
  end
  
  #タスクの状態を参照するメソッド
  def ResAtr(tskid)
    return @TaskAtr[ResName(tskid,"Task")]
  end

  #タスク状態の一覧を表示するメソッド（デバッグ用）
  def PrintAtr()
    @TaskAtr.each{|key,value|
      print key ," = ",value ,"\n"
    }
  end
  
  #実行中のタスクが存在するか関数　Resrunningと同様の処理で行けるか？
  def ExistRunning?()
    @TaskAtr.each{|key,value|
      if "RUNNING"==value
        return 1
      end
    }
    return nil
  end
  
  #実行中のタスク名を返す関数
  def ResRunning()
    @TaskAtr.each{|key,value|
      if "RUNNING"==value
        return key
      end
    }
    return nil
  end  
end

=begin 確認用
asp_resource = ASP_ResourceClass.new
asp_resource.PrintAtr
p asp_resource.ResAtr(5)
p asp_resource.ResRunning
asp_resource.ChangeAtr("MAIN_TASK","RUNNING")
p asp_resource.ResAtr(5)
p asp_resource.ResRunning
=end

AspResource = AspResourceClass.new

#1行ずつログを読み取り
logs.each do|line|
  line.chomp!
  # 各OS共通部分の取得
  if /\[(\d+)\] (.*)/ =~ line
    time = $1
    pattern = $2
  end
  #各パターン毎の処理の記述
  if /dispatch to task (\d+)\./ =~ pattern
    if AspResource.ExistRunning?()
      print "[",time,"]",$Context[-1],".preempt()\n"
      print "[",time,"]",$Context[-1],".state=RUNNABLE\n"
      AspResource.ChangeAtr($Context[-1],"RUNNABLE")
    end
    print "[",time,"]",AspResource.ResName($1,"Task"),".dispatch()\n"
    print "[",time,"]",AspResource.ResName($1,"Task"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName($1,"Task"),"\n"
    $Context.push AspResource.ResName($1,"Task")
    AspResource.ChangeAtr(AspResource.ResName($1,"Task"),"RUNNING")
  elsif /task (\d+) becomes ([^\.]+)\./ =~pattern
    if AspResource.ResAtr($1) == "DORMANT" && $2 == "RUNNABLE"
      print"[",time,"]",AspResource.ResName($1,"Task"),".activate()\n"
    elsif AspResource.ResAtr($1) == "RUNNING" && $2 == "DORMANT"
      print"[",time,"]",AspResource.ResName($1,"Task"),".exit()\n"
    elsif AspResource.ResAtr($1) == "RUNNING" && $2 == "WAITING"
      print"[",time,"]",AspResource.ResName($1,"Task"),".wait()\n"
    elsif AspResource.ResAtr($1) == "RUNNABLE" && $2 == "SUSPENDED"
      print"[",time,"]",AspResource.ResName($1,"Task"),".suspended()\n"
    elsif AspResource.ResAtr($1) == "WAITING" && $2 == "WAITING-SUSPENDED"
      print"[",time,"]",AspResource.ResName($1,"Task"),".suspended()\n"
    elsif AspResource.ResAtr($1) == "SUSPENDED" && $2 == "RUNNABLE"
      print"[",time,"]",AspResource.ResName($1,"Task"),".resume()\n"
    elsif AspResource.ResAtr($1) == "WAITING-SUSPENDED" && $2 == "WAITING"
      print"[",time,"]",AspResource.ResName($1,"Task"),".resume()\n"
    elsif AspResource.ResAtr($1) == "WAITING" && $2 == "RUNNABLE"
      print"[",time,"]",AspResource.ResName($1,"Task"),".releaseFromWaiting()\n"
    elsif AspResource.ResAtr($1) == "WAITING-SUSPENDED" && $2 == "SUSPENDED"
      print"[",time,"]",AspResource.ResName($1,"Task"),".releaseFromWaiting()\n"
    elsif AspResource.ResAtr($1) == "SUSPENDED" && $2 == "DORMANT"
      print"[",time,"]",AspResource.ResName($1,"Task"),".terminate()\n"
    elsif AspResource.ResAtr($1) == "WAITING-SUSPENDED" && $2 == "DORMANT"
      print"[",time,"]",AspResource.ResName($1,"Task"),".terminate()\n"
    elsif AspResource.ResAtr($1) == "WAITING" && $2 == "DORMANT"
      print"[",time,"]",AspResource.ResName($1,"Task"),".terminate()\n"
    elsif AspResource.ResAtr($1) == "RUNNABLE" && $2 == "DORMANT"
      print"[",time,"]",AspResource.ResName($1,"Task"),".terminate()\n"
    end
    print "[",time,"]",AspResource.ResName($1,"Task"),".state=",$2,"\n"
    AspResource.ChangeAtr(AspResource.ResName($1,"Task"),$2)
  elsif /enter to ((?!sns)(?!get_utm)(?!ext_ker)[^ix]\w+[_]\w+)( (.+))?\.?/ =~pattern
    if AspResource.ExistRunning?()
      if nil == $2
        print "[",time,"]",AspResource.ResRunning(),".enterSVC(",$1,")\n"
      else
        print "[",time,"]",AspResource.ResRunning(),".enterSVC(",$1,",",$3.delete(" "),")\n"
      end
    end
  elsif /leave from ((?!sns)(?!get_utm)(?!ext_ker)[^ix]\w+[_]\w+)( (.+))?\.?/ =~pattern
    if AspResource.ExistRunning?()
      if nil == $2
        print "[",time,"]",AspResource.ResRunning(),".leaveSVC(",$1,")\n"
      else
        print "[",time,"]",AspResource.ResRunning(),".leaveSVC(",$1,",",$3.delete(" "),")\n"
      end
    end
  elsif /enter to ((i\w+[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[-1],".enterSVC(",$1,")\n"
    else
      print "[",time,"]",$Context[-1],".enterSVC(",$1,",",$4.delete(" "),")\n"  
    end
  elsif /leave from ((i\w+[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[-1],".leaveSVC(",$1,")\n"
    else
      print "[",time,"]",$Context[-1],".leaveSVC(",$1,",",$4.delete(" "),")\n"
    end
  elsif /enter to ((x?sns[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[-1],".enterSVC(",$1,")\n"      
    else
      print "[",time,"]",$Context[-1],".enterSVC(",$1,",",$4.delete(" "),")\n"      
    end
  elsif /leave from ((x?sns[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[-1],".leaveSVC(",$1,")\n"
    else
      print "[",time,"]",$Context[-1],".leaveSVC(",$1,",",$4.delete(" "),")\n"
    end
  elsif /enter to get_utm( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[-1],".enterSVC(get_utm)\n"
    else
      print "[",time,"]",$Context[-1],".enterSVC(get_utm,",$2.delete(" "),")\n"
    end
  elsif /leave from get_utm( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[-1],".leaveSVC(get_utm)\n"
    else
      print "[",time,"]",$Context[-1],".leaveSVC(get_utm,",$2.delete(" "),")\n"
    end
  elsif	/enter to ext_ker( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[-1],".enterSVC(ext_ker)\n"
    else
      print "[",time,"]",$Context[-1],".enterSVC(ext_ker,",$2.delete(" "),")\n"
    end
  elsif /leave from ext_ker( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[-1],".leaveSVC(ext_ker)\n"
    else
      print "[",time,"]",$Context[-1],".leaveSVC(ext_ker,",$2.delete(" "),")\n"
    end
  elsif /enter to int handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"InterruptHandler"),".enter()\n"
    print "[",time,"]",AspResource.ResName($1,"InterruptHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName($1,"InterruptHandler"),"\n"
    $Context.push AspResource.ResName($1,"InterruptHandler")
  elsif	/leave from int handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"InterruptHandler"),".leave()\n"
    print "[",time,"]",AspResource.ResName($1,"InterruptHandler"),".state=DORMANT\n"
    $Context.pop
  elsif /enter to isr ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"InterruptServiceRoutine"),".enter()\n"
    print "[",time,"]",AspResource.ResName($1,"InterruptServiceRoutine"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName($1,"InterruptServiceRoutine"),"\n"
    $Context.push AspResource.ResName($1,"InterruptServiceRoutine")
  elsif	/leave from isr ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"InterruptServiceRoutine"),".leave()\n"
    print "[",time,"]",AspResource.ResName($1,"InterruptServiceRoutine"),".state=DORMANT\n"
    $Context.pop
  elsif /enter to cyclic handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"CyclicHandler"),".enter()\n"
    print "[",time,"]",AspResource.ResName($1,"CyclicHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName($1,"CyclicHandler"),"\n"
    $Context.push AspResource.ResName($1,"CyclicHandler")
  elsif	/leave from cyclic handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"CyclicHandler"),".leave()\n"
    print "[",time,"]",AspResource.ResName($1,"CyclicHandler"),".state=DORMANT\n"
    $Context.pop
  elsif /enter to alarm handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"AlarmHandler"),".enter()\n"
    print "[",time,"]",AspResource.ResName($1,"AlarmHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName($1,"AlarmHandler"),"\n"
    $Context.push AspResource.ResName($1,"AlarmHandler")
  elsif	/leave from alarm handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"AlarmHandler"),".leave()\n"
    print "[",time,"]",AspResource.ResName($1,"AlarmHandler"),".state=DORMANT\n"
    $Context.pop
  elsif /enter to exc handler ([^\.]+)\.?/ =~pattern 
    print "[",time,"]",AspResource.ResName($1,"CPUExceptionHandler"),".enter()\n"
    print "[",time,"]",AspResource.ResName($1,"CPUExceptionHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName($1,"CPUExceptionHandler"),"\n"
    $Context.push AspResource.ResName($1,"CPUExceptionHandler")
  elsif /leave from exc handler ([^\.]+)\.?/ =~pattern 
    print "[",time,"]",AspResource.ResName($1,"CPUExceptionHandler"),".leave()\n"
    print "[",time,"]",AspResource.ResName($1,"CPUExceptionHandler"),".state=DORMANT\n"
    $Context.pop
  elsif /enter to tex ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName(nil,"TaskExceptionRoutine"),".enter()\n"
    print "[",time,"]",AspResource.ResName(nil,"TaskExceptionRoutine"),".state=RUNNING\n"
    print "[",time,"]CurrentContext.name=",AspResource.ResName(nil,"TaskExceptionRoutine"),"\n"
    #$Context.push AspResource.ResName(nil,"TaskExceptionRoutine")
  elsif /leave from tex ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName(nil,"TaskExceptionRoutine"),".leave()\n"
    print "[",time,"]",AspResource.ResName(nil,"TaskExceptionRoutine"),".state=DORMANT\n"
    #$Context.pop
  elsif /applog str : ID ([^: ]+) : ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"ApplogString"),".str=",$2,"\n"
  elsif /applog strtask : TASK ([^: ]+) : ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"Task"),".applog_str=",$2,"\n"
  elsif	/applog state : ID ([^: ]+) : (\d+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"ApplogState"),".state=",$2,"\n"
  elsif	/applog statetask : TASK ([^: ]+) : (\d+)\.?/ =~ pattern
    print "[",time,"]",AspResource.ResName($1,"Task"),".applog_state=",$2,"\n"
  else
    #パターンに一致しなければ処理を行わない
  end
end

