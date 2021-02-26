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
ISR(PORTC_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/
/* UART0                                                                */
/************************************************************************/
extern uint8_t rxbuff_pointer;

ISR(TCD1_OVF_vect, ISR_NAKED)
{
	rxbuff_pointer = 0;
	timer_type1_stop(&TCD1);
	
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