#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_TAG_ID(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_RESERVED1(void);
void app_read_REG_NOTIFICATIONS(void);
void app_read_REG_TRIGGER_NOTIFICATIONS(void);
void app_read_REG_TIME_ON_BUZZER(void);
void app_read_REG_TIME_ON_LED_TOP(void);
void app_read_REG_TIME_ON_LED_BOTTOM(void);
void app_read_REG_BUZZER_FREQUENCY(void);
void app_read_REG_LED_TOP_BLINK_PERIOD(void);
void app_read_REG_LED_BOTTOM_BLINK_PERIOD(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_RESERVED5(void);
void app_read_REG_TAG_MATCH0(void);
void app_read_REG_TAG_MATCH1(void);
void app_read_REG_TAG_MATCH2(void);
void app_read_REG_TAG_MATCH3(void);

bool app_write_REG_TAG_ID(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_RESERVED1(void *a);
bool app_write_REG_NOTIFICATIONS(void *a);
bool app_write_REG_TRIGGER_NOTIFICATIONS(void *a);
bool app_write_REG_TIME_ON_BUZZER(void *a);
bool app_write_REG_TIME_ON_LED_TOP(void *a);
bool app_write_REG_TIME_ON_LED_BOTTOM(void *a);
bool app_write_REG_BUZZER_FREQUENCY(void *a);
bool app_write_REG_LED_TOP_BLINK_PERIOD(void *a);
bool app_write_REG_LED_BOTTOM_BLINK_PERIOD(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_RESERVED5(void *a);
bool app_write_REG_TAG_MATCH0(void *a);
bool app_write_REG_TAG_MATCH1(void *a);
bool app_write_REG_TAG_MATCH2(void *a);
bool app_write_REG_TAG_MATCH3(void *a);


#endif /* _APP_FUNCTIONS_H_ */