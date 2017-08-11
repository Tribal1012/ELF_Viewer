// 기본 DLL 파일입니다.

#include "stdafx.h"

#include "ELF Viewer dll.h"


namespace ELFViewer {
	/* 함수 : elf32output
	 * 기능 : elf 구조체에 저장된 값을 통해 내용에 대한 정보 출력, 임시로 만든것이라 break로 대충 해둠.
	 * 반환 : char*
	 */
	char* ELF32_header::elf32output_data_endian(char endian)
	{
		switch(endian)
		{
			case 0x00:
				return "알 수 없음(ELFDATAONE)";
			case 0x01:
				return "Little Endian(ELFDATA2LSB)";
			case 0x02:
				return "Big Endian(ELFDATA2MSB)";
			default:
				return "What?!";
		}
	}

	char* ELF32_header::elf32output_file_version(char f_version)
	{
		switch(f_version)
		{
			case 0x00:
				return "알 수 없음(E_NONE)";
			case 0x01:
				return "현재 버전(E_CURRENT)";
			default:
				return "What?!";
		}
	}

	char* ELF32_header::elf32output_type(short type)
	{
		switch(type)
		{
			case 0x00:
				return "파일의 타입이 없음(ET_NONE)";
			case 0x01:
				return "재배치 가능 파일(ET_REL)";
			case 0x02:
				return "실행 가능한 파일(ET_EXEC)";
			case 0x03:
				return "공유 오브젝트 파일(ET_DYN)";
			case 0x04:
				return "Core 파일(ET_CORE)";
			case 0xfe00:
				return "(ET_LOSS)";
			case 0xfeff:
				return "(ET_HIOS)";
			case 0xff00:
				return "프로세서에 의존적인 파일(ET_LOPROC)";
			case 0xffff:
				return "프로세서에 의존적인 파일2(ET_HIPROC)";
			default:
				return "What?!";
		}
	}

	char* ELF32_header::elf32output_machine(short machine)
	{
		switch(machine)
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
			case 0x9026:
				return "Alpha(EM_ALPHA)";
			case 0xA390:
				return "IBM S390(EM_S390)";
			default:
				return "Reserved";
		}
	}

	char* ELF32_header::elf32output_version(int version)
	{
		switch(version)
		{
			case 0x00:
				return "Invalid 버전(EV_NONE)";
			case 0x01:
				return "현재 버전(EV_CURRENT)";
			case 0x02:
				return "다음 버전(EV_NUM)";
			default:
				return "What?!";
		}
	}

	/* 함수 : elf64output
	 * 기능 : elf 구조체에 저장된 값을 통해 내용에 대한 정보 출력, 임시로 만든것이라 break로 대충 해둠.
	 * 반환 : char*
	 */
	char* ELF64_header::elf64output_data_endian(char endian)
	{
		switch(endian)
		{
			case 0x00:
				return "알 수 없음(ELFDATAONE)";
			case 0x01:
				return "Little Endian(ELFDATA2LSB)";
			case 0x02:
				return "Big Endian(ELFDATA2MSB)";
			default:
				return "What?!";
		}
	}

	char* ELF64_header::elf64output_file_version(char f_version)
	{
		switch(f_version)
		{
			case 0x00:
				return "알 수 없음(E_NONE)";
			case 0x01:
				return "현재 버전(E_CURRENT)";
			default:
				return "What?!";
		}
	}

	char* ELF64_header::elf64output_type(short type)
	{
		switch(type)
		{
			case 0x00:
				return "파일의 타입이 없음(ET_NONE)";
			case 0x01:
				return "재배치 가능 파일(ET_REL)";
			case 0x02:
				return "실행 가능한 파일(ET_EXEC)";
			case 0x03:
				return "공유 오브젝트 파일(ET_DYN)";
			case 0x04:
				return "Core 파일(ET_CORE)";
			case 0xfe00:
				return "(ET_LOSS)";
			case 0xfeff:
				return "(ET_HIOS)";
			case 0xff00:
				return "프로세서에 의존적인 파일(ET_LOPROC)";
			case 0xffff:
				return "프로세서에 의존적인 파일2(ET_HIPROC)";
			default:
				return "What?!";
		}
	}

	char* ELF64_header::elf64output_machine(short machine)
	{
		switch(machine)
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
			case 0x9026:
				return "Alpha(EM_ALPHA)";
			case 0xA390:
				return "IBM S390(EM_S390)";
			default:
				return "Reserved";
		}
	}

	char* ELF64_header::elf64output_version(int version)
	{
		switch(version)
		{
			case 0x00:
				return "Invalid 버전(EV_NONE)";
			case 0x01:
				return "현재 버전(EV_CURRENT)";
			case 0x02:
				return "다음 버전(EV_NUM)";
			default:
				return "What?!";
		}
	}

	/* 함수 : program32output
	 * 기능 : 32bit program 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
	char* Program32_header::program32output_type(int type)
	{
		switch(type)
		{
			case 0x00:
				return "(PT_NULL)";
			case 0x01:
				return "(PT_LOAD)";
			case 0x02:
				return "(PT_DYNAMIC)";
			case 0x03:
				return "(PT_INERP)";
			case 0x04:
				return "(PT_NOTE)";
			case 0x05:
				return "(PT_SHLIB)";
			case 0x06:
				return "(PT_PHDR)";
			case 0x60000000:
				return "(PT_LOOS)";
			case 0x6FFFFFFF:
				return "(PT_HIOS)";
			case 0x70000000:
				return "(PT_LOPROC)";
			case 0x7FFFFFFF:
				return "(PT_HIPROC)";
			default:
				return "What?!";
		}
	}

	char* Program32_header::program32output_flags(int flags)
	{	
		switch(flags)
		{
			case 0x00:
				return "(PF_NONE)";
			case 0x01:
				return "(PF_Exec)";
			case 0x02:
				return "(PF_Write)";
			case 0x03:
				return "(PF_Write_Exec)";
			case 0x04:
				return "(PT_Read)";
			case 0x05:
				return "(PT_Read_Exec)";
			case 0x06:
				return "(PT_Read_Write)";
			case 0x07:
				return "(PT_Read_Wirte_Exec)";
			default:
				return "What?!";
		}
	}

	/* 함수 : program64output
	 * 기능 : 64bit program 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
	char* Program64_header::program64output_type(int type)
	{
		switch(type)
		{
			case 0x00:
				return "(PT_NULL)";
			case 0x01:
				return "(PT_LOAD)";
			case 0x02:
				return "(PT_DYNAMIC)";
			case 0x03:
				return "(PT_INERP)";
			case 0x04:
				return "(PT_NOTE)";
			case 0x05:
				return "(PT_SHLIB)";
			case 0x06:
				return "(PT_PHDR)";
			case 0x60000000:
				return "(PT_LOOS)";
			case 0x6FFFFFFF:
				return "(PT_HIOS)";
			case 0x70000000:
				return "(PT_LOPROC)";
			case 0x7FFFFFFF:
				return "(PT_HIPROC)";
			default:
				return "What?!";
		}
	}

	char* Program64_header::program64output_flags(int flags)
	{	
		switch(flags)
		{
			case 0x00:
				return "(PF_NONE)";
			case 0x01:
				return "(PF_Exec)";
			case 0x02:
				return "(PF_Write)";
			case 0x03:
				return "(PF_Write_Exec)";
			case 0x04:
				return "(PT_Read)";
			case 0x05:
				return "(PT_Read_Exec)";
			case 0x06:
				return "(PT_Read_Write)";
			case 0x07:
				return "(PT_Read_Wirte_Exec)";
			default:
				return "What?!";
		}
	}

	/* 함수 : section32output
	 * 기능 : 32bit section 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
	char* Section32_header::section32output_name(int name)
	{
		switch(name)
		{
			case 0x00:
				return "SHN_UNDEF";
			case 0x0B:
				return ".interp";
			case 0x13:
				return ".note.ABI-tag";
			case 0x21:
				return ".note.gnu.build.id";
			case 0x34:
				return ".gnu.hash";
			case 0x3E:
				return ".dynsym";
			case 0x46:
				return ".dynstr";
			case 0x4E:
				return ".gnu.version";
			case 0x5B:
				return ".gnu.version_r";
			case 0x6A:
				return ".rel.dyn";
			case 0x73:
				return ".rel.plt";
			case 0x7C:
				return ".init";
			case 0x77:
				return ".plt";
			case 0x82:
				return ".text";
			case 0x88:
				return ".fini";
			case 0x8E:
				return ".rodata";
			case 0x96:
				return ".eh_frame_hdr";
			case 0xA4:
				return ".eh_frame";
			case 0xAE:
				return ".init_array";
			case 0xBA:
				return ".fini_array";
			case 0xC6:
				return ".jcr";
			case 0xCB:
				return "dynamic";
			case 0xD4:
				return ".got";
			case 0xD9:
				return ".got.plt";
			case 0xE2:
				return ".data";
			case 0xE8:
				return ".bss";
			case 0xED:
				return ".comment";
			case 0x01:
				return ".shstrtab";
			default:
				return "What?!";
		}
	}

	char* Section32_header::section32output_type(int type)
	{
		switch(type)
		{
			case 0x00:
				return "(SHT_NULL)";
			case 0x01:
				return "(SHT_PROGBITS)";
			case 0x02:
				return "(SHT_SYMTAB)";
			case 0x03:
				return "(SHT_STRTAB)";
			case 0x04:
				return "(SHT_RELA)";
			case 0x05:
				return "(SHT_HASH)";
			case 0x06:
				return "(SHT_DYNAMIC)";
			case 0x07:
				return "(SHT_NOTE)";
			case 0x08:
				return "(SHT_NOBITS)";
			case 0x09:
				return "(SHT_REL)";
			case 0x0A:
				return "(SHT_SHLIB)";
			case 0x0B:
				return "(SHT_DYNSYM)";
			case 0x60000000:
				return "(SHT_LOOS)";
			case 0x6FFFFFFF:
				return "(SHT_HIOS)";
			case 0x70000000:
				return "(SHT_LOPROC)";
			case 0x7FFFFFFF:
				return "(SHT_HIPROC)";
			default:
				return "What?!";
		}
	}

	char* Section32_header::section32output_flags(int flags)
	{	
		switch(flags)
		{
			case 0x00:
				return "(SF32_NONE)";
			case 0x01:
				return "(SF32_Exec)";
			case 0x02:
				return "(SF32_Alloc)";
			case 0x03:
				return "(SF32_Alloc_Exec)";
			case 0x04:
				return "(SF32_Write_Exec)";
			case 0x05:
				return "(SF32_Write_Alloc)";
			case 0x06:
				return "(SF32_Write_Alloc_Exec)";
			default:
				return "What?!";
		}
	}

	/* 함수 : section64output
	 * 기능 : 64bit section 구조체에 저장된 값을 통해 내용에 대한 정보 출력
	 * 반환 : char*
	 */
	char* Section64_header::section64output_name(int name)
	{
		switch(name)
		{
			case 0x00:
				return "SHN_UNDEF";
			case 0x1B:
				return ".interp";
			case 0x23:
				return ".note.ABI-tag";
			case 0x31:
				return ".note.gnu.build.id";
			case 0x44:
				return ".gnu.hash";
			case 0x4E:
				return ".dynsym";
			case 0x56:
				return ".dynstr";
			case 0x5E:
				return ".gnu.version";
			case 0x6B:
				return ".gnu.version_r";
			case 0x7A:
				return ".rela.dyn";
			case 0x84:
				return ".rela.plt";
			case 0x8E:
				return ".init";
			case 0x89:
				return ".plt";
			case 0x94:
				return ".text";
			case 0x9A:
				return ".fini";
			case 0xA0:
				return ".rodata";
			case 0xA8:
				return ".eh_frame_hdr";
			case 0xB6:
				return ".eh_frame";
			case 0xC0:
				return ".init_array";
			case 0xCC:
				return ".fini_array";
			case 0xD8:
				return ".jcr";
			case 0xDD:
				return "dynamic";
			case 0xE6:
				return ".got";
			case 0xEB:
				return ".got.plt";
			case 0xF4:
				return ".data";
			case 0xFA:
				return ".bss";
			case 0xFF:
				return ".comment";
			case 0x11:
				return ".shstrtab";
			case 0x01:
				return ".symtab";
			case 0x09:
				return ".strtab";
			default:
				return "What?!";
		}
	}

	char* Section64_header::section64output_type(int type)
	{
		switch(type)
		{
			case 0x00:
				return "(SHT_NULL)";
			case 0x01:
				return "(SHT_PROGBITS)";
			case 0x02:
				return "(SHT_SYMTAB)";
			case 0x03:
				return "(SHT_STRTAB)";
			case 0x04:
				return "(SHT_RELA)";
			case 0x05:
				return "(SHT_HASH)";
			case 0x06:
				return "(SHT_DYNAMIC)";
			case 0x07:
				return "(SHT_NOTE)";
			case 0x08:
				return "(SHT_NOBITS)";
			case 0x09:
				return "(SHT_REL)";
			case 0x0A:
				return "(SHT_SHLIB)";
			case 0x0B:
				return "(SHT_DYNSYM)";
			case 0x60000000:
				return "(SHT_LOOS)";
			case 0x6FFFFFFF:
				return "(SHT_HIOS)";
			case 0x70000000:
				return "(SHT_LOPROC)";
			case 0x7FFFFFFF:
				return "(SHT_HIPROC)";
			default:
				return "What?!";
		}
	}

	char* Section64_header::section64output_flags(__int64 flags)
	{	
		switch(flags)
		{
			case 0x00:
				return "(SF32_NONE)";
			case 0x01:
				return "(SF32_Exec)";
			case 0x02:
				return "(SF32_Alloc)";
			case 0x03:
				return "(SF32_Alloc_Exec)";
			case 0x04:
				return "(SF32_Write_Exec)";
			case 0x05:
				return "(SF32_Write_Alloc)";
			case 0x06:
				return "(SF32_Write_Alloc_Exec)";
			default:
				return "What?!";
		}
	}
}