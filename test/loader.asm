section .bss

fileinfo:
	.version: resd 1
	.dummy: resd 1
	.jit_got: resq 1
	.llvm_got: resq 1
	.mono_eh_frame: resq 1
	.llvm_get_method: resq 1
	.llvm_get_unbox_tramp: resq 1
	.jit_code_start: resq 1
	.jit_code_end: resq 1
	.method_addresses: resq 1
	.blob: resq 1
	.class_name_table: resq 1
	.class_info_offsets: resq 1
	.method_info_offsets: resq 1
	.ex_info_offsets: resq 1
	.extra_method_info_offsets: resq 1
	.extra_method_table: resq 1
	.got_info_offsets: resq 1
	.llvm_got_info_offsets: resq 1
	.image_table: resq 1
	.mem_end: resq 1
	.assembly_guid: resq 1
	.runtime_version: resq 1
	.specific_trampolines: resq 1
	.static_rgctx_trampolines: resq 1
	.imt_trampolines: resq 1
	.gsharedvt_arg_trampolines: resq 1
	.globals: resq 1
	.assembly_name: resq 1
	.plt: resq 1
	.plt_end: resq 1
	.unwind_info: resq 1
	.unbox_trampolines: resq 1
	.unbox_trampolines_end: resq 1
	.unbox_trampoline_addresses: resq 1
	.weak_field_indexes: resq 1
	.plt_got_offset_base: resd 1
	.got_size: resd 1
	.plt_size: resd 1
	.nmethods: resd 1
	.flags: resd 1
	.opts: resd 1
	.simd_opts: resd 1
	.gc_name_index: resd 1
	.num_rgctx_fetch_trampolines: resd 1
	.double_align: resd 1
	.long_align: resd 1
	.generic_tramp_num: resd 1
	.tramp_page_size: resd 1
	.nshared_got_entries: resd 1
	.datafile_size: resd 1

	.table_offsets: resd 10
	.num_trampolines: resd 4
	.trampoline_got_offset_base: resd 4
	.trampoline_size: resd 4
	.tramp_page_code_offsets: resd 4
	.aotid: resb 16

section .text


