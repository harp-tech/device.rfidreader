#ifndef _APP_IOS_AND_REGS_H_
#define _APP_IOS_AND_REGS_H_
#include "cpu.h"

void init_ios(void);
/************************************************************************/
/* Definition of input pins                                             */
/************************************************************************/
// TAG_IN_RANGE           Description: Tag in range

#define read_TAG_IN_RANGE read_io(PORTC, 3)     // TAG_IN_RANGE

/************************************************************************/
/* Definition of output pins                                            */
/************************************************************************/
// BUZZER                 Description: Buzzer control
// LED_DETECT_TOP         Description: Detect notification LED on top
// LED_DETECT_BOTTOM      Description: Detect notification LED on bottom
// OUT0                   Description: Digital output OUT0
// LED_OUT0               Description: Notification of digital output OUT0

/* BUZZER */
#define set_BUZZER set_io(PORTD, 0)
#define clr_BUZZER clear_io(PORTD, 0)
#define tgl_BUZZER toggle_io(PORTD, 0)
#define read_BUZZER read_io(PORTD, 0)

/* LED_DETECT_TOP */
#define set_LED_DETECT_TOP set_io(PORTD, 4)
#define clr_LED_DETECT_TOP clear_io(PORTD, 4)
#define tgl_LED_DETECT_TOP toggle_io(PORTD, 4)
#define read_LED_DETECT_TOP read_io(PORTD, 4)

/* LED_DETECT_BOTTOM */
#define set_LED_DETECT_BOTTOM set_io(PORTC, 1)
#define clr_LED_DETECT_BOTTOM clear_io(PORTC, 1)
#define tgl_LED_DETECT_BOTTOM toggle_io(PORTC, 1)
#define read_LED_DETECT_BOTTOM read_io(PORTC, 1)

/* OUT0 */
#define set_OUT0 set_io(PORTD, 7)
#define clr_OUT0 clear_io(PORTD, 7)
#define tgl_OUT0 toggle_io(PORTD, 7)
#define read_OUT0 read_io(PORTD, 7)

/* LED_OUT0 */
#define set_LED_OUT0 set_io(PORTD, 6)
#define clr_LED_OUT0 clear_io(PORTD, 6)
#define tgl_LED_OUT0 toggle_io(PORTD, 6)
#define read_LED_OUT0 read_io(PORTD, 6)


/************************************************************************/
/* Registers' structure                                                 */
/************************************************************************/
typedef struct
{
	uint64_t REG_TAG_ID_ARRIVED;
	uint64_t REG_TAG_ID_LEAVED;
	uint8_t REG_OUT;
	uint8_t REG_NOTIFICATIONS;
	uint8_t REG_TRIGGER_NOTIFICATIONS;
	uint16_t REG_TIME_ON_BUZZER;
	uint16_t REG_TIME_ON_LED_TOP;
	uint16_t REG_TIME_ON_LED_BOTTOM;
	uint16_t REG_BUZZER_FREQUENCY;
	uint16_t REG_LED_TOP_BLINK_PERIOD;
	uint16_t REG_LED_BOTTOM_BLINK_PERIOD;
	uint8_t REG_RESERVED1;
	uint8_t REG_RESERVED2;
	uint64_t REG_TAG_MATCH0;
	uint64_t REG_TAG_MATCH1;
	uint64_t REG_TAG_MATCH2;
	uint64_t REG_TAG_MATCH3;
	uint16_t REG_TAG_MATCH0_OUT0_PERIOD;
	uint16_t REG_TAG_MATCH1_OUT0_PERIOD;
	uint16_t REG_TAG_MATCH2_OUT0_PERIOD;
	uint16_t REG_TAG_MATCH3_OUT0_PERIOD;
	uint16_t REG_TAG_ID_ARRIVED_PERIOD;
	uint16_t REG_OUT0_PERIOD;
} AppRegs;

/************************************************************************/
/* Registers' address                                                   */
/************************************************************************/
/* Registers */
#define ADD_REG_TAG_ID_ARRIVED              32 // U64    Tag unique number was detected
#define ADD_REG_TAG_ID_LEAVED               33 // U64    Tag unique number left the antenna coverage area
#define ADD_REG_OUT                         34 // U8     Writes to digital outputs
#define ADD_REG_NOTIFICATIONS               35 // U8     Enable the available notifications
#define ADD_REG_TRIGGER_NOTIFICATIONS       36 // U8     Trigger the correspondent notifications
#define ADD_REG_TIME_ON_BUZZER              37 // U16    Time the buzzer will be ON in milliseconds (minimum is 2) (sensitive to multiples of 2)
#define ADD_REG_TIME_ON_LED_TOP             38 // U16    Time the top LED will be ON in milliseconds (minimum is 2) (sensitive to multiples of 2)
#define ADD_REG_TIME_ON_LED_BOTTOM          39 // U16    Time the bottom LED will be ON in milliseconds (minimum is 2) (sensitive to multiples of 2)
#define ADD_REG_BUZZER_FREQUENCY            40 // U16    Frequency of buzzer's notification (between 200 and 15000)
#define ADD_REG_LED_TOP_BLINK_PERIOD        41 // U16    Blink period of top LED in milliseconds (minimum is 2) (sensitive to multiples of 2)
#define ADD_REG_LED_BOTTOM_BLINK_PERIOD     42 // U16    Blink period of bottom LED in milliseconds (minimum is 2) (sensitive to multiples of 2)
#define ADD_REG_RESERVED1                   43 // U8     Reserved
#define ADD_REG_RESERVED2                   44 // U8     Reserved
#define ADD_REG_TAG_MATCH0                  45 // U64    Notifies and sends TAG_ID event if the readed tag matches. Equal to 0 if not used.
#define ADD_REG_TAG_MATCH1                  46 // U64    Notifies and sends TAG_ID event if the readed tag matches. Equal to 0 if not used.
#define ADD_REG_TAG_MATCH2                  47 // U64    Notifies and sends TAG_ID event if the readed tag matches. Equal to 0 if not used.
#define ADD_REG_TAG_MATCH3                  48 // U64    Notifies and sends TAG_ID event if the readed tag matches. Equal to 0 if not used.
#define ADD_REG_TAG_MATCH0_OUT0_PERIOD      49 // U16    Defines the amount of time in ms that the digital output OUT0 will be at logic high when TAG_ID0 is detected
#define ADD_REG_TAG_MATCH1_OUT0_PERIOD      50 // U16    Defines the amount of time in ms that the digital output OUT0 will be at logic high when TAG_ID1 is detected
#define ADD_REG_TAG_MATCH2_OUT0_PERIOD      51 // U16    Defines the amount of time in ms that the digital output OUT0 will be at logic high when TAG_ID2 is detected
#define ADD_REG_TAG_MATCH3_OUT0_PERIOD      52 // U16    Defines the amount of time in ms that the digital output OUT0 will be at logic high when TAG_ID3 is detected
#define ADD_REG_TAG_ID_ARRIVED_PERIOD       53 // U16    When a tag is detected, OUT0 will be at high level for this amount of time in ms
#define ADD_REG_OUT0_PERIOD                 54 // U16    When writing to this register, the OUT0 will be on for this amount of time in ms

/************************************************************************/
/* PWM Generator registers' memory limits                               */
/*                                                                      */
/* DON'T change the APP_REGS_ADD_MIN value !!!                          */
/* DON'T change these names !!!                                         */
/************************************************************************/
/* Memory limits */
#define APP_REGS_ADD_MIN                    0x20
#define APP_REGS_ADD_MAX                    0x36
#define APP_NBYTES_OF_REG_BANK              77

/************************************************************************/
/* Registers' bits                                                      */
/************************************************************************/
#define B_OUT0                             (1<<0)       // Digital output 0
#define B_BUZZER                           (1<<0)       // Enables notification on buzzer
#define B_TOP_LED                          (1<<0)       // Enables notification on top's LED
#define B_BOTTOM_LED                       (1<<0)       // Enables notification on bottom's LED
#define B_TRIG_BUZZER                      (1<<0)       // Enables notification on buzzer
#define B_TRIG_TOP_LED                     (1<<0)       // Enables notification on top's LED
#define B_TRIG_BOTTOM_LED                  (1<<0)       // Enables notification on bottom's LED

#endif /* _APP_REGS_H_ */