{
  "TimeScale": "ns",
  "TimeRadix": 10,
  "ConvertRules": [
    "asp"
  ],
  "VisualizeRules": [
    "asp"
  ],
  "ResourceHeaders": {
    "Task": {
      "DisplayName": "タスク",
      "Attributes": {
        "prcId": {
          "DisplayName": "プロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": "",
          "Color": "ffb90cd3"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": "",
          "Color": "ff17ed97"
        },
        "atr": {
          "DisplayName": "属性",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": "",
          "Color": "fff920ef"
        },
        "pri": {
          "DisplayName": "優先度",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": "",
          "Color": "ffd3b419"
        },
        "exinf": {
          "DisplayName": "拡張情報",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": "",
          "Color": "ff0e19ef"
        },
        "task": {
          "DisplayName": "起動関数",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": "",
          "Color": "ffd6560c"
        },
        "stksz": {
          "DisplayName": "スタックサイズ",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": "",
          "Color": "ff6cf22e"
        },
        "stk": {
          "DisplayName": "スタック番地",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": "",
          "Color": "ffd730f4"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": "DORMANT",
          "Color": "ffdb083d"
        }
      },
      "Behaviors": {
        "preempt": {
          "DisplayName": "プリエンプト",
          "Arguments": {},
          "Color": "ff02d310"
        },
        "dispatch": {
          "DisplayName": "ディスパッチ",
          "Arguments": {},
          "Color": "ff1f34f2"
        },
        "activate": {
          "DisplayName": "起動",
          "Arguments": {},
          "Color": "fff73022"
        },
        "wait": {
          "DisplayName": "待ち",
          "Arguments": {},
          "Color": "ff4c2ad3"
        },
        "exit": {
          "DisplayName": "終了",
          "Arguments": {},
          "Color": "ffe827bb"
        },
        "resume": {
          "DisplayName": "再開",
          "Arguments": {},
          "Color": "ff0de5c1"
        },
        "suspend": {
          "DisplayName": "強制待ち",
          "Arguments": {},
          "Color": "ff2004ef"
        },
        "terminate": {
          "DisplayName": "強制終了",
          "Arguments": {},
          "Color": "ffac09f7"
        },
        "releaseFromWaiting": {
          "DisplayName": "待ち解除",
          "Arguments": {},
          "Color": "ff2e47e8"
        },
        "enterSVC": {
          "DisplayName": "サービスコールに入る",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "fff99602"
        },
        "leaveSVC": {
          "DisplayName": "サービスコールから出る",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff28ef78"
        },
        "act_tsk": {
          "DisplayName": "act_tsk",
          "Arguments": {
            "tskid": "Number"
          },
          "Color": "ff0d78e2"
        },
        "dly_tsk": {
          "DisplayName": "dly_tsk",
          "Arguments": {
            "dlytim": "Number"
          },
          "Color": "ff1be271"
        },
        "slp_tsk": {
          "DisplayName": "slp_tsk",
          "Arguments": {},
          "Color": "ffea3407"
        },
        "wup_tsk": {
          "DisplayName": "wup_tsk",
          "Arguments": {
            "tskid": "Number"
          },
          "Color": "ff2add6f"
        }
      },
      "Color": "ffdb6206"
    }
  },
  "Resources": {
    "LOGTASK": {
      "DisplayName": "ログタスク",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 1,
        "atr": "TA_ACT",
        "pri": 3,
        "exinf": "LOGTASK_PORTID",
        "task": "logtask_main",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "00ff0000",
      "Visible": true
    },
    "TASK1": {
      "DisplayName": "TASK1",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 2,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": 1,
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "0000ff00",
      "Visible": true
    },
    "TASK2": {
      "DisplayName": "TASK2",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 3,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": 2,
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "00ff00ff",
      "Visible": true
    },
    "TASK3": {
      "DisplayName": "TASK3",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 4,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": 3,
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "0000ffff",
      "Visible": true
    },
    "MAIN_TASK": {
      "DisplayName": "MAIN_TASK",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 5,
        "atr": "TA_ACT",
        "pri": 5,
        "exinf": 0,
        "task": "main_task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "000000ff",
      "Visible": true
    }
  }
}