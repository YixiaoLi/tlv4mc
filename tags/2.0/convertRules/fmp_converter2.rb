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
# $Context = Hash::new{|i| Array.new}  #現在のコンテキストの名前を格納する変数
# $Context = Hash.new([])
# $Context = Hash.new([])
# @TaskAtr = Hash::new{|hash, key| hash[key] = {} }
# array = Array.new(5){ |i| Array.new(5,0) }
$PrcArray = Array.new #プロセッサ管理用の配列
$PrcNum = 0 #プロセッサ数を管理する変数

#リソースの情報を管理するスーパークラス#
class ResourceClass 
  #リソースを管理するクラスの初期化（リソースクラスに変更？）
  def initialize
  end
#リソースを管理するためのハッシュを作成
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
class  FmpResourceClass < ResourceClass
  #リソースを管理するクラスの初期化（リソースクラスに変更？）
  def initialize
    pnum = #プロセッサ数を数える
      # @TaskAtr = Hash::new #タスクの属性を保持するハッシュ
      @TaskAtr = Hash::new{|hash, key| hash[key] = {} }
      @CyclicHandlerAtr = Hash::new{|hash, key| hash[key] = {} }
      @AlarmHandlerAtr = Hash::new{|hash, key| hash[key] = {} }
    self.CreHash #それぞれのタスクの状態を保持するハッシュの生成
  end
  def CreHash
    #それぞれのタスクの状態を保持するハッシュをプロセッサ毎に生成
    #プロセッサ数を数える
    $ruby_obj["Resources"].each{|key,value|#すべてのリソースにおいて，
      if @TaskAtr.key?(value["Attributes"]["prcId"]) #ハッシュ内にプロセッサIDのハッシュが存在するか確認し、
      else #存在しない場合は
        $PrcArray.push value["Attributes"]["prcId"] #プロセッサ管理用の配列にプロセッサIDのものを追加
        $PrcNum = $PrcNum + 1 #プロセッサ数を変更
      end
      case value["Type"]
      when "Task"
        @TaskAtr[value["Attributes"]["prcId"]][key] = value["Attributes"]["state"] #TaskAtrに現在の状態を値としてハッシュを追加
      when  "CyclicHandler"
        @CyclicHandlerAtr[value["Attributes"]["prcIdC"]][key] = value["Attributes"]["state"] #TaskAtrに現在の状態を値としてハッシュを追加
      when "AlarmHandler"
        @AlarmHandlerAtr[value["Attributes"]["prcIdA"]][key] = value["Attributes"]["state"] #TaskAtrに現在の状態を値としてハッシュを追加
        #"prcIdA"
      else
      end
    }
    $Context = Array.new($PrcNum+1){Array.new}
  end
  #タスクidからリソース名を取得するメソッド
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
      #AttributeのIDで判断
      #    TaskExceptionRoutinの場合(タスクIDを持たない)
      if nil == tskid && value["Type"] == type && prcid == value["Attributes"][prcid_type]
        return key
      end
      #    TaskExceptionRoutin以外
      #マイグレーションが起こるもの(IDは固有)
      if type == "Task" || type == "CyclicHandler" || type == "AlarmHandler" || type == "ApplogString" || type == "ApplogState"
        if value["Attributes"]["id"].to_i == tskid.to_i && value["Type"] == type
          return key
        else
        end
        #マイグレーションが起こらないもの
      else
        if value["Attributes"]["id"].to_i == tskid.to_i && value["Type"] == type && prcid == value["Attributes"][prcid_type]
          return key
        else
        end
      end
    }
    return nil #例外処理をきちんと行う
  end
  #タスクの状態を変更するメソッド
  #タスクidではなく，タスク名を入れるように変更
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
  
  #リソースの状態を参照するメソッド 引数(プロセッサID，リソースID，リソース属性)
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
  
  #タスク状態の一覧を表示するメソッド（デバッグ用）
  def PrintAtr()
    p "ptintAtr"
    $PrcArray.each{|key1|
      p "prcID = ",key1
      @TaskAtr[key1].each{|key2,value|
        print key2 ," = ",value ,"\n"
      }   
    }
  end

  #タスク状態の一覧を表示するメソッド（デバッグ用）
  def PrintAtr2()
    p @TaskAtr
    p @CyclicHandlerAtr
    p @AlarmHandlerAtr
  end
  
  #実行中のタスクが存在するか関数　Resrunningと同様の処理で行けるか？
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
  
  #実行中のタスク名を返す関数
  def ResRunning(prcid)
    @TaskAtr[prcid].each{|key,value|
      if "RUNNING"==value
        return key
      end
    }
    return nil
  end  
  def Migration(fromprcid,toprcid,id,type)
    #リソースidでいいのか?まぁ簡単に名前ゲットできるしひとまずこっちで．
    #元のプロセッサにあるリソースの情報を取り除く
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

=begin #FMPスクリプト確認用関数

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

#1行ずつログを読み取り
logs.each do|line|
  line.chomp!
  # 各OS共通部分の取得
  if /\[(\d+)\]:\[(\d+)\]: (.*)/ =~ line
    time = $1
    prcid = ($2).to_i
    pattern = $3
  end
  #各パターン毎の処理の記述
  if /dispatch to task (\d+)\./ =~ pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      # FmpResource.PrintAtr
      print "[",time,"]",$Context[prcid][-1],".preempt()\n"
      print "[",time,"]",$Context[prcid][-1],".state=RUNNABLE\n"
      # $Context[prcid].each{|key| puts "key = ",key} 
      # puts "$Context[1] = "
      # $Context[1].each{|key| puts "key = ",key} 
      # puts "$Context[1] = "
      # $Context[2].each{|key| puts "key = ",key} 
      FmpResource.ChangeAtr(prcid,$Context[prcid][-1],"Task","RUNNABLE")
    end
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".dispatch()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"Task"),"\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"Task")

    FmpResource.ChangeAtr(prcid,FmpResource.ResName(prcid,$1,"Task"),"Task","RUNNING")
  elsif /task (\d+) becomes ([^\.]+)\./ =~pattern
    if FmpResource.ResAtr(prcid,$1,"Task") == "DORMANT" && $2 == "RUNNABLE"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".activate()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNING" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".exit()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNING" && $2 == "WAITING"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".wait()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNABLE" && $2 == "SUSPENDED"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".suspended()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING" && $2 == "WAITING-SUSPENDED"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".suspended()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "SUSPENDED" && $2 == "RUNNABLE"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".resume()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING-SUSPENDED" && $2 == "WAITING"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".resume()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING" && $2 == "RUNNABLE"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".releaseFromWaiting()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING-SUSPENDED" && $2 == "SUSPENDED"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".releaseFromWaiting()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "SUSPENDED" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING-SUSPENDED" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "WAITING" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\n"
    elsif FmpResource.ResAtr(prcid,$1,"Task") == "RUNNABLE" && $2 == "DORMANT"
      print"[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".terminate()\n"
    end
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".state=",$2,"\n"
    FmpResource.ChangeAtr(prcid,FmpResource.ResName(prcid,$1,"Task"),"Task",$2)
  elsif /enter to ((?!sns)(?!get_utm)(?!ext_ker)[^ix]\w+[_]\w+)( (.+))?\.?/ =~pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      if nil == $2
        print "[",time,"]",FmpResource.ResRunning(prcid),".enterSVC(",$1,")\n"
      else
        print "[",time,"]",FmpResource.ResRunning(prcid),".enterSVC(",$1,",",$3.delete(" "),")\n"
      end
    end
  elsif /leave from ((?!sns)(?!get_utm)(?!ext_ker)[^ix]\w+[_]\w+)( (.+))?\.?/ =~pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      if nil == $2
        print "[",time,"]",FmpResource.ResRunning(prcid),".leaveSVC(",$1,")\n"
      else
        print "[",time,"]",FmpResource.ResRunning(prcid),".leaveSVC(",$1,",",$3.delete(" "),")\n"
      end
    end
  elsif /task (\d+) migrates from processor (\d+) to processor (\d+)\./ =~pattern
    if FmpResource.ExistRunning?(prcid,"Task")
      if FmpResource.ResAtr($2.to_i,$1,"Task") == "RUNNING"
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"Task"),".state=RUNNABLE\n"
      end
    end
    if nil != FmpResource.ResName($2.to_i,$1,"Task")
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"Task"),".prcId=",$3,"\n"      
      FmpResource.Migration($2.to_i,$3.to_i,$1,"Task")
    end
  elsif /cyclic handler (\d+) migrates from processor (\d+) to processor (\d+)\./ =~pattern
    if FmpResource.ExistRunning?(prcid,"CyclicHandler")
      if FmpResource.ResAtr($2.to_i,FmpResource.ResName(prcid,$1,"CyclicHandler"),"CyclicHandler") == "RUNNING"
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"CyclicHandler"),".state=RUNNABLE\n"
      end
    end
    if nil != FmpResource.ResName($2.to_i,$1,"CyclicHandler")
      print "[",time,"]",FmpResource.ResName($2.to_i,$1,"CyclicHandler"),".prcIdC=",$3,"\n"      
      FmpResource.Migration($2.to_i,$3.to_i,$1,"CyclicHandler")
    end
  elsif /alarm handler (\d+) migrates from processor (\d+) to processor (\d+)\./ =~pattern
    if FmpResource.ExistRunning?(prcid,"AlarmHandler")
      if FmpResource.ResAtr($2.to_i,FmpResource.ResName(prcid,$1,"AlarmHandler"),"AlarmHandler") == "RUNNING"
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"AlarmHandler"),".state=RUNNABLE\n"
      end
    end
    if nil != FmpResource.ResName($2.to_i,$1,"AlarmHandler")
        print "[",time,"]",FmpResource.ResName($2.to_i,$1,"AlarmHandler"),".prcIdA=",$3,"\n"      
      FmpResource.Migration($2.to_i,$3.to_i,$1,"AlarmHandler")
    end
  elsif /enter to ((i\w+[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,")\n"
    else
        print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,",",$4.delete(" "),")\n"  
    end
  elsif /leave from ((i\w+[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,")\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,",",$4.delete(" "),")\n"
    end
    #以下２つテスト未実施 $4の方がいい？
  elsif /enter to ((x?sns[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,")\n"      
    else
      print "[",time,"]",$Context[prcid][-1],".enterSVC(",$1,",",$4.delete(" "),")\n"      
    end
  elsif /leave from ((x?sns[_]\w+))( (.+))?\.?/ =~ pattern
    if nil == $3
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,")\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(",$1,",",$4.delete(" "),")\n"
    end
    #以下４つテスト怪しい
  elsif /enter to get_utm( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".enterSVC(get_utm)\n"
    else
      print "[",time,"]",$Context[prcid][-1],".enterSVC(get_utm,",$2.delete(" "),")\n"
    end
  elsif /leave from get_utm( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(get_utm)\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(get_utm,",$2.delete(" "),")\n"
    end
  elsif	/enter to ext_ker( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".enterSVC(ext_ker)\n"
    else
      print "[",time,"]",$Context[prcid][-1],".enterSVC(ext_ker,",$2.delete(" "),")\n"
    end
  elsif /leave from ext_ker( (.+))?\.?/ =~ pattern
    if nil == $1
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(ext_ker)\n"
    else
      print "[",time,"]",$Context[prcid][-1],".leaveSVC(ext_ker,",$2.delete(" "),")\n"
    end
  elsif /enter to int handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".enter()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"InterruptHandler"),"\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"InterruptHandler")
  elsif	/leave from int handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".leave()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptHandler"),".state=DORMANT\n"
    $Context[prcid].pop
  elsif /enter to isr ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".enter()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),"\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"InterruptServiceRoutine")
  elsif	/leave from isr ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".leave()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"InterruptServiceRoutine"),".state=DORMANT\n"
    $Context[prcid].pop
  elsif /enter to cyclic handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".enter()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"CyclicHandler"),"\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"CyclicHandler")
  elsif	/leave from cyclic handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".leave()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CyclicHandler"),".state=DORMANT\n"
    $Context[prcid].pop
  elsif /enter to alarm handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".enter()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"AlarmHandler"),"\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"AlarmHandler")
  elsif	/leave from alarm handler ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".leave()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"AlarmHandler"),".state=DORMANT\n"
    $Context[prcid].pop
  elsif /enter to exc handler ([^\.]+)\.?/ =~pattern 
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".enter()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),"\n"
    $Context[prcid].push FmpResource.ResName(prcid,$1,"CPUExceptionHandler")
  elsif /leave from exc handler ([^\.]+)\.?/ =~pattern 
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".leave()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"CPUExceptionHandler"),".state=DORMANT\n"
    $Context[prcid].pop
  elsif /enter to tex ([^\.]+)\.?/ =~ pattern
    if FmpResource.ResName(prcid,$1,"TaskExceptionRoutine") != nil
    print "[",time,"]",FmpResource.ResName(prcid,$1,"TaskExceptionRoutine"),".enter()\n"
    print "[",time,"]",FmpResource.ResName(prcid,$1,"TaskExceptionRoutine"),".state=RUNNING\n"
    print "[",time,"]CurrentContext_PRC",prcid,".name=",FmpResource.ResName(prcid,$1,"TaskExceptionRoutine"),"\n"
    end
  elsif /leave from tex ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,nil,"TaskExceptionRoutine"),".leave()\n"
    print "[",time,"]",FmpResource.ResName(prcid,nil,"TaskExceptionRoutine"),".state=DORMANT\n"
  elsif /applog str : ID ([^: ]+) : ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"ApplogString"),".str=",$2,"\n"
  elsif /applog strtask : TASK ([^: ]+) : ([^\.]+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".applog_str=",$2,"\n"
  elsif	/applog state : ID ([^: ]+) : (\d+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"ApplogState"),".state=",$2,"\n"
  elsif	/applog statetask : TASK ([^: ]+) : (\d+)\.?/ =~ pattern
    print "[",time,"]",FmpResource.ResName(prcid,$1,"Task"),".applog_state=",$2,"\n"
  else
    #エラー処理が必要
  end
end


