#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)

/************************************************************************/ 
/* CARD_PRESENT                                                         */
/************************************************************************/
ISR(PORTA_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* TAG_IN_RANGE                                                         */
/************************************************************************/
extern bool id_event_was_sent;

ISR(PORTC_INT0_vect, ISR_NAKED)
{
	if (read_TAG_IN_RANGE)
	{
		core_func_mark_user_timestamp();
	}
	else
	{
		if (id_event_was_sent)
		{
			id_event_was_sent = false;
			
			app_regs.REG_TAG_ID_LEAVED = app_regs.REG_TAG_ID_ARRIVED;
			core_func_send_event(ADD_REG_TAG_ID_LEAVED, true);
		}
	}	
	
	reti();
}

/************************************************************************/
/* UART0                                                                */
/************************************************************************/
extern uint8_t rxbuff_pointer;
extern uint8_t rxbuff_uart0[];

extern uint16_t out0_timeout_ms;

extern void notify(uint8_t notify_mask);

uint8_t reverse_byte(uint8_t num)
{
    uint8_t NO_OF_BITS = 8;
    uint8_t reverse_num = 0;
    uint8_t i;
	 
    for (i = 0; i < NO_OF_BITS; i++)
	 {
        if ((num & (1 << i)))
		  {
            reverse_num |= 1 << ((NO_OF_BITS - 1) - i);
		  }
    }
    return reverse_num;
}

ISR(TCD1_OVF_vect, ISR_NAKED)
{
	/* Stop timer */
	timer_type1_stop(&TCD1);
	
	/* 16 payload bytes
	
	      * Card format: 125 kHz nominal carrier (EM 4001 or compatible)
	      * Payload: 10 ASCII (5 bytes)
	      * Checksum: 2 ASCII (1 byte)
			
	   30 payload bytes
		
	      * Card format: ISO11785
	      * Payload: 16 ASCII (8 bytes)
	      * Checksum: 4 ASCII (2 byte)
	      * Extension bits: 6 ASCII (3 bytes)
	*/
	
	if (rxbuff_pointer == 16 || rxbuff_pointer == 30)
	{
		/* Check message start */
		if (rxbuff_uart0[0] != 0x02) return;	// STX
		
		/* Check message termination */
		if (rxbuff_uart0[rxbuff_pointer - 3] != 0x0D) return;	// CR
		if (rxbuff_uart0[rxbuff_pointer - 2] != 0x0A) return;	// LF
		if (rxbuff_uart0[rxbuff_pointer - 1] != 0x03) return;	// ETX
		
		/* Convert from ASCII */
		for (uint8_t i = 1; i < rxbuff_pointer - 4 + 1; i++)
		{
			if (rxbuff_uart0[i] <= 57)
				rxbuff_uart0[i] = rxbuff_uart0[i] - 48;
			else
				rxbuff_uart0[i] = rxbuff_uart0[i] - 65 + 10;
		}
		for (uint8_t i = 0; i < (rxbuff_pointer - 4 + 1) / 2; i++)
		{
			rxbuff_uart0[i] = (rxbuff_uart0[i*2+1] << 4) + rxbuff_uart0[i*2+2];
		}
		
		/* Confirm checksum */
		if (rxbuff_pointer == 16)
		{
			uint8_t checksum = rxbuff_uart0[0] ^ rxbuff_uart0[1] ^ rxbuff_uart0[2] ^ rxbuff_uart0[3] ^ rxbuff_uart0[4];
			
			if (checksum != rxbuff_uart0[5])
			{
					rxbuff_pointer = 0;	
					reti();
			}
		}
		else
		{
			// Checksum confirmation for ISO11785 not implemented yet
		}
		
		/* Convert tag ID to the 64 bits register */
		if (rxbuff_pointer == 16)
		{
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+0) = rxbuff_uart0[4];
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+1) = rxbuff_uart0[3];
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+2) = rxbuff_uart0[2];
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+3) = rxbuff_uart0[1];
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+4) = rxbuff_uart0[0];
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+5) = 0;
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+6) = 0;
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+7) = 0;
		}
		else
		{
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+0) = reverse_byte(rxbuff_uart0[0]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+1) = reverse_byte(rxbuff_uart0[1]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+2) = reverse_byte(rxbuff_uart0[2]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+3) = reverse_byte(rxbuff_uart0[3]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+4) = reverse_byte(rxbuff_uart0[4]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+5) = reverse_byte(rxbuff_uart0[5]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+6) = reverse_byte(rxbuff_uart0[6]);
			*(((uint8_t*)(&app_regs.REG_TAG_ID_ARRIVED))+7) = reverse_byte(rxbuff_uart0[7]);
			
			uint64_t id           = (app_regs.REG_TAG_ID_ARRIVED & 0x3FFFFFFFFF);
 			uint64_t country_code = (app_regs.REG_TAG_ID_ARRIVED & 0xFFC000000000) >> 38;
			 
			app_regs.REG_TAG_ID_ARRIVED = country_code * 1000000000000 + id;
		}
		
		/* Check for matching */
		if ((app_regs.REG_TAG_MATCH0 != 0) || (app_regs.REG_TAG_MATCH1 != 0) || (app_regs.REG_TAG_MATCH2 != 0) || (app_regs.REG_TAG_MATCH3 != 0 ))
		{
			if (app_regs.REG_TAG_ID_ARRIVED == app_regs.REG_TAG_MATCH0)
			{
				core_func_send_event(ADD_REG_TAG_ID_ARRIVED, (rxbuff_pointer == 16) ? false : true);
				id_event_was_sent = true;
				notify(app_regs.REG_NOTIFICATIONS);
				out0_timeout_ms = app_regs.REG_TAG_MATCH0_OUT0_PERIOD;
				return;				
			}
			if (app_regs.REG_TAG_ID_ARRIVED == app_regs.REG_TAG_MATCH1)
			{
				core_func_send_event(ADD_REG_TAG_ID_ARRIVED, (rxbuff_pointer == 16) ? false : true);
				id_event_was_sent = true;
				notify(app_regs.REG_NOTIFICATIONS);
				out0_timeout_ms = app_regs.REG_TAG_MATCH1_OUT0_PERIOD;
				return;
			}
			if (app_regs.REG_TAG_ID_ARRIVED == app_regs.REG_TAG_MATCH2)
			{
				core_func_send_event(ADD_REG_TAG_ID_ARRIVED, (rxbuff_pointer == 16) ? false : true);
				id_event_was_sent = true;
				notify(app_regs.REG_NOTIFICATIONS);
				out0_timeout_ms = app_regs.REG_TAG_MATCH2_OUT0_PERIOD;
				return;
			}
			if (app_regs.REG_TAG_ID_ARRIVED == app_regs.REG_TAG_MATCH3)
			{
				core_func_send_event(ADD_REG_TAG_ID_ARRIVED, (rxbuff_pointer == 16) ? false : true);
				id_event_was_sent = true;
				notify(app_regs.REG_NOTIFICATIONS);
				out0_timeout_ms = app_regs.REG_TAG_MATCH3_OUT0_PERIOD;
				return;
			}
		}
		else
		{
			core_func_send_event(ADD_REG_TAG_ID_ARRIVED, (rxbuff_pointer == 16) ? false : true);
			id_event_was_sent = true;
			out0_timeout_ms = app_regs.REG_TAG_ID_ARRIVED_PERIOD;
			notify(app_regs.REG_NOTIFICATIONS);
		}
	}
	
	rxbuff_pointer = 0;	
	reti();
}

/************************************************************************/
/* Buzzer                                                               */
/************************************************************************/
extern bool stop_buzzer;
ISR(TCC0_OVF_vect, ISR_NAKED)
{
	set_BUZZER;
	
	reti();
}

ISR(TCC0_CCA_vect, ISR_NAKED)
{
	clr_BUZZER;
	
	if (stop_buzzer == true)
	{		
		timer_type0_stop(&TCC0);
		clr_BUZZER;
	}
	
	reti();
}