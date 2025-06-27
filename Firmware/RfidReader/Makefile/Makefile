# MCU and Programmer Settings
MCU = atxmega32a4u
F_CPU = 32000000UL

# Compiler and Linker
CC = avr-gcc
OBJCOPY = avr-objcopy
CFLAGS = -mmcu=$(MCU) -DF_CPU=$(F_CPU) \
-funsigned-char \
-funsigned-bitfields \
-O3 \
-ffunction-sections \
-fdata-sections \
-fpack-struct \
-fshort-enums \
-Wall

LDFLAGS = -L.. -Wl,--gc-sections
LDLIBS = -lATxmega32A4U-1.13


# Source Files (one level up)
SRC = ../app.c \
../app_funcs.c \
../app_ios_and_regs.c \
../interrupts.c \
../main.c \
../uart0.c

# Output file
TARGET = main

OBJ = $(SRC:.c=.o)

all: $(TARGET).hex

$(TARGET).elf: $(OBJ)
	$(CC) $(CFLAGS) $(LDFLAGS) -o $@ $^ $(LDLIBS)

%.o: %.c
	$(CC) $(CFLAGS) -c $< -o $@

$(TARGET).hex: $(TARGET).elf
	$(OBJCOPY) -O ihex -R .eeprom $< $@

clean:
	rm -f ../*.o *.o *.elf *.hex
