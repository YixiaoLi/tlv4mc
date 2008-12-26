{
	"TimeScale"		:"ns",
	"TimeRadix"     :10,
	"ConvertRules"	:["asp"],
	"VisualizeRules":["asp"],
	"ResourceHeaders":["asp"],
	"Resources":
	{
		"LOGTASK":{
			"DisplayName":"ログタスク",
			"Type"	:"Task",
			"Color" :"ff0000",
			"Attributes":
			{
				"prcId"	:1,
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
			"Color" :"00ff00",
			"Attributes":
			{
				"prcId"	:1,
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
			"Color" :"ff00ff",
			"Attributes":
			{
				"prcId"	:2,
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
			"Color" :"00ffff",
			"Attributes":
			{
				"prcId"	:2,
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
			"Color" :"0000ff",
			"Attributes":
			{
				"prcId"	:1,
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
