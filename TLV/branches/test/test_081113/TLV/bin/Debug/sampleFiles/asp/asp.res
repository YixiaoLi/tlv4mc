﻿{
	"TimeScale"		:"ns",
	"ConvertRule"	:"asp",
	"ResourceHeader":"asp",
	"Resources":
	{
		"LOGTASK":{
			"Type"	:"Task",
			"Attributes":
			{
				"id"	:1,
				"atr"	:"TA_ACT",
				"pri"	:3,
				"exinf"	:"LOGTASK_PORTID",
				"task"	:"logtask_main",
				"stksz"	:4096,
				"stk"	:"NULL"
			}
		},
		"TASK1":{
			"Type"	:"Task",
			"Attributes":
			{
				"id"	:2,
				"atr"	:"TA_NULL",
				"pri"	:10,
				"exinf"	:1,
				"task"	:"task",
				"stksz"	:4096,
				"stk"	:"NULL"
			}
		},
		"TASK2":{
			"Type"	:"Task",
			"Attributes":
			{
				"id"	:3,
				"atr"	:"TA_NULL",
				"pri"	:10,
				"exinf"	:2,
				"task"	:"task",
				"stksz"	:4096,
				"stk"	:"NULL"
			}
		},
		"TASK3":{
			"Type"	:"Task",
			"Attributes":
			{
				"id"	:4,
				"atr"	:"TA_NULL",
				"pri"	:10,
				"exinf"	:3,
				"task"	:"task",
				"stksz"	:4096,
				"stk"	:"NULL"
			}
		},
		"MAIN_TASK":{
			"Type"	:"Task",
			"Attributes":
			{
				"id"	:5,
				"atr"	:"TA_ACT",
				"pri"	:5,
				"exinf"	:0,
				"task"	:"main_task",
				"stksz"	:4096,
				"stk"	:"NULL"
			}
		}
	}
}