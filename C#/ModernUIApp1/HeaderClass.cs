using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModernUIApp1
{
    class HeaderClass
    {
        private int offset32;
        private long offset64;

        public class ELF32_Header
        {
            public byte[] magic = new byte[16];
            public byte[] type = new byte[2];
            public byte[] machine = new byte[2];
            public byte[] version = new byte[4];
            public byte[] entry = new byte[4];
            public byte[] phoff = new byte[4];
            public byte[] shoff = new byte[4];
            public byte[] flags = new byte[4];
            public byte[] ehsize = new byte[2];
            public byte[] phentsize = new byte[2];
            public byte[] phnum = new byte[2];
            public byte[] shentsize = new byte[2];
            public byte[] shnum = new byte[2];
            public byte[] shstrndx = new byte[2];

            public ELF32_Header()
            { }

            /* 함수 : out*
             * 기능 : elf 구조체에 저장된 값 변환하여 출력
             * 반환 : a lot of type
             */
            public short outtype(byte[] type)
            {
                return BitConverter.ToInt16(type, 0);
            }
            public short outmachine(byte[] machine)
            {
                return BitConverter.ToInt16(machine, 0);
            }
            public int outversion(byte[] version)
            {
                return BitConverter.ToInt32(version, 0);
            }
            public int outentry(byte[] entry)
            {
                return BitConverter.ToInt32(entry, 0);
            }
            public int outphoff(byte[] phoff)
            {
                return BitConverter.ToInt32(phoff, 0);
            }
            public int outshoff(byte[] shoff)
            {
                return BitConverter.ToInt32(shoff, 0);
            }
            public int outflags(byte[] flags)
            {
                return BitConverter.ToInt32(flags, 0);
            }
            public short outehsize(byte[] ehsize)
            {
                return BitConverter.ToInt16(ehsize, 0);
            }
            public short outphsize(byte[] phsize)
            {
                return BitConverter.ToInt16(phsize, 0);
            }
            public short outphnum(byte[] phnum)
            {
                return BitConverter.ToInt16(phnum, 0);
            }
            public short outshsize(byte[] shsize)
            {
                return BitConverter.ToInt16(shsize, 0);
            }
            public short outshnum(byte[] shnum)
            {
                return BitConverter.ToInt16(shnum, 0);
            }
            public short outshstrndx(byte[] shstrndx)
            {
                return BitConverter.ToInt16(shstrndx, 0);
            }

            /* 함수 : elf32output
             * 기능 : elf 구조체에 저장된 값을 통해 내용에 대한 정보 출력
             * 반환 : string
             */
            public string elf32output_data_endian(byte endian)
            {
                switch (endian)
                {
                    case 0x00:
                        return "I don't know(ELFDATAONE)";
                    case 0x01:
                        return "Little Endian(ELFDATA2LSB)";
                    case 0x02:
                        return "Big Endian(ELFDATA2MSB)";
                    default:
                        return "What?!";
                }
            }
            public string elf32output_file_version(byte f_version)
            {
                switch (f_version)
                {
                    case 0x00:
                        return "I don't know(E_NONE)";
                    case 0x01:
                        return "Current Version(E_CURRENT)";
                    default:
                        return "What?!";
                }
            }
            public string elf32output_type(short type)
            {
                switch (type)
                {
                    case 0x00:
                        return "No Type (ET_NONE)";
                    case 0x01:
                        return "Relocation File (ET_REL)";
                    case 0x02:
                        return "Excutable File (ET_EXEC)";
                    case 0x03:
                        return "Shared object File(ET_DYN)";
                    case 0x04:
                        return "Core File (ET_CORE)";
                    /*case 0xfe00:
                        return "(ET_LOSS)";
                    case 0xfeff:
                        return "(ET_HIOS)";
                    case 0xff00:
                        return "프로세서에 의존적인 파일(ET_LOPROC)";
                    case 0xffff:
                        return "프로세서에 의존적인 파일2(ET_HIPROC)";*/
                    default:
                        return "What?!";
                }
            }
            public string elf32output_machine(short machine)
            {
                switch (machine)
                {
                    case 0x00:
                        return "특정 Machine을 구분하지 않음.(EM_NONE)";
                    case 0x01:
                        return "AT&T WE32100(EM_M32)";
                    case 0x02:
                        return "SPARC(EM_SPARC)";
                    case 0x03:
                        return "Intel 80386(EM_386)";
                    case 0x04:
                        return "Motorola 68000(EM_68K)";
                    case 0x05:
                        return "Motorola 88000(EM_88K)";
                    case 0x06:
                        return "사용되지 않음(EM_486)";
                    case 0x07:
                        return "Intel 80860(EM_860)";
                    case 0x08:
                        return "MIPS R3000(EM_MIPS, officially, big-endian only)";
                    case 0x09:
                        return "(EM_S370)";
                    case 0x0A:
                        return "MIPS R4000 big-endian(EM_MIPS_RS4_BE 10)";
                    case 0x0F:
                        return "HPPA(EM_PARISC)";
                    case 0x11:
                        return "(EM_VPP500)";
                    case 0x12:
                        return "Sun's \"v8plus\"(EM_SPARC32PLUS)";
                    case 0x13:
                        return "(EM_960)";
                    case 0x14:
                        return "PowerPC(EM_PPC)";
                    case 0x15:
                        return "PowerPC64(EM_PPC64)";
                    case 0x16:
                        return "(EM_S390)";
                    case 0x24:
                        return "(EM_V800)";
                    case 0x25:
                        return "(EM_FR20)";
                    case 0x26:
                        return "(EM_RH32)";
                    case 0x27:
                        return "(EM_RCE)";
                    case 0x28:
                        return "(EM_ARM)";
                    case 0x29:
                        return "(EM_ALPHA)";
                    case 0x2A:
                        return "SuperH(EM_SH)";
                    case 0x2B:
                        return "SPARC v9 64-bit(EM_SPARCV9)";
                    case 0x2C:
                        return "(EM_TRICORE)";
                    case 0x2D:
                        return "(EM_ARC)";
                    case 0x2E:
                        return "(EM_H8_300)";
                    case 0x2F:
                        return "(EM_H8_300H)";
                    case 0x30:
                        return "(EM_H8S)";
                    case 0x31:
                        return "(EM_H8_500)";
                    case 0x32:
                        return "HP/Intel IA-64(EM_IA_64)";
                    case 0x33:
                        return "(EM_MIPS_X)";
                    case 0x34:
                        return "(EM_CORDFIRE)";
                    case 0x35:
                        return "(EM_68HC12)";
                    case 0x36:
                        return "(EM_MMA)";
                    case 0x37:
                        return "(EM_PCP)";
                    case 0x38:
                        return "(EM_NCPU)";
                    case 0x39:
                        return "(EM_NDR1)";
                    case 0x3A:
                        return "(EM_STARCORE)";
                    case 0x3B:
                        return "(EM_ME16)";
                    case 0x3C:
                        return "(EM_ST100)";
                    case 0x3D:
                        return "(EM_TINYJ)";
                    case 0x3E:
                        return "AMD x86-(EM_X86_64)";
                    case 0x3F:
                        return "(EM_PDSP)";
                    case 0x40:
                        return "(EM_PDP10)";
                    case 0x41:
                        return "(EM_PDP11)";
                    case 0x42:
                        return "(EM_FX66)";
                    case 0x43:
                        return "(EM_ST9PLUS)";
                    case 0x44:
                        return "(EM_ST7)";
                    case 0x45:
                        return "(EM_68HC16)";
                    case 0x46:
                        return "(EM_68HC11)";
                    case 0x47:
                        return "(EM_68HC08)";
                    case 0x48:
                        return "(EM_68HC05)";
                    case 0x49:
                        return "(EM_SVX)";
                    case 0x4B:
                        return "(EM_ST19)";
                    case 0x4C:
                        return "(EM_CRIS)";
                    case 0x4D:
                        return "(EM_JAVELIN)";
                    case 0x4E:
                        return "(EM_FIREPATH)";
                    case 0x4F:
                        return "(EM_ZSP)";
                    case 0x50:
                        return "(EM_MMIX)";
                    case 0x51:
                        return "(EM_HUANY)";
                    case 0x52:
                        return "(EM_PRISM)";
                    case 0x53:
                        return "(EM_AVR)";
                    case 0x54:
                        return "(EM_FR30)";
                    case 0x55:
                        return "(EM_D10V)";
                    case 0x56:
                        return "(EM_D30V)";
                    case 0x57:
                        return "(EM_V850)";
                    case 0x58:
                        return "(EM_M32R)";
                    case 0x59:
                        return "(EM_MN10300)";
                    case 0x5A:
                        return "(EM_MN10200)";
                    case 0x5B:
                        return "(EM_PJ)";
                    case 0x5C:
                        return "(EM_OPENRISC)";
                    case 0x5D:
                        return "(EM_ARC_A5)";
                    case 0x5E:
                        return "(EM_XTENSA)";
                    case 0x5F:
                        return "(EM_VIDEOCORE)";
                    case 0x60:
                        return "(EM_TMM_GPP)";
                    case 0x61:
                        return "(EM_NS32K)";
                    case 0x62:
                        return "(EM_TPC)";
                    case 0x63:
                        return "(EM_SNP1K)";
                    case 0x64:
                        return "(EM_ST200)";
                    case 0x65:
                        return "(EM_IP2K)";
                    case 0x66:
                        return "(EM_MAX)";
                    case 0x67:
                        return "(EM_CR)";
                    case 0x68:
                        return "(EM_F2MC16)";
                    case 0x69:
                        return "(EM_MSP430)";
                    case 0x6A:
                        return "(EM_BLACKFIN)";
                    case 0x6B:
                        return "(EM_SE_C33)";
                    case 0x6C:
                        return "(EM_SEP)";
                    case 0x6D:
                        return "(EM_ARCA)";
                    case 0x6E:
                        return "(EM_UNICORE)";
                    /*case 0x9026:
                        return "Alpha(EM_ALPHA)";
                    case 0xA390:
                        return "IBM S390(EM_S390)";*/
                    default:
                        return "Reserved";
                }
            }
            public string elf32output_version(int version)
            {
                switch (version)
                {
                    case 0x00:
                        return "Invalid Version(EV_NONE)";
                    case 0x01:
                        return "Current Version(EV_CURRENT)";
                    case 0x02:
                        return "Next Version(EV_NUM)";
                    default:
                        return "What?!";
                }
            }
        };

        public class ELF64_Header                   //자료형 수정 필요
        {
            public byte[] magic = new byte[16];
            public byte[] type = new byte[2];
            public byte[] machine = new byte[2];
            public byte[] version = new byte[4];
            public byte[] entry = new byte[8];
            public byte[] phoff = new byte[8];
            public byte[] shoff = new byte[8];
            public byte[] flags = new byte[4];
            public byte[] ehsize = new byte[2];
            public byte[] phentsize = new byte[2];
            public byte[] phnum = new byte[2];
            public byte[] shentsize = new byte[2];
            public byte[] shnum = new byte[2];
            public byte[] shstrndx = new byte[2];

            public ELF64_Header()
            { }

            /* 함수 : out*
             * 기능 : elf 구조체에 저장된 값 변환하여 출력
             * 반환 : a lot of type
             */
            public short outtype(byte[] type)
            {
                return BitConverter.ToInt16(type, 0);
            }
            public short outmachine(byte[] machine)
            {
                return BitConverter.ToInt16(machine, 0);
            }
            public int outversion(byte[] version)
            {
                return BitConverter.ToInt32(version, 0);
            }
            public long outentry(byte[] entry)
            {
                return BitConverter.ToInt32(entry, 0);
            }
            public long outphoff(byte[] phoff)
            {
                return BitConverter.ToInt64(phoff, 0);
            }
            public long outshoff(byte[] shoff)
            {
                return BitConverter.ToInt64(shoff, 0);
            }
            public int outflags(byte[] flags)
            {
                return BitConverter.ToInt32(flags, 0);
            }
            public short outehsize(byte[] ehsize)
            {
                return BitConverter.ToInt16(ehsize, 0);
            }
            public short outphsize(byte[] phsize)
            {
                return BitConverter.ToInt16(phsize, 0);
            }
            public short outphnum(byte[] phnum)
            {
                return BitConverter.ToInt16(phnum, 0);
            }
            public short outshsize(byte[] shsize)
            {
                return BitConverter.ToInt16(shsize, 0);
            }
            public short outshnum(byte[] shnum)
            {
                return BitConverter.ToInt16(shnum, 0);
            }
            public short outshstrndx(byte[] shstrndx)
            {
                return BitConverter.ToInt16(shstrndx, 0);
            }

            /* 함수 : elf64output
             * 기능 : elf 구조체에 저장된 값을 통해 내용에 대한 정보 출력
             * 반환 : string
             */
            public string elf64output_data_endian(byte endian)
            {
                switch (endian)
                {
                    case 0x00:
                        return "I don't know(ELFDATAONE)";
                    case 0x01:
                        return "Little Endian(ELFDATA2LSB)";
                    case 0x02:
                        return "Big Endian(ELFDATA2MSB)";
                    default:
                        return "What?!";
                }
            }
            public string elf64output_file_version(byte f_version)
            {
                switch (f_version)
                {
                    case 0x00:
                        return "I don't know(E_NONE)";
                    case 0x01:
                        return "Current Version(E_CURRENT)";
                    default:
                        return "What?!";
                }
            }
            public string elf64output_type(short type)
            {
                switch (type)
                {
                    case 0x00:
                        return "No Type (ET_NONE)";
                    case 0x01:
                        return "Relocation File (ET_REL)";
                    case 0x02:
                        return "Excutable File (ET_EXEC)";
                    case 0x03:
                        return "Shared object File (ET_DYN)";
                    case 0x04:
                        return "Core File (ET_CORE)";
                    /*case 0xfe00:
                        return "(ET_LOSS)";
                    case 0xfeff:
                        return "(ET_HIOS)";
                    case 0xff00:
                        return "프로세서에 의존적인 파일(ET_LOPROC)";
                    case 0xffff:
                        return "프로세서에 의존적인 파일2(ET_HIPROC)";*/
                    default:
                        return "What?!";
                }
            }
            public string elf64output_machine(short machine)
            {
                switch (machine)
                {
                    case 0x00:
                        return "특정 Machine을 구분하지 않음.(EM_NONE)";
                    case 0x01:
                        return "AT&T WE32100(EM_M32)";
                    case 0x02:
                        return "SPARC(EM_SPARC)";
                    case 0x03:
                        return "Intel 80386(EM_386)";
                    case 0x04:
                        return "Motorola 68000(EM_68K)";
                    case 0x05:
                        return "Motorola 88000(EM_88K)";
                    case 0x06:
                        return "사용되지 않음(EM_486)";
                    case 0x07:
                        return "Intel 80860(EM_860)";
                    case 0x08:
                        return "MIPS R3000(EM_MIPS, officially, big-endian only)";
                    case 0x09:
                        return "(EM_S370)";
                    case 0x0A:
                        return "MIPS R4000 big-endian(EM_MIPS_RS4_BE 10)";
                    case 0x0F:
                        return "HPPA(EM_PARISC)";
                    case 0x11:
                        return "(EM_VPP500)";
                    case 0x12:
                        return "Sun's \"v8plus\"(EM_SPARC32PLUS)";
                    case 0x13:
                        return "(EM_960)";
                    case 0x14:
                        return "PowerPC(EM_PPC)";
                    case 0x15:
                        return "PowerPC64(EM_PPC64)";
                    case 0x16:
                        return "(EM_S390)";
                    case 0x24:
                        return "(EM_V800)";
                    case 0x25:
                        return "(EM_FR20)";
                    case 0x26:
                        return "(EM_RH32)";
                    case 0x27:
                        return "(EM_RCE)";
                    case 0x28:
                        return "(EM_ARM)";
                    case 0x29:
                        return "(EM_ALPHA)";
                    case 0x2A:
                        return "SuperH(EM_SH)";
                    case 0x2B:
                        return "SPARC v9 64-bit(EM_SPARCV9)";
                    case 0x2C:
                        return "(EM_TRICORE)";
                    case 0x2D:
                        return "(EM_ARC)";
                    case 0x2E:
                        return "(EM_H8_300)";
                    case 0x2F:
                        return "(EM_H8_300H)";
                    case 0x30:
                        return "(EM_H8S)";
                    case 0x31:
                        return "(EM_H8_500)";
                    case 0x32:
                        return "HP/Intel IA-64(EM_IA_64)";
                    case 0x33:
                        return "(EM_MIPS_X)";
                    case 0x34:
                        return "(EM_CORDFIRE)";
                    case 0x35:
                        return "(EM_68HC12)";
                    case 0x36:
                        return "(EM_MMA)";
                    case 0x37:
                        return "(EM_PCP)";
                    case 0x38:
                        return "(EM_NCPU)";
                    case 0x39:
                        return "(EM_NDR1)";
                    case 0x3A:
                        return "(EM_STARCORE)";
                    case 0x3B:
                        return "(EM_ME16)";
                    case 0x3C:
                        return "(EM_ST100)";
                    case 0x3D:
                        return "(EM_TINYJ)";
                    case 0x3E:
                        return "AMD x86-(EM_X86_64)";
                    case 0x3F:
                        return "(EM_PDSP)";
                    case 0x40:
                        return "(EM_PDP10)";
                    case 0x41:
                        return "(EM_PDP11)";
                    case 0x42:
                        return "(EM_FX66)";
                    case 0x43:
                        return "(EM_ST9PLUS)";
                    case 0x44:
                        return "(EM_ST7)";
                    case 0x45:
                        return "(EM_68HC16)";
                    case 0x46:
                        return "(EM_68HC11)";
                    case 0x47:
                        return "(EM_68HC08)";
                    case 0x48:
                        return "(EM_68HC05)";
                    case 0x49:
                        return "(EM_SVX)";
                    case 0x4B:
                        return "(EM_ST19)";
                    case 0x4C:
                        return "(EM_CRIS)";
                    case 0x4D:
                        return "(EM_JAVELIN)";
                    case 0x4E:
                        return "(EM_FIREPATH)";
                    case 0x4F:
                        return "(EM_ZSP)";
                    case 0x50:
                        return "(EM_MMIX)";
                    case 0x51:
                        return "(EM_HUANY)";
                    case 0x52:
                        return "(EM_PRISM)";
                    case 0x53:
                        return "(EM_AVR)";
                    case 0x54:
                        return "(EM_FR30)";
                    case 0x55:
                        return "(EM_D10V)";
                    case 0x56:
                        return "(EM_D30V)";
                    case 0x57:
                        return "(EM_V850)";
                    case 0x58:
                        return "(EM_M32R)";
                    case 0x59:
                        return "(EM_MN10300)";
                    case 0x5A:
                        return "(EM_MN10200)";
                    case 0x5B:
                        return "(EM_PJ)";
                    case 0x5C:
                        return "(EM_OPENRISC)";
                    case 0x5D:
                        return "(EM_ARC_A5)";
                    case 0x5E:
                        return "(EM_XTENSA)";
                    case 0x5F:
                        return "(EM_VIDEOCORE)";
                    case 0x60:
                        return "(EM_TMM_GPP)";
                    case 0x61:
                        return "(EM_NS32K)";
                    case 0x62:
                        return "(EM_TPC)";
                    case 0x63:
                        return "(EM_SNP1K)";
                    case 0x64:
                        return "(EM_ST200)";
                    case 0x65:
                        return "(EM_IP2K)";
                    case 0x66:
                        return "(EM_MAX)";
                    case 0x67:
                        return "(EM_CR)";
                    case 0x68:
                        return "(EM_F2MC16)";
                    case 0x69:
                        return "(EM_MSP430)";
                    case 0x6A:
                        return "(EM_BLACKFIN)";
                    case 0x6B:
                        return "(EM_SE_C33)";
                    case 0x6C:
                        return "(EM_SEP)";
                    case 0x6D:
                        return "(EM_ARCA)";
                    case 0x6E:
                        return "(EM_UNICORE)";
                    /*case 0x9026:
                        return "Alpha(EM_ALPHA)";
                    case 0xA390:
                        return "IBM S390(EM_S390)";*/
                    default:
                        return "Reserved";
                }
            }
            public string elf64output_version(int version)
            {
                switch (version)
                {
                    case 0x00:
                        return "Invalid Version(EV_NONE)";
                    case 0x01:
                        return "Current Version(EV_CURRENT)";
                    case 0x02:
                        return "Next Version(EV_NUM)";
                    default:
                        return "What?!";
                }
            }
        };

        public class Program32_Header
        {
            public byte[] type = new byte[4];
            public byte[] offset = new byte[4];
            public byte[] vaddr = new byte[4];
            public byte[] paddr = new byte[4];
            public byte[] filesz = new byte[4];
            public byte[] memsz = new byte[4];
            public byte[] flags = new byte[4];
            public byte[] align = new byte[4];

            public Program32_Header()
            { }

            /* 함수 : out*
             * 기능 : 32bit program 구조체에 저장된 값 변환하여 출력
             * 반환 : a lot of type
             */
            public int outtype(byte[] type)
            {
                return BitConverter.ToInt32(type, 0);
            }
            public int outoffset(byte[] offset)
            {
                return BitConverter.ToInt32(offset, 0);
            }
            public int outvaddr(byte[] vaddr)
            {
                return BitConverter.ToInt32(vaddr, 0);
            }
            public int outpaddr(byte[] paddr)
            {
                return BitConverter.ToInt32(paddr, 0);
            }
            public int outfilesz(byte[] filesz)
            {
                return BitConverter.ToInt32(filesz, 0);
            }
            public int outmemsz(byte[] memsz)
            {
                return BitConverter.ToInt32(vaddr, 0);
            }
            public int outflags(byte[] flags)
            {
                return BitConverter.ToInt32(flags, 0);
            }
            public int outalign(byte[] align)
            {
                return BitConverter.ToInt32(align, 0);
            }

            /* 함수 : Program32output
             * 기능 : Program 헤더 구조체에 저장된 값을 통해 내용에 대한 정보 출력
             * 반환 : string
             */
            public string program32output_type(int type)
            {
                switch (type)
                {
                    case 0x00:
                        return "(PT_NULL)       ";
                    case 0x01:
                        return "(PT_LOAD)       ";
                    case 0x02:
                        return "(PT_DYNAMIC)    ";
                    case 0x03:
                        return "(PT_INERP)      ";
                    case 0x04:
                        return "(PT_NOTE)       ";
                    case 0x05:
                        return "(PT_SHLIB)      ";
                    case 0x06:
                        return "(PT_PHDR)       ";
                    case 0x60000000:
                        return "(PT_LOOS)       ";
                    case 0x6FFFFFFF:
                        return "(PT_HIOS)       ";
                    case 0x70000000:
                        return "(PT_LOPROC)     ";
                    case 0x7FFFFFFF:
                        return "(PT_HIPROC)     ";
                    default:
                        return "What?!          ";
                }
            }
            public string program32output_flags(int flags)
            {
                switch (flags)
                {
                    case 0x00:
                        return "(PF_NONE)            ";
                    case 0x01:
                        return "(PF_Exec)            ";
                    case 0x02:
                        return "(PF_Write)           ";
                    case 0x03:
                        return "(PF_Write_Exec)      ";
                    case 0x04:
                        return "(PT_Read)            ";
                    case 0x05:
                        return "(PT_Read_Exec)       ";
                    case 0x06:
                        return "(PT_Read_Write)      ";
                    case 0x07:
                        return "(PT_Read_Wirte_Exec) ";
                    default:
                        return "What?!              ";
                }
            }
        };

        public class Program64_Header
        {
            public byte[] type = new byte[4];
            public byte[] flags = new byte[4];
            public byte[] offset = new byte[8];
            public byte[] vaddr = new byte[8];
            public byte[] paddr = new byte[8];
            public byte[] filesz = new byte[8];
            public byte[] memsz = new byte[8];
            public byte[] align = new byte[8];

            public Program64_Header()
            { }

            /* 함수 : out*
             * 기능 : 64bit Program 구조체에 저장된 값 변환하여 출력
             * 반환 : a lot of type
             */
            public int outtype(byte[] type)
            {
                return BitConverter.ToInt32(type, 0);
            }
            public int outflags(byte[] flags)
            {
                return BitConverter.ToInt32(flags, 0);
            }
            public long outoffset(byte[] offset)
            {
                return BitConverter.ToInt64(offset, 0);
            }
            public long outvaddr(byte[] vaddr)
            {
                return BitConverter.ToInt64(vaddr, 0);
            }
            public long outpaddr(byte[] paddr)
            {
                return BitConverter.ToInt64(paddr, 0);
            }
            public long outfilesz(byte[] filesz)
            {
                return BitConverter.ToInt64(filesz, 0);
            }
            public long outmemsz(byte[] memsz)
            {
                return BitConverter.ToInt64(vaddr, 0);
            }
            public long outalign(byte[] align)
            {
                return BitConverter.ToInt64(align, 0);
            }

            /* 함수 : Program64output
             * 기능 : Program 헤더 구조체에 저장된 값을 통해 내용에 대한 정보 출력
             * 반환 : string
             */
            public string program64output_type(long type)
            {
                switch (type)
                {
                    case 0x00:
                        return "(PT_NULL)       ";
                    case 0x01:
                        return "(PT_LOAD)       ";
                    case 0x02:
                        return "(PT_DYNAMIC)    ";
                    case 0x03:
                        return "(PT_INERP)      ";
                    case 0x04:
                        return "(PT_NOTE)       ";
                    case 0x05:
                        return "(PT_SHLIB)      ";
                    case 0x06:
                        return "(PT_PHDR)       ";
                    case 0x60000000:
                        return "(PT_LOOS)       ";
                    case 0x6FFFFFFF:
                        return "(PT_HIOS)       ";
                    case 0x70000000:
                        return "(PT_LOPROC)     ";
                    case 0x7FFFFFFF:
                        return "(PT_HIPROC)     ";
                    default:
                        return "What?!          ";
                }
            }
            public string program64output_flags(long flags)
            {
                switch (flags)
                {
                    case 0x00:
                        return "(PF_NONE)            ";
                    case 0x01:
                        return "(PF_Exec)            ";
                    case 0x02:
                        return "(PF_Write)           ";
                    case 0x03:
                        return "(PF_Write_Exec)      ";
                    case 0x04:
                        return "(PT_Read)            ";
                    case 0x05:
                        return "(PT_Read_Exec)       ";
                    case 0x06:
                        return "(PT_Read_Write)      ";
                    case 0x07:
                        return "(PT_Read_Wirte_Exec) ";
                    default:
                        return "What?!               ";
                }
            }
        };

        public class Section32_Header
        {
            public byte[] name = new byte[4];
            public byte[] type = new byte[4];
            public byte[] flags = new byte[4];
            public byte[] addr = new byte[4];
            public byte[] offset = new byte[4];
            public byte[] size = new byte[4];
            public byte[] link = new byte[4];
            public byte[] info = new byte[4];
            public byte[] addralign = new byte[4];
            public byte[] entsize = new byte[4];

            public Section32_Header()
            { }

            /* 함수 : out*
             * 기능 : 32bit program 구조체에 저장된 값 변환하여 출력
             * 반환 : a lot of type
             */
            public int outname(byte[] name)
            {
                return BitConverter.ToInt32(name, 0);
            }
            public int outtype(byte[] type)
            {
                return BitConverter.ToInt32(type, 0);
            }
            public int outflags(byte[] flags)
            {
                return BitConverter.ToInt32(flags, 0);
            }
            public int outaddr(byte[] addr)
            {
                return BitConverter.ToInt32(addr, 0);
            }
            public int outoffset(byte[] offset)
            {
                return BitConverter.ToInt32(offset, 0);
            }
            public int outsize(byte[] size)
            {
                return BitConverter.ToInt32(size, 0);
            }
            public int outlink(byte[] link)
            {
                return BitConverter.ToInt32(link, 0);
            }
            public int outinfo(byte[] info)
            {
                return BitConverter.ToInt32(info, 0);
            }
            public int outaddralign(byte[] addralign)
            {
                return BitConverter.ToInt32(addralign, 0);
            }
            public int outentsize(byte[] entsize)
            {
                return BitConverter.ToInt32(entsize, 0);
            }

            /* 함수 : Section32output
             * 기능 : Section 헤더 구조체에 저장된 값을 통해 내용에 대한 정보 출력
             * 반환 : string
             */
            public string section32output_name(int name)
            {
                switch (name)
                {
                    case 0x00:
                        return "SHN_UNDEF              ";
                    case 0x0B:
                        return ".interp                    ";
                    case 0x13:
                        return ".note.ABI-tag          ";
                    case 0x21:
                        return ".note.gnu.build.id     ";
                    case 0x34:
                        return ".gnu.hash              ";
                    case 0x3E:
                        return ".dynsym                ";
                    case 0x46:
                        return ".dynstr                     ";
                    case 0x4E:
                        return ".gnu.version           ";
                    case 0x5B:
                        return ".gnu.version_r         ";
                    case 0x6A:
                        return ".rel.dyn                    ";
                    case 0x73:
                        return ".rel.plt                    ";
                    case 0x7C:
                        return ".init                       ";
                    case 0x77:
                        return ".plt                       ";
                    case 0x82:
                        return ".text                     ";
                    case 0x88:
                        return ".fini                     ";
                    case 0x8E:
                        return ".rodata                ";
                    case 0x96:
                        return ".eh_frame_hdr          ";
                    case 0xA4:
                        return ".eh_frame              ";
                    case 0xAE:
                        return ".init_array              ";
                    case 0xBA:
                        return ".fini_array                ";
                    case 0xC6:
                        return ".jcr                         ";
                    case 0xCB:
                        return "dynamic                ";
                    case 0xD4:
                        return ".got                          ";
                    case 0xD9:
                        return ".got.plt                     ";
                    case 0xE2:
                        return ".data                       ";
                    case 0xE8:
                        return ".bss                          ";
                    case 0xED:
                        return ".comment               ";
                    case 0x01:
                        return ".shstrtab              ";
                    default:
                        return "What?!                 ";
                }
            }
            public string section32output_type(int type)
            {
                switch (type)
                {
                    case 0x00:
                        return "(SHT_NULL)             ";
                    case 0x01:
                        return "(SHT_PROGBITS)         ";
                    case 0x02:
                        return "(SHT_SYMTAB)           ";
                    case 0x03:
                        return "(SHT_STRTAB)           ";
                    case 0x04:
                        return "(SHT_RELA)             ";
                    case 0x05:
                        return "(SHT_HASH)             ";
                    case 0x06:
                        return "(SHT_DYNAMIC)          ";
                    case 0x07:
                        return "(SHT_NOTE)             ";
                    case 0x08:
                        return "(SHT_NOBITS)           ";
                    case 0x09:
                        return "(SHT_REL)              ";
                    case 0x0A:
                        return "(SHT_SHLIB)            ";
                    case 0x0B:
                        return "(SHT_DYNSYM)           ";
                    case 0x60000000:
                        return "(SHT_LOOS)             ";
                    case 0x6FFFFFFF:
                        return "(SHT_HIOS)             ";
                    case 0x70000000:
                        return "(SHT_LOPROC)           ";
                    case 0x7FFFFFFF:
                        return "(SHT_HIPROC)           ";
                    default:
                        return "What?!                 ";
                }
            }
            public string section32output_flags(int flags)
            {
                switch (flags)
                {
                    case 0x00:
                        return "(SF32_NONE)            ";
                    case 0x01:
                        return "(SF32_Exec)            ";
                    case 0x02:
                        return "(SF32_Alloc)           ";
                    case 0x03:
                        return "(SF32_Alloc_Exec)      ";
                    case 0x04:
                        return "(SF32_Write_Exec)      ";
                    case 0x05:
                        return "(SF32_Write_Alloc)     ";
                    case 0x06:
                        return "(SF32_Write_Alloc_Exec)";
                    default:
                        return "What?!                 ";
                }
            }
        };

        public class Section64_Header
        {
            public byte[] name = new byte[4];
            public byte[] type = new byte[4];
            public byte[] flags = new byte[8];
            public byte[] addr = new byte[8];
            public byte[] offset = new byte[8];
            public byte[] size = new byte[8];
            public byte[] link = new byte[4];
            public byte[] info = new byte[4];
            public byte[] addralign = new byte[8];
            public byte[] entsize = new byte[8];

            public Section64_Header()
            { }

            /* 함수 : out*
             * 기능 : 32bit program 구조체에 저장된 값 변환하여 출력
             * 반환 : a lot of type
             */
            public int outname(byte[] name)
            {
                return BitConverter.ToInt32(name, 0);
            }
            public int outtype(byte[] type)
            {
                return BitConverter.ToInt32(type, 0);
            }
            public long outflags(byte[] flags)
            {
                return BitConverter.ToInt64(flags, 0);
            }
            public long outaddr(byte[] addr)
            {
                return BitConverter.ToInt64(addr, 0);
            }
            public long outoffset(byte[] offset)
            {
                return BitConverter.ToInt64(offset, 0);
            }
            public long outsize(byte[] size)
            {
                return BitConverter.ToInt64(size, 0);
            }
            public int outlink(byte[] link)
            {
                return BitConverter.ToInt32(link, 0);
            }
            public int outinfo(byte[] info)
            {
                return BitConverter.ToInt32(info, 0);
            }
            public long outaddralign(byte[] addralign)
            {
                return BitConverter.ToInt64(addralign, 0);
            }
            public long outentsize(byte[] entsize)
            {
                return BitConverter.ToInt64(entsize, 0);
            }

            /* 함수 : Section64output
             * 기능 : Section 헤더 구조체에 저장된 값을 통해 내용에 대한 정보 출력
             * 반환 : string
             */
            public string section64output_name(int name)
            {
                switch (name)
                {
                    case 0x00:
                        return "SHN_UNDEF              ";
                    case 0x1B:
                        return ".interp                      ";
                    case 0x23:
                        return ".note.ABI-tag          ";
                    case 0x31:
                        return ".note.gnu.build.id     ";
                    case 0x44:
                        return ".gnu.hash              ";
                    case 0x4E:
                        return ".dynsym                ";
                    case 0x56:
                        return ".dynstr                        ";
                    case 0x5E:
                        return ".gnu.version           ";
                    case 0x6B:
                        return ".gnu.version_r         ";
                    case 0x7A:
                        return ".rela.dyn              ";
                    case 0x84:
                        return ".rela.plt                   ";
                    case 0x8E:
                        return ".init                         ";
                    case 0x89:
                        return ".plt                        ";
                    case 0x94:
                        return ".text                        ";
                    case 0x9A:
                        return ".fini                         ";
                    case 0xA0:
                        return ".rodata                ";
                    case 0xA8:
                        return ".eh_frame_hdr          ";
                    case 0xB6:
                        return ".eh_frame              ";
                    case 0xC0:
                        return ".init_array                 ";
                    case 0xCC:
                        return ".fini_array                 ";
                    case 0xD8:
                        return ".jcr                          ";
                    case 0xDD:
                        return "dynamic                ";
                    case 0xE6:
                        return ".got                        ";
                    case 0xEB:
                        return ".got.plt                      ";
                    case 0xF4:
                        return ".data                       ";
                    case 0xFA:
                        return ".bss                        ";
                    case 0xFF:
                        return ".comment               ";
                    case 0x11:
                        return ".shstrtab              ";
                    case 0x01:
                        return ".symtab                ";
                    case 0x09:
                        return ".strtab                     ";
                    default:
                        return "What?!                 ";
                }
            }
            public string section64output_type(int type)
            {
                switch (type)
                {
                    case 0x00:
                        return "(SHT_NULL)             ";
                    case 0x01:
                        return "(SHT_PROGBITS)         ";
                    case 0x02:
                        return "(SHT_SYMTAB)           ";
                    case 0x03:
                        return "(SHT_STRTAB)           ";
                    case 0x04:
                        return "(SHT_RELA)             ";
                    case 0x05:
                        return "(SHT_HASH)             ";
                    case 0x06:
                        return "(SHT_DYNAMIC)          ";
                    case 0x07:
                        return "(SHT_NOTE)             ";
                    case 0x08:
                        return "(SHT_NOBITS)           ";
                    case 0x09:
                        return "(SHT_REL)              ";
                    case 0x0A:
                        return "(SHT_SHLIB)            ";
                    case 0x0B:
                        return "(SHT_DYNSYM)           ";
                    case 0x60000000:
                        return "(SHT_LOOS)             ";
                    case 0x6FFFFFFF:
                        return "(SHT_HIOS)             ";
                    case 0x70000000:
                        return "(SHT_LOPROC)           ";
                    case 0x7FFFFFFF:
                        return "(SHT_HIPROC)           ";
                    default:
                        return "What?!                 ";
                }
            }
            public string section64output_flags(long flags)
            {
                switch (flags)
                {
                    case 0x00:
                        return "(SF32_NONE)            ";
                    case 0x01:
                        return "(SF32_Exec)            ";
                    case 0x02:
                        return "(SF32_Alloc)           ";
                    case 0x03:
                        return "(SF32_Alloc_Exec)      ";
                    case 0x04:
                        return "(SF32_Write_Exec)      ";
                    case 0x05:
                        return "(SF32_Write_Alloc)     ";
                    case 0x06:
                        return "(SF32_Write_Alloc_Exec)";
                    default:
                        return "What?!                 ";
                }
            }
        };

        /// <summary>
        /// 굉장굉장한 클래스 선언 부분!!!!!----------------------------------------------------------------------------------------
        /// </summary>
        public ELF32_Header elf32_header = new ELF32_Header();
        public ELF64_Header elf64_header = new ELF64_Header();
        public Program32_Header[] p32_header = new Program32_Header[20]
            { new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(),
            new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header(), new Program32_Header()};
        public Program64_Header[] p64_header = new Program64_Header[20]
            { new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(),
            new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header(), new Program64_Header()};
        public Section32_Header[] s32_header = new Section32_Header[40]
            { new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(),
            new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(),
            new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(),
            new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header(), new Section32_Header()};
        public Section64_Header[] s64_header = new Section64_Header[40]
            { new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(),
            new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(),
            new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(),
            new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header(), new Section64_Header()};

        public byte[] StrByte { get; private set; }

        public HeaderClass()
        {
            offset32 = 0;
            offset64 = 0;
        }

        /* 함수 : ELF32_INPUT
        * 기능 : 32bit ELF 파일의 헤더 정보를 프로그램 내부에 할당된 구조체에 복사
        * 반환 : void
        */
        public void ELF32_INPUT(byte[] datas)
        {
            try
            {
                unsafe
                {
                    /*32bit ELF 내용 복사*/
                    Array.Copy(datas, offset32, elf32_header.magic, 0, 16);
                    offset32 = 16;
                    Array.Copy(datas, offset32, elf32_header.type, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.machine, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.version, 0, 4);
                    offset32 += 4;
                    Array.Copy(datas, offset32, elf32_header.entry, 0, 4);
                    offset32 += 4;
                    Array.Copy(datas, offset32, elf32_header.phoff, 0, 4);
                    offset32 += 4;
                    Array.Copy(datas, offset32, elf32_header.shoff, 0, 4);
                    offset32 += 4;
                    Array.Copy(datas, offset32, elf32_header.flags, 0, 4);
                    offset32 += 4;
                    Array.Copy(datas, offset32, elf32_header.ehsize, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.phentsize, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.phnum, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.shentsize, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.shnum, 0, 2);
                    offset32 += 2;
                    Array.Copy(datas, offset32, elf32_header.shstrndx, 0, 2);
                    offset32 += 2;

                    offset32 = elf32_header.outphoff(elf32_header.phoff);
                    int num = elf32_header.outphnum(elf32_header.phnum);
                    /*32bit Program Header 내용 복사*/
                    for (int i = 0; i < num; i++)
                    {
                        Array.Copy(datas, offset32, p32_header[i].type, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].offset, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].vaddr, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].paddr, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].filesz, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].memsz, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].flags, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, p32_header[i].align, 0, 4);
                        offset32 += 4;
                    }
                    offset32 = elf32_header.outshoff(elf32_header.shoff);
                    num = elf32_header.outshnum(elf32_header.shnum);
                    /*32bit Section Header 내용 복사*/
                    for (int i = 0; i < num; i++)
                    {
                        Array.Copy(datas, offset32, s32_header[i].name, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].type, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].flags, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].addr, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].offset, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].size, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].link, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].info, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].addralign, 0, 4);
                        offset32 += 4;
                        Array.Copy(datas, offset32, s32_header[i].entsize, 0, 4);
                        offset32 += 4;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
        }

        /* 함수 : ELF64_INPUT
        * 기능 : 64bit ELF 파일의 헤더 정보를 프로그램 내부에 할당된 구조체에 복사
        * 반환 : void
        */
        public void ELF64_INPUT(byte[] datas)
        {
            try
            {
                unsafe
                {
                    /*64bit ELF 내용 복사*/
                    Array.Copy(datas, offset64, elf64_header.magic, 0, 16);
                    offset64 = 16;
                    Array.Copy(datas, offset64, elf64_header.type, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.machine, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.version, 0, 4);
                    offset64 += 4;
                    Array.Copy(datas, offset64, elf64_header.entry, 0, 8);
                    offset64 += 8;
                    Array.Copy(datas, offset64, elf64_header.phoff, 0, 8);
                    offset64 += 8;
                    Array.Copy(datas, offset64, elf64_header.shoff, 0, 8);
                    offset64 += 8;
                    Array.Copy(datas, offset64, elf64_header.flags, 0, 4);
                    offset64 += 4;
                    Array.Copy(datas, offset64, elf64_header.ehsize, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.phentsize, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.phnum, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.shentsize, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.shnum, 0, 2);
                    offset64 += 2;
                    Array.Copy(datas, offset64, elf64_header.shstrndx, 0, 2);
                    offset64 += 2;

                    offset64 = elf64_header.outphoff(elf64_header.phoff);
                    int num = elf64_header.outphnum(elf64_header.phnum);
                    /*64bit Program Header 내용 복사*/
                    for (int i = 0; i < num; i++)
                    {
                        Array.Copy(datas, offset64, p64_header[i].type, 0, 4);
                        offset64 += 4;
                        Array.Copy(datas, offset64, p64_header[i].flags, 0, 4);
                        offset64 += 4;
                        Array.Copy(datas, offset64, p64_header[i].offset, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, p64_header[i].vaddr, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, p64_header[i].paddr, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, p64_header[i].filesz, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, p64_header[i].memsz, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, p64_header[i].align, 0, 8);
                        offset64 += 8;
                    }

                    offset64 = elf64_header.outshoff(elf64_header.shoff);
                    num = elf64_header.outshnum(elf64_header.shnum);
                    /*64bit Section Header 내용 복사*/
                    for (int i = 0; i < num; i++)
                    {
                        Array.Copy(datas, offset64, s64_header[i].name, 0, 4);
                        offset64 += 4;
                        Array.Copy(datas, offset64, s64_header[i].type, 0, 4);
                        offset64 += 4;
                        Array.Copy(datas, offset64, s64_header[i].flags, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, s64_header[i].addr, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, s64_header[i].offset, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, s64_header[i].size, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, s64_header[i].link, 0, 4);
                        offset64 += 4;
                        Array.Copy(datas, offset64, s64_header[i].info, 0, 4);
                        offset64 += 4;
                        Array.Copy(datas, offset64, s64_header[i].addralign, 0, 8);
                        offset64 += 8;
                        Array.Copy(datas, offset64, s64_header[i].entsize, 0, 8);
                        offset64 += 8;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message.ToString());
            }
        }

        public string ELFbitcheck(byte bitcheck)                    //ELF Bit Check!
        {
            switch (bitcheck)
            {
                case 0x00:
                    return "UNKNOWN";
                case 0x01:
                    return "ELF32";
                case 0x02:
                    return "ELF64";
                default:
                    return "What?!";
            }
        }

        public string ELFendian(byte endian)                    //Endian Check!
        {
            switch (endian)
            {
                case 0x00:
                    return "ELFDATAONE";
                case 0x01:
                    return "Little Endian(ELFDATA2LSB)";
                case 0x02:
                    return "Big Endian(ELFDATA2MSB)";
                default:
                    return "What?!";
            }
        }

        public string ELFversion(byte version)                  //ELF File Version Check!
        {
            switch (version)
            {
                case 0x00:
                    return "I don't know(E_NONE)";
                case 0x01:
                    return "Current Version(E_CURRENT)";
                default:
                    return "What?!";
            }
        }

        public string ELFOSABI(byte OSABI)                  //OSABI
        {
            switch (OSABI)
            {
                case 0x00:
                    return "UNIX";
                case 0x01:
                    return "HPUX";
                case 0x02:
                    return "NETBSD";
                case 0x06:
                    return "SOLALIS";
                case 0x07:
                    return "AIX";
                case 0x08:
                    return "IRIX";
                case 0x09:
                    return "FREEBSD";
                case 0x0A:
                    return "TRU64";
                case 0x0B:
                    return "MODESTO";
                case 0x0C:
                    return "OPENBSD";
                case 0x0D:
                    return "OPENVMS";
                case 0x0E:
                    return "NSK";
                case 0x0F:
                    return "AROS";
                default:
                    return "What?!";
            }
        }

        // 바이트 배열을 String으로 변환 
        private string ByteToString(byte[] strByte)
        {
            string str = Encoding.Default.GetString(StrByte);
            return str;
        }
        // String을 바이트 배열로 변환 
        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }
    };
}
