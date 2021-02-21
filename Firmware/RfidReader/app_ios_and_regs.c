#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTA, 4, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // CARD_PRESENT
	io_pin2in(&PORTC, 3, PULL_IO_TRISTATE, SENSE_IO_EDGES_BOTH);         // TAG_IN_RANGE

	/* Configure input interrupts */
	io_set_int(&PORTA, INT_LEVEL_LOW, 0, (1<<4), false);                 // CARD_PRESENT
	io_set_int(&PORTC, INT_LEVEL_LOW, 0, (1<<3), false);                 // TAG_IN_RANGE

	/* Configure output pins */
	io_pin2out(&PORTD, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // BUZZER
	io_pin2out(&PORTD, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_DETECT_TOP
	io_pin2out(&PORTC, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // LED_DETECT_BOTTOM

	/* Initialize output pins */
	clr_BUZZER;
	clr_LED_DETECT_TOP;
	clr_LED_DETECT_BOTTOM;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U64,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U16,
	TYPE_U8,
	TYPE_U8,
	TYPE_U64,
	TYPE_U64,
	TYPE_U64,
	TYPE_U64
};

uint16_t app_regs_n_elements[] = {
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_TAG_ID),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_RESERVED1),
	(uint8_t*)(&app_regs.REG_NOTIFICATIONS),
	(uint8_t*)(&app_regs.REG_TRIGGER_NOTIFICATIONS),
	(uint8_t*)(&app_regs.REG_TIME_ON_BUZZER),
	(uint8_t*)(&app_regs.REG_TIME_ON_LED_TOP),
	(uint8_t*)(&app_regs.REG_TIME_ON_LED_BOTTOM),
	(uint8_t*)(&app_regs.REG_BUZZER_FREQUENCY),
	(uint8_t*)(&app_regs.REG_LED_TOP_BLINK_PERIOD),
	(uint8_t*)(&app_regs.REG_LED_BOTTOM_BLINK_PERIOD),
	(uint8_t*)(&app_regs.REG_RESERVED4),
	(uint8_t*)(&app_regs.REG_RESERVED5),
	(uint8_t*)(&app_regs.REG_TAG_MATCH0),
	(uint8_t*)(&app_regs.REG_TAG_MATCH1),
	(uint8_t*)(&app_regs.REG_TAG_MATCH2),
	(uint8_t*)(&app_regs.REG_TAG_MATCH3)
};