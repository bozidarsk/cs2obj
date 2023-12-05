rm -f test/csout
rm -f test/csout.so
rm -f test/csoutglobal.so
rm -f test/asmout
rm -f test/out

mcs test/test0.cs -out:test/csout
if [[ $? -ne 0 ]]; then exit 1; fi

# nasm -f elf64 test/test0.asm -o test/asmout
# if [[ $? -ne 0 ]]; then exit 2; fi

mono --aot=full test/csout
if [[ $? -ne 0 ]]; then exit 3; fi

# symbols=$(readelf -Ws --dyn-syms test/csout.so | grep FUNC | sed -E 's/\s+[0-9]+:\s+[0-9a-f]+\s+[0-9]+\s+FUNC\s+LOCAL\s+DEFAULT\s+[0-9]+\s+([a-zA-Z0-9_]+)/ --globalize-symbol \1 /' | tr -d '\n')
# if [[ $? -ne 0 ]]; then exit 4; fi

# objcopy $symbols test/csout.so test/csoutglobal.so
# if [[ $? -ne 0 ]]; then exit 5; fi

# ld -m elf_x86_64 test/asmout test/csoutglobal.so -o test/out   
# if [[ $? -ne 0 ]]; then exit 6; fi
