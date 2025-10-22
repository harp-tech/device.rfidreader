#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#include "uart0.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);

/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "RfidReader";

void hwbp_app_initialize(void)
{
    /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 2;
    uint8_t fwH = 1;
    uint8_t fwL = 4;
    uint8_t ass = 0;
    
   	/* Start core */
    core_func_start_core(
        2094,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name,
        false,	// The device is not able to repeat the harp timestamp clock
        false,	// The device is not able to generate the harp timestamp clock
        3		// Default timestamp offset
    );
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	timer_type0_stop(&TCC0);
	
	clr_BUZZER;
	clr_LED_DETECT_TOP;
	clr_LED_DETECT_BOTTOM;
}

/************************************************************************/
/* User functions                                                       */
/************************************************************************/
/* Add your functions here or load external functions if needed */

/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_define_clock_default(void)
{
	/* Device don't have clock input or output */
}

void core_callback_initialize_hardware(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
	
	/* Initialize hardware */
	uart0_init(12, 4, false);   // 1 Mb/s
	uart0_enable();
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */
	app_regs.REG_NOTIFICATIONS = B_BUZZER | B_TOP_LED | B_BOTTOM_LED;
	app_regs.REG_TIME_ON_BUZZER = 500;				// 500 ms second
	app_regs.REG_TIME_ON_LED_TOP = 500;				// 500 ms second
	app_regs.REG_TIME_ON_LED_BOTTOM = 500;			// 500 ms second
	app_regs.REG_BUZZER_FREQUENCY = 1000;			// 1 KHz
	app_regs.REG_LED_TOP_BLINK_PERIOD = 100;		// 100 ms
	app_regs.REG_LED_BOTTOM_BLINK_PERIOD = 100;	// 100 ms
	app_regs.REG_TAG_MATCH0 = 0;
	app_regs.REG_TAG_MATCH1 = 0;
	app_regs.REG_TAG_MATCH2 = 0;
	app_regs.REG_TAG_MATCH3 = 0;
	app_regs.REG_TAG_MATCH0_OUT0_PERIOD = 0;
	app_regs.REG_TAG_MATCH1_OUT0_PERIOD = 0;
	app_regs.REG_TAG_MATCH2_OUT0_PERIOD = 0;
	app_regs.REG_TAG_MATCH3_OUT0_PERIOD = 0;
	app_regs.REG_TAG_ID_ARRIVED_PERIOD = 0;
}

void core_callback_registers_were_reinitialized(void)
{
	/* Update registers if needed */
	app_write_REG_BUZZER_FREQUENCY(&app_regs.REG_BUZZER_FREQUENCY);
	
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	/* Update visual indicators */
	
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
	clr_LED_DETECT_TOP;
	clr_LED_DETECT_BOTTOM;
	clr_LED_OUT0;
}

/************************************************************************/
/* Serial RX                                                            */
/************************************************************************/
uint8_t rxbuff_pointer = 0;
extern uint8_t rxbuff_uart0[];

uint16_t out0_timeout_ms = 0;

/*
[02]000C845A994B
[03][02]000C845A994B
[03][02]0077D1AD000B
[03][02]000C845A77A5
[03][02]021000001705
[03][02]000C845A7EAC
[03]

[02]000C845A79AB
[03][02]000C845A79AB
[03]

STX (02h) DATA (10 ASCII) CHECK SUM (2 ASCII) CR LF ETX (03h)

*/

uint16_t buzzer_time_on = 0;
uint16_t top_led_time_on = 0;
uint16_t bottom_led_time_on = 0;
uint16_t top_led_period = 0;
uint16_t bottom_led_period = 0;

bool stop_buzzer;

extern uint8_t buzzer_prescaler;
extern uint16_t buzzer_target_count;

bool id_event_was_sent = false;

void notify(uint8_t notify_mask)
{
	if ((notify_mask & B_BUZZER) && (app_regs.REG_TIME_ON_BUZZER > 1))
	{
		// Replace with timer TCD0 on several places -- do a search
		timer_type0_pwm(&TCC0, buzzer_prescaler, buzzer_target_count, buzzer_target_count>>1, INT_LEVEL_LOW, INT_LEVEL_LOW);
		buzzer_time_on = app_regs.REG_TIME_ON_BUZZER;
		stop_buzzer = false;
	}
	
	if (core_bool_is_visual_enabled())
	{
		if ((notify_mask & B_TOP_LED) && (app_regs.REG_TIME_ON_LED_TOP > 1))
		{
			set_LED_DETECT_TOP;
			top_led_time_on = app_regs.REG_TIME_ON_LED_TOP;
			top_led_period = app_regs.REG_LED_TOP_BLINK_PERIOD >> 1;		// Divide by 2
		}
	
		if ((notify_mask & B_BOTTOM_LED) && (app_regs.REG_TIME_ON_LED_BOTTOM > 1))
		{
			set_LED_DETECT_BOTTOM;
			bottom_led_time_on = app_regs.REG_TIME_ON_LED_BOTTOM;
			bottom_led_period = app_regs.REG_LED_BOTTOM_BLINK_PERIOD >> 1;	// Divide by 2
		}
	}
}

void uart0_rcv_byte_callback(uint8_t byte_received)
{
	if (rxbuff_pointer == 0)
	{
		timer_type1_enable(&TCD1, TIMER_PRESCALER_DIV1024, 156, INT_LEVEL_LOW);		// ~5 ms
																											// 1 byte = 833 us @ 9600bps
		id_event_was_sent = false;
		app_regs.REG_TAG_ID_ARRIVED = 0;
	}
	
	rxbuff_uart0[rxbuff_pointer++] = byte_received;
	TCD1_CNT = 0;
}



/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void) {}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
void core_callback_t_before_exec(void) {}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) {}
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void)
{
	if (out0_timeout_ms)
	{
		if (!read_OUT0)
		{
			out0_timeout_ms++;
			
			set_OUT0;
			if (core_bool_is_visual_enabled())
			set_LED_OUT0;
			
			app_regs.REG_OUT = B_OUT0;
			core_func_send_event(ADD_REG_OUT, true);
		}
		
		out0_timeout_ms--;
		
		if (out0_timeout_ms == 0)
		{
			if (read_OUT0)
			{
				clr_OUT0;
				clr_LED_OUT0;
				
				app_regs.REG_OUT = 0;
				core_func_send_event(ADD_REG_OUT, true);
			}
		}
	}
	
	if (buzzer_time_on)
	{
		if (--buzzer_time_on == 0)
		{
			stop_buzzer = true;
		}
	}
	
	if (top_led_time_on)
	{
		if (--top_led_period == 0)
		{
			tgl_LED_DETECT_TOP;
			top_led_period = app_regs.REG_LED_TOP_BLINK_PERIOD >> 1;		// Divide by 2
		}
		
		if (--top_led_time_on == 0)
		{
			clr_LED_DETECT_TOP;
		}
	}

	if (bottom_led_time_on)
	{
		if (--bottom_led_period == 0)
		{
			tgl_LED_DETECT_BOTTOM;
			bottom_led_period = app_regs.REG_LED_BOTTOM_BLINK_PERIOD >> 1;	// Divide by 2
		}
		
		if (--bottom_led_time_on == 0)
		{
			clr_LED_DETECT_BOTTOM;
		}
	}
}

/************************************************************************/
/* Callbacks: clock control                                             */
/************************************************************************/
void core_callback_clock_to_repeater(void) {}
void core_callback_clock_to_generator(void) {}
void core_callback_clock_to_unlock(void) {}
void core_callback_clock_to_lock(void) {}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}