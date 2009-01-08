{
	"TimeScale" :"us",
	"TimeRadix" :10,
	"ConvertRules"   :["asp","fmp"],
	"VisualizeRules" :["asp","fmp"],
	"ResourceHeaders":["fmp"],
	"Resources":
	{
		"TASK1":{
			"Type":"Task",
			"Attributes":
			{
				"prcId" :1,
				"id"    :1,
				"atr"   :"TA_NULL",
				"pri"   :10,
				"exinf" :1,
				"task"  :"task,"
				"stksz" :4096,
				"stk"   :"NULL",
				"state" :"DORMANT"
			}
		},
		"TASK2":{
			"Type":"Task",
			"Attributes":
			{
				"prcId" :1,
				"id"    :2,
				"atr"   :"TA_NULL",
				"pri"   :10,
				"exinf" :2,
				"task"  :"task,"
				"stksz" :4096,
				"stk"   :"NULL",
				"state" :"DORMANT"
			}
		},
		"TASK3":{
			"Type":"Task",
			"Attributes":
			{
				"prcId" :1,
				"id"    :3,
				"atr"   :"TA_NULL",
				"pri"   :10,
				"exinf" :3,
				"task"  :"task,"
				"stksz" :4096,
				"stk"   :"NULL",
				"state" :"DORMANT"
			}
		},
		"MAIN_TASK":{
			"Type":"Task",
			"Attributes":
			{
				"prcId" :1,
				"id"    :4,
				"atr"   :"TA_ACT",
				"pri"   :5,
				"exinf" :0,
				"task"  :"main_task,"
				"stksz" :4096,
				"stk"   :"NULL",
				"state" :"RUNABLE"
			}
		}
	}
}

