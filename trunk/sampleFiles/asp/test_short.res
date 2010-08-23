{
  "TimeScale": "us",
  "TimeRadix": 10,
  "Path": "C:\\OJL\\sampleFiles\\asp\\test_short.res",
  "ConvertRules": [
    "asp"
  ],
  "VisualizeRules": [
    "toppers",
    "asp"
  ],
  "ResourceHeaders": {
    "Context": {
      "DisplayName": "コンテキスト",
      "Attributes": {
        "name": {
          "DisplayName": "コンテキスト名",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff123ece"
        }
      },
      "Color": "ff9b27d1"
    },
    "InterruptHandler": {
      "DisplayName": "割込みハンドラ",
      "Attributes": {
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffed1a9c"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff157dd3"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "割込みハンドラー開始",
          "Arguments": {},
          "Color": "ffe8d50d"
        },
        "leave": {
          "DisplayName": "割込みハンドラー終了",
          "Arguments": {},
          "Color": "ff9808db"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffb4ce0c"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff0cf232"
        }
      },
      "Color": "ffd711f9"
    },
    "InterruptServiceRoutine": {
      "DisplayName": "割込みサービスルーチン",
      "Attributes": {
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffe20b41"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffb1ea04"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "割込みサービスルーチン開始",
          "Arguments": {},
          "Color": "ff1207ef"
        },
        "leave": {
          "DisplayName": "割込みサービスルーチン終了",
          "Arguments": {},
          "Color": "ff50f404"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "fff218e3"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff0ccc88"
        }
      },
      "Color": "ff0721ed"
    },
    "CyclicHandler": {
      "DisplayName": "周期ハンドラ",
      "Attributes": {
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffd804ca"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffcc063a"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "周期ハンドラー開始",
          "Arguments": {},
          "Color": "ff5e30f4"
        },
        "leave": {
          "DisplayName": "周期ハンドラー終了",
          "Arguments": {},
          "Color": "ff5efc32"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff4824cc"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff14e83b"
        }
      },
      "Color": "ffce0429"
    },
    "AlarmHandler": {
      "DisplayName": "アラームハンドラ",
      "Attributes": {
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffdb209a"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff94e21f"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "アラームハンドラー開始",
          "Arguments": {},
          "Color": "ff6211f9"
        },
        "leave": {
          "DisplayName": "アラームハンドラー終了",
          "Arguments": {},
          "Color": "ffc110ce"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff0fd8bd"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "fff2308a"
        }
      },
      "Color": "ff07f4d5"
    },
    "CPUExceptionHandler": {
      "DisplayName": "CPU例外ハンドラ",
      "Attributes": {
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "fff42e6d"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff2ef2c4"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "CPU例外ハンドラー開始",
          "Arguments": {},
          "Color": "ffcfe224"
        },
        "leave": {
          "DisplayName": "CPU例外ハンドラー終了",
          "Arguments": {},
          "Color": "ffdb380a"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff105bef"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffade520"
        }
      },
      "Color": "ffd64515"
    },
    "TaskExceptionRoutine": {
      "DisplayName": "タスク例外ルーチン",
      "Attributes": {
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff1130e0"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "fff99d13"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "タスク例外ルーチン開始",
          "Arguments": {},
          "Color": "ff2d09f4"
        },
        "leave": {
          "DisplayName": "タスク例外ルーチン終了",
          "Arguments": {},
          "Color": "ff66f904"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff0abbd6"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff6f20d6"
        }
      },
      "Color": "ff22d859"
    },
    "Task": {
      "DisplayName": "タスク",
      "Attributes": {
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff392ad6"
        },
        "atr": {
          "DisplayName": "属性",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffb6d108"
        },
        "pri": {
          "DisplayName": "優先度",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ff2ac9f9"
        },
        "exinf": {
          "DisplayName": "拡張情報",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff1c30ea"
        },
        "task": {
          "DisplayName": "起動関数",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffd12402"
        },
        "stksz": {
          "DisplayName": "スタックサイズ",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff0b9ee8"
        },
        "stk": {
          "DisplayName": "スタック番地",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff28d673"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": "DORMANT",
          "Color": "ffd127b4"
        },
        "applog_str": {
          "DisplayName": "文字列",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": "",
          "Color": "ff1be8f7"
        },
        "applog_state": {
          "DisplayName": "ユーザ定義状態",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": 0,
          "Color": "ff61ce0e"
        }
      },
      "Behaviors": {
        "preempt": {
          "DisplayName": "プリエンプト",
          "Arguments": {},
          "Color": "ffe88712"
        },
        "dispatch": {
          "DisplayName": "ディスパッチ",
          "Arguments": {},
          "Color": "fff70743"
        },
        "activate": {
          "DisplayName": "起動",
          "Arguments": {},
          "Color": "ff02e88f"
        },
        "wait": {
          "DisplayName": "待ち",
          "Arguments": {},
          "Color": "ff51dd0b"
        },
        "exit": {
          "DisplayName": "終了",
          "Arguments": {},
          "Color": "fff20e93"
        },
        "resume": {
          "DisplayName": "再開",
          "Arguments": {},
          "Color": "ff0d3cd8"
        },
        "suspend": {
          "DisplayName": "強制待ち",
          "Arguments": {},
          "Color": "ffdae023"
        },
        "terminate": {
          "DisplayName": "強制終了",
          "Arguments": {},
          "Color": "ffae08d3"
        },
        "releaseFromWaiting": {
          "DisplayName": "待ち解除",
          "Arguments": {},
          "Color": "fffc9211"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff81db20"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffe514d4"
        },
        "act_tsk": {
          "DisplayName": "act_tsk",
          "Arguments": {
            "tskid": "Number"
          },
          "Color": "fff43207"
        },
        "dly_tsk": {
          "DisplayName": "dly_tsk",
          "Arguments": {
            "dlytim": "Number"
          },
          "Color": "ffceb80e"
        },
        "slp_tsk": {
          "DisplayName": "slp_tsk",
          "Arguments": {},
          "Color": "ff84e004"
        },
        "wup_tsk": {
          "DisplayName": "wup_tsk",
          "Arguments": {
            "tskid": "Number"
          },
          "Color": "ff4708ce"
        }
      },
      "Color": "ffc1ea2e"
    },
    "ApplogString": {
      "DisplayName": "アプリログ（文字列）",
      "Attributes": {
        "id": {
          "DisplayName": "ID",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "fffc20ed"
        },
        "str": {
          "DisplayName": "文字列",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": "",
          "Color": "ff1eedd8"
        }
      },
      "Color": "ffdcef2b"
    },
    "ApplogState": {
      "DisplayName": "アプリログ（状態）",
      "Attributes": {
        "id": {
          "DisplayName": "ID",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "fff2218a"
        },
        "state": {
          "DisplayName": "ユーザ定義状態",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": 0,
          "Color": "ff1429cc"
        }
      },
      "Color": "ffe72dfc"
    }
  },
  "Resources": {
    "CurrentContext": {
      "DisplayName": "CurrentContext",
      "Type": "Context",
      "Attributes": {
        "name": "None"
      },
      "Color": "ff64ef07",
      "Visible": true
    },
    "LOGTASK": {
      "DisplayName": "LOGTASK",
      "Type": "Task",
      "Attributes": {
        "id": 1,
        "atr": "TA_ACT",
        "pri": 3,
        "exinf": "LOGTASK_PORTID",
        "task": "logtask_main",
        "stksz": 1024,
        "stk": "NULL",
        "state": "RUNNABLE",
        "applog_str": "",
        "applog_state": 0
      },
      "Color": "fff71b31",
      "Visible": true
    },
    "TASK1": {
      "DisplayName": "TASK1",
      "Type": "Task",
      "Attributes": {
        "id": 2,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "1",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT",
        "applog_str": "",
        "applog_state": 0
      },
      "Color": "ffbecc26",
      "Visible": true
    },
    "TASK2": {
      "DisplayName": "TASK2",
      "Type": "Task",
      "Attributes": {
        "id": 3,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "2",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT",
        "applog_str": "",
        "applog_state": 0
      },
      "Color": "ffb81bf7",
      "Visible": true
    },
    "TASK3": {
      "DisplayName": "TASK3",
      "Type": "Task",
      "Attributes": {
        "id": 4,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "3",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT",
        "applog_str": "",
        "applog_state": 0
      },
      "Color": "ff5ef202",
      "Visible": true
    },
    "MAIN_TASK": {
      "DisplayName": "MAIN_TASK",
      "Type": "Task",
      "Attributes": {
        "id": 5,
        "atr": "TA_ACT",
        "pri": 5,
        "exinf": "0",
        "task": "main_task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "RUNNABLE",
        "applog_str": "",
        "applog_state": 0
      },
      "Color": "ffe29f22",
      "Visible": true
    },
    "INH_INHNO_TIMER_target_timer_handler": {
      "DisplayName": "INH_INHNO_TIMER_target_timer_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "id": 5,
        "state": "DORMANT"
      },
      "Color": "ff06ccbb",
      "Visible": true
    },
    "INH_INHNO_SIO_sio_handler0": {
      "DisplayName": "INH_INHNO_SIO_sio_handler0",
      "Type": "InterruptHandler",
      "Attributes": {
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ff0728fc",
      "Visible": true
    },
    "CYCHDR1": {
      "DisplayName": "CYCHDR1",
      "Type": "CyclicHandler",
      "Attributes": {
        "id": 1,
        "state": "DORMANT"
      },
      "Color": "ffd8110a",
      "Visible": true
    },
    "ALMHDR1": {
      "DisplayName": "ALMHDR1",
      "Type": "AlarmHandler",
      "Attributes": {
        "id": 1,
        "state": "DORMANT"
      },
      "Color": "ff0cf930",
      "Visible": true
    },
    "CPUEXC1": {
      "DisplayName": "CPUEXC1",
      "Type": "CPUExceptionHandler",
      "Attributes": {
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ffd60857",
      "Visible": true
    },
    "TASK_TEX": {
      "DisplayName": "TASK_TEX",
      "Type": "TaskExceptionRoutine",
      "Attributes": {
        "state": "DORMANT",
        "id": null
      },
      "Color": "ffdb7e0d",
      "Visible": true
    }
  }
}