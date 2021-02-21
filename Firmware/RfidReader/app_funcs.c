#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"


/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_TAG_ID,
	&app_read_REG_RESERVED0,
	&app_read_REG_RESERVED1,
	&app_read_REG_NOTIFICATIONS,
	&app_read_REG_TRIGGER_NOTIFICATIONS,
	&app_read_REG_TIME_ON_BUZZER,
	&app_read_REG_TIME_ON_LED_TOP,
	&app_read_REG_TIME_ON_LED_BOTTOM,
	&app_read_REG_BUZZER_FREQUENCY,
	&app_read_REG_LED_TOP_BLINK_PERIOD,
	&app_read_REG_LED_BOTTOM_BLINK_PERIOD,
	&app_read_REG_RESERVED4,
	&app_read_REG_RESERVED5,
	&app_read_REG_TAG_MATCH0,
	&app_read_REG_TAG_MATCH1,
	&app_read_REG_TAG_MATCH2,
	&app_read_REG_TAG_MATCH3
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_TAG_ID,
	&app_write_REG_RESERVED0,
	&app_write_REG_RESERVED1,
	&app_write_REG_NOTIFICATIONS,
	&app_write_REG_TRIGGER_NOTIFICATIONS,
	&app_write_REG_TIME_ON_BUZZER,
	&app_write_REG_TIME_ON_LED_TOP,
	&app_write_REG_TIME_ON_LED_BOTTOM,
	&app_write_REG_BUZZER_FREQUENCY,
	&app_write_REG_LED_TOP_BLINK_PERIOD,
	&app_write_REG_LED_BOTTOM_BLINK_PERIOD,
	&app_write_REG_RESERVED4,
	&app_write_REG_RESERVED5,
	&app_write_REG_TAG_MATCH0,
	&app_write_REG_TAG_MATCH1,
	&app_write_REG_TAG_MATCH2,
	&app_write_REG_TAG_MATCH3
};


/************************************************************************/
/* REG_TAG_ID                                                           */
/************************************************************************/
void app_read_REG_TAG_ID(void) {}
bool app_write_REG_TAG_ID(void *a) {return false;}


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void) {}
bool app_write_REG_RESERVED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED0 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void) {}
bool app_write_REG_RESERVED1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED1 = reg;
	return true;
}


/************************************************************************/
/* REG_NOTIFICATIONS                                                    */
/************************************************************************/
void app_read_REG_NOTIFICATIONS(void) {}
bool app_write_REG_NOTIFICATIONS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_NOTIFICATIONS = reg;
	return true;
}


/************************************************************************/
/* REG_TRIGGER_NOTIFICATIONS                                            */
/************************************************************************/
extern void notify(uint8_t notify_mask);

void app_read_REG_TRIGGER_NOTIFICATIONS(void) {}
bool app_write_REG_TRIGGER_NOTIFICATIONS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	notify(reg);	

	app_regs.REG_TRIGGER_NOTIFICATIONS = reg;
	return true;
}


/************************************************************************/
/* REG_TIME_ON_BUZZER                                                   */
/************************************************************************/
void app_read_REG_TIME_ON_BUZZER(void) {}
bool app_write_REG_TIME_ON_BUZZER(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_TIME_ON_BUZZER = reg;
	return true;
}


/************************************************************************/
/* REG_TIME_ON_LED_TOP                                                  */
/************************************************************************/
void app_read_REG_TIME_ON_LED_TOP(void) {}
bool app_write_REG_TIME_ON_LED_TOP(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_TIME_ON_LED_TOP = reg;
	return true;
}


/************************************************************************/
/* REG_TIME_ON_LED_BOTTOM                                               */
/************************************************************************/
void app_read_REG_TIME_ON_LED_BOTTOM(void) {}
bool app_write_REG_TIME_ON_LED_BOTTOM(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_TIME_ON_LED_BOTTOM = reg;
	return true;
}


/************************************************************************/
/* REG_BUZZER_FREQUENCY                                                 */
/************************************************************************/
uint8_t buzzer_prescaler;
uint16_t buzzer_target_count;

void app_read_REG_BUZZER_FREQUENCY(void) {}
bool app_write_REG_BUZZER_FREQUENCY(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
	if (reg > 15000) return false;
	if (reg < 200) return false;
	
	calculate_timer_16bits(32000000, reg, &buzzer_prescaler, &buzzer_target_count);	

	app_regs.REG_BUZZER_FREQUENCY = reg;
	return true;
}


/************************************************************************/
/* REG_LED_TOP_BLINK_PERIOD                                             */
/************************************************************************/
void app_read_REG_LED_TOP_BLINK_PERIOD(void) {}
bool app_write_REG_LED_TOP_BLINK_PERIOD(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
	if (reg < 2)
		return false;
	
	app_regs.REG_LED_TOP_BLINK_PERIOD = reg;
	return true;
}


/************************************************************************/
/* REG_LED_BOTTOM_BLINK_PERIOD                                          */
/************************************************************************/
void app_read_REG_LED_BOTTOM_BLINK_PERIOD(void) {}
bool app_write_REG_LED_BOTTOM_BLINK_PERIOD(void *a)
{
	uint16_t reg = *((uint16_t*)a);
	
	if (reg < 2)
		return false;
	
	app_regs.REG_LED_BOTTOM_BLINK_PERIOD = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED4                                                        */
/************************************************************************/
void app_read_REG_RESERVED4(void) {}
bool app_write_REG_RESERVED4(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED4 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void) {}
bool app_write_REG_RESERVED5(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED5 = reg;
	return true;
}


/************************************************************************/
/* REG_TAG_MATCH0                                                       */
/************************************************************************/
void app_read_REG_TAG_MATCH0(void) {}
bool app_write_REG_TAG_MATCH0(void *a)
{
	uint64_t reg = *((uint64_t*)a);

	app_regs.REG_TAG_MATCH0 = reg;
	return true;
}


/************************************************************************/
/* REG_TAG_MATCH1                                                       */
/************************************************************************/
void app_read_REG_TAG_MATCH1(void) {}
bool app_write_REG_TAG_MATCH1(void *a)
{
	uint64_t reg = *((uint64_t*)a);

	app_regs.REG_TAG_MATCH1 = reg;
	return true;
}


/************************************************************************/
/* REG_TAG_MATCH2                                                       */
/************************************************************************/
void app_read_REG_TAG_MATCH2(void) {}
bool app_write_REG_TAG_MATCH2(void *a)
{
	uint64_t reg = *((uint64_t*)a);

	app_regs.REG_TAG_MATCH2 = reg;
	return true;
}


/************************************************************************/
/* REG_TAG_MATCH3                                                       */
/************************************************************************/
void app_read_REG_TAG_MATCH3(void) {}
bool app_write_REG_TAG_MATCH3(void *a)
{
	uint64_t reg = *((uint64_t*)a);

	app_regs.REG_TAG_MATCH3 = reg;
	return true;
}