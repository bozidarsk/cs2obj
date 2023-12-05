global _start
extern Display_DrawSquare_uint_uint_uint_uint_Color

section .data
	color: dq 0x007f007f

section .text

_start:
	mov rdi, 0
	mov rsi, 0
	mov rdx, 50
	mov rcx, 50
	mov r8, color
	call Display_DrawSquare_uint_uint_uint_uint_Color
