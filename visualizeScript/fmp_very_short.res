{
  "TimeScale": "us",
  "TimeRadix": 10,
  "ConvertRules": [
    "fmp"
  ],
  "VisualizeRules": [
    "toppers",
    "fmp",
    "fmp_core2"
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
          "Color": "ffef15ec"
        }
      },
      "Color": "fffc400c"
    },
    "InterruptHandler": {
      "DisplayName": "割込みハンドラ",
      "Attributes": {
        "prcIdI": {
          "DisplayName": "割込みハンドラプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ffd67404"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff02ce8d"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff294dce"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "割込みハンドラー開始",
          "Arguments": {},
          "Color": "ff14e2c0"
        },
        "leave": {
          "DisplayName": "割込みハンドラー終了",
          "Arguments": {},
          "Color": "ffe522db"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff1d80f9"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffd10a6d"
        }
      },
      "Color": "ff17ef67"
    },
    "InterruptServiceRoutine": {
      "DisplayName": "割込みサービスルーチン",
      "Attributes": {
        "prcIdR": {
          "DisplayName": "割込みサービスルーチンプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ffd3061a"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff02bfd1"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffe22706"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "割込みサービスルーチン開始",
          "Arguments": {},
          "Color": "fff90c6b"
        },
        "leave": {
          "DisplayName": "割込みサービスルーチン終了",
          "Arguments": {},
          "Color": "ff0fd8a2"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff2922f4"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffcc222b"
        }
      },
      "Color": "ff9111db"
    },
    "CyclicHandler": {
      "DisplayName": "周期ハンドラ",
      "Attributes": {
        "prcIdC": {
          "DisplayName": "周期ハンドラプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ffc317d3"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff2147ef"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffd81795"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "周期ハンドラー開始",
          "Arguments": {},
          "Color": "ffd17804"
        },
        "leave": {
          "DisplayName": "周期ハンドラー終了",
          "Arguments": {},
          "Color": "ff0ecc8c"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffea198c"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff0223f9"
        }
      },
      "Color": "ff24e2c6"
    },
    "AlarmHandler": {
      "DisplayName": "アラームハンドラ",
      "Attributes": {
        "prcIdA": {
          "DisplayName": "アラームハンドラプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ffea2502"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffe02c50"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "fff7e331"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "アラームハンドラー開始",
          "Arguments": {},
          "Color": "ff7ed613"
        },
        "leave": {
          "DisplayName": "アラームハンドラー終了",
          "Arguments": {},
          "Color": "ffed17a2"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff138dd3"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff7b07ef"
        }
      },
      "Color": "ff02d17e"
    },
    "CPUExceptionHandler": {
      "DisplayName": "CPU例外ハンドラ",
      "Attributes": {
        "prcIdE": {
          "DisplayName": "CPU例外ハンドラプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "fffc16ca"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffb7f427"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff0659dd"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "CPU例外ハンドラー開始",
          "Arguments": {},
          "Color": "ff11f9e6"
        },
        "leave": {
          "DisplayName": "CPU例外ハンドラー終了",
          "Arguments": {},
          "Color": "ffcc6804"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff186fcc"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff2e12cc"
        }
      },
      "Color": "ffdb2030"
    },
    "TaskExceptionRoutine": {
      "DisplayName": "タスク例外ルーチン",
      "Attributes": {
        "prcIdX": {
          "DisplayName": "タスク例外ルーチンプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ff20f7b3"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff99d310"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff171bed"
        }
      },
      "Behaviors": {
        "enter": {
          "DisplayName": "タスク例外ルーチン開始",
          "Arguments": {},
          "Color": "ffd85120"
        },
        "leave": {
          "DisplayName": "タスク例外ルーチン終了",
          "Arguments": {},
          "Color": "ff2a46d3"
        },
        "enterSVC": {
          "DisplayName": "サービスコール開始",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff85cc28"
        },
        "leaveSVC": {
          "DisplayName": "サービスコール終了",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffea3317"
        }
      },
      "Color": "ffe01d3a"
    },
    "Task": {
      "DisplayName": "タスク",
      "Attributes": {
        "prcId": {
          "DisplayName": "タスクプロセッサID",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ff65fc07"
        },
        "id": {
          "DisplayName": "ID",
          "VariableType": "Number",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff2084f7"
        },
        "atr": {
          "DisplayName": "属性",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff1ff4c3"
        },
        "pri": {
          "DisplayName": "優先度",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": true,
          "Default": null,
          "Color": "ff2fd626"
        },
        "exinf": {
          "DisplayName": "拡張情報",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff2f08cc"
        },
        "task": {
          "DisplayName": "起動関数",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff88d129"
        },
        "stksz": {
          "DisplayName": "スタックサイズ",
          "VariableType": "Number",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": null,
          "Color": "ff6d04ed"
        },
        "stk": {
          "DisplayName": "スタック番地",
          "VariableType": "String",
          "AllocationType": "Static",
          "CanGrouping": false,
          "Default": null,
          "Color": "ffe02ad4"
        },
        "state": {
          "DisplayName": "状態",
          "VariableType": "String",
          "AllocationType": "Dynamic",
          "CanGrouping": false,
          "Default": "DORMANT",
          "Color": "ff1e9cfc"
        }
      },
      "Behaviors": {
        "preempt": {
          "DisplayName": "プリエンプト",
          "Arguments": {},
          "Color": "ff8f1df9"
        },
        "dispatch": {
          "DisplayName": "ディスパッチ",
          "Arguments": {},
          "Color": "ff0626dd"
        },
        "activate": {
          "DisplayName": "起動",
          "Arguments": {},
          "Color": "ffa80ece"
        },
        "wait": {
          "DisplayName": "待ち",
          "Arguments": {},
          "Color": "ff0b1eed"
        },
        "exit": {
          "DisplayName": "終了",
          "Arguments": {},
          "Color": "ffd30ef2"
        },
        "resume": {
          "DisplayName": "再開",
          "Arguments": {},
          "Color": "ff1dd144"
        },
        "suspend": {
          "DisplayName": "強制待ち",
          "Arguments": {},
          "Color": "ffd3301b"
        },
        "terminate": {
          "DisplayName": "強制終了",
          "Arguments": {},
          "Color": "ff02e041"
        },
        "releaseFromWaiting": {
          "DisplayName": "待ち解除",
          "Arguments": {},
          "Color": "ffd8db27"
        },
        "enterSVC": {
          "DisplayName": "サービスコールに入る",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ff2be5b0"
        },
        "leaveSVC": {
          "DisplayName": "サービスコールから出る",
          "Arguments": {
            "name": "String",
            "args": "String"
          },
          "Color": "ffd92ded"
        }
      },
      "Color": "ff09efc5"
    }
  },
  "Resources": {
    "CurrentContext_PRC1": {
      "DisplayName": "CurrentContext_PRC1",
      "Type": "Context",
      "Attributes": {
        "name": "None"
      },
      "Color": "ffe26404",
      "Visible": true
    },
    "CurrentContext_PRC2": {
      "DisplayName": "CurrentContext_PRC2",
      "Type": "Context",
      "Attributes": {
        "name": "None"
      },
      "Color": "ff0969ef",
      "Visible": true
    },
    "LOGTASK1": {
      "DisplayName": "LOGTASK1",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 1,
        "atr": "TA_ACT",
        "pri": 3,
        "exinf": "LOGTASK_PORTID_PRC1",
        "task": "logtask_main",
        "stksz": 1024,
        "stk": "NULL",
        "state": "RUNNABLE"
      },
      "Color": "ff24cc2a",
      "Visible": true
    },
    "LOGTASK2": {
      "DisplayName": "LOGTASK2",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 2,
        "atr": "TA_ACT",
        "pri": 3,
        "exinf": "LOGTASK_PORTID_PRC2",
        "task": "logtask_main",
        "stksz": 1024,
        "stk": "NULL",
        "state": "RUNNABLE"
      },
      "Color": "ff2add98",
      "Visible": true
    },
    "TASK1_1": {
      "DisplayName": "TASK1_1",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 3,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "0x10000|1",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "ffd127ba",
      "Visible": true
    },
    "TASK1_2": {
      "DisplayName": "TASK1_2",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 4,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "0x10000|2",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "ff3910ef",
      "Visible": true
    },
    "TASK1_3": {
      "DisplayName": "TASK1_3",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 5,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "0x10000|3",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "ff94f927",
      "Visible": true
    },
    "MAIN_TASK1": {
      "DisplayName": "MAIN_TASK1",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 6,
        "atr": "TA_ACT",
        "pri": 5,
        "exinf": "1",
        "task": "main_task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "RUNNABLE"
      },
      "Color": "ff02e820",
      "Visible": true
    },
    "SERVER_TASK1": {
      "DisplayName": "SERVER_TASK1",
      "Type": "Task",
      "Attributes": {
        "prcId": 1,
        "id": 7,
        "atr": "TA_ACT",
        "pri": 4,
        "exinf": "1",
        "task": "server_task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "RUNNABLE"
      },
      "Color": "ff1313d6",
      "Visible": true
    },
    "TASK2_1": {
      "DisplayName": "TASK2_1",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 8,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "0x20000|1",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "ff13badb",
      "Visible": true
    },
    "TASK2_2": {
      "DisplayName": "TASK2_2",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 9,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "0x20000|2",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "fffc0ff8",
      "Visible": true
    },
    "TASK2_3": {
      "DisplayName": "TASK2_3",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 10,
        "atr": "TA_NULL",
        "pri": 10,
        "exinf": "0x20000|3",
        "task": "task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "DORMANT"
      },
      "Color": "ff650adb",
      "Visible": true
    },
    "MAIN_TASK2": {
      "DisplayName": "MAIN_TASK2",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 11,
        "atr": "TA_ACT",
        "pri": 5,
        "exinf": "2",
        "task": "main_task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "RUNNABLE"
      },
      "Color": "ff10e52c",
      "Visible": true
    },
    "SERVER_TASK2": {
      "DisplayName": "SERVER_TASK2",
      "Type": "Task",
      "Attributes": {
        "prcId": 2,
        "id": 12,
        "atr": "TA_ACT",
        "pri": 4,
        "exinf": "2",
        "task": "server_task",
        "stksz": 4096,
        "stk": "NULL",
        "state": "RUNNABLE"
      },
      "Color": "ffcbd604",
      "Visible": true
    },
    "INH_INHNO_TIMER_PRC1_target_timer_handler": {
      "DisplayName": "INH_INHNO_TIMER_PRC1_target_timer_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "prcIdI": 1,
        "id": 5,
        "state": "DORMANT"
      },
      "Color": "fff75e22",
      "Visible": true
    },
    "INH_INHNO_TIMER_PRC2_target_timer_handler": {
      "DisplayName": "INH_INHNO_TIMER_PRC2_target_timer_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "prcIdI": 2,
        "id": 5,
        "state": "DORMANT"
      },
      "Color": "ffb7ce0a",
      "Visible": true
    },
    "INH_INHNO_SIO_PRC1_sio_handler": {
      "DisplayName": "INH_INHNO_SIO_PRC1_sio_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "prcIdI": 1,
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ffb014e0",
      "Visible": true
    },
    "INH_INHNO_SIO_PRC2_sio_handler": {
      "DisplayName": "INH_INHNO_SIO_PRC2_sio_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "prcIdI": 2,
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "fff90493",
      "Visible": true
    },
    "INH_INHNO_IPI_PRC1__kernel_ipi_handler": {
      "DisplayName": "INH_INHNO_IPI_PRC1__kernel_ipi_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "prcIdI": 1,
        "id": 16,
        "state": "DORMANT"
      },
      "Color": "ff08b7d6",
      "Visible": true
    },
    "INH_INHNO_IPI_PRC2__kernel_ipi_handler": {
      "DisplayName": "INH_INHNO_IPI_PRC2__kernel_ipi_handler",
      "Type": "InterruptHandler",
      "Attributes": {
        "prcIdI": 2,
        "id": 16,
        "state": "DORMANT"
      },
      "Color": "ff1409e8",
      "Visible": true
    },
    "CYCHDR1": {
      "DisplayName": "CYCHDR1",
      "Type": "CyclicHandler",
      "Attributes": {
        "prcIdC": 1,
        "id": 1,
        "state": "DORMANT"
      },
      "Color": "ff25a1ce",
      "Visible": true
    },
    "CYCHDR2": {
      "DisplayName": "CYCHDR2",
      "Type": "CyclicHandler",
      "Attributes": {
        "prcIdC": 2,
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ff04f257",
      "Visible": true
    },
    "ALMHDR1": {
      "DisplayName": "ALMHDR1",
      "Type": "AlarmHandler",
      "Attributes": {
        "prcIdA": 1,
        "id": 1,
        "state": "DORMANT"
      },
      "Color": "ffdb0d58",
      "Visible": true
    },
    "ALMHDR2": {
      "DisplayName": "ALMHDR2",
      "Type": "AlarmHandler",
      "Attributes": {
        "prcIdA": 2,
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ff78cc24",
      "Visible": true
    },
    "CPUEXC1": {
      "DisplayName": "CPUEXC1",
      "Type": "CPUExceptionHandler",
      "Attributes": {
        "prcIdE": 1,
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ff0c63f9",
      "Visible": true
    },
    "CPUEXC2": {
      "DisplayName": "CPUEXC2",
      "Type": "CPUExceptionHandler",
      "Attributes": {
        "prcIdE": 2,
        "id": 2,
        "state": "DORMANT"
      },
      "Color": "ffdd188e",
      "Visible": true
    },
    "TASK_TEX_PRC1": {
      "DisplayName": "TASK_TEX_PRC1",
      "Type": "TaskExceptionRoutine",
      "Attributes": {
        "prcIdX": 1,
        "state": "DORMANT",
        "id": null
      },
      "Color": "ff2983f2",
      "Visible": true
    },
    "TASK_TEX_PRC2": {
      "DisplayName": "TASK_TEX_PRC2",
      "Type": "TaskExceptionRoutine",
      "Attributes": {
        "prcIdX": 2,
        "state": "DORMANT",
        "id": null
      },
      "Color": "ff5ad806",
      "Visible": true
    }
  }
}